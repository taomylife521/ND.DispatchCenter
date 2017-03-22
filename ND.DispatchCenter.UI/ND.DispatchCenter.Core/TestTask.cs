using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TestTask.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/23 18:47:01         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/23 18:47:01          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core
{
    [Serializable]
   public class TestTask:AbstractTask
    {
       public override string TaskName()
       {
           return TaskCustomConfig["123"] + "TaskName";
       }

       public override string TaskDescription()
       {
           return TaskCustomConfig["123"] + "TaskDescription";
       }
       public override RunTaskResult RunTask()
       {
           ShowProcessIngLog("开始执行");
           ShowProcessIngLog(JsonConvert.SerializeObject(TaskCustomConfig));
           ShowProcessIngLog("执行完毕");
           return new RunTaskResult();
       }


       public override void InitTaskCustomConfig()
       {
           TaskCustomConfig["123"] = "hello world ";
       }
    }
}
