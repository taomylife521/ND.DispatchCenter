using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.TaskModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：DefaultAssemblyScan.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/23 18:06:44         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/23 18:06:44          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ScanModule
{
    /// <summary>
    /// 默认程序集扫描器
    /// </summary>
   public class DefaultAssemblyScan:IAssemblyScan
    {
       private static readonly Dictionary<string, Assembly> AssembliesesDict = new Dictionary<string,Assembly>();
        private  readonly string _path;
        public  DefaultAssemblyScan()
            : this(GetBinPath())
        {

        }
       public  DefaultAssemblyScan(string path)
       {
           _path = path;
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
            if (File.Exists(_path))//要是文件的话
            {
                List<string> lstFiles = new List<string>() { _path };

                Assembly[] assemblies2 = lstFiles.ToArray().Select(x =>
                {
                    byte[] assemblyBuf = File.ReadAllBytes(x);
                    return Assembly.Load(assemblyBuf);
                }).Distinct().ToArray();
                return assemblies2;

            }
            string[] files = Directory.GetFiles(_path, "*.dll", SearchOption.AllDirectories).ToArray();//Path.Combine(_path,"Tasks") Path.Combine(_path, "Tasks")

            Assembly[] assemblies = files.Select(x =>
            {
                byte[] assemblyBuf = File.ReadAllBytes(x);
                return Assembly.Load(assemblyBuf);
            }).Distinct().ToArray();

            return assemblies;
        }

       

       
        private static string GetBinPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            return path == Environment.CurrentDirectory + "\\" ? path : Path.Combine(path, "bin");
        }
    }
}
