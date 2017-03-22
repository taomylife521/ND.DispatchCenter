using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：RunStatus.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/23 17:41:30         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/23 17:41:30          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    public enum RunStatus
    {
        [Description("正常")]
        Normal = 0,

        [Description("失败")]
        Failed = 1,

        [Description("异常")]
        Exception = 2

    }
}
