using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.ListenerModule;
using ND.DispatchCenter.Core.LogModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskDescriptorExtention.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/25 11:02:50         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/25 11:02:50          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
   public static class TaskDescriptorExtention
    {
       private static object obj = new object();
        #region 更新任务信息
        public static void UpdateTaskInfo(this TaskDescriptor task,RunTaskResult result)
        {
            TaskDescriptor t = TaskProvider.Instance.TaskList.FirstOrDefault(x => x.TaskKey == task.TaskKey);
            lock (t)
            {
           
              if (t != null)
              {
                  if (result.RunStatus == RunStatus.Normal)
                  {
                      t.TotalSucessCount += 1;
                      t.TotalRunCount += 1;
                      t.TaskLastEndTime = DateTime.Now;
                      t.TaskStatus = TaskWorkStatus.Stop;
                      t.LastDateTime = DateTime.Now;
                    
                     // t.RunRecord.Add(task.TaskKey + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), result);
                  }
                  else
                  {
                     t.TotalFailedCount += 1;
                     t.TotalRunCount += 1;
                     t.TaskLastEndTime = DateTime.Now;
                     t.TaskStatus = TaskWorkStatus.Stop;
                     t.LastDateTime = DateTime.Now;
                     t.TaskLastErrorTime = DateTime.Now;
                    // t.RunRecord.Add(task.TaskKey + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), result);
                  }
                  LogHelper.WriteTaskStatisticsLog(typeof(TaskDescriptorExtention), result,task);
                  UIListener.UpdateTaskUI(OperatorType.RefreshStatusBar);
              }
              TaskProvider.Instance.TaskList.PersistenceTask(true);

            }
        } 
        #endregion

        #region 更新任务配置
        public static void UpdateTaskConfig(this TaskDescriptor task,Dictionary<string,string> taskConfig)
        {
           
            foreach (KeyValuePair<string,string> item in taskConfig)
	            {
                    task.ImplementationInstance.TaskCustomConfig[item.Key] = item.Value;
	            }
           
           
        }
        #endregion

        #region 更新任务状态
       public static void UpdateTaskStatus(this TaskDescriptor task,TaskWorkStatus taskStatus)
        {
            TaskDescriptor t = TaskProvider.Instance.TaskList.FirstOrDefault(x => x.TaskKey == task.TaskKey);
            if (t.TaskStatus == taskStatus)//如果已经相等就不用修改
           {
               return;
           }
            lock (t)
            {
                if(t!= null)
                {
                    t.TaskStatus = taskStatus;
                }
            }
        }
        #endregion
    
      

       
    }
}
