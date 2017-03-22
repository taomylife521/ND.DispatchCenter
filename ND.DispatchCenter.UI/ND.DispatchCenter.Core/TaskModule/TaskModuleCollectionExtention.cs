using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.ListenerModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskModuleCollectionExtention.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/1 14:30:29         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/1 14:30:29          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
   public static class TaskModuleCollectionExtention
    {
       /// <summary>
       /// 修改任务模块
       /// </summary>
       /// <param name="taskCollection"></param>
       /// <param name="taskModule"></param>
       public static RunTaskResult ModifyTaskModule(this TaskModuleCollection taskCollection, TaskModuleDescriptor taskModule)
       {
           try
           {
               //删除原有模块，并复制原有模块文件到该目录
               TaskModuleProvider.Instance.TaskList.Remove(TaskModuleProvider.Instance.TaskList.SingleOrDefault(x => x.TaskModuleKey == taskModule.TaskModuleKey), false);
               TaskModuleProvider.Instance.TaskList.Add(taskModule, false,true);
               UIListener.UpdateTaskModuleUI(OperatorType.Refresh, taskModule);
               return new RunTaskResult() { RunStatus = RunStatus.Normal };
           }
           catch(Exception ex)
           {
               return new RunTaskResult() { RunStatus = RunStatus.Exception, Ex = ex };
           }   
       }


       /// <summary>
       /// 删除任务模块
       /// </summary>
       /// <param name="taskCollection"></param>
       /// <param name="taskModule"></param>
       public static RunTaskResult DeleteTaskModule(this TaskModuleCollection taskCollection, TaskModuleDescriptor taskModule)
       {
           try
           {
               TaskModuleProvider.Instance.TaskList.Remove(TaskModuleProvider.Instance.TaskList.SingleOrDefault(x => x.TaskModuleKey == taskModule.TaskModuleKey), false);
             List<TaskDescriptor> tasks= TaskProvider.Instance.TaskList.Where(x => x.TaskModuleKey == taskModule.TaskModuleKey).ToList();
               tasks.ForEach(x=>{
                   TaskProvider.Instance.TaskList.Remove(x,false);
               });
               if (Directory.Exists(Path.Combine(taskModule.TaskModuleFolderAddr,taskModule.TaskModuleName)))
               {
                   Directory.Delete(Path.Combine(taskModule.TaskModuleFolderAddr, taskModule.TaskModuleName),true);
               }
               UIListener.UpdateTaskModuleUI(OperatorType.Delete, taskModule);
               return new RunTaskResult() { RunStatus = RunStatus.Normal };
           }
           catch (Exception ex)
           {
               return new RunTaskResult() { RunStatus= RunStatus.Exception,Ex=ex};
           }
       }
    }
}
