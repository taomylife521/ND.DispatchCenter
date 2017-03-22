using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskProvider.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/25 18:12:03         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/25 18:12:03          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 任务提供类
    /// </summary>
    public  class TaskProvider : IDisposable
    {
        private static readonly TaskProvider provider;//默认依赖对象
        public static TaskProvider Instance { get { return provider; } }
       private  TaskCollection _taskList { get; set; }
       public TaskCollection TaskList { get { return _taskList; } }

       static TaskProvider()
       {
           
           provider = new TaskProvider();
         

           
       }
      
       /// <summary>
       /// 设置任务集合
       /// </summary>
       /// <param name="taskList"></param>
       public void SetTaskCollection(TaskCollection taskList)
       {
           _taskList = taskList;
       }
      
       private TaskProvider()
       {
           _taskList = new TaskCollection();
       }

       public  TaskDescriptor GetTaskDescriptor(Func<TaskDescriptor, bool> func)
       {
           TaskDescriptor task = provider.TaskList.SingleOrDefault(func);
           return task;
       }

       public void Dispose()
       {
           this.Dispose();
       }
       #region 初始化任务
       public static void Initialize(string path, string moduleKey, string moduleName, string taskModuleAssemblyKey, string taskModuleAssemblyName)
       {
           TaskBuilder builder = new TaskBuilder(new TaskBuildOptions(path), moduleKey, moduleName, taskModuleAssemblyKey, taskModuleAssemblyName);
           builder.BuildAsync();//异步载入任务 
       } 
       #endregion
    }

    //public static class TaskProviderExtentions
    //{
    //    public static TaskDescriptor GetTaskDescriptor(this TaskProvider provider, Func<TaskDescriptor, bool> func)
    //    {
    //        TaskDescriptor task = provider.TaskList.SingleOrDefault(func);
    //        return task;
    //    }
    //}
}
