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
    public partial class AddCommandConfigForm : DevExpress.XtraEditors.XtraForm
    {
        private CommandDescriptor _command;
        public AddCommandConfigForm(CommandDescriptor command)
        {
            InitializeComponent();
            _command = command;
        }

        #region 添加命令配置键值
        private void btnAddCommandConfig_Click(object sender, EventArgs e)
        {
            if (_command.CommandInstance.CommandParamsList.ContainsKey(this.txtCommandConfigKey.Text))
           {
               MessageBox.Show("不能添加重复的键");
               return;
           }
            _command.CommandInstance.CommandParamsList.Add(this.txtCommandConfigKey.Text, this.txtCommandConfigValue.Text);
            CommandConfigForm form = (CommandConfigForm)this.Owner;
           Dictionary<string, string> dic = CommandProvider.Instance.CommandList.SingleOrDefault(x => x.CommandKey == _command.CommandKey).CommandInstance.CommandParamsList;
           if (dic.ContainsKey(this.txtCommandConfigKey.Text))
            {
                dic[this.txtCommandConfigKey.Text] = this.txtCommandConfigValue.Text;
            }
           else
           {
               dic.Add(this.txtCommandConfigKey.Text, this.txtCommandConfigValue.Text);
           }
           CommandProvider.Instance.CommandList.SingleOrDefault(x => x.CommandKey == _command.CommandKey).CommandInstance.CommandParamsList = dic;
           form.BindGridView();
           MessageBox.Show("添加成功");
           this.Hide();
        } 
        #endregion
    }
}