using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandExtention.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/3/4 10:51:27         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/3/4 10:51:27          
//             修改理由：         
//**********************************************************************
namespace ND.DispatchCenter.Core.ListenerModule.Command
{
    public static class CommandExtention
    {
        #region 是否存在该任务
        /// <summary>
        /// 是否存在该任务
        /// </summary>
        /// <param name="command"></param>
        /// <param name="taskKey"></param>
        public static CommandResult<TaskDescriptor> IsExistTask(this ICommand command, string taskKey,string taskName="")
        {
            //先判断当前命令提供的参数是否都有
            TaskDescriptor task = new TaskDescriptor();
            if (!string.IsNullOrEmpty(taskKey))
            {
                task = TaskProvider.Instance.TaskList.FirstOrDefault(x => x.TaskKey == taskKey);
            }
            if(!string.IsNullOrEmpty(taskName))
            {
                task = TaskProvider.Instance.TaskList.FirstOrDefault(x => x.ImplementationInstance.TaskName() == taskName);
            }
            if (task == null)
            {
                return new CommandResult<TaskDescriptor>() { RunStatus = RunStatus.Failed, Message = "未找到该任务" };
            }
            return new CommandResult<TaskDescriptor>() { RunStatus = RunStatus.Normal, Data = task };
        } 
        #endregion

        #region 检查任务配置是否正确
        public static CommandResult<TaskDescriptor> IsRightTaskConfig(this ICommand command, string taskKey, Dictionary<string, string> taskConfig,string taskName="")
        {
            CommandResult<TaskDescriptor> cmd = command.IsExistTask(taskKey,taskName);//校验是否存在该任务
            if(cmd.RunStatus != RunStatus.Normal)
            {
                return cmd;
            }
            TaskDescriptor task = cmd.Data;
            if(task.ImplementationInstance.TaskCustomConfig.Count <= 0)//说明不需要提供任何配置
            {
                return new CommandResult<TaskDescriptor>() { RunStatus = RunStatus.Normal,Data=task };
            }
            StringBuilder strErr = new StringBuilder();
            bool flag = true;
            foreach (var item in taskConfig)//检查任务需要的配置是否都已经提供
            {
                if(!task.ImplementationInstance.TaskCustomConfig.ContainsKey(item.Key))
                {
                    flag = false;
                    strErr.AppendLine(item.Key + " Not Empty");
                }
            }
            if(!flag)
            {
                return new CommandResult<TaskDescriptor>() { RunStatus = RunStatus.Failed, Message = strErr.ToString() };
            }
            return new CommandResult<TaskDescriptor>() { RunStatus = RunStatus.Normal,Data=task };
        }
        #endregion

     

       

        #region 检查命令配置参数是否正确
        public static CommandResult IsRightCommandParams(this ICommand cmd, string[] cmdParam)
        {
            StringBuilder strErr = new StringBuilder();
            try
            {
              
                bool flag = true;
                if (cmd.CommandParamsList.Count <= 0)//如果命令不需要提供任何参数直接返回
                {
                    return new CommandResult() { RunStatus = RunStatus.Normal };
                }
                if (cmdParam.Length <=0 || string.IsNullOrEmpty(cmdParam[0]))
                {

                    foreach (var item in cmd.CommandParamsList)
                    {
                        flag = false;
                        strErr.AppendLine("InvalidCommandParam_" + item.Key);
                    }

                }
                if (!flag)
                {
                    return new CommandResult() { RunStatus = RunStatus.Failed, Message = strErr.ToString() };
                }
                Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(cmdParam[0]);
                foreach (var item in dic)
                {
                    if (!cmd.CommandParamsList.ContainsKey(item.Key))
                    {
                        flag = false;
                        strErr.AppendLine("InvalidCommandParam_" + item.Key);
                    }else
                    {
                        cmd.CommandParamsList[item.Key] = item.Value;
                    }
                }
                if (!flag)
                {
                    return new CommandResult() { RunStatus = RunStatus.Failed, Message = strErr.ToString() };
                }
                return new CommandResult() { RunStatus = RunStatus.Normal };
            }
            catch(Exception ex)
            {
                foreach (var item in cmd.CommandParamsList)
                {
                    strErr.AppendLine("InvalidCommandParam_" + item.Key);
                }
                return new CommandResult() { RunStatus = RunStatus.Exception, Message = strErr.ToString() };
            }
        }
        #endregion


    }
}
