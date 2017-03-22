using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

//**********************************************************************
//
// 文件名称(File Name)：TaskModuleDescriptor.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/2/26 17:18:35         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/2/26 17:18:35          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.TaskModule
{
    [BsonIgnoreExtraElements] 
    public class TaskModuleDescriptor
    {
        public TaskModuleDescriptor()
        {
            AssemblyCollection = new TaskModuleAssemblyCollection();
        }
        /// <summary>
        /// 任务程序集集合
        /// </summary>
        public TaskModuleAssemblyCollection AssemblyCollection { get; set; }

        /// <summary>
        /// 任务模块主键
        /// </summary>
        public string TaskModuleKey { get; set; }

        /// <summary>
        /// 任务模块地址
        /// </summary>
        public string TaskModuleFolderAddr{ get; set; }

        /// <summary>
        /// 任务模块名称
        /// </summary>
        public string TaskModuleName { get; set; }

        /// <summary>
        /// 任务模块描述
        /// </summary>
        public string TaskModuleDecription { get; set; }

        /// <summary>
        /// 任务模块创建时间
        /// </summary>
        public DateTime TaskModuleCreateTime { get; set; }

        /// <summary>
        /// 任务模块更新时间
        /// </summary>
        public DateTime TaskModuleUpdateTime { get; set; }

      

      
    }
}
