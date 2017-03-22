using ND.DispatchCenter.Core.TaskModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TestTask2.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 15:57:07         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 15:57:07          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core
{
        [Serializable]
    public class TestTask2:AbstractTask
    {
        public override string TaskName()
        {
            return "TestTask2";
        }

        public override string TaskDescription()
        {
            return "I am TestTask2";
        }

        public override RunTaskResult RunTask()
        {
            return new RunTaskResult() { RunStatus= RunStatus.Failed,Message="执行失败!"};
        }

        public override void InitTaskCustomConfig()
        {
           
        }
    }
}
