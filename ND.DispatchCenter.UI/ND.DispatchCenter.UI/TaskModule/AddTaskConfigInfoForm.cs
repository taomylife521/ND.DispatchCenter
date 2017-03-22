using ND.DispatchCenter.Core.TaskModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ND.DispatchCenter.UI
{
    public partial class AddTaskConfigInfoForm : Form
    {
        private TaskDescriptor taskDescriptor = new TaskDescriptor();
        public AddTaskConfigInfoForm(TaskDescriptor task)
        {
            InitializeComponent();
            taskDescriptor = task;
        }

        private void btnAddTaskConfig_Click(object sender, EventArgs e)
        {

           if(taskDescriptor.ImplementationInstance.TaskCustomConfig.ContainsKey(this.txtTaskConfigKey.Text))
           {
               MessageBox.Show("不能添加重复的键");
               return;
           }
           taskDescriptor.ImplementationInstance.TaskCustomConfig.Add(this.txtTaskConfigKey.Text, this.txtTaskConfigValue.Text);
           TaskConfigInfoForm form = (TaskConfigInfoForm)this.Owner;
            
           Dictionary<string, string> dic = TaskProvider.Instance.TaskList.SingleOrDefault(x => x.TaskKey == taskDescriptor.TaskKey).ImplementationInstance.TaskCustomConfig;
           if (dic.ContainsKey(this.txtTaskConfigKey.Text))
            {
                dic[this.txtTaskConfigKey.Text] = this.txtTaskConfigValue.Text;
            }
           else
           {
               dic.Add(this.txtTaskConfigKey.Text, this.txtTaskConfigValue.Text);
           }
           TaskProvider.Instance.TaskList.SingleOrDefault(x => x.TaskKey == taskDescriptor.TaskKey).ImplementationInstance.TaskCustomConfig = dic;
           form.BindTaskConfig();
           MessageBox.Show("添加成功");
           this.Hide();
        }
    }
}
