using ND.DispatchCenter.CommandClientLib;
using ND.DispatchCenter.CommandClientLib.Model;
using Newtonsoft.Json;
using Sodao.FastSocket.Client;
using Sodao.FastSocket.Client.Messaging;
using Sodao.FastSocket.Client.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandClient
{
    public partial class CommandClientForm : Form
    {
        //ClientLib client = new ClientLib();
        public CommandClientForm()
        {
            InitializeComponent();
            this.cmbCommandName.SelectedIndex = 0;
            Control.CheckForIllegalCrossThreadCalls = false;
            ClientLib.onCommandResponse += client_onCommandResponse;
            LoadCmdParam2();
        }

        /// <summary>
        /// 命令返回结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_onCommandResponse(object sender, CommandResponse e)
        {
            StringBuilder strMsg = new StringBuilder();
            strMsg.AppendLine("命令:" + e.CmdName + "收到服务返回消息:");
            strMsg.AppendLine("消息状态:" + e.Result.RunStatus.ToString());
            strMsg.AppendLine("消息描述:" + e.Result.Message);
            strMsg.AppendLine("消息异常:");
            strMsg.Append(e.Result.Ex == null ? "" : JsonConvert.SerializeObject(e.Result.Ex));
            strMsg.AppendLine("消息内容:");
            strMsg.Append(e.Result.Data == null ? "" : JsonConvert.SerializeObject(e.Result.Data));
            this.txtResponseParam.Text = "";
            this.txtResponseParam.Text = strMsg.ToString();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            #region test
            // Sodao.FastSocket.SocketBase.Log.Trace.EnableConsole();
            // var client = new SocketClient<CommandLineMessage>(new CommandLineProtocol(), 1024000, 1024000, 30000, 30000);
            // client.TryRegisterEndPoint(Guid.NewGuid().ToString(), new[] { new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1500) }, null);
            // string cmdName =this.cmbCommandName.Text;
            //string cmdParam= this.txtCommandParam.Text;
            //CommandLineMessage requestParam = new CommandLineMessage(1, cmdName, cmdParam);
            //this.txtRequestParam.Text = "";
            //this.txtRequestParam.Text= JsonConvert.SerializeObject(requestParam);
            //SendCommmd(new CommandLineMessage(1, cmdName, cmdParam), client); 
            #endregion

            this.txtRequestParam.Text = "";
            string cmdName =this.cmbCommandName.Text;
            string cmdParam= this.txtCommandParam.Text;
            CommandRequest request = new CommandRequest(cmdName,cmdParam);
            CommandLineMessage requestParam = new CommandLineMessage(1, cmdName, cmdParam);
            this.txtRequestParam.Text= JsonConvert.SerializeObject(requestParam);
            ClientLib.SendCommand(request);
        }

        #region 载入命令参数
        private void LoadCmdParam2()
        {
            string cmdName = "CmdParam";//this.cmbCommandName.Text;
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "CommandName", this.cmbCommandName.Text } };
            CommandRequest request = new CommandRequest(cmdName, JsonConvert.SerializeObject(dic));
            Action<CommandResponse<object>> action = (res)=>
            {
                if (res.Result.RunStatus == RunStatus.Normal)
                {
                    Dictionary<string, Dictionary<string, string>> task = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(res.Result.Data.ToString());
                    this.txtCommandParam.Text = task[this.cmbCommandName.Text]["CommandParam"];
                    lbCmdDicription.Text = task[this.cmbCommandName.Text]["CommandDecription"];
                }
                else
                {
                    MessageBox.Show("载入当前命令参数出错:" +res.Result.Message+",异常信息:"+ JsonConvert.SerializeObject(res.Result.Ex));
                }
            };
            ClientLib.SendCommand(request,action);
           
        } 
        #endregion



        #region 选择命令修改
        private void cmbCommandName_SelectedIndexChanged(object sender, EventArgs e)
        {
            // LoadCmdParam(this.cmbCommandName.Text);
            LoadCmdParam2();
        } 
        #endregion

        #region 接收消息
        void client_ReceivedUnknowMessage(Sodao.FastSocket.SocketBase.IConnection arg1, CommandLineMessage arg2)
        {
            MessageBox.Show("aaaa:" + arg2.CmdName);
        }
        #endregion

        #region 获取任务列表
        private void btnGetTaskList_Click(object sender, EventArgs e)
        {
            Action<CommandResponse<List<TaskDetailInfo>>> action = (args) =>
            {
                MessageBox.Show(JsonConvert.SerializeObject(args));
            };
            ClientLib.GetTaskListCommand(action);
        }
        #endregion

        #region 执行任务
        private void btnExcuteTask_Click(object sender, EventArgs e)
        {
            //Action<CommandResponse<string>> action=(args)=>{
            //    MessageBox.Show(JsonConvert.SerializeObject(args));
            //};
           //Dictionary<string, string> dic= JsonConvert.DeserializeObject<Dictionary<string, string>>(this.txtCommandParam.Text);
          //List<Dictionary<string,string>> dicParam= JsonConvert.DeserializeObject<List<Dictionary<string,string>>>(dic["TaskConfig"]);
         // ClientLib.ExcuteTaskCommand(dic["TaskKey"], dicParam, action);

            // Action<CommandResponse<string>> action2 = (args)=>{//提交执行命令类
            //   MessageBox.Show(JsonConvert.SerializeObject(args));
            // };
            //Action<CommandResponse<List<TaskDetailInfo>>> action = (args)=>{//获取任务列表类
            //    TaskDetailInfo task = args.Result.Data.FirstOrDefault(x => x.TaskName == "CreateOrderToPartenerTask");
            //    Dictionary<string, string> dic = new Dictionary<string, string>();
            //    dic.Add("orderNumForCus", this.txtCommandParam.Text);
            //    dic.Add("isPay", "0");
            //    List<Dictionary<string, string>> lst = new List<Dictionary<string, string>>();
            //    lst.Add(dic);
            //    ClientLib.ExcuteTaskCommand(task.TaskKey, lst, action2);
            //};
            //ClientLib.GetTaskListCommand(action);
           // for (int i = 0; i < 1000; i++)
            //{
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("orderNumForCus", this.txtCommandParam.Text+1.ToString());
                dic.Add("isPay", "0");
                List<Dictionary<string, string>> lst = new List<Dictionary<string, string>>();
                lst.Add(dic);

                ClientLib.ExcuteTaskCommandByTaskName("CreateOrderToPartenerTask", lst);
           // }
        }
        #endregion

        #region 旧代码
        //#region 载入命令参数
        //private void LoadCmdParam(string cmdName)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>() { { "CommandName", cmdName } };
        //    CommandLineMessage cType = new CommandLineMessage(1, "CmdParam", JsonConvert.SerializeObject(dic));
        //    var client = new SocketClient<CommandLineMessage>(new CommandLineProtocol(), 1024, 1024, 30000, 30000);
        //    client.TryRegisterEndPoint(Guid.NewGuid().ToString(), new[] { new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1500) }, null);
        //    client.Send(client.NewRequest("list", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cType) + System.Environment.NewLine), 30000,
        //          ex => MessageBox.Show("发送消息失败:" + JsonConvert.SerializeObject(ex)),
        //          message =>
        //          {
        //              CommandResult res = JsonConvert.DeserializeObject<CommandResult>(message.CmdName);
        //              if (res.RunStatus == RunStatus.Normal)
        //              {
        //                  Dictionary<string, string> task = JsonConvert.DeserializeObject<Dictionary<string, string>>(res.Data.ToString());
        //                  this.txtCommandParam.Text = task["TaskConfig"];
        //                  lbCmdDicription.Text = task["TaskDecription"];
        //              }
        //              else
        //              {
        //                  MessageBox.Show("载入当前命令参数出错:" + JsonConvert.SerializeObject(res.Ex));
        //              }

        //          }));
        //}
        //#endregion

        //#region 发送命令
        //private void SendCommmd(CommandLineMessage cType, SocketClient<CommandLineMessage> client)
        //{
        //    var guid = Guid.NewGuid().ToString();
        //    var source = new TaskCompletionSource<bool>();
        //    string cmdName = cType.CmdName.ToString();
        //    client.ReceivedUnknowMessage += client_ReceivedUnknowMessage;


        //    // CmdInfo cmdInfo = new CmdInfo() { CmdType = CmdType.TaskList };
        //    client.Send(client.NewRequest("list", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cType) + System.Environment.NewLine), 30000,
        //           ex => MessageBox.Show("发送消息失败:" + JsonConvert.SerializeObject(ex)),
        //           message =>
        //           {

        //               // MessageBox.Show(message.CmdName);
        //               // MessageBox.Show(message.Parameters[0]);
        //               // CommandLineMessage res2 = JsonConvert.DeserializeObject<CommandLineMessage>(message.CmdName);
        //               StringBuilder strMessage = new StringBuilder();
        //               strMessage.Append(message.CmdName);
        //               message.Parameters.ToList().ForEach(x =>
        //               {
        //                   strMessage.Append(x);
        //               });
        //               CommandResult res = JsonConvert.DeserializeObject<CommandResult>(strMessage.ToString());
        //               StringBuilder strMsg = new StringBuilder();
        //               strMsg.AppendLine("命令:" + cmdName + "收到服务返回消息:");
        //               strMsg.AppendLine("消息状态:" + res.RunStatus.ToString());
        //               strMsg.AppendLine("消息描述:" + res.Message);
        //               strMsg.AppendLine("消息异常:");
        //               strMsg.Append(res.Ex == null ? "" : JsonConvert.SerializeObject(res.Ex));
        //               strMsg.AppendLine("消息内容:");
        //               strMsg.Append(res.Data == null ? "" : JsonConvert.SerializeObject(res.Data));
        //               this.txtResponseParam.Text = "";
        //               this.txtResponseParam.Text = strMsg.ToString();
        //           }));
        //}
        //#endregion

        
        #endregion
    }
}
