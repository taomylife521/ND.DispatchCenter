

using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using ND.DispatchCenter.Core.Helper;

namespace ND.DispatchCenter.Core.LogModule
{
 
   public class LogHelper
    {
       
       static LogHelper()
       {
          
       }
       private LogHelper()
       {

       }
   

       #region MyRegion
        /// <summary>
       ///记录任务统计日志
       /// </summary>
       /// <param name="type">The type.</param>
       /// <param name="message">The message.</param>
       /// <param name="logType">Type of the log.</param>
       public static void WriteTaskStatisticsLog(Type type, RunTaskResult result, TaskDescriptor task)
       {
           try
           {
               #region NLog
               //NLog.ILogger logger = LogManager.GetCurrentClassLogger(type);
               //FileTarget target = LogManager.Configuration.AllTargets.FirstOrDefault(x => x.Name == "ftaskStatistics_wrapped") as FileTarget;
               //string path = AppDomain.CurrentDomain.BaseDirectory;
               //string dirPath = Path.Combine(path, "Logs\\TaskLogs\\StatisticsLog\\" + task.TaskType.FullName + "\\" + DateTime.Now.Year + "-" + DateTime.Now.Month + "\\").ToString();
               //string filePath = Path.Combine(dirPath, DateTime.Now.ToString("yyyy-MM-dd") + ".log");
               //target.FileName = filePath;
               //StringBuilder strLog = new StringBuilder();
               //strLog.AppendLine("");
               //strLog.AppendLine("======== " + DateTime.Now.ToString() + " ========");
               //strLog.AppendLine("Title--" + task.TaskType.FullName);
               //strLog.AppendLine("TaskName--" + task.ImplementationInstance.TaskName());
               //strLog.AppendLine("TaskConfig--" + JsonConvert.SerializeObject(task.ImplementationInstance.TaskCustomConfig));
               //string isSucess = result.RunStatus == RunStatus.Normal ? "是" : "否";
               //strLog.AppendLine("是否成功：" + isSucess);
               //strLog.AppendLine("执行结果：" + JsonConvert.SerializeObject(result));
               //strLog.AppendLine("=====================================");
               //logger.Trace(strLog.ToString()); 
               #endregion


               #region Mongo
               var collectionName = "taskstatistics_" + task.ImplementationInstance.TaskName();
               MongoDbHelper<TaskStatics> mongo = new MongoDbHelper<TaskStatics>(collectionName, true);
               mongo.InsertOne(new TaskStatics(task,result)); 
               #endregion
           }
           catch (Exception ex)
           {
               WriteSystemLog(type, JsonConvert.SerializeObject(ex));
           }
       } 
       #endregion

       #region 记录实时任务日志 Info
       /// <summary>
       ///记录任务日志
       /// </summary>
       /// <param name="type">The type.</param>
       /// <param name="message">The message.</param>
       /// <param name="logType">Type of the log.</param>
       public static void WriteTaskRealTimeLog(Type type, string message,string taskName="SystemTask")
       {
           try
           {
               #region NLog
               //NLog.ILogger logger = LogManager.GetCurrentClassLogger();
               //FileTarget target = LogManager.Configuration.AllTargets.FirstOrDefault(x => x.Name == "ftask_wrapped") as FileTarget;
               //string path = AppDomain.CurrentDomain.BaseDirectory;
               //string dirPath = Path.Combine(path, "Logs\\TaskLogs\\RealTimeLog\\" + taskName + "\\" + DateTime.Now.Year + "-" + DateTime.Now.Month + "\\").ToString();
               //string filePath = Path.Combine(dirPath, DateTime.Now.ToString("yyyy-MM-dd") + ".log");
               //target.FileName = filePath;
               //logger.Info(message); 
               #endregion

               #region Mongo
               var collectionName = "taskrealtime_" + taskName;
               MongoDbHelper<TempLog> mongo = new MongoDbHelper<TempLog>(collectionName, true);
               mongo.InsertOne(new TempLog(message)); 
               #endregion
           }
           catch (Exception ex)
           {
               WriteSystemLog(type, JsonConvert.SerializeObject(ex));
           }
       } 
       #endregion

       #region 记录系统日志 Error
       /// <summary>
       ///记录系统日志
       /// </summary>
       /// <param name="type">The type.</param>
       /// <param name="message">The message.</param>
       /// <param name="logType">Type of the log.</param>
       public static void WriteSystemLog(Type type, string message, LogType logType = LogType.Info)
       {
           try
           {
               #region Mongo
               var collectionName = "systemlog"; //+ DateTime.Now.ToString("yyMMdd");
               MongoDbHelper<TempLog> mongo = new MongoDbHelper<TempLog>(collectionName, true);
               mongo.InsertOne(new TempLog(message)); 
               #endregion

               #region NLog
               // NLog.ILogger logger = LogManager.GetCurrentClassLogger();
               // logger.Debug(message); 
               #endregion
           }
           catch (Exception ex)
           {

           }
       } 
       #endregion

       #region 记录命令监控日志 Fatal
       /// <summary>
       ///记录系统日志
       /// </summary>
       /// <param name="type">The type.</param>
       /// <param name="message">The message.</param>
       /// <param name="logType">Type of the log.</param>
       public static void WriteListenerCommandLog(Type type, string message, LogType logType = LogType.Info)
       {
           try
           {
               #region Mongo
               var collectionName = "listenlog"; //+ DateTime.Now.ToString("yyMMdd");
               MongoDbHelper<TempLog> mongo = new MongoDbHelper<TempLog>(collectionName, true);
               mongo.InsertOne(new TempLog(message)); 
               #endregion

               #region NLog
               // NLog.ILogger logger = LogManager.GetCurrentClassLogger();
               // logger.Fatal(message); 
               #endregion
           }
           catch (Exception ex)
           {
               WriteSystemLog(type, JsonConvert.SerializeObject(ex));
           }
       }
       #endregion
    }


}

 [BsonIgnoreExtraElements] 
public class TempLog
{
    public TempLog(string _msg)
    {
        Msg = _msg;
    }
    public string Msg { get; set; }


    public DateTime dt { get { return  DateTime.Now;} }
}


public class TaskStatics
{
    public TaskStatics(TaskDescriptor _task,RunTaskResult _res)
    {
        Task = _task;
        Result = _res;
    }
    public TaskDescriptor Task { get; set; }

    public RunTaskResult Result { get; set; }

    public DateTime dt { get { return DateTime.Now; } }


}