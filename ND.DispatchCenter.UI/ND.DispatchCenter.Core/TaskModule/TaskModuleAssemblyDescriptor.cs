using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskModuleAssemblyDescriptor.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/1 9:40:32         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/1 9:40:32          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 任务模块下程序集描述
    /// </summary>
    public class TaskModuleAssemblyDescriptor
    {
        /// <summary>
        /// 任务模块Key
        /// </summary>
        public string TaskModuleKey { get; set; }

        /// <summary>
        /// 任务模块名称
        /// </summary>
        public string TaskModuleName { get; set; }

        /// <summary>
        /// 任务模块下程序集Key
        /// </summary>
        public string TaskModuleAssemblyKey { get; set; }

        /// <summary>
        /// 任务模块下程序集名称
        /// </summary>
        public string TaskModuleAssemblyName { get; set; }

        /// <summary>
        /// 任务模块下程序集地址
        /// </summary>
        public string TaskModuleAssemblyAddr { get; set; }

        /// <summary>
        /// 任务模块下程序集创建更新时间
        /// </summary>
        public string TaskModuleAssemblyUpdateTime { get; set; }
    }
}
