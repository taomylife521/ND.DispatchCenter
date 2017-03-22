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
// 文件名称(File Name)：TaskListCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 11:30:27         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 11:30:27          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
    #region TaskDetailInfo
    public class TaskDetailInfo
    {
        public TaskDetailInfo()
        {
            TaskCustomConfig = new Dictionary<string, string>();
        }
        /// <summary>
        /// 任务模块程序集key
        /// </summary>
        public string TaskModuleAssemblyKey { get; set; }

        /// <summary>
        /// 任务模块名称
        /// </summary>
        public string TaskModuleName { get; set; }

        /// <summary>
        /// 任务模块key
        /// </summary>
        public string TaskModuleKey { get; set; }

        /// <summary>
        /// 任务主键
        /// </summary>
        public string TaskKey { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }


        /// <summary>
        /// 任务描述
        /// </summary>
        public string TaskDescription { get; set; }

        /// <summary>
        /// 总共成功数量
        /// </summary>
        public int TotalSucessCount { get; set; }

        /// <summary>
        /// 总共失败数量
        /// </summary>
        public int TotalFailedCount { get; set; }

        /// <summary>
        /// 总共运行次数
        /// </summary>
        public int TotalRunCount { get; set; }


        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskWorkStatus TaskStatus { get; set; }


        /// <summary>
        /// 任务自定义配置
        /// </summary>
       public System.Collections.Generic.Dictionary<string, string> TaskCustomConfig { get; set; }

        /// <summary>
        /// 最后一次运行时间
        /// </summary>
        public DateTime LastDateTime { get; set; }

        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime TaskCreateTime { get; set; }

        /// <summary>
        /// 任务更新时间
        /// </summary>
        public DateTime TaskUpdateTime { get; set; }

        /// <summary>
        /// 任务上一次结束时间
        /// </summary>
        public DateTime TaskLastEndTime { get; set; }

        /// <summary>
        /// 任务出错时间
        /// </summary>
        public DateTime TaskLastErrorTime { get; set; }
    } 
    #endregion
    public class TaskListCommand:AbstractCommand
    {

       
        public override void InitCommandParamList()
        {
           
        }

        public override CommandResult ExcuteCommand(CommandLineMessage cmdContent)
        {
            try
            {
                if (TaskProvider.Instance.TaskList.Count<= 0)
                {
                    return new CommandResult() { RunStatus = RunStatus.Failed, Message = "未找到任何任务" };
                }
                StringBuilder strData = new StringBuilder();
                List<TaskDetailInfo> taskList = new List<TaskDetailInfo>();
                TaskProvider.Instance.TaskList.ToList().ForEach(x=>{
                    //strData.AppendLine("任务编号:" + x.TaskKey + ",任务名称:" + x.ImplementationInstance.TaskName() + ",任务描述:" + x.ImplementationInstance.TaskDescription() + ",任务状态:" + x.TaskStatus.ToString());
                    taskList.Add(new TaskDetailInfo()
                    {
                        LastDateTime=x.LastDateTime,
                        TaskCreateTime=x.TaskCreateTime,
                        TaskKey=x.TaskKey,
                        TaskLastEndTime=x.TaskLastEndTime,
                        TaskLastErrorTime=x.TaskLastErrorTime,
                        TaskModuleAssemblyKey=x.TaskModuleAssemblyKey,
                        TaskModuleKey=x.TaskModuleKey,
                        TaskModuleName=x.TaskModuleName,
                        TaskStatus = x.TaskStatus,
                        TaskUpdateTime=x.TaskUpdateTime,
                        TotalFailedCount=x.TotalFailedCount,
                        TotalRunCount=x.TotalRunCount,
                        TotalSucessCount=x.TotalSucessCount,
                        TaskCustomConfig = x.ImplementationInstance.TaskCustomConfig,
                        TaskName=x.ImplementationInstance.TaskName(),
                        TaskDescription = x.ImplementationInstance.TaskDescription()
                        
                    });
                });

                return new CommandResult() { RunStatus = RunStatus.Normal, Data = taskList };
            }
            catch (Exception ex)
            {
                return new CommandResult() { RunStatus = RunStatus.Exception, Message = ex.Message, Ex = ex };
            }
        }

       
        
        public override string CommandDiscription()
        {
            return "获取当前任务运行列表,Data返回类型：string";
        }
    }
}
