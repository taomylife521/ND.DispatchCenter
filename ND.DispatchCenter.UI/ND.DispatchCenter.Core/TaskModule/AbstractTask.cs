using ND.DispatchCenter.Core.ListenerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AbstractTask.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/23 17:56:18         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/23 17:56:18          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    [Serializable]
    public abstract class AbstractTask : ITask, IDisposable
    {
        public static event EventHandler<string> OnProcessing;
        public AbstractTask()
        {
            TaskCustomConfig = new Dictionary<string, string>();
            //SafeDisposeOperator = new TaskSafeDispose();
            InitTaskCustomConfig();
        }

        public void ShowProcessIngLog(string content)
        {
            if (OnProcessing != null)
            {
                OnProcessing(this, content);
            }
        }

        ///// <summary>
        ///// 任务安全释放类
        ///// </summary>
        //public TaskSafeDispose SafeDisposeOperator;

        /// <summary>
        /// 任务名称
        /// </summary>
        /// <returns></returns>
        public abstract string TaskName();

        /// <summary>
        /// 任务描述
        /// </summary>
        /// <returns></returns>
        public abstract string TaskDescription();

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <returns></returns>
        public abstract RunTaskResult RunTask();



        /// <summary>
        /// 初始化任务配置
        /// </summary>
        public abstract void InitTaskCustomConfig();

        ///// <summary>
        ///// 监听开启任务
        ///// </summary>
        //public abstract void ListenToRunTask();


        /// <summary>
        /// 任务自定义配置
        /// </summary>
        public Dictionary<string, string> TaskCustomConfig
        {
            get;
            set;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            //if (SafeDisposeOperator != null)
            //{
            //    SafeDisposeOperator.DisposedState = TaskDisposedState.Disposing;
            //    SafeDisposeOperator.WaitDisposeFinished();  
            //}
            this.Dispose();
        }


       // public event EventHandler<string> OnProcessing;
    }
}
