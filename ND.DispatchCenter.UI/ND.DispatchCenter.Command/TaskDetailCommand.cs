using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using Sodao.FastSocket.Server.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskDetailCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 11:30:49         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 11:30:49          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
   public class TaskDetailCommand:AbstractCommand
    {
        //public  string ExcuteCommand()
        //{
        //    string taskKey =CommandArgs.ParamsList["TaskKey"].ToString();
        //  TaskDescriptor task= TaskProvider.Instance.TaskList.SingleOrDefault(x => x.TaskKey == taskKey);
        //  return JsonConvert.SerializeObject(task);
        //}

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
                return new CommandResult() { RunStatus = RunStatus.Normal, Data = res.Data };
            }
            catch(Exception ex)
            {
                return new CommandResult() { RunStatus = RunStatus.Exception, Message = ex.Message, Ex = ex };
            }
        }

        public override string CommandDiscription()
        {
            return "获取当前任务详情,返回Data为TaskDescriptor对象";
        }
    }
}
