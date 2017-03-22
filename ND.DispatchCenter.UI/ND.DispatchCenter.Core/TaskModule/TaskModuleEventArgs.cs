using ND.DispatchCenter.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskModuleEventArgs.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/1 14:01:17         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/1 14:01:17          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
   public class TaskModuleEventArgs:EventArgs
    {
       /// <summary>
       /// 操作类型
       /// </summary>
      public OperatorType OpreatorType { get; set; }

       /// <summary>
       /// TaskModule描述
       /// </summary>
      public TaskModuleDescriptor TaskModule { get; set; }
    }
}
