//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/2/24 9:48:45         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/2/24 9:48:45           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/2/24 9:48:45         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.DispatchCenter.Core.TaskModule
{
   public interface ITaskBuilder
    {
       /// <summary>
       /// 组建当前任务集合
       /// </summary>
       /// <returns></returns>
       TaskCollection Build();

       /// <summary>
       /// 异步组建当前任务集合
       /// </summary>
       /// <returns></returns>
       void BuildAsync(Action<TaskCollection> action);
    }
}
