using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ND.DispatchCenter.Core.TaskModule;

namespace ND.DispatchCenter.UI
{
    public partial class TaskConfigInfoForm : DevExpress.XtraEditors.XtraForm
    {
        private TaskDescriptor taskDescriptor = new TaskDescriptor();
        public TaskConfigInfoForm(TaskDescriptor task)
        {
            InitializeComponent();
            taskDescriptor = task;
            BindTaskConfig();

        }
        #region 绑定任务配置
        public void BindTaskConfig()
        {
            this.dataGridView1.Rows.Clear();
            foreach (KeyValuePair<string, string> item in taskDescriptor.ImplementationInstance.TaskCustomConfig)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = item.Key;
                this.dataGridView1.Rows[index].Cells[1].Value = item.Value;
                index++;
            }

            //this.dataGridView1.DataSource = taskDescriptor.ImplementationInstance.TaskCustomConfig;

        } 
        #endregion

        #region 新增配置
        private void btnAddTaskConfig_Click(object sender, EventArgs e)
        {
            AddTaskConfigInfoForm addConfig = new AddTaskConfigInfoForm(taskDescriptor);
            addConfig.Owner = this;
            addConfig.StartPosition = FormStartPosition.CenterScreen;
            addConfig.Show();
        } 
        #endregion

        #region 更新内存中配置
        private void btnOk_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[0].Value!=null)
                {
                    if (dic.ContainsKey(this.dataGridView1.Rows[i].Cells[0].Value.ToString()))
                    {
                        MessageBox.Show("不能添加相同键的值");
                        return;
                    }
                    dic.Add(this.dataGridView1.Rows[i].Cells[0].Value.ToString(), this.dataGridView1.Rows[i].Cells[1].Value.ToString());
                }
            }
            TaskProvider.Instance.TaskList.SingleOrDefault(x => x.TaskKey == taskDescriptor.TaskKey).ImplementationInstance.TaskCustomConfig = dic;//重新设置配置
            this.Hide();
            
        } 
        #endregion

      
    }
}