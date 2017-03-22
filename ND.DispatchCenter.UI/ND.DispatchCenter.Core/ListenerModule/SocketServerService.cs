using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Messaging;
using Sodao.FastSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.DispatchCenter.Core.Extentions;
using ND.DispatchCenter.Core.ListenerModule.Command;

//**********************************************************************
//
// 文件名称(File Name)：SocketServerService.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/25 15:52:10         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/25 15:52:10          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule
{
    /// <summary>
    /// 用于监听查询端口
    /// </summary>
    public class SocketServerService : AbsSocketService<CommandLineMessage>
    {
        public static event EventHandler<string> onSocketMsg;

        public void OnSocketMsg(string e)
        {
            if(onSocketMsg != null)
            {
                onSocketMsg(this, e);
            }
        }
        public override void OnConnected(IConnection connection)
        {
           
            base.OnConnected(connection);
            connection.BeginReceive();
            OnSocketMsg(connection.RemoteEndPoint.ToString() + " " + connection.ConnectionID.ToString() + " 连接" + (int)PortType.SearchPort + "成功");
        }

        #region test
        //public override void OnReceived(IConnection connection, CommandLineMessage message)
        //{
        //    try
        //    {
        //        base.OnReceived(connection, message);


        //        CmdInfo cmdInfo = JsonConvert.DeserializeObject<CmdInfo>(message.CmdName);


        //        string result = "";
        //        OnSocketMsg("connection:" + connection.ConnectionID.ToString() + " 发送参数:" + message.CmdName);
        //        #region 命令分配
        //        switch (cmdInfo.CmdType)
        //        {
        //            case CmdType.TaskList://任务列表
        //                {

        //                    CommandLineMessage replyMsg = new CommandLineMessage(cmdInfo.CmdType.ToString(), new string[] { JsonConvert.SerializeObject(TaskProvider.Instance.TaskList) });
        //                    //replyMsg.Parameters[0] = JsonConvert.SerializeObject(TaskProvider.Instance.TaskList);
        //                    message.Reply(connection, JsonConvert.SerializeObject(replyMsg));
        //                }
        //                break;
        //            case CmdType.TaskDetail://任务详情
        //                {
        //                    TaskDescriptor task = TaskProvider.Instance.TaskList.ToList().SingleOrDefault(x => x.TaskKey == cmdInfo.TaskKey);
        //                    if (task == null)
        //                    {
        //                        message.Reply(connection, " TaskKey:" + cmdInfo.TaskKey + " Is Not Find");
        //                    }
        //                    else
        //                    {
        //                        if (cmdInfo.IsRunTask)
        //                        {
        //                            RunTaskResult res = task.ImplementationInstance.RunTask();
        //                            task.UpdateTaskInfo(res);
        //                            result = " 运行结果:" + JsonConvert.SerializeObject(res);
        //                        }
        //                        message.Reply(connection, JsonConvert.SerializeObject(task) + result);
        //                    }
        //                }
        //                break;
        //            case CmdType.TaskConfig://任务配置
        //                {
        //                    TaskDescriptor task = TaskProvider.Instance.TaskList.ToList().SingleOrDefault(x => x.TaskKey == cmdInfo.TaskKey);
        //                    if (task == null)
        //                    {
        //                        message.Reply(connection, " TaskKey:" + cmdInfo.TaskKey + " Is Not Find");
        //                        return;
        //                    }
        //                    if (cmdInfo.IsRunTask)
        //                    {
        //                        RunTaskResult res = task.ImplementationInstance.RunTask();
        //                        task.UpdateTaskInfo(res);
        //                        result = " 运行结果:" + JsonConvert.SerializeObject(res);
        //                    }
        //                    message.Reply(connection, JsonConvert.SerializeObject(task.ImplementationInstance.TaskCustomConfig) + result);
        //                }
        //                break;
        //            case CmdType.TaskStatus://任务状态
        //                {
        //                    TaskDescriptor task = TaskProvider.Instance.TaskList.ToList().SingleOrDefault(x => x.TaskKey == cmdInfo.TaskKey);
        //                    if (task == null)
        //                    {
        //                        message.Reply(connection, " TaskKey:" + cmdInfo.TaskKey + " Is Not Find");
        //                    }
        //                    if (cmdInfo.IsRunTask)
        //                    {
        //                        RunTaskResult res = task.ImplementationInstance.RunTask();
        //                        task.UpdateTaskInfo(res);
        //                        result = " 运行结果:" + JsonConvert.SerializeObject(res);
        //                    }
        //                    message.Reply(connection, JsonConvert.SerializeObject(task.TaskStatus.description().ToString()) + result);
        //                }
        //                break;
        //            default:
        //                message.Reply(connection, "error unknow command ");
        //                break;
        //        }

        //        #endregion


        //    }
        //    catch (Exception ex)
        //    {
        //        // message.Reply(connection, JsonConvert.SerializeObject("服务端返回异常:"+ex));
        //    }
        //} 
        #endregion

        public override void OnReceived(IConnection connection, CommandLineMessage message)
        {
            try
            {
                base.OnReceived(connection, message);
                OnSocketMsg(connection.RemoteEndPoint.ToString() + "请求,内容: " + JsonConvert.SerializeObject(message));
                CommandLineMessage cmdMsg = JsonConvert.DeserializeObject<CommandLineMessage>(message.CmdName);
                try
                {
                    if (cmdMsg==null || string.IsNullOrEmpty(cmdMsg.CmdName))
                    {
                        message.Reply(connection, "不允许的请求!");//JsonConvert.SerializeObject(ex)
                        OnSocketMsg("执行异常,服务端回复:不允许的请求!");
                        OnDisconnected(connection, null);
                    }
                    else
                    {
                        CommandDescriptor cmd = CommandProvider.Instance.CommandList.FirstOrDefault(x => x.CommandName == cmdMsg.CmdName.ToLower());
                        if (cmd == null)
                        {
                            OnSocketMsg("服务端回复:" + connection.RemoteEndPoint.ToString() + ",内容:未知的命令! ");
                            message.Reply(connection, JsonConvert.SerializeObject(new CommandResult() { RunStatus = RunStatus.Failed, Message = "未知的命令" }));
                            return;
                        }
                        //if (cmd.Port != (int)PortType.SearchPort)
                        //{
                        //    OnSocketMsg("服务端回复:" + connection.RemoteEndPoint.ToString() + ",内容:当前命令无权限访问该端口! ");
                        //    message.Reply(connection, JsonConvert.SerializeObject(new CommandResult() { RunStatus = RunStatus.Failed, Message = "当前命令无权限访问该端口" }));
                        //    return;
                        //}
                        CommandResult res = cmd.CommandInstance.ExcuteCommand(cmdMsg);
                        // CommandLineMessage msg = new CommandLineMessage(cmdMsg.CmdName, JsonConvert.SerializeObject(res));
                        message.Reply(connection, JsonConvert.SerializeObject(res));
                        OnSocketMsg("服务端回复:" + connection.RemoteEndPoint.ToString() + ",内容: " + JsonConvert.SerializeObject(res));
                    }
                }
                catch(Exception ex)
                {
                    message.Reply(connection, JsonConvert.SerializeObject(ex));//JsonConvert.SerializeObject(ex)
                    OnSocketMsg("执行异常,服务端回复:" + connection.RemoteEndPoint.ToString() + ",内容: " + JsonConvert.SerializeObject(ex));
                    OnDisconnected(connection, null);
                }

            }
            catch(Exception ex)
            {
                message.Reply(connection, "不允许的请求!");//JsonConvert.SerializeObject(ex)
                OnSocketMsg("执行异常,服务端回复:不允许的请求!");
            }
            
        }

           
        public override void OnDisconnected(IConnection connection, Exception ex)
        {
            base.OnDisconnected(connection, ex);
            OnSocketMsg(connection.RemoteEndPoint.ToString() + " 断开连接");
        }

        public override void OnException(IConnection connection, Exception ex)
        {
            base.OnException(connection, ex);
            OnSocketMsg("connection:" + connection.ConnectionID.ToString()+",异常信息:"+ex.ToString());
        }
        
        public override void OnSendCallback(IConnection connection, Packet packet, bool isSuccess)
        {
            base.OnSendCallback(connection, packet, isSuccess);
            if (isSuccess)
            {
               
                base.OnDisconnected(connection, null);
                OnSocketMsg("服务端回复 connection:" + connection.ConnectionID.ToString() + " 成功,已移除该客户端！");
            }
            else
            {
                OnSocketMsg("服务端回复 connection:" + connection.ConnectionID.ToString() + " 失败");
            }
        }
    }
}
