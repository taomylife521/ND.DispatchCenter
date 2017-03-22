using ND.DispatchCenter.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：MongoAssemblyScan.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/4/27 16:38:17         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/4/27 16:38:17          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ScanModule
{
    /// <summary>
    /// 适用Mongo扫描程序集
    /// </summary>
   public class MongoAssemblyScan:IAssemblyScan
    {
         
        private  readonly string _dllFileName;
        public  MongoAssemblyScan()
            : this(GetBinPath())
        {

        }
        public MongoAssemblyScan(string dllFileName)
       {
           _dllFileName = dllFileName;
       }
       /// <summary>
       /// 扫描指定条件的程序集
       /// </summary>
       /// <param name="predicate"></param>
       /// <returns></returns>
        public System.Reflection.Assembly[] Scan(Func<System.Reflection.Assembly, bool> predicate)
        {
          return ScanAll().Where(predicate).ToArray();
        }
      
        /// <summary>
        /// 扫描所有
        /// </summary>
        /// <returns></returns>
        public  System.Reflection.Assembly[] ScanAll()
        {
            
            List<Assembly> lstAssembly = new List<Assembly>();
            Assembly assembly = MongoFileHelper.Instance.ReadAssembly(_dllFileName);
           if (assembly != null)
           {
               lstAssembly.Add(assembly);
           }
           return lstAssembly.ToArray();
        }

       

       
        private static string GetBinPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            return path == Environment.CurrentDirectory + "\\" ? path : Path.Combine(path, "bin");
        }
    }
}
