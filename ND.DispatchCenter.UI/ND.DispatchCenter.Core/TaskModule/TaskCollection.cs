using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.DispatchCenter.Core.Helper;

//**********************************************************************
//
// 文件名称(File Name)：TaskCollection.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 9:42:54         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 9:42:54          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 任务集合
    /// </summary>
    public class TaskCollection : IList<TaskDescriptor>
    {
        List<TaskDescriptor> _taskDescriptors = new List<TaskDescriptor>();
       // private  List<TaskDescriptor> _taskDescriptors = new List<TaskDescriptor>();
        /// <summary>
        /// 操作模块集合时的消息事件
        /// </summary>
        public event EventHandler<TaskEventArgs> onTaskOperationMessage;
        public void OnTaskOperationMessage(TaskEventArgs e)
        {
          
            if(onTaskOperationMessage != null)
            {
                onTaskOperationMessage(this, e);
            }
        }
        public TaskCollection()
        {
          
        }
        public int IndexOf(TaskDescriptor item)
        {
            return _taskDescriptors.IndexOf(item);
        }

        public void Insert(int index, TaskDescriptor item)
        {
            _taskDescriptors.Insert(index, item);
           // PersistenceTask();
            OnTaskOperationMessage(new TaskEventArgs() { OpreatorType = Helper.OperatorType.Add, Task = item });
        }

        public void RemoveAt(int index)
        {
            TaskDescriptor item = _taskDescriptors.ToList()[index];
            _taskDescriptors.RemoveAt(index);
            OnTaskOperationMessage(new TaskEventArgs() { OpreatorType = Helper.OperatorType.Delete, Task = item });
        }

        public TaskDescriptor this[int index]
        {
            get { return _taskDescriptors[index]; }
            set { _taskDescriptors[index] = value; }
        }

        public void Add(TaskDescriptor item)
        {
            _taskDescriptors.Add(item); 
           // PersistenceTask();
            OnTaskOperationMessage(new TaskEventArgs() { OpreatorType = Helper.OperatorType.Add, Task = item });
        }

        public void Add(TaskDescriptor item, bool isTriggerEvent = true)
        {
            _taskDescriptors.Add(item);
           // PersistenceTask();
            if(isTriggerEvent)
                       OnTaskOperationMessage(new TaskEventArgs() { OpreatorType = Helper.OperatorType.Add, Task = item });
        }

        public void Clear()
        {
            _taskDescriptors.Clear();
        }

        public bool Contains(TaskDescriptor item)
        {
            return _taskDescriptors.Contains(item);
        }

        public void CopyTo(TaskDescriptor[] array, int arrayIndex)
        {
            _taskDescriptors.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _taskDescriptors.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TaskDescriptor item)
        {
            bool flag= _taskDescriptors.Remove(item);
            //PersistenceTask();
            OnTaskOperationMessage(new TaskEventArgs() { OpreatorType = Helper.OperatorType.Delete, Task = item });
            return flag;
        }

        public bool Remove(TaskDescriptor item,bool isTriggerEvent=true)
        {
            bool flag= _taskDescriptors.Remove(item);
            //PersistenceTask();
            if(isTriggerEvent)
                 OnTaskOperationMessage(new TaskEventArgs() { OpreatorType = Helper.OperatorType.Delete, Task = item });
            return flag;
        }

       

        public IEnumerator<TaskDescriptor> GetEnumerator()
        {
            return _taskDescriptors.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region 持久化任务模块
        public void PersistenceTask(bool isDirectToMongo=false)
        {
            try
            {
                #region File
                //if (_taskDescriptors.Count > 0)
                //{
                //    string path = AppDomain.CurrentDomain.BaseDirectory;
                //    path = Path.Combine(path, "Config\\TaskConfig.txt").ToString();
                //    //path = "e://TaskConfig.txt";
                //    File.WriteAllText(path, JsonConvert.SerializeObject(_taskDescriptors));//
                //} 
                #endregion

               
                #region Mongo
                MongoDbHelper<TaskDescriptor> mongo = new MongoDbHelper<TaskDescriptor>("taskconfig");
                if (!isDirectToMongo)
                {
                    _taskDescriptors.ForEach(m =>
                    {
                        TaskDescriptor task = mongo.FirstOrDefault(x => x.TaskModuleName == m.TaskModuleName && x.TaskTypeName == m.TaskTypeName);
                        if (task != null)
                        {
                            //m.TaskCreateTime = task.TaskCreateTime;
                            m.TaskUpdateTime = task.TaskUpdateTime;
                            m.TaskLastEndTime = task.TaskLastEndTime;
                            m.TaskLastErrorTime = task.TaskLastErrorTime;
                            m.TotalFailedCount = task.TotalFailedCount;
                            m.TotalRunCount = task.TotalRunCount;
                            m.TotalSucessCount = task.TotalSucessCount;
                        }
                    });
                }
                
               
                mongo.InsertBatch(_taskDescriptors, true);
                #endregion
             

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }
        #endregion
    }
}
