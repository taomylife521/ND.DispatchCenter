using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：DispatchCenterClientSection.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/5/5 10:54:40         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/5/5 10:54:40          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.CommandClientLib.Config
{
    public class DispatchCenterClientSection : ConfigurationSection
    {
        [ConfigurationProperty("AlarmMail", IsRequired = true, DefaultValue = "taojp@niding.net")]
        public string AlarmMail
        {
            get { return base["AlarmMail"].ToString(); }
            set { base["AlarmMail"] = value; }
        }

        [ConfigurationProperty("Master", IsRequired = true)]
        public DispatchCenterClientDetailSection Master
        {
            get { return (DispatchCenterClientDetailSection)base["Master"]; }
            set { base["Master"] = value; }
        }

        [ConfigurationProperty("Slaves", IsRequired = false)]
        [ConfigurationCollection(typeof(DispatchCenterClientCollection), AddItemName = "Slave")]
        public DispatchCenterClientCollection Slaves
        {
            get { return (DispatchCenterClientCollection)base["Slaves"]; }
            set { base["Slaves"] = value; }
        }
    }
}
