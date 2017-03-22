using ND.DispatchCenter.Core.ListenerModule;
using ND.DispatchCenter.Core.ListenerModule.Command;
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
// 文件名称(File Name)：CommitTaskConfigAndRunCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/4 10:47:56         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/4 10:47:56          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Command
{
    /// <summary>
    /// 所有执行命令前必须加上Excute
    /// </summary>
    public class ExcuteTaskConfigAndRunCommand : AbstractCommand
    {
        
        public override CommandResult ExcuteCommand(CommandLineMessage cmdContent)
        {
          CommandResult res=  this.IsRightCommandParams(cmdContent.Parameters);
            if(res.RunStatus != RunStatus.Normal)
            {
                return res;
            }
            string taskKey = CommandParamsList["TaskKey"].ToString();

            #region 校验配置
            List<Dictionary<string, string>> taskConfig = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(CommandParamsList["TaskConfig"].ToString());
            bool flag = true;
            CommandResult result = new CommandResult();
            for (int i = 0; i < taskConfig.Count; i++)
            {
                CommandResult<TaskDescriptor> cmd = this.IsRightTaskConfig(taskKey, taskConfig[i]);
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
                 return new CommandResult() { RunStatus = RunStatus.Exception, Message = "任务开启异常",Ex=ex };
             }
            return new CommandResult() { RunStatus = RunStatus.Normal, Message="任务已经开始执行"  };
        }

        public override void InitCommandParamList()
        {
            CommandParamsList.Add("TaskKey", "");
            CommandParamsList.Add("TaskConfig", "");
        }

        public override void InitPort()
        {
            base.InitPort();
            Port = (int)PortType.ExcutePort;
        }
        public override string CommandDiscription()
        {
            return "提交任务参数并执行,其中TaskConfig为List<Dictionary<string,string>>，每项对应TaskKey任务的TaskConfig,返回Data为null";
        }
    }
}
