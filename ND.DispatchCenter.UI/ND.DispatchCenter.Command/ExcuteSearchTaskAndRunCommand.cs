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
// 文件名称(File Name)：SearchTaskAndRunCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/17 11:06:45         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/17 11:06:45          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Command
{
    public class ExcuteSearchTaskAndRunCommand : AbstractCommand
    {
        public override void InitCommandParamList()
        {
            CommandParamsList["TaskName"] = "";
            CommandParamsList["TaskConfig"] = "";
        }

        public override string CommandDiscription()
        {
            return "查找任务并执行当前任务配置";
        }

        public override CommandResult ExcuteCommand(Sodao.FastSocket.Server.Messaging.CommandLineMessage cmdContent)
        {
            #region 校验命令参数
            CommandResult res = this.IsRightCommandParams(cmdContent.Parameters);
            if (res.RunStatus != RunStatus.Normal)
            {
                return res;
            } 
            #endregion

            #region 校验配置
            string taskKey = "";
            List<Dictionary<string, string>> taskConfig = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(CommandParamsList["TaskConfig"].ToString());
            bool flag = true;
            CommandResult result = new CommandResult();
            for (int i = 0; i < taskConfig.Count; i++)
            {
                CommandResult<TaskDescriptor> cmd = this.IsRightTaskConfig("", taskConfig[i], CommandParamsList["TaskName"]);
                taskKey = cmd.Data.TaskKey;
                if (cmd.RunStatus != RunStatus.Normal)
                {
                    flag = false;
                    result = new CommandResult() { RunStatus = cmd.RunStatus, Message = cmd.Message, Ex = cmd.Ex };
                    break;
                }

            }
            if (!flag)
            {
                return result;
            }
            #endregion

            #region 执行
            try
            {
                for (int i = 0; i < taskConfig.Count; i++)
                {
                    // TaskDescriptor task2 = TaskProvider.Instance.TaskList.FirstOrDefault(x => x.TaskKey == taskKey);//初始值
                    TaskDescriptor task = TaskDescriptor.DeepCopy(TaskProvider.Instance.TaskList.FirstOrDefault(x => x.TaskKey == taskKey));//深克隆
                    // TaskDescriptor task1 = TaskProvider.Instance.TaskList.FirstOrDefault(x => x.TaskKey == taskKey).Clone() as TaskDescriptor;//浅拷贝
                    task.UpdateTaskConfig(taskConfig[i]);//更新任务配置


                    CommandTaskCollection.Instance.Add(System.Guid.NewGuid().ToString(), task);
                }
            }
            catch (Exception ex)
            {
                return new CommandResult() { RunStatus = RunStatus.Exception, Message = "任务开启异常", Ex = ex };
            }
            return new CommandResult() { RunStatus = RunStatus.Normal, Message = "任务已经开始执行" };
            #endregion
        }
    }
}
