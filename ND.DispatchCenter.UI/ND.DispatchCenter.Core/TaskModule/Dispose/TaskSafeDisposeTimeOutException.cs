using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskSafeDisposeTimeOutException.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 18:05:32         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 18:05:32          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule.Dispose
{
    [Serializable]
   public class TaskSafeDisposeTimeOutException:Exception
    {
        public TaskSafeDisposeTimeOutException()
            : base("任务终止时,资源未释放超时。请检查代码是否在检测到任务处于DisposedState=Disposing时,释放任务当前资源,并终止任务继续运行业务代码,并将DisposedState=Finished")
        {

        }

        //父类实现了ISerializable接口的，子类也必须有序列化构造函数，否则反序列化时会出错。
        protected TaskSafeDisposeTimeOutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }
}
