using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：PortType.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/7 17:17:33         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/7 17:17:33          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule
{
   public enum PortType
    {
        [Description("查询端口")]
       SearchPort=2000,

        [Description("执行端口")]
       ExcutePort=2001,
    }
}
