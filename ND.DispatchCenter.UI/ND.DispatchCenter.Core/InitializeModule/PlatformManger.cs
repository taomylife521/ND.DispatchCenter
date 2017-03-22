using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.ListenerModule;
using ND.DispatchCenter.Core.ListenerModule.Command;
using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using Sodao.FastSocket.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：PlatformManger.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/29 15:41:56         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/29 15:41:56          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.InitializeModule
{
   public class PlatformManger
    {
       #region Event
        /// <summary>
        /// 任务模块初始化完成事件
        /// </summary>
        public event EventHandler<TaskModuleEventArgs> onTaskModuleInitializeComplete;

        /// <summary>
        /// 任务初始化完成事件
        /// </summary>
        public event EventHandler<TaskEventArgs> onTaskInitializeComplete;

        /// <summary>
        /// 命令初始化完成事件
        /// </summary>
        public event EventHandler<CommandEventArgs> onCommandInitializeComplete;

        /// <summary>
        /// 任务初始化完成事件
        /// </summary>
        public event EventHandler<string> onListenerInitializeComplete; 
        #endregion

       #region 事件方法
        private void OnCommandInitializeComplete(CommandEventArgs e)
        {
            if (onCommandInitializeComplete != null)
            {
                onCommandInitializeComplete(this, e);
            }
        }
       private  void OnTaskModuleInitializeComplete(TaskModuleEventArgs e)
       {
           if (onTaskModuleInitializeComplete != null)
           {
               onTaskModuleInitializeComplete(this, e);
           }
       }

       private void OnTaskInitializeComplete(TaskEventArgs e)
       {
           if (onTaskInitializeComplete != null)
           {
               onTaskInitializeComplete(this, e);
           }
       }

       private void OnListenerInitializeComplete(string e)
       {
           if (onListenerInitializeComplete != null)
           {
               onListenerInitializeComplete(this, e);
           }
       } 
       #endregion

       /// <summary>
       /// 初始化各个模块
       /// </summary>
       public void Initialize( )
       {
           try
           {
               #region 注册各个模块事件
               //注册动态模块更新事件
               TaskModuleProvider.Instance.TaskList.onOperationMessage += TaskList_onOperationMessage;

               //注册任务模块更新事件
               TaskProvider.Instance.TaskList.onTaskOperationMessage += TaskList_onTaskOperationMessage;

               //注册命令更新事件
               CommandProvider.Instance.CommandList.onCommandOperationMessage += CommandList_onCommandOperationMessage;

               //注册监听完成后事件
               SocketServerService.onSocketMsg += SocketServerService_onSocketMsg;
               #endregion

               #region 初始化任务模块
               TaskModuleProvider.Initialize();//任务模块初始化
               #endregion

               #region 初始化命令模块
               CommandProvider.Refresh();//初始化命令模块
               #endregion

               #region 初始化监听
               StartListenerAsync();//异步开启监听 
               #endregion
           }
           catch(Exception ex)
           {
               OnListenerInitializeComplete("监听初始化异常:"+ex.Message);
           }

         

          
       }

      

       #region 注册事件方法
       void CommandList_onCommandOperationMessage(object sender, CommandEventArgs e)
       {
           OnCommandInitializeComplete(e);
       }
       void SocketServerService_onSocketMsg(object sender, string e)
       {
           OnListenerInitializeComplete(e);
       }

       void TaskList_onTaskOperationMessage(object sender, TaskEventArgs e)
       {
           OnTaskInitializeComplete(e);
       }

       void TaskList_onOperationMessage(object sender, TaskModuleEventArgs e)
       {
           OnTaskModuleInitializeComplete(e);
       }
       
       #endregion

       #region  开启监听
       private  void StartListenerAsync()
       {
           try
           {
               SocketServerManager.Init();
               SocketServerManager.Start();
               OnListenerInitializeComplete("服务器已启动,开始监听");
           }
           catch(Exception ex)
           {
               OnListenerInitializeComplete("服务器启动失败:"+ex.Message+",参数:"+JsonConvert.SerializeObject(ex));
           }
       } 
       #endregion

    }
}
