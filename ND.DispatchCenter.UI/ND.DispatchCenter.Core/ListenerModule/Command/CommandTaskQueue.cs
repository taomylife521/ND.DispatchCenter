using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandTaskQueue.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/4 14:07:48         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/4 14:07:48          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
    /// <summary>
    /// 命令任务
    /// </summary>
    public class CommandTaskCollection : IDictionary<string,TaskDescriptor>
    {
        private ConcurrentDictionary<string,TaskDescriptor> _taskList;
        public ConcurrentDictionary<string,TaskDescriptor> TaskList { get { return _taskList; } set { _taskList = value; } }
      private static readonly CommandTaskCollection taskCollection;//任务集合
      public static CommandTaskCollection Instance { get { return taskCollection; } }

        static CommandTaskCollection()
        {
            taskCollection = new CommandTaskCollection();
        }
      
        private CommandTaskCollection()
        {
            _taskList = new ConcurrentDictionary<string, TaskDescriptor>();
        }
      public void Clear()
      {
          _taskList.ToList().Clear();
      }

     
      public int Count
      {
          get { return _taskList.Count; }
      }

      public bool IsReadOnly
      {
          get { return false; }
      }





      public void Add(string key, TaskDescriptor value)
      {
          _taskList.TryAdd(key, value); 
          Task.Factory.StartNew(() =>
          {
              RunTaskResult result = new RunTaskResult();
              try
              {
                  //string tempKey = key;
                  value.TaskStatus = TaskWorkStatus.Running;
                  value.UpdateTaskStatus(TaskWorkStatus.Running);
                   result = value.ImplementationInstance.RunTask();//执行方法
                  value.UpdateTaskInfo(result);//更新任务信息
                  int count = _taskList.ToLookup(x => x.Value.TaskKey == value.TaskKey && x.Value.TaskStatus == TaskWorkStatus.Running).ToList().Count();//先判断任务队列中是否还有没有执行完得相同任务如果有，则不更改状态，没有则更改状态为Stop
                  if (count <= 0)//如果没有在执行中的任务则更改为停止状态
                  {
                      value.UpdateTaskStatus(TaskWorkStatus.Stop);
                  }
                 // TaskDescriptor t = new TaskDescriptor();
                  _taskList.TryRemove(key, out value);//移除命令队列中的任务
              }
              catch(Exception ex)
              {
                  value.UpdateTaskInfo(new RunTaskResult() { RunStatus = RunStatus.Exception,Message=ex.Message,Ex=ex,Data=JsonConvert.SerializeObject(value)});
              }
              finally
              {
                  value = null;
                  result = null;
                  key = null;
              }
                
          });
        
      }

      public bool ContainsKey(string key)
      {
          return _taskList.ContainsKey(key);
      }

      public ICollection<string> Keys
      {
          get { return _taskList.Keys; }
      }

      public bool Remove(string key)
      {
          TaskDescriptor a = new TaskDescriptor();
          return _taskList.TryRemove(key,out a);
      }

      public bool TryGetValue(string key, out TaskDescriptor value)
      {
          return _taskList.TryGetValue(key, out value);
      }

      public ICollection<TaskDescriptor> Values
      {
          get { return _taskList.Values; }
      }

      public TaskDescriptor this[string key]
      {
          get { return _taskList[key]; }
          set { _taskList[key] = value; }
      }

      public void Add(KeyValuePair<string, TaskDescriptor> item)
      {
          Add(item.Key, item.Value);
      }

      public bool Contains(KeyValuePair<string, TaskDescriptor> item)
      {
          return Contains(item);
      }

      public void CopyTo(KeyValuePair<string, TaskDescriptor>[] array, int arrayIndex)
      {
          _taskList.ToList().CopyTo(array, arrayIndex);
      }

      public bool Remove(KeyValuePair<string, TaskDescriptor> item)
      {
         return _taskList.ToList().Remove(item);
      }

      public IEnumerator<KeyValuePair<string, TaskDescriptor>> GetEnumerator()
      {
          return _taskList.ToList().GetEnumerator();
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
          return GetEnumerator();
      }
    }
}
