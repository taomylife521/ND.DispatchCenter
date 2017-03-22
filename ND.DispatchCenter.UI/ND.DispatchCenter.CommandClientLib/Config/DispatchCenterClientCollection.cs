using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：DispatchCenterClientCollection.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/5/5 11:39:28         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/5/5 11:39:28          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.CommandClientLib.Config
{
    [ConfigurationCollection(typeof(DispatchCenterClientDetailSection))]
    public class DispatchCenterClientCollection : ConfigurationElementCollection        
    {
        new public DispatchCenterClientDetailSection this[string name]    
　　    {        get
            {
                return (DispatchCenterClientDetailSection)base.BaseGet(name);    
    　　    }  
       }
        protected override ConfigurationElement CreateNewElement()
        {
            return new DispatchCenterClientDetailSection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DispatchCenterClientDetailSection)element).Name;   
        }
    }
}
