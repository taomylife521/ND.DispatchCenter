using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LogType.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/29 14:36:52         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/29 14:36:52          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.LogModule
{
   public enum LogType
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}
