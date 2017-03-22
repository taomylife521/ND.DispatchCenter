using ND.DispatchCenter.Core.ScanModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandBuildOptions.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 16:33:19         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 16:33:19          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
    public class CommandBuildOptions
    {
         ///// <summary>
       ///// 获取或设置程序集扫描器
       ///// </summary>
       //public IAssemblyScan AssemblyScaner { get; set; }

       /// <summary>
       /// 获取或设置任务类型扫描器
       /// </summary>
       public ICommandTypeScan CommandTypeScaner { get; set; }
       public CommandBuildOptions()
       {
          // AssemblyScaner = new DefaultAssemblyScan();
           CommandTypeScaner = new DefaultCommandTypeScan();
       }

       public CommandBuildOptions(string path)
       {
           // AssemblyScaner = new DefaultAssemblyScan();
           CommandTypeScaner = new DefaultCommandTypeScan(path);
       }
    }
}
