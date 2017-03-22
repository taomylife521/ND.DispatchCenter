using ND.DispatchCenter.Core.ScanModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskBuildOptions.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 9:50:27         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 9:50:27          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 任务组建配置信息
    /// </summary>
   public class TaskBuildOptions
    {
       ///// <summary>
       ///// 获取或设置程序集扫描器
       ///// </summary>
       //public IAssemblyScan AssemblyScaner { get; set; }

       /// <summary>
       /// 获取或设置任务类型扫描器
       /// </summary>
       public ITaskTypeScan TaskTypeScaner { get; set; }
       public TaskBuildOptions()
       {
          // AssemblyScaner = new DefaultAssemblyScan();
           TaskTypeScaner = new DefaultTaskTypeScan();
       }

       public TaskBuildOptions(string path)
       {
           // AssemblyScaner = new DefaultAssemblyScan();
           TaskTypeScaner = new DefaultTaskTypeScan(path);
       }
    }
}
