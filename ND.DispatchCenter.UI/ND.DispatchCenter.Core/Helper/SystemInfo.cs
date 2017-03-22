using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

//**********************************************************************
//
// 文件名称(File Name)：SystemInfoHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/7 11:32:09         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/7 11:32:09          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.Helper
{
    public class SystemInfo
    {
         private int m_ProcessorCount = 0;   //CPU个数 
        private PerformanceCounter pcCpuLoad;   //CPU计数器 
        private long m_PhysicalMemory = 0;   //物理内存 

        private const int GW_HWNDFIRST = 0; 
        private const int GW_HWNDNEXT = 2; 
        private const int GWL_STYLE = (-16); 
        private const int WS_VISIBLE = 268435456; 
        private const int WS_BORDER = 8388608; 

        #region AIP声明 
        [DllImport("IpHlpApi.dll")] 
        extern static public uint GetIfTable(byte[] pIfTable, ref uint pdwSize, bool bOrder); 

        [DllImport("User32")] 
        private extern static int GetWindow(int hWnd, int wCmd); 
         
        [DllImport("User32")] 
        private extern static int GetWindowLongA(int hWnd, int wIndx); 

        [DllImport("user32.dll")] 
        private static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize); 

        [DllImport("user32", CharSet = CharSet.Auto)] 
        private extern static int GetWindowTextLength(IntPtr hWnd); 
        #endregion 

        #region 构造函数 
        ///  
        /// 构造函数，初始化计数器等 
        ///  
        public SystemInfo() 
        { 
            //初始化CPU计数器 
               //获取当前进程对象
             Process cur = Process.GetCurrentProcess();
            pcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total"); 
            pcCpuLoad.MachineName = "."; 
            pcCpuLoad.NextValue(); 

            //CPU个数 
            m_ProcessorCount = Environment.ProcessorCount; 

            //获得物理内存 
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem"); 
            ManagementObjectCollection moc = mc.GetInstances(); 
            foreach (ManagementObject mo in moc) 
            { 
                if (mo["TotalPhysicalMemory"] != null) 
                { 
                    m_PhysicalMemory = long.Parse(mo["TotalPhysicalMemory"].ToString()); 
                } 
            }             
        }  
        #endregion 

        #region CPU个数 
        ///  
        /// 获取CPU个数 
        ///  
        public int ProcessorCount 
        { 
            get 
            { 
                return m_ProcessorCount; 
            } 
        } 
        #endregion 

        #region CPU占用率 
        ///  
        /// 获取CPU占用率 
        ///  
        public float CpuLoad 
        { 
            get 
            { 
                return pcCpuLoad.NextValue(); 
            } 
        } 
        #endregion 

        #region 可用内存 
        ///  
        /// 获取可用内存 
        ///  
        public long MemoryAvailable 
        { 
            get 
            { 
                long availablebytes = 0; 
                //ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_PerfRawData_PerfOS_Memory"); 
                //foreach (ManagementObject mo in mos.Get()) 
                //{ 
                //    availablebytes = long.Parse(mo["Availablebytes"].ToString()); 
                //} 
                ManagementClass mos = new ManagementClass("Win32_OperatingSystem"); 
                foreach (ManagementObject mo in mos.GetInstances()) 
                { 
                    if (mo["FreePhysicalMemory"] != null) 
                    { 
                        availablebytes = 1024 * long.Parse(mo["FreePhysicalMemory"].ToString()); 
                    } 
                } 
                return availablebytes; 
            } 
        } 
        #endregion 

        #region 物理内存 
        ///  
        /// 获取物理内存 
        ///  
        public long PhysicalMemory 
        { 
            get 
            { 
                return m_PhysicalMemory; 
            } 
        } 
        #endregion 



     

    
      

      
    }
}
