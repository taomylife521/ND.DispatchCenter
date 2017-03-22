using ND.DispatchCenter.Core.TaskModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandResult.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 13:41:06         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 13:41:06          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
    public class CommandResult<T>
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        public RunStatus RunStatus { get; set; }

        /// <summary>
        /// 运行输出消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 运行异常
        /// </summary>
        public Exception Ex { get; set; }

        /// <summary>
        /// 运行异常
        /// </summary>
        public T Data { get; set; }

        public CommandResult() { }
        public CommandResult(RunStatus runStatus)
        {
            RunStatus = runStatus;
        }

        public CommandResult(RunStatus runStatus, string message)
            : this(runStatus)
        {
            Message = message;
        }

        public CommandResult(RunStatus runStatus, string message, Exception ex)
            : this(runStatus, message)
        {
            Ex = ex;
        }

        public CommandResult(RunStatus runStatus, string message, Exception ex, T data)
            : this(runStatus, message, ex)
        {
            Data = data;
        }
    }

    public class CommandResult : CommandResult<object>
    {
        
    }
}
