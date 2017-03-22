//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/2/23 18:04:14         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/2/23 18:04:14           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/2/23 18:04:14         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.DispatchCenter.Core.ScanModule
{
    /// <summary>
    /// 扫描接口
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
   public interface IScan<out TItem>
    {
       /// <summary>
       /// 扫描指定条件的项
       /// </summary>
       /// <param name="predicate"></param>
       /// <returns></returns>
       TItem[] Scan(Func<TItem, bool> predicate);

       /// <summary>
       /// 扫描所有项
       /// </summary>
       /// <returns></returns>
       TItem[] ScanAll();

      
    }
}
