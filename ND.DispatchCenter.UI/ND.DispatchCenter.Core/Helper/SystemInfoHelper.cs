using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：SystemInfoHelper2.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/7 11:54:30         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/7 11:54:30          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.Helper
{
   public class SystemInfoHelper
    {
        //获取当前进程对象
       public Process cur = Process.GetCurrentProcess();
       public PerformanceCounter totalcpu { get; set; }
       public PerformanceCounter curpcp { get; set; }
       public PerformanceCounter curpc { get; set; }
       public PerformanceCounter curtime { get; set; }
       public double value = 0;
      public  SystemInfo sys = new SystemInfo();
      public const int KB_DIV = 1024;
      public const int MB_DIV = 1024 * 1024;
      public const int GB_DIV = 1024 * 1024 * 1024;
       public  SystemInfoHelper()
       {
           
 
              curpcp = new PerformanceCounter("Process", "Working Set - Private", cur.ProcessName);
              curpc = new PerformanceCounter("Process", "Working Set", cur.ProcessName);
              curtime = new PerformanceCounter("Process", "% Processor Time", cur.ProcessName);
 
             //上次记录CPU的时间
             TimeSpan prevCpuTime = TimeSpan.Zero;
             //Sleep的时间间隔
             int interval = 1000;

             totalcpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

           
            

            //第一种方法计算CPU使用率
                 //当前时间
                 TimeSpan curCpuTime = cur.TotalProcessorTime;
                 //计算
                  value = (curCpuTime - prevCpuTime).TotalMilliseconds / interval / Environment.ProcessorCount * 100;
                 prevCpuTime = curCpuTime;
 
                
                
       }
    }
}
