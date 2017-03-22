using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandDescriptor.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 13:34:41         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 13:34:41          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
    /// <summary>
    /// 命令描述
    /// </summary>
    public class CommandDescriptor
    {
        public CommandDescriptor(string commandKey, string commandName, int port, string commandAssemblyName, string commandTypeName,ICommand commandInstance)
        {
            CommandKey = commandKey;
            CommandName = commandName;
            Port = port;
            CommandAssemblyName = commandAssemblyName;
            CommandTypeName = commandTypeName;
            RunRecord = new Dictionary<string, CommandResult>();
            CreateTime = DateTime.Now;
            CommandInstance = commandInstance;
        }
        public CommandDescriptor()
        { }

        /// <summary>
        /// 命令Key
        /// </summary>
        public string CommandKey { get; set; }

        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// 监听端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 命令描述
        /// </summary>
        public string CommandDescrption { get; set; }

        /// <summary>
        /// 命令程序集名称
        /// </summary>
        public string CommandAssemblyName { get; set; }

        /// <summary>
        /// 命令类型名称
        /// </summary>
        public string CommandTypeName { get; set; }

        /// <summary>
        /// 命令实例
        /// </summary>
        public ICommand CommandInstance { get; set; }

        /// <summary>
        /// 最后一次运行时间
        /// </summary>
        public DateTime LastDateTime { get; set; }

        /// <summary>
        /// 命令创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 命令更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

 
        /// <summary>
        /// 命令运行记录 key 为调用ip  value 为执行结果
        /// </summary>
        public Dictionary<string, CommandResult> RunRecord { get; set; }
    }
}
