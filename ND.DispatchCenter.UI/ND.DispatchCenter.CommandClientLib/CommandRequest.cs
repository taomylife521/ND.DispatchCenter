using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/7 15:30:08         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/7 15:30:08          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.CommandClientLib
{
    public class CommandRequest
    {
        public  CommandRequest(string cmdName,string cmdParam)
        {
            //CmdParam = new Dictionary<string, string>();
            CmdName = cmdName;
            CmdParam = cmdParam;
        }
        /// <summary>
        /// 命令名称
        /// </summary>
        public string CmdName { get; set; }

        /// <summary>
        /// 命令参数
        /// </summary>
        public string CmdParam { get; set; }
    }
}
