using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskRealTimeLogEventArgs.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/7 10:15:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/7 10:15:36          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 用于封装实时执行任务日志参数
    /// </summary>
   public class TaskRealTimeLogEventArgs:EventArgs
    {
       public TaskRealTimeLogEventArgs()
       {
           Task = new TaskDescriptor();
       }
       /// <summary>
       /// 输出的实时消息
       /// </summary>
       public string Message { get; set; }

       /// <summary>
       /// 任务描述对象
       /// </summary>
       public TaskDescriptor Task { get; set; }
    }
}
