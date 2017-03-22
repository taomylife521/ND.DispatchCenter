using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AbstractCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 11:33:04         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 11:33:04          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
    public abstract class AbstractCommand : ICommand
    {

        
        public AbstractCommand()
       {
           CommandParamsList = new Dictionary<string, string>();
           InitCommandParamList();
           CommandDiscription();
           InitPort();
       }
       
        /// <summary>
        /// 初始化端口
        /// </summary>
        public virtual void InitPort()
        {
            Port = (int)PortType.SearchPort;
        }
        /// <summary>
        /// 初始化命令参数列表
        /// </summary>
       public abstract void InitCommandParamList();


       public Dictionary<string, string> CommandParamsList
       {
           get;
           set;
       }

       public event EventHandler<string> OnProcessing;

       #region 执行命令日志
       public void ShowCommandLog(string e)
       {
           if (OnProcessing != null)
           {
               OnProcessing(this, e);
           }
       } 
       #endregion


        public abstract string CommandDiscription();


        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmdContent"></param>
        /// <returns></returns>
        public abstract CommandResult ExcuteCommand(Sodao.FastSocket.Server.Messaging.CommandLineMessage cmdContent);


        public int Port
        {
            get;
            set;
        }
    }
}
