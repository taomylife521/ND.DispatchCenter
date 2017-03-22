using ND.DispatchCenter.Core.TaskModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.DispatchCenter.Core.Extentions;
using Sodao.FastSocket.Server.Messaging;
using Newtonsoft.Json;

//**********************************************************************
//
// 文件名称(File Name)：TaskStatusCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 11:31:19         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 11:31:19          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
   public class TaskStatusCommand:AbstractCommand
    {
        public override void InitCommandParamList()
        {

            CommandParamsList.Add("TaskKey", "");
        }

        public override CommandResult ExcuteCommand(CommandLineMessage cmdContent)
        {
            try
            {
                CommandResult result = this.IsRightCommandParams(cmdContent.Parameters);
                if (result.RunStatus != RunStatus.Normal)
                {
                    return result;
                }
                string taskKey = CommandParamsList["TaskKey"].ToString();
                CommandResult<TaskDescriptor> res = this.IsExistTask(taskKey);
                if (res.RunStatus != RunStatus.Normal)
                {
                    return new CommandResult() { RunStatus = res.RunStatus, Message = res.Message, Ex = res.Ex, Data = res.Data };
                }
                StringBuilder strMsg = new StringBuilder();
               TaskDescriptor t= TaskProvider.Instance.TaskList.FirstOrDefault(x => x.TaskKey == taskKey);
                //CommandTaskCollection.Instance.TaskList.Where(x => x.Value.TaskKey == taskKey).ToList().ForEach(x => {
                //    strMsg.AppendLine("任务名称:" + x.Value.ImplementationInstance.TaskName()+",任务状态:"+x.Value.TaskStatus.description().ToString()+"任务配置:"+JsonConvert.SerializeObject(x.Value.ImplementationInstance.TaskCustomConfig)+"\r\n");
                //});
                return new CommandResult() { RunStatus = RunStatus.Normal, Data = t.TaskStatus.description() };
            }
            catch (Exception ex)
            {
                return new CommandResult() { RunStatus = RunStatus.Exception, Message = ex.Message, Ex = ex };
            }
        }

        public override string CommandDiscription()
        {
            return "获取当前任务状态,Data返回类型:string";
        }
    }
}
