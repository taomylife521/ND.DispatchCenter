using ND.DispatchCenter.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandEventArgs.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 15:02:46         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 15:02:46          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
   public class CommandEventArgs:EventArgs
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperatorType OpreatorType { get; set; }

        /// <summary>
        /// 命令描述
        /// </summary>
        public CommandDescriptor Command { get; set; }
    }
}
