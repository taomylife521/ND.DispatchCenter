using ND.DispatchCenter.Core.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskModuleManger.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/26 17:16:05         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/26 17:16:05          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 任务模块集合
    /// </summary>
    public class TaskModuleCollection : IList<TaskModuleDescriptor>
    {
        /// <summary>
        /// 操作模块集合时的消息事件
        /// </summary>
        public event EventHandler<TaskModuleEventArgs> onOperationMessage;

        public void OnOperationMessage(TaskModuleEventArgs e)
        {
           
            if(onOperationMessage != null)
            {
                onOperationMessage(this, e);
            }
        }
      private readonly List<TaskModuleDescriptor> _taskModuleList = new List<TaskModuleDescriptor>();
       public int IndexOf(TaskModuleDescriptor item)
       {
           return _taskModuleList.IndexOf(item);
       }

       public void Insert(int index, TaskModuleDescriptor item)
       {
           _taskModuleList.Insert(index, item);
           PersistenceTaskModule();//先持久化
           OnOperationMessage(new TaskModuleEventArgs{ OpreatorType= OperatorType.Add,TaskModule=item});
         
       }

       public void RemoveAt(int index)
       {
          
           _taskModuleList.RemoveAt(index);
           PersistenceTaskModule();//先持久化
           OnOperationMessage(new TaskModuleEventArgs { OpreatorType = OperatorType.Delete, TaskModule = _taskModuleList[index] });
         
       }

       public TaskModuleDescriptor this[int index]
       {
           get { return _taskModuleList[index]; }
           set { _taskModuleList[index] = value; }
       }

       public void Add(TaskModuleDescriptor item)
       {
          
           _taskModuleList.Add(item);
           PersistenceTaskModule();//先持久化
           OnOperationMessage(new TaskModuleEventArgs { OpreatorType = OperatorType.Add, TaskModule = item });
        
       }

       public void Add(TaskModuleDescriptor item,bool isTriggerEvent=true)
       {
           
           _taskModuleList.Add(item);
           PersistenceTaskModule();//先持久化
           if (isTriggerEvent)
           {
               OnOperationMessage(new TaskModuleEventArgs { OpreatorType = OperatorType.Add, TaskModule = item });
           }
          
       }

       public void Add(TaskModuleDescriptor item, bool isTriggerEvent = true,bool isPersistenceTaskModule=true)
       {

           _taskModuleList.Add(item);
           if (isPersistenceTaskModule)
           {
               PersistenceTaskModule();//先持久化
           }
           if (isTriggerEvent)
           {
               OnOperationMessage(new TaskModuleEventArgs { OpreatorType = OperatorType.Add, TaskModule = item });
           }

       }

       public void Clear()
       {
          
           _taskModuleList.Clear();
           PersistenceTaskModule();//先持久化
           OnOperationMessage(new TaskModuleEventArgs { OpreatorType = OperatorType.Clear });
         
       }

       public bool Contains(TaskModuleDescriptor item)
       {
           return _taskModuleList.Contains(item);
       }

       public void CopyTo(TaskModuleDescriptor[] array, int arrayIndex)
       {
           _taskModuleList.CopyTo(array, arrayIndex);
       }

       public int Count
       {
           get { return _taskModuleList.Count(); }
       }

       public bool IsReadOnly
       {
           get { return false; }
       }

       public bool Remove(TaskModuleDescriptor item)
       {
         
           bool flag= _taskModuleList.Remove(item);
           PersistenceTaskModule();//先持久化
           if(flag)
           {
               
               OnOperationMessage(new TaskModuleEventArgs { OpreatorType = OperatorType.Delete, TaskModule=item });
           }
         
           return flag;
       }

       public bool Remove(TaskModuleDescriptor item,bool isTriggerEvent=true)
       {

           bool flag = _taskModuleList.Remove(item);
           PersistenceTaskModule();//先持久化
           if (flag)
           {
               if (isTriggerEvent)
               {
                   OnOperationMessage(new TaskModuleEventArgs { OpreatorType = OperatorType.Delete, TaskModule = item });
               }
           }
          
           return flag;
       }

      

       //public bool Remove(TaskModuleDescriptor item, bool isPersistenceTaskModule = true)
       //{
       //    bool flag = _taskModuleList.Remove(item);
       //    if(flag)
       //    {
       //        OnOperationMessage(new TaskModuleEventArgs { OpreatorType = OperatorType.Delete, TaskModule = item });
       //    }
       //    PersistenceTaskModule();
       //    return flag;
       //}

       public IEnumerator<TaskModuleDescriptor> GetEnumerator()
       {
           return _taskModuleList.GetEnumerator();
       }

       System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
       {
           return GetEnumerator();
       }

       #region 持久化任务模块
       public void PersistenceTaskModule()
       {
           try
           {
               #region File
               //string path = AppDomain.CurrentDomain.BaseDirectory;
               //path = Path.Combine(path, "Config\\TaskModuleConfig.txt").ToString();
               //File.WriteAllText(path, JsonConvert.SerializeObject(_taskModuleList)); 
               #endregion

               #region Mongo
               MongoDbHelper<TaskModuleDescriptor> mongo = new MongoDbHelper<TaskModuleDescriptor>("taskmoduleconfig");
               mongo.InsertBatch(_taskModuleList,true);
               #endregion
              
           }
           catch(Exception ex)
           {

           }

       } 
       #endregion
    }
}
