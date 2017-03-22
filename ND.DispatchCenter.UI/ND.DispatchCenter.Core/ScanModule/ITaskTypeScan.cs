using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：IAssemblyTypeScan.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 9:57:07         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 9:57:07          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ScanModule
{
    /// <summary>
    /// 程序集类型扫描
    /// </summary>
    public interface ITaskTypeScan : IScan<Type>
    {
    }

    /// <summary>
    /// 程序集类型扫描
    /// </summary>
    public interface ICommandTypeScan : IScan<Type>
    {
    }
}
