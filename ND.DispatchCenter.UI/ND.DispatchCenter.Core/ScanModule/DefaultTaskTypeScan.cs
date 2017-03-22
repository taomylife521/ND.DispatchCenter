using ND.DispatchCenter.Core.TaskModule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：DefaultAssemblyTypeScan.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/24 9:57:49         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/24 9:57:49          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ScanModule
{
    /// <summary>
    /// 所有继承ITask 的任务扫描
    /// </summary>
    public class DefaultTaskTypeScan : ITaskTypeScan
    {
        private IAssemblyScan AssemblyScaner { get; set; }
        public DefaultTaskTypeScan()
        {
            AssemblyScaner = new MongoAssemblyScan();
        }

        public DefaultTaskTypeScan(string path)
        {
            AssemblyScaner = new MongoAssemblyScan(path);
        }
        public Type[] Scan(Func<Type, bool> predicate)
        {
           return ScanAll().Where(predicate).ToArray();
        }

        public Type[] ScanAll()
        {
            Assembly[] assemblies = AssemblyScaner.ScanAll();
            return assemblies.SelectMany(assembly =>
                assembly.GetTypes().Where(type =>
                    typeof(ITask).IsAssignableFrom(type) && !type.IsAbstract))
                .Distinct().ToArray();
        }

      


      
    }
}
