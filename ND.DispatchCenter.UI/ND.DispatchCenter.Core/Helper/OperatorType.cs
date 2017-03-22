using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：OperatorType.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/29 10:30:16         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/29 10:30:16          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.Helper
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperatorType
    {
         [Description("添加")]
         Add=0,

         [Description("重置重新绑定")]
         Reset = 1,

        [Description("刷新")]
        Refresh=2,

        [Description("删除")]
        Delete=3,

        [Description("清空")]
        Clear = 4,

        [Description("刷新状态栏")]
        RefreshStatusBar = 5,

        [Description("刷新状态栏")]
        RefreshModuleList = 6,

      
    }
}
