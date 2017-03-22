using ND.DispatchCenter.Core.ListenerModule.Command;
using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CmdParamCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/5 14:51:32         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/5 14:51:32          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Command
{
    public class CmdParamCommand : AbstractCommand
    {
        /// <summary>
        /// 初始化命令需要的参数
        /// </summary>
        public override void InitCommandParamList()
        {
            CommandParamsList["CommandName"] = "";
        }

        /// <summary>
        /// 命令描述
        /// </summary>
        /// <returns></returns>
        public override string CommandDiscription()
        {
            return "获取命令需要的参数,返回Data类型 Dictionary<string, Dictionary<string, string>> key 为命令名称 值[CommandDecription]:命令描述,值[CommandParam]:命令参数";
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmdContent"></param>
        /// <returns></returns>
        public override CommandResult ExcuteCommand(Sodao.FastSocket.Server.Messaging.CommandLineMessage cmdContent)
        {
            Dictionary<string, Dictionary<string, string>> dicResult = new Dictionary<string, Dictionary<string, string>>();
            CommandResult result = this.IsRightCommandParams(cmdContent.Parameters);
            if (result.RunStatus != RunStatus.Normal)
            {
                return result;
            }
            if( CommandParamsList["CommandName"].ToString().ToLower() == "all")
            {
               
                CommandProvider.Instance.CommandList.ToList().ForEach(x =>
                {
                     Dictionary<string, string> dic2 = new Dictionary<string, string>();
                     dic2.Add("CommandDecription", x.CommandInstance.CommandDiscription());
                     dic2.Add("CommandParam", JsonConvert.SerializeObject(x.CommandInstance.CommandParamsList));
                     dicResult.Add(x.CommandName, dic2);
                   
                });
                return new CommandResult() { RunStatus = Core.TaskModule.RunStatus.Normal, Data = dicResult };
            }
            CommandDescriptor cmd = CommandProvider.Instance.CommandList.FirstOrDefault(x => x.CommandName == CommandParamsList["CommandName"]);
            if(cmd == null)
            {
                return new CommandResult() { RunStatus = Core.TaskModule.RunStatus.Failed, Message="当前未找到该命令对应的参数和描述" };
            }
            Dictionary<string,string> dic = new Dictionary<string,string>();
            dic.Add("CommandDecription",cmd.CommandInstance.CommandDiscription());
            dic.Add("CommandParam",JsonConvert.SerializeObject(cmd.CommandInstance.CommandParamsList));
            dicResult.Add(CommandParamsList["CommandName"].ToString(), dic);
            return new CommandResult() { RunStatus = Core.TaskModule.RunStatus.Normal, Data = dicResult };
        }
    }
}
