using ND.DispatchCenter.Core.ScanModule;
using ND.DispatchCenter.Core.TaskModule;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AssemblyScanExtention.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/23 18:16:33         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/23 18:16:33          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.Extentions
{
   public static class AssemblyScanExtention
    {
       /// <summary>
       /// 初始化任务集合
       /// </summary>
       /// <param name="scaner"></param>
       /// <returns></returns>
       public static ConcurrentDictionary<string, Func<ITask>> InitTaskColletion(this DefaultAssemblyScan scaner)
       {
           ConcurrentDictionary<string, Func<ITask>> _taskCollection = new ConcurrentDictionary<string, Func<ITask>>();
           try
           {
              
               Assembly[] assemblies = scaner.ScanAll();
               foreach (var assembly in assemblies)
               {
                   var typeList = (from a in
                                       (from t in assembly.GetTypes()
                                        select new { TheType = t, Interfaces = t.GetInterfaces() })
                                   where a.Interfaces.Length >= 1 && a.Interfaces.Contains(typeof(ITask)) //&& !a.TheType.IsAbstract
                                   select a).ToList();
                   foreach (var t in typeList)
                   {
                       if(t.Interfaces.Contains(typeof(ITask)))
                       {
                          
                         
                           ConstructorInfo constructorInfo = t.TheType.GetConstructor(Type.EmptyTypes);
                           if (constructorInfo != null)
                           {
                               Func<ITask> func = Expression.Lambda<Func<ITask>>(Expression.New(constructorInfo)).Compile();
                               _taskCollection.TryAdd(t.TheType.FullName, func);
                           }
                           
                          
                       }
                       //Type type = t.Interfaces.FirstOrDefault(tp => { return tp != typeof(ITask); });
                       //ConstructorInfo constructorInfo = t.TheType.GetConstructor(Type.EmptyTypes);
                       //if (constructorInfo != null)
                       //{
                       //    Func<object> func = Expression.Lambda<Func<object>>(Expression.New(constructorInfo)).Compile();
                       //    _taskCollection.TryAdd(type.FullName, func);
                       //}
                   }
               }
               return _taskCollection;
           }
           catch(Exception ex)
           {
               return _taskCollection;
           }

           
       }
    }
}
