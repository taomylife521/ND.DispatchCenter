using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.ListenerModule;
using ND.DispatchCenter.Core.LogModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskModuleProvider.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/26 17:33:45         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/26 17:33:45          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 任务模块提供
    /// </summary>
    public class TaskModuleProvider 
    {
        private static readonly TaskModuleProvider provider;//默认依赖对象
        public static TaskModuleProvider Instance { get { return provider; } }
        private TaskModuleCollection _taskList { get; set; }
        public TaskModuleCollection TaskList { get { return _taskList; } }

       static TaskModuleProvider()
       {
           provider = new TaskModuleProvider();
          // Initialize();
       }

       #region 设置任务集合
       /// <summary>
       /// 设置任务集合
       /// </summary>
       /// <param name="taskList"></param>
       public void SetTaskCollection(TaskModuleCollection taskList)
       {
           _taskList = taskList;
       } 
       #endregion

       private TaskModuleProvider()
       {
           _taskList = new TaskModuleCollection();
       }

       #region 获取任务模块描述
       public TaskModuleDescriptor GetTaskModuleDescriptor(Func<TaskModuleDescriptor, bool> func)
       {
           TaskModuleDescriptor task = provider.TaskList.SingleOrDefault(func);
           return task;
       } 
       #endregion

       #region 初始化任务模块
       public static void Initialize()
       {
           try
           {
               string pathTemp = "";
               string tasks = "";
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "----------------"+DateTime.Now+":初始化开始----------------");
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "开始载入任务模块");
               #region MyRegion
               //string path = AppDomain.CurrentDomain.BaseDirectory;
               //pathTemp = Path.Combine(path, "Config\\TaskModuleConfig.txt").ToString();
               //tasks = File.ReadAllText(pathTemp);
               //List<TaskModuleDescriptor> tkModules = JsonConvert.DeserializeObject<List<TaskModuleDescriptor>>(tasks); 
               #endregion

               #region 替换mongo
               MongoDbHelper<TaskModuleDescriptor> mongo = new MongoDbHelper<TaskModuleDescriptor>("taskmoduleconfig");
               List<TaskModuleDescriptor> tkModules = mongo.FindAll();
               if (tkModules.Count <= 0)
               {
                   LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "未从Mongo中检测到任何任务模块存在");
               } 
               #endregion
               tkModules.ForEach(x =>
               {
                   provider._taskList.Add(x,false,false);
               });
             //  LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "载入任务模块目录");
               string dir = "";
                 string taskModuleAssemblyKey  = "";
                 string taskModuleAssemblyName = "";
               //创建任务模块路径
                 #region 创建任务模块路径
                 //for (int i = 0; i < provider._taskList.Count; i++)
                 //{
                 //    dir = Path.Combine(path, "Tasks\\" + provider._taskList[i].TaskModuleName);
                 //    string[] files = Directory.GetFiles(dir, "*.dll", SearchOption.AllDirectories).ToArray();
                 //    #region 加载任务模块程序集集合
                 //    for (int j = 0; j < files.Length; j++)
                 //    {
                 //        taskModuleAssemblyKey = System.Guid.NewGuid().ToString();
                 //        taskModuleAssemblyName = files[j].Substring(files[j].LastIndexOf('\\') + 1);
                 //        provider._taskList[i].AssemblyCollection.Add(new TaskModuleAssemblyDescriptor()
                 //        {
                 //            TaskModuleAssemblyAddr = files[j],
                 //            TaskModuleAssemblyKey = taskModuleAssemblyKey,
                 //            TaskModuleAssemblyName = taskModuleAssemblyName,
                 //            TaskModuleAssemblyUpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                 //            TaskModuleKey = provider._taskList[i].TaskModuleKey,
                 //            TaskModuleName = provider._taskList[i].TaskModuleName
                 //        });
                 //        TaskProvider.Initialize(dir, provider._taskList[i].TaskModuleKey, provider._taskList[i].TaskModuleName, taskModuleAssemblyKey, taskModuleAssemblyName);
                 //    }
                 //    #endregion

                 //    if (!Directory.Exists(dir))
                 //    {
                 //        Directory.CreateDirectory(dir);
                 //    }

                 //} 
                 #endregion
                 UIListener.UpdateTaskModuleUI(OperatorType.Reset);
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "初始化任务模块成功");
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "动态载入任务列表");
               
               for (int i = 0; i < provider._taskList.Count; i++)
                   {
                      for(int j=0;j< provider._taskList[i].AssemblyCollection.Count;j++)
                      {
                          TaskProvider.Initialize(provider._taskList[i].AssemblyCollection[j].TaskModuleAssemblyName, provider._taskList[i].TaskModuleKey, provider._taskList[i].TaskModuleName, provider._taskList[i].AssemblyCollection[j].TaskModuleAssemblyKey, provider._taskList[i].AssemblyCollection[j].TaskModuleAssemblyName);
                      }
                   }
               UIListener.UpdateTaskUI(OperatorType.Reset);
               RefreshTaskInfo();//刷新任务信息
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "动态载入任务列表成功");
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "开始序列化任务模块和任务列表到MongoDB");
               //重新序列化
               TaskModuleProvider.Instance._taskList.PersistenceTaskModule();//序列化modle
              
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "序列化任务模块和任务列表到MongoDB完成");
              
           }
           catch(Exception ex)
           {
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "初始化任务模块和任务列表失败:"+JsonConvert.SerializeObject(ex));
           }
           LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "----------------"+DateTime.Now+":初始化结束----------------");
          
       }

       public static void RefreshTaskInfo(bool isRefresh=false)
       {
           try
           {
               #region File
               //string path = AppDomain.CurrentDomain.BaseDirectory;
               //string pathTemp = Path.Combine(path, "Config\\TaskConfig.txt").ToString();
               //string tasks = File.ReadAllText(pathTemp);
               //List<TaskDescriptor> taskList = JsonConvert.DeserializeObject<List<TaskDescriptor>>(tasks);
               #endregion

               #region Mongo
               MongoDbHelper<TaskDescriptor> mongo = new MongoDbHelper<TaskDescriptor>("taskconfig");
               List<TaskDescriptor> taskList = mongo.FindAll();
               #endregion
           
              taskList.ForEach(x =>
              {
                  TaskProvider.Instance.TaskList.ToList().ForEach(y =>
                  {
                      if(x.TaskTypeName == y.TaskTypeName && x.TaskModuleKey==y.TaskModuleKey )//同一个类型名称,同一个模块
                      {
                          y.LastDateTime = x.LastDateTime;
                          if(isRefresh)
                          {
                              y.TaskCreateTime = DateTime.Now;
                              y.TaskUpdateTime = DateTime.Now;
                          }
                          else
                          {
                              y.TaskCreateTime = x.TaskCreateTime;
                              y.TaskUpdateTime = x.TaskUpdateTime;
                          }
                          y.TaskLastEndTime = x.TaskLastEndTime;
                          y.TaskLastErrorTime = x.TaskLastErrorTime;
                          y.TotalFailedCount = x.TotalFailedCount;
                          y.TotalRunCount = x.TotalRunCount;
                          y.TotalSucessCount = x.TotalSucessCount;
                          
                      }
                  });
              });
              TaskProvider.Instance.TaskList.PersistenceTask(true);//序列化任务
           }
           catch(Exception ex)
           {
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "刷新任务失败:" + JsonConvert.SerializeObject(ex));
           }
       }
       #endregion

       #region 初始化单个任务模块
       public static void Initialize(TaskModuleDescriptor taskModule)
       {
           try
           {
               string dir = Path.Combine(taskModule.TaskModuleFolderAddr,taskModule.TaskModuleName);
               string taskModuleAssemblyKey = "";
               string taskModuleAssemblyName = "";
              List<TaskModuleDescriptor> moduleList= TaskModuleProvider.Instance.TaskList.Where(x => x.TaskModuleKey == taskModule.TaskModuleKey).ToList();
              moduleList.ForEach(x =>
              {//先从当前内存中移除该模块
                     TaskModuleProvider.Instance.TaskList.Remove(x,false);
               });
               if (TaskProvider.Instance.TaskList.Count > 0)//移除子任务
               {
                   //TaskProvider.Instance.TaskList.Where(x => x.TaskModuleKey == taskModule.TaskModuleKey).ToList().Clear();
                   List<TaskDescriptor> taskList = TaskProvider.Instance.TaskList.Where(x => x.TaskModuleKey == taskModule.TaskModuleKey).ToList();
                   taskList.ForEach(x =>
                   {
                     bool flag=  TaskProvider.Instance.TaskList.Remove(x, false);
                   });
               }
             //  taskModule.AssemblyCollection.Clear();//重新加载程序集
                  // string[] files = Directory.GetFiles(dir, "*.dll", SearchOption.AllDirectories).ToArray();
                   #region 加载任务模块程序集集合
                   for (int j = 0; j < taskModule.AssemblyCollection.ToList().Count; j++)
                   {
                       //taskModuleAssemblyKey = System.Guid.NewGuid().ToString();
                       //taskModuleAssemblyName = files[j].Substring(files[j].LastIndexOf('\\') + 1);
                       //taskModule.AssemblyCollection.Add(new TaskModuleAssemblyDescriptor()
                       //{
                       //    TaskModuleAssemblyAddr = files[j],
                       //    TaskModuleAssemblyKey = taskModuleAssemblyKey,
                       //    TaskModuleAssemblyName = taskModuleAssemblyName,
                       //    TaskModuleAssemblyUpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                       //    TaskModuleKey = taskModule.TaskModuleKey,
                       //    TaskModuleName = taskModule.TaskModuleName
                       //});
                       TaskProvider.Initialize(taskModule.AssemblyCollection.ToList()[j].TaskModuleAssemblyName, taskModule.TaskModuleKey, taskModule.TaskModuleName, taskModuleAssemblyKey, taskModuleAssemblyName);
                   }
                   taskModule.TaskModuleUpdateTime = DateTime.Now;
                 
                   TaskModuleProvider.Instance.TaskList.Add(taskModule, false);
                   RefreshTaskInfo(true);//刷新任务信息
                  // TaskProvider.Instance.TaskList.PersistenceTask();//持久化任务
                   UIListener.UpdateTaskModuleUI(OperatorType.Refresh, taskModule);
               
                   #endregion
               
              
           }
           catch (Exception ex)
           {
               LogHelper.WriteSystemLog(typeof(TaskModuleProvider), "初始化任务模块失败:" + JsonConvert.SerializeObject(ex));
           }
       }
       #endregion


       public void Dispose()
       {
           this.Dispose();
       }
    }
}
