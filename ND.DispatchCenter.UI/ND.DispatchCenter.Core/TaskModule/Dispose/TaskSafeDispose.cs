using ND.DispatchCenter.Core.TaskModule.Dispose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskSafeDispose.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 18:02:53         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 18:02:53          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    public class TaskSafeDispose
    {
        private int MaxWaitTime = 5;//秒
        private const int CheckStateTime = 1;//秒

        public TaskDisposedState DisposedState = TaskDisposedState.None;
        public TaskSafeDispose(int maxwaittime = 5)
        {
            MaxWaitTime = maxwaittime;
            DisposedState = TaskDisposedState.None;
        }
        /// <summary>
        /// 阻塞等待资源释放标识,若DisposedState=Finished,则终止等待;若超时,则报错
        /// </summary>
        public void WaitDisposeFinished()
        {
            int count = 0;
            while ((count * CheckStateTime) < MaxWaitTime)
            {
                if(DisposedState == TaskDisposedState.Finished)
                    return;
                System.Threading.Thread.Sleep(CheckStateTime*1000);
                count++;
            }
            throw new TaskSafeDisposeTimeOutException();
        }
    }
}
