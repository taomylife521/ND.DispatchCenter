using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskDisposedState.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 18:03:13         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 18:03:13          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    /// <summary>
    /// 任务释放状态
    /// </summary>
   public enum TaskDisposedState
    {
        [Description("未开始")]
        None=0,
        [Description("正在释放")]
        Disposing=1,
        [Description("释放完毕")]
        Finished=2,
    }
}
