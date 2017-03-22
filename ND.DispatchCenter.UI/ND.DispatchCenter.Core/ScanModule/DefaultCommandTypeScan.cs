using ND.DispatchCenter.Core.ListenerModule.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：DefaultCommandTypeScan.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/3 16:37:28         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/3 16:37:28          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ScanModule
{
    public class DefaultCommandTypeScan : ICommandTypeScan
    {
        private IAssemblyScan AssemblyScaner { get; set; }
        public DefaultCommandTypeScan()
        {
            AssemblyScaner = new MongoAssemblyScan();
        }

        public DefaultCommandTypeScan(string path)
        {
            AssemblyScaner = new MongoAssemblyScan(path);
        }
        public Type[] Scan(Func<Type, bool> predicate)
        {
           return ScanAll().Where(predicate).ToArray();
        }

        public Type[] ScanAll()
        {
            try
            {
                Assembly[] assemblies = AssemblyScaner.ScanAll();
                return assemblies.SelectMany(assembly =>
                    assembly.GetTypes().Where(type =>
                        typeof(ICommand).IsAssignableFrom(type) && !type.IsAbstract))
                    .Distinct().ToArray();
            }
            catch(Exception ex)
            {
                LogModule.LogHelper.WriteSystemLog(typeof(DefaultCommandTypeScan), JsonConvert.SerializeObject(ex));
                return new Type[] { };
            }
        }
    }
}
