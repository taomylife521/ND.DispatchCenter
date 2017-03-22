using ND.DispatchCenter.CommandClientLib.Config;
using ND.DispatchCenter.CommandClientLib.Model;
using Newtonsoft.Json;
using Sodao.FastSocket.Client;
using Sodao.FastSocket.Client.Messaging;
using Sodao.FastSocket.Client.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ND.DispatchCenter.CommandClientLib
{
    public class ClientLib
    {
        #region Proproty
        static SocketClient<CommandLineMessage> client;
        public static event EventHandler<CommandResponse> onCommandResponse;
        public static DispatchCenterClientSection clientSection;
    
        #endregion

        #region static Constructor
        static ClientLib()
        {
             clientSection = ConfigurationManager.GetSection("DispatchCenterClient") as DispatchCenterClientSection; 
            if(clientSection == null)
            {
                throw new Exception("DispatchCenterClient config node not exists");
            }
            client = new SocketClient<CommandLineMessage>(new CommandLineProtocol(), clientSection.Master.SocketBufferSize, clientSection.Master.MessageBufferSize, clientSection.Master.MillisecondsSendTimeout, clientSection.Master.MillisecondsReceiveTimeout);
            client.TryRegisterEndPoint(Guid.NewGuid().ToString(), new[] { new IPEndPoint(IPAddress.Parse(clientSection.Master.Ip), clientSection.Master.SearchPort) });
           
            if (clientSection.Slaves.Count > 0)
            {

                List<DispatchCenterClientDetailSection> slaves = clientSection.Slaves.Cast<DispatchCenterClientDetailSection>().ToList();//从节点
                slaves.ForEach(x =>
                {
                    client.TryRegisterEndPoint(Guid.NewGuid().ToString(), new[] { new IPEndPoint(IPAddress.Parse(x.Ip), x.SearchPort) });
                });
            }
           
            // }
            client.ReceivedUnknowMessage += client_ReceivedUnknowMessage;
           
           
        } 
        #endregion

        #region Event Method
        private static void OnCommandResponse(CommandResponse e)
        {
            if (onCommandResponse != null)
            {
                onCommandResponse(null, e);
            }
        } 
        #endregion

        #region 发送命令
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="clientRequest">命令请求</param>
        /// <param name="action">自定义返回操作</param>
        public static  void SendCommand(CommandRequest clientRequest,Action<CommandResponse<object>> action=null)
        {
            #region test
            //var guid = Guid.NewGuid().ToString();
            //CommandLineMessage clientMsg = new CommandLineMessage(1, clientRequest.CmdName, clientRequest.CmdParam);
            //var source = new TaskCompletionSource<bool>();
            //string cmdName = clientMsg.CmdName.ToString();
            //if (clientRequest.CmdName.ToLower().IndexOf("excute") > -1)
            //{
            //    client.TryRegisterEndPoint(Guid.NewGuid().ToString(), new[] { new IPEndPoint(IPAddress.Parse(config.Ip), config.ExcutePort) }, null);
            //}
            //else
            //{
            //    client.TryRegisterEndPoint(Guid.NewGuid().ToString(), new[] { new IPEndPoint(IPAddress.Parse(config.Ip), config.SearchPort) }, null);
            //}
            //client.ReceivedUnknowMessage += client_ReceivedUnknowMessage;
            //client.Send(client.NewRequest(guid, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientMsg) + System.Environment.NewLine), config.MillisecondsReceiveTimeout, ex => source.TrySetException(ex), message =>
            //{
            //    StringBuilder strMessage = new StringBuilder();
            //    strMessage.Append(message.CmdName);
            //    message.Parameters.ToList().ForEach(x =>
            //    {
            //        strMessage.Append(x);
            //    });
            //    CommandResult result = JsonConvert.DeserializeObject<CommandResult>(strMessage.ToString());
            //    CommandResponse res = new CommandResponse(clientRequest.CmdName, result);
            //    if (action != null)
            //    {

            //        action(res);
            //    }
            //    else
            //    {
            //        OnCommandResponse(res);
            //        //serverResult(res);
            //    }
            //})); 
            #endregion
            Send(clientRequest, action);
        } 
        #endregion

        #region 发送
        private static void Send<T>(CommandRequest clientRequest, Action<CommandResponse<T>> action = null)
        {
           try{
                var guid = Guid.NewGuid().ToString();
                var source = new TaskCompletionSource<bool>();
                CommandLineMessage clientMsg = new CommandLineMessage(1, clientRequest.CmdName, clientRequest.CmdParam);
                //if (clientRequest.CmdName.ToLower().IndexOf("excute") > -1)
                //{
                //    client.TryRegisterEndPoint(Guid.NewGuid().ToString(), new[] { new IPEndPoint(IPAddress.Parse(config.Ip), config.ExcutePort) }, null);
                //}
                //else
                //{
                // ip = string.IsNullOrEmpty(ip) ? clientSection.Master.Ip : ip;
                // searchPort = searchPort == 0 ? clientSection.Master.SearchPort : searchPort;

               
                client.Send(client.NewRequest(guid, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientMsg) + System.Environment.NewLine), clientSection.Master.MillisecondsReceiveTimeout, ex =>
                {
                    if(ex.Message.ToLower().IndexOf("pendingsendtimeout")>-1)
                    {
                        DispatchCenterEmailHelper.SendAllMail("当前没有可用的任务调度从节点,请查看任务调度所在服务器状态!", "异常信息:" + JsonConvert.SerializeObject(ex) + ",配置节点信息:" + JsonConvert.SerializeObject(clientSection) + "连接对象:" + JsonConvert.SerializeObject(client) + ",\r\n发送内容:" + JsonConvert.SerializeObject(clientMsg), clientSection.AlarmMail);// string.Join(";", lstMailAddr.ToArray())
                    }
                   
                    //异常
                }, message =>
                {
                    try
                    {
                        StringBuilder strMessage = new StringBuilder();
                        strMessage.Append(message.CmdName);
                        message.Parameters.ToList().ForEach(x =>
                        {
                            strMessage.Append(x);
                        });
                        CommandResult result = JsonConvert.DeserializeObject<CommandResult>(strMessage.ToString());
                        CommandResponse res2 = new CommandResponse(clientRequest.CmdName, result);
                        CommandResult<T> result2 = new CommandResult<T>()
                        {
                            RunStatus = result.RunStatus,
                            Message = result.Message,
                            Ex = result.Ex,
                            Data = result.RunStatus != RunStatus.Normal ? default(T) : (result.Data == null ? default(T) : JsonConvert.DeserializeObject<T>(result.Data.ToString()))
                        };
                        CommandResponse<T> res = new CommandResponse<T>(clientRequest.CmdName, result2);
                        if (action != null)
                        {
                            action(res);

                        }
                        else
                        {
                            OnCommandResponse(res2);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {

                        // client.Stop();
                    }
                }));
            }
            catch(Exception ex)
           {
               string e = ex.Message;
           }
        }
        #endregion

        #region 把从节点推选为主节点
        //private static void SelectSlaveToMaster<T>(CommandRequest clientRequest, Action<CommandResponse<T>> action = null,DispatchCenterClientConfig config=null)
        //{
           
        //        if(config!=null)
        //        {
        //            config.FailedCount += 1;
        //        }
        //        List<DispatchCenterClientConfig> slaves = slaveList.ToList().Where(x => x.FailedCount <= 0).ToList();//从节点
        //        if (slaves.Count <= 0)//如果没有可用的从节点
        //        {
        //            Task.Factory.StartNew(()=>
        //            {
        //                List<string> lstMailAddr = new List<string>() { "514935323@qq.com", "taojp@niding.net" };
        //                //发送报警短信，所有节点都不可用
        //                DispatchCenterEmailHelper.SendAllMail("当前没有可用的任务调度从节点,请查看任务调度所在服务器状态!","Master:" +JsonConvert.SerializeObject(clientSection)+",Slave:"+JsonConvert.SerializeObject(slaveList), string.Join(";", lstMailAddr.ToArray()));
        //            });
        //            return;
        //        }
        //        int index = 0;
        //        slaves.ForEach(x =>
        //        {
        //            if (index == 0)
        //            {
        //                client = new SocketClient<CommandLineMessage>(new CommandLineProtocol(), x.SocketBufferSize, x.MessageBufferSize, x.MillisecondsSendTimeout, x.MillisecondsReceiveTimeout);
        //               // x.FailedCount += 1;
        //                Send(clientRequest, action);
        //                index++;
        //            }
        //        });
            
           
        //}
        #endregion

        #region 获取任务列表
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="action">任务返回结果</param>
        public static void GetTaskListCommand(Action<CommandResponse<List<TaskDetailInfo>>> action)
        {
            try
            {
                CommandRequest clientRequest = new CommandRequest("tasklist", "");
                Send(clientRequest, action);
            }
            catch(Exception ex)
            {

            }
            finally
            {
               // client.Stop();
            }
            
        }
        #endregion

        #region 执行任务根据TaskKey
        /// <summary>
        /// 执行任务命令根据TaskKey
        /// </summary>
        /// <param name="taskKey">任务编号</param>
        /// <param name="taskCustomConfig">任务配置数组,同一个任务可以配置多个不同的配置,开启多线程执行</param>
        /// <param name="action">任务执行完成返回结果</param>
        public static void ExcuteTaskCommandByTaskKey(string taskKey, List<Dictionary<string, string>> taskCustomConfig, Action<CommandResponse<string>> action)
        {
            try
            {
                Dictionary<string, string> requestParam = new Dictionary<string, string>() {
                { "TaskKey", taskKey },
                {"TaskConfig",JsonConvert.SerializeObject(taskCustomConfig)} 
            };
                CommandRequest clientRequest = new CommandRequest("excutetaskconfigandrun", JsonConvert.SerializeObject(requestParam));
                Send(clientRequest, action);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                //client.Stop();
            }
            
        }
        #endregion

        #region 执行任务根据TaskName
        /// <summary>
        /// 执行任务命令根据TaskName
        /// </summary>
        /// <param name="taskKey">任务编号</param>
        /// <param name="taskCustomConfig">任务配置数组,同一个任务可以配置多个不同的配置,开启多线程执行</param>
        /// <param name="action">任务执行完成返回结果</param>
        public static void ExcuteTaskCommandByTaskName(string taskName, List<Dictionary<string, string>> taskCustomConfig, Action<CommandResponse<string>> action)
        {
            try
            {
                Dictionary<string, string> requestParam = new Dictionary<string, string>() {
                { "TaskName", taskName },
                {"TaskConfig",JsonConvert.SerializeObject(taskCustomConfig)} 
               };
                CommandRequest clientRequest = new CommandRequest("excutesearchtaskandrun", JsonConvert.SerializeObject(requestParam));
                Send(clientRequest, action);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                //client.Stop();
            }
            
        }
        #endregion

        #region 执行任务根据TaskName
        /// <summary>
        /// 执行任务命令根据TaskName
        /// </summary>
        /// <param name="taskKey">任务编号</param>
        /// <param name="taskCustomConfig">任务配置数组,同一个任务可以配置多个不同的配置,开启多线程执行</param>
        /// <param name="action">任务执行完成返回结果</param>
        public static CommandResult ExcuteTaskCommandByTaskName(string taskName, List<Dictionary<string, string>> taskCustomConfig)
        {
            try
            {
                Action<CommandResponse<string>> action2 = (args) =>
                {//提交执行命令类

                };
                ExcuteTaskCommandByTaskName(taskName, taskCustomConfig, action2);
                return new CommandResult() { RunStatus = RunStatus.Normal,Message="执行成功"};
            }
            catch(Exception ex)
            {
                return new CommandResult() { RunStatus = RunStatus.Exception, Message = "执行失败:" + ex.Message, Ex = ex, Data ="任务名称:"+taskName+"主要参数:"+ JsonConvert.SerializeObject(taskCustomConfig) };
            }
            finally
            {
               
               // client.Stop();
            }
        }
        #endregion



        #region client_ReceivedUnknowMessage
        static void client_ReceivedUnknowMessage(Sodao.FastSocket.SocketBase.IConnection arg1, CommandLineMessage arg2)
        {

        } 
        #endregion
    }
}
