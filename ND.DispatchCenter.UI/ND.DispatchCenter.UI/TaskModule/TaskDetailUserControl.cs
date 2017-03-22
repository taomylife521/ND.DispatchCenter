using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ND.DispatchCenter.Core.TaskModule;
using ND.DispatchCenter.Core.ListenerModule;
using System.Threading;
using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.LogModule;

namespace ND.DispatchCenter.UI
{
    [Serializable]
    public partial class TaskDetailUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private TaskDescriptor taskDescriptor = new TaskDescriptor();
        //封装一个用于向LISTBOX控件显示信息的方法
        Action<string> showMsg = null;

        event EventHandler<TaskRealTimeLogEventArgs> onListBoxItemAdded;

        public event EventHandler<EventArgs> onRunTask;

       

        public TaskDetailUserControl()
        {
            try
            {
               
                InitializeComponent();
                showMsg = new Action<string>((txt) =>
                {

                    // MyInvoke mi = new MyInvoke(UpdatelbxLog);
                    if (this.IsHandleCreated)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            if (this.lbxLog.Items.Count >= 100) this.lbxLog.Items.Remove(this.lbxLog.Items[0]);
                            //更新 
                            this.lbxLog.Items.Add(txt);
                        }));
                    }
                });


                this.onListBoxItemAdded += (obj, arg) =>
                {
                    try
                    {
                        LogHelper.WriteTaskRealTimeLog(null, arg.Message, arg.Task.ImplementationInstance.TaskName());
                    }
                    catch { }
                };
                this.timer1.Tick += timer1_Tick;
                this.timer1.Start();
               
            }
            catch(Exception ex)
            { }

           
        }

        ~TaskDetailUserControl()
        {
            this.Dispose();
        }
      
        public TaskDetailUserControl(TaskDescriptor task):this()
        {
            taskDescriptor = task;
            
            //AbstractTask.OnProcessing -= ImplementationInstance_OnProcessing;
            AbstractTask.OnProcessing += ImplementationInstance_OnProcessing;
            //taskDescriptor.ImplementationInstance.OnProcessing += ImplementationInstance_OnProcessing;
            this.lbTaskName.Text = task.ImplementationInstance.TaskName();
            this.lbTaskDescription.Text = task.ImplementationInstance.TaskDescription();
            this.txtTaskKey.Text = task.TaskKey;
            BindTaskStatisticsInfo();
            
        }

        #region 绑定任务统计信息
        public void BindTaskStatisticsInfo()
        {
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new Action(() =>
                {
                    lbxTaskStatistics.Items.Clear();
                    lbxTaskStatistics.Items.Add("任务key:" + taskDescriptor.TaskKey);
                    lbxTaskStatistics.Items.Add("任务类型名称:" + taskDescriptor.TaskType.FullName);
                    lbxTaskStatistics.Items.Add("任务总成功次数:" + taskDescriptor.TotalSucessCount);
                    lbxTaskStatistics.Items.Add("任务总失败次数:" + taskDescriptor.TotalFailedCount);
                    lbxTaskStatistics.Items.Add("任务总共调度次数:" + taskDescriptor.TotalRunCount);
                    lbxTaskStatistics.Items.Add("任务状态:" + taskDescriptor.TaskStatus.ToString());
                    lbxTaskStatistics.Items.Add("最后一次运行时间:" + taskDescriptor.LastDateTime.ToString());
                    lbxTaskStatistics.Items.Add("任务创建时间:" + taskDescriptor.TaskCreateTime.ToString());
                    lbxTaskStatistics.Items.Add("任务更新时间:" + taskDescriptor.TaskUpdateTime.ToString());
                    lbxTaskStatistics.Items.Add("任务上一次结束时间:" + taskDescriptor.TaskLastEndTime.ToString());
                    lbxTaskStatistics.Items.Add("任务出错时间:" + taskDescriptor.TaskLastErrorTime.ToString());
                   
                }));
            }
          
        }
        #endregion

        #region 立即执行
        private void button1_Click(object sender, EventArgs e)
        {
            
                lbxLog.Items.Clear();
                Action action = new Action(() =>
                {
                    try
                    {
                        taskDescriptor.TaskStatus = TaskWorkStatus.Running;
                        BindTaskStatisticsInfo();
                        UIListener.UpdateTaskUI(OperatorType.RefreshStatusBar);
                        RunTaskResult res = taskDescriptor.ImplementationInstance.RunTask();
                        if (res.RunStatus == RunStatus.Normal)
                        {
                          
                            showMsg(DateTime.Now + ":成功执行!" );
                        }
                        else
                        {
                           
                            showMsg(DateTime.Now + ":执行失败!");
                        }
                        taskDescriptor.UpdateTaskInfo(res);
                        BindTaskStatisticsInfo();
                        
                    }
                    catch (Exception ex)
                    {
                        RunTaskResult res = new RunTaskResult() { RunStatus = RunStatus.Exception, Ex = ex, Message = DateTime.Now + ":执行异常", Data = null };
                        taskDescriptor.UpdateTaskInfo(res);
                    }
                });
                TaskHelper.Instance.Start(action);
        }

        void ImplementationInstance_OnProcessing(object sender, string e)
        {

            //showMsg(DateTime.Now + ":" + e);

            if (sender.GetType().FullName== taskDescriptor.ImplementationInstance.GetType().FullName)
            {
                showMsg(DateTime.Now + ":" + e);
                TaskRealTimeLogEventArgs args = new TaskRealTimeLogEventArgs()
                {
                    Message = DateTime.Now + ":" + e,
                    Task = taskDescriptor
                };
                onListBoxItemAdded(this, args);
            }



        }

       
        #endregion

        #region 打开配置信息窗口
        private void btnTaskConfig_Click(object sender, EventArgs e)
        {
            TaskConfigInfoForm configInfo = new TaskConfigInfoForm(taskDescriptor);
            configInfo.StartPosition = FormStartPosition.CenterScreen;
            configInfo.Show(this);
        } 
        #endregion

     
      

        private void lbTaskName_MouseHover(object sender, EventArgs e)
        {
            StringBuilder strInfo = new StringBuilder();
            strInfo.AppendLine("任务key:" + taskDescriptor.TaskKey);
            strInfo.AppendLine("任务类型名称:" + taskDescriptor.TaskType.FullName);
            strInfo.AppendLine("任务总成功次数:" + taskDescriptor.TotalSucessCount);
            strInfo.AppendLine("任务总失败次数:" + taskDescriptor.TotalFailedCount);
            strInfo.AppendLine("任务总共调度次数:" + taskDescriptor.TotalRunCount);
            strInfo.AppendLine("任务状态:" + taskDescriptor.TaskStatus.ToString());
            strInfo.AppendLine("最后一次运行时间:" + taskDescriptor.LastDateTime.ToString());
            strInfo.AppendLine("任务创建时间:" + taskDescriptor.TaskCreateTime.ToString());
            strInfo.AppendLine("任务更新时间:" + taskDescriptor.TaskUpdateTime.ToString());
            strInfo.AppendLine("任务上一次结束时间:" + taskDescriptor.TaskLastEndTime.ToString());
            strInfo.AppendLine("任务出错时间:" + taskDescriptor.TaskLastErrorTime.ToString());
            lbTaskName.ToolTip = strInfo.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BindTaskStatisticsInfo();
           
        }

        private void TaskDetailUserControl_Load(object sender, EventArgs e)
        {
            //Control.CheckForIllegalCrossThreadCalls = false;
        }

       
        
    }
}
