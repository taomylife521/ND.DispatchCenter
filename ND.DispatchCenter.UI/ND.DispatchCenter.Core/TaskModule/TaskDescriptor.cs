using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskDescriptor.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 9:36:31         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 9:36:31          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    [Serializable]
    [BsonIgnoreExtraElementsAttribute(true)]
   
   
   public class TaskDescriptor:ICloneable,IDisposable
    {
   

       public TaskDescriptor()
       { }
        public TaskDescriptor(Type task, ITask implementationInstance, string taskKey)
        {
            TaskType = task;
            ImplementationInstance = implementationInstance;
            TaskKey = taskKey;
            TaskCreateTime = DateTime.Now;
            TaskTypeName = task.FullName;
           // RunRecord = new Dictionary<string, RunTaskResult>();
        }
        public TaskDescriptor(Type task, ITask implementationInstance, string taskKey, string moduleKey, string moduleName, string taskModuleAssemblyKey)
        {
            TaskType = task;
            ImplementationInstance = implementationInstance;
            TaskKey = taskKey;
            TaskCreateTime = DateTime.Now;
            //RunRecord = new Dictionary<string, RunTaskResult>();
            TaskModuleKey = moduleKey;
            TaskModuleName = moduleName;
            TaskModuleAssemblyKey = taskModuleAssemblyKey;
            TaskTypeName = task.FullName;
            
        }
        public TaskDescriptor(Type task, ITask implementationInstance, string taskKey, int totalSucessCount = 0)
            : this(task, implementationInstance, taskKey)
        {
            TotalSucessCount = totalSucessCount;
        }
        public TaskDescriptor(Type task, ITask implementationInstance, string taskKey, int totalSucessCount = 0, int totalFailedCount = 0)
            : this(task, implementationInstance, taskKey, totalSucessCount)
        {
            TotalFailedCount = totalFailedCount;
        }

       public TaskDescriptor(
               Type task, ITask implementationInstance, 
               string taskKey, int totalSucessCount = 0, int totalFailedCount = 0, 
               int totalRunCount = 0, TaskWorkStatus taskStatus= TaskWorkStatus.Stop  
           )
       {
           TaskType = task;
           ImplementationInstance = implementationInstance;
           TaskKey = taskKey;
           TotalSucessCount = totalSucessCount;
           TotalFailedCount = totalFailedCount;
           TotalRunCount = totalRunCount;
           TaskStatus = taskStatus;
           TaskTypeName = task.FullName;
          
          
       }


     

       /// <summary>
       /// 任务模块key
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
       /// 任务类型
       /// </summary>
       [JsonIgnore]
       [BsonIgnore]
       public Type TaskType { get; set; }

       /// <summary>
       /// 类型名称
       /// </summary>
     
       public string TaskTypeName { get; set; }

       /// <summary>
       /// 任务实例
       /// </summary>
       //[JsonConverter(typeof(TaskDescriptorConverter<ITask>))]
        [JsonIgnore]
        [BsonIgnore]
       public ITask ImplementationInstance { get; set; }

       /// <summary>
       /// 任务主键
       /// </summary>
       public string TaskKey { get; set; }

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
       /// 最后一次运行时间
       /// </summary>
     [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
       public DateTime LastDateTime { get; set; }

       /// <summary>
       /// 任务创建时间
       /// </summary>
          [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
       public DateTime TaskCreateTime { get; set; }

       /// <summary>
       /// 任务更新时间
       /// </summary>
          [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
       public DateTime TaskUpdateTime { get; set; }

       /// <summary>
       /// 任务上一次结束时间
       /// </summary>
          [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
       public DateTime TaskLastEndTime { get; set; }

       /// <summary>
       /// 任务出错时间
       /// </summary>
          [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
       public DateTime TaskLastErrorTime { get; set; }


       ///// <summary>
       ///// 任务运行记录 key 为taskId加时间点 value 为执行结果
       ///// </summary>
       //public Dictionary<string,RunTaskResult> RunRecord { get; set; }



       public object Clone()
       {
           #region 序列化克隆对象
           //BinaryFormatter Formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
           //MemoryStream stream = new MemoryStream();
           //Formatter.Serialize(stream, this);
           //stream.Position = 0;
           //TaskDescriptor clonedObj = (TaskDescriptor)Formatter.Deserialize(stream);
           //stream.Close();
           //return clonedObj; 
           #endregion

           #region 反射克隆对象
           //Object targetDeepCopyObj;
           //Type targetType = this.GetType();
           ////值类型  
           //if (targetType.IsValueType == true)
           //{
           //    targetDeepCopyObj = this;
           //}
           ////引用类型   
           //else
           //{
           //    targetDeepCopyObj = System.Activator.CreateInstance(targetType);   //创建引用对象   
           //    System.Reflection.MemberInfo[] memberCollection = this.GetType().GetMembers();

           //    foreach (System.Reflection.MemberInfo member in memberCollection)
           //    {
           //        if (member.MemberType == System.Reflection.MemberTypes.Field)
           //        {
           //            System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)member;
           //            Object fieldValue = field.GetValue(this);
           //            if (fieldValue is ICloneable)
           //            {
           //                field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
           //            }
           //            else
           //            {
           //                field.SetValue(targetDeepCopyObj, fieldValue);
           //            }

           //        }
           //        else if (member.MemberType == System.Reflection.MemberTypes.Property)
           //        {
           //            System.Reflection.PropertyInfo myProperty = (System.Reflection.PropertyInfo)member;
           //            MethodInfo info = myProperty.GetSetMethod(false);
           //            if (info != null)
           //            {
           //                object propertyValue = myProperty.GetValue(this, null);
           //                if (propertyValue is ICloneable)
           //                {
           //                    myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
           //                }
           //                else
           //                {
           //                    myProperty.SetValue(targetDeepCopyObj, propertyValue, null);
           //                }
           //            }

           //        }
           //    }
           //}
           //return targetDeepCopyObj;  
           #endregion

           #region 浅拷贝
           return this.MemberwiseClone();
           #endregion

       }

       public static T DeepCopy<T>(T obj)
       {
           //如果是字符串或值类型则直接返回
           if (obj is string || obj.GetType().IsValueType) return obj;

           object retval = Activator.CreateInstance(obj.GetType());
           PropertyInfo[] fields = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
           foreach (PropertyInfo field in fields)
           {
               try { field.SetValue(retval, DeepCopy(field.GetValue(obj))); }
               catch { }
           }
           return (T)retval;
       }

       public void Dispose()
       {
           this.Dispose();
       }
    }
}
