using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandCollectionExtention.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 14:52:21         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 14:52:21          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
   public static class CommandCollectionExtention
    {
        #region 修改命令
        /// <summary>
        /// 修改命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandResult ModifyCommand(this CommandCollection collection, CommandDescriptor command)
        {
            try
            {
                CommandProvider.Instance.CommandList.Remove(command, false);
                CommandProvider.Instance.CommandList.Add(command);
                return new CommandResult() { RunStatus = TaskModule.RunStatus.Normal };
            }
            catch (Exception ex)
            {
                return new CommandResult() { RunStatus = TaskModule.RunStatus.Exception, Ex = ex };
            }
        } 
        #endregion


    }
}
