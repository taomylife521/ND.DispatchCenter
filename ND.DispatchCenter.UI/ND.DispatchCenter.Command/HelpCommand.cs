using ND.DispatchCenter.Core.ListenerModule.Command;
using Newtonsoft.Json;
using Sodao.FastSocket.Server.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：HelpCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 18:26:48         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 18:26:48          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Command
{
   public  class HelpCommand: AbstractCommand
    {
       public override CommandResult ExcuteCommand(CommandLineMessage cmdContent)
        {
            StringBuilder strCommand = new StringBuilder();
            CommandProvider.Instance.CommandList.ToList().ForEach(x =>
            {
                strCommand.AppendLine("命令：" + x.CommandName.ToLower() + ",命令描述:" + x.CommandDescrption + ",命令参数:" + JsonConvert.SerializeObject(x.CommandInstance.CommandParamsList)+"\r\n");
            });
            return new CommandResult() { RunStatus = Core.TaskModule.RunStatus.Normal,Data = strCommand.ToString() };
        }

        public override void InitCommandParamList()
        {
           
        }

        public override string CommandDiscription()
        {
            return "帮助命令 Data为string";
        }
    }
}
