using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/26 13:56:52         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/26 13:56:52          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.Helper
{
    public class TaskHelper
    {
        public static TaskHelper Instance{get;set;}
        private TaskHelper()
        {

        }
        static TaskHelper()
        {
            Instance = new TaskHelper();
        }

        public void Start(Action action)
        {
            Task.Factory.StartNew(action);
        }
    }
}
