using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.ListenerModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskBuilder.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 9:48:08         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 9:48:08          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 任务组建类
    /// </summary>
    public class TaskBuilder:ITaskBuilder
    {
        private readonly TaskBuildOptions _options;
        public TaskBuilder Instance;
        public  TaskCollection taskCollection = new TaskCollection();
        public int index = 0;
        private string _moduleKey;
        private string _moduleName;
        private string _taskModuleAssemblyKey;
        private string _taskModuleAssemblyName;

        #region 构造函数
        public TaskBuilder()
            : this(new TaskBuildOptions(), Guid.NewGuid().ToString(), "", Guid.NewGuid().ToString(),"")
        {

        }
        public TaskBuilder(TaskBuildOptions options, string moduleKey, string moduleName, string taskModuleAssemblyKey, string taskModuleAssemblyName)
        {
            _options = options;
            _moduleKey = moduleKey;
            _moduleName = moduleName;
            _taskModuleAssemblyKey = taskModuleAssemblyKey;
            _taskModuleAssemblyName = taskModuleAssemblyName;
            
        } 
        #endregion

        #region Build
        public TaskCollection Build()
        {
            //任务模块初始化下

            TaskCollection tasks = new TaskCollection();
            TaskBuildOptions options = _options;
            Type[] allTypes = options.TaskTypeScaner.ScanAll();
            AddTypeWithInterfaces(tasks, allTypes);
            return tasks;
        } 
        #endregion

       

        #region AddTypeWithInterfaces
        private void AddTypeWithInterfaces(TaskCollection tasks, Type[] implementationTypes)
        {
            foreach (Type implementationType in implementationTypes)
            {
                //if(implementationType.IsInstanceOfType(ITask))
                //{

                //}
                if (taskCollection.Where(x => x.TaskType.FullName == implementationType.FullName).ToList().Count > 0)
                {
                    continue;
                }
                if (implementationType.IsAbstract || implementationType.IsInterface)
                {
                    continue;
                }
                ITask task = Activator.CreateInstance(implementationType) as ITask;
                tasks.Add(new TaskDescriptor(implementationType, task, System.Guid.NewGuid().ToString(), _moduleKey, _moduleName, _taskModuleAssemblyKey));// System.Guid.NewGuid().ToString()
                index++;


            }
        } 
        #endregion

        #region 异步组建任务 BuildAsync
        /// <summary>
        /// 异步组建任务
        /// </summary>
        /// <param name="action"></param>
        public void BuildAsync(Action<TaskCollection> action)
        {
            //Task.Factory.StartNew(() =>
            //{

            //        while (true)
            //        {
                         try
                          {
                            TaskCollection tks = Build();
                            if (TaskProvider.Instance.TaskList.Count <= 0)//说明是第一次加载
                            {
                                tks.ToList().ForEach(m =>
                                {
                                    TaskProvider.Instance.TaskList.Add(m);
                                });
                               
                            }
                            else//不是第一次加载先把已经存在的给删除
                            {

                                TaskProvider.Instance.TaskList.ToList().ForEach(x =>
                                {
                                    tks.Remove(tks.SingleOrDefault(y => y.TaskType.FullName == x.TaskType.FullName));
                                });
                                tks.ToList().ForEach(m =>
                                {
                                    TaskProvider.Instance.TaskList.Add(m);
                                });
                             
                        
                            }
                            if (tks.Count > 0)
                            {
                                action(tks);
                               
                            }
                        }
                         catch (Exception ex)
                         {
                             //记录出错日志
                         }
                        //Thread.Sleep(3000);
                  //  }
               
            //});
        } 
        #endregion

        #region 异步组建任务 BuildAsync
        /// <summary>
        /// 异步组建任务
        /// </summary>
        /// <param name="action"></param>
        public void BuildAsync()
        {
            //Task.Factory.StartNew(() =>
            //{

            //        while (true)
            //        {
            try
            {
                TaskCollection tks = Build();
                //if (TaskProvider.Instance.TaskList.Count <= 0)//说明是第一次加载
                //{
                //    tks.ToList().ForEach(m =>
                //    {
                //        TaskProvider.Instance.TaskList.Add(m,false);
                //        TaskProvider.Instance.TaskList.OnTaskOperationMessage(new TaskEventArgs() { OpreatorType = Helper.OperatorType.Refresh, Task = m });
                //    });
                  

                //}
                //else//不是第一次加载先把所有的删除重新替换
                //{

                //    //TaskProvider.Instance.TaskList.ToList().ForEach(x =>
                //    //{
                //    //   x= tks.SingleOrDefault(y => y.TaskType.FullName == x.TaskType.FullName);
                //    //   x.TaskUpdateTime = DateTime.Now;
                    //});
                    tks.ToList().ForEach(m =>
                    {
                        //if (TaskProvider.Instance.TaskList.Count > 0)
                        //{
                        //   TaskDescriptor task= TaskProvider.Instance.TaskList.FirstOrDefault(x => x.TaskTypeName == m.TaskTypeName);
                        //   if (task != null)
                        //   {
                        //       TaskProvider.Instance.TaskList.Remove(task, false);
                        //   }
                           

                        //}
                       
                        m.TaskUpdateTime = DateTime.Now;
                        TaskProvider.Instance.TaskList.Add(m,false);
                        UIListener.UpdateTaskUI(OperatorType.Refresh, m);
                       
                    });


               // }
               
            }
            catch (Exception ex)
            {
                //记录出错日志
                LogModule.LogHelper.WriteSystemLog(typeof(TaskBuilder), "初始化任务模块地址" +JsonConvert.SerializeObject(_options)+ "下的任务失败");
            }
            //Thread.Sleep(3000);
            //  }

            //});
        }
        #endregion

       
    }
}
