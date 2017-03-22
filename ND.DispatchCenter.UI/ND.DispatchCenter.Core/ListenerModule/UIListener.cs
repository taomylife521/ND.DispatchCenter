using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.ListenerModule.Command;
using ND.DispatchCenter.Core.TaskModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：Listener.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/2 17:51:53         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/2 17:51:53          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule
{
    public class UIListener
    {
        public static void UpdateTaskModuleUI(OperatorType op, TaskModuleDescriptor task = null)
        {
            TaskModuleProvider.Instance.TaskList.OnOperationMessage(new TaskModuleEventArgs() { OpreatorType = op,  TaskModule = task });
        }

       

        /// <summary>
        /// 更新任务ui
        /// </summary>
        /// <param name="op"></param>
        /// <param name="task"></param>
        public static void UpdateTaskUI(OperatorType op,TaskDescriptor task=null)
        {
            TaskProvider.Instance.TaskList.OnTaskOperationMessage(new TaskEventArgs() { OpreatorType = op, Task= task});
        }

        /// <summary>
        /// 更新命令ui
        /// </summary>
        /// <param name="op"></param>
        /// <param name="task"></param>
        public static void UpdateCommandUI(OperatorType op, CommandDescriptor command = null)
        {
            CommandProvider.Instance.CommandList.OnCommandOperationMessage(new CommandEventArgs() { OpreatorType = op,Command=command});
        }
    }
}
