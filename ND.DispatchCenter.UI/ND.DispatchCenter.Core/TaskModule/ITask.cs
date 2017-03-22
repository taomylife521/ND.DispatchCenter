using ND.DispatchCenter.Core.ListenerModule;
//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/2/23 17:36:21         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/2/23 17:36:21           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/2/23 17:36:21         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.DispatchCenter.Core.TaskModule
{
    public interface ITask
    {
        System.Collections.Generic.Dictionary<string, string> TaskCustomConfig { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        /// <returns></returns>
        string TaskName();

        /// <summary>
        /// 任务描述
        /// </summary>
        /// <returns></returns>
        string TaskDescription();

        void InitTaskCustomConfig();

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <returns></returns>
        RunTaskResult RunTask();
    }
}
