using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskDetailInfo.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/8 13:40:58         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/8 13:40:58          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.CommandClientLib.Model
{
    #region TaskDetailInfo
    public class TaskDetailInfo
    {
        public TaskDetailInfo()
        {
            TaskCustomConfig = new Dictionary<string, string>();
        }
        /// <summary>
        /// 任务模块程序集key
        /// </summary>
        public string TaskModuleAssemblyKey { get; set; }

        /// <summary>
        /// 任务模块名称
        /// </summary>
        public string TaskModuleName { get; set; }

        /// <summary>
        /// 任务模块key
        /// </summary>
        public string TaskModuleKey { get; set; }

        /// <summary>
        /// 任务主键
        /// </summary>
        public string TaskKey { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }


        /// <summary>
        /// 任务描述
        /// </summary>
        public string TaskDescription { get; set; }

        /// <summary>
        /// 总共成功数量
        /// </summary>
        public int TotalSucessCount { get; set; }

        /// <summary>
        /// 总共失败数量
        /// </summary>
        public int TotalFailedCount { get; set; }

        /// <summary>
        /// 总共运行次数
        /// </summary>
        public int TotalRunCount { get; set; }


        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskWorkStatus TaskStatus { get; set; }


        /// <summary>
        /// 任务自定义配置
        /// </summary>
        public System.Collections.Generic.Dictionary<string, string> TaskCustomConfig { get; set; }

        /// <summary>
        /// 最后一次运行时间
        /// </summary>
        public DateTime LastDateTime { get; set; }

        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime TaskCreateTime { get; set; }

        /// <summary>
        /// 任务更新时间
        /// </summary>
        public DateTime TaskUpdateTime { get; set; }

        /// <summary>
        /// 任务上一次结束时间
        /// </summary>
        public DateTime TaskLastEndTime { get; set; }

        /// <summary>
        /// 任务出错时间
        /// </summary>
        public DateTime TaskLastErrorTime { get; set; }
    }
    #endregion

    #region TaskWorkStatus
    public enum TaskWorkStatus
    {
        [Description("停止")]
        Stop = 0,
        [Description("运行中")]
        Running = 1
    } 
    #endregion
}
