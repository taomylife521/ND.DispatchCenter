using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：MasterSection.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/5/5 10:56:54         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/5/5 10:56:54          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.CommandClientLib.Config
{

 
    public class DispatchCenterClientDetailSection : ConfigurationElement
    {
        /// <summary>
        /// 失败次数
        /// </summary> 
        [ConfigurationProperty("Name", IsRequired = false, DefaultValue = "")]
        public string Name
        {
            get { return base["Name"].ToString(); }
            set { base["Name"] = value; }
        }

        [ConfigurationProperty("SocketBufferSize", IsRequired = true, DefaultValue = "4194304")]
        public int SocketBufferSize
        {
            get { return (int)base["SocketBufferSize"]; }
            set { base["SocketBufferSize"] = value; }
        }

        /// <summary>
        /// Message Buffer Size
        /// 默认1024 bytes
        /// </summary>
         [ConfigurationProperty("MessageBufferSize", IsRequired = true, DefaultValue = "4194304")]
        public int MessageBufferSize
        {
            get { return (int)base["MessageBufferSize"]; }
            set { base["MessageBufferSize"] = value; }
        }
        /// <summary>
        /// 发送超时时间
        /// </summary> 
          [ConfigurationProperty("MillisecondsSendTimeout", IsRequired = true, DefaultValue = "30000")]
        public int MillisecondsSendTimeout
        {
            get { return (int)base["MillisecondsSendTimeout"]; }
            set { base["MillisecondsSendTimeout"] = value; }
        
        }

        /// <summary>
        /// 接收超时时间
        /// </summary> 
       [ConfigurationProperty("MillisecondsReceiveTimeout", IsRequired = true, DefaultValue = "30000")]
        public int MillisecondsReceiveTimeout
        {
            get { return (int)base["MillisecondsReceiveTimeout"]; }
            set { base["MillisecondsReceiveTimeout"] = value; }
        }

        /// <summary>
        /// ip
        /// </summary> 
          [ConfigurationProperty("Ip", IsRequired = true,DefaultValue="127.0.0.1")]
        public string Ip
        {
            get { return base["Ip"].ToString(); }
            set { base["Ip"] = value; }
        }

        /// <summary>
        /// 查询端口
        /// </summary>
         [ConfigurationProperty("SearchPort", IsRequired = true,DefaultValue="2000")]
        public int SearchPort
        {
            get { return (int)base["SearchPort"]; }
            set { base["SearchPort"] = value; }
        }

        /// <summary>
        /// 查询端口
        /// </summary> 
         [ConfigurationProperty("ExcutePort", IsRequired = true,DefaultValue="2001")]
        public int ExcutePort
        {
            get { return (int)base["ExcutePort"]; }
            set { base["ExcutePort"] = value; }
        }

        

        
    }
}
