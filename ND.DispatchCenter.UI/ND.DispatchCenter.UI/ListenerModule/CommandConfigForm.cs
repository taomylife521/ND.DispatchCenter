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
using ND.DispatchCenter.Core.ListenerModule.Command;

namespace ND.DispatchCenter.UI.ListenerModule
{
    public partial class CommandConfigForm : DevExpress.XtraEditors.XtraForm
    {
        private CommandDescriptor commandDescriptor = new CommandDescriptor();
        public CommandConfigForm(CommandDescriptor command)
        {
            InitializeComponent();
            commandDescriptor = command;
            BindGridView();
            btnAddTaskConfig.Visible = false;
            btnOk.Visible = false;
        }

        #region 新增命令配置参数
        private void btnAddTaskConfig_Click(object sender, EventArgs e)
        {
            AddCommandConfigForm addConfig = new AddCommandConfigForm(commandDescriptor);
            addConfig.Owner = this;
            addConfig.StartPosition = FormStartPosition.CenterScreen;
            addConfig.Show();
        } 
        #endregion

        public void BindGridView()
        {
            this.dataGridView1.Rows.Clear();
            foreach (KeyValuePair<string, string> item in commandDescriptor.CommandInstance.CommandParamsList)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = item.Key;
                this.dataGridView1.Rows[index].Cells[1].Value = item.Value;
                index++;
            }

        }

       
    }
}