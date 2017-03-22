using Sodao.FastSocket.Server.Messaging;
//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/3/3 11:31:54         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/3/3 11:31:54           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/3/3 11:31:54         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.DispatchCenter.Core.ListenerModule.Command
{
   public interface ICommand
    {
       int Port { get; set; }
       /// <summary>
       /// 命令参数列表
       /// </summary>
        Dictionary<string, string> CommandParamsList { get; set; }

       /// <summary>
       /// 命令执行消息
       /// </summary>
        event EventHandler<string> OnProcessing;

        /// <summary>
        /// 命令描述
        /// </summary>
        /// <returns></returns>
        string CommandDiscription();

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <returns></returns>
        CommandResult ExcuteCommand(CommandLineMessage cmdContent);

       /// <summary>
       /// 初始化命令参数列表
       /// </summary>
       /// <returns></returns>
       void InitCommandParamList();
    }

   
}
