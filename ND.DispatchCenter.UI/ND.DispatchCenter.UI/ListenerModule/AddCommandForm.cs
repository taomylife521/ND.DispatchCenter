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
using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.ListenerModule.Command;
using System.IO;

namespace ND.DispatchCenter.UI.ListenerModule
{
    public partial class AddCommandForm : DevExpress.XtraEditors.XtraForm
    {
        private OperatorType _operatorType;
        private CommandDescriptor _command;
        public AddCommandForm(OperatorType operatorType,CommandDescriptor command=null)
        {
            InitializeComponent();
            _operatorType = operatorType;
            _command = command;
            BindContent();
        }

        #region 绑定命令内容
        private void BindContent()
        {
            this.txtCommandKey.Text = System.Guid.NewGuid().ToString();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            this.cmbPort.SelectedIndex = 0;
            switch (_operatorType)
            {
                case OperatorType.Delete:
                case OperatorType.Add:
                    {
                        this.Text = "添加命令";
                        _command = new CommandDescriptor();
                        btnCommandAdd.Visible = true;
                        btnCommandModify.Visible = false;
                    }
                    break;
                case OperatorType.Refresh:
                    {
                        this.Text = "修改命令";
                        btnCommandAdd.Visible = false;
                        btnCommandModify.Visible = true;

                        if (_command != null)
                        {
                            txtCommandKey.Text = _command.CommandKey;
                            txtCommandName.Text = _command.CommandName;
                            txtCommandDescription.Text = _command.CommandDescrption;
                            txtCommandAssemblyName.Text = _command.CommandAssemblyName;
                            txtCommandTypeName.Text = _command.CommandTypeName;
                            this.cmbPort.SelectedValue = _command.Port;
                        }
                    }
                    break;
                default:
                    {
                        
                    }
                    break;
            }
            btnCommandAdd.Visible = false;
            btnCommandModify.Visible = false;
        } 
        #endregion

        #region 添加命令
        private void btnCommandAdd_Click(object sender, EventArgs e)
        {
            _command.CommandKey = this.txtCommandKey.Text;//命令key
            _command.CommandName = this.txtCommandName.Text;//命令名称
            _command.CreateTime = DateTime.Now;//命令创建时间
            _command.Port = Convert.ToInt32(this.cmbPort.SelectedText);//命令端口
            _command.CommandDescrption = this.txtCommandDescription.Text;//命令描述
            _command.CommandAssemblyName = txtCommandAssemblyName.Text;//命令程序集名称
            _command.CommandTypeName = txtCommandTypeName.Text;//命令类型名称
            if (CommandProvider.Instance.CommandList.Where(x => x.CommandName == _command.CommandName).ToList().Count > 0)
            {
                XtraMessageBox.Show("命令名称不能重复");
                return;
            }
            CommandProvider.Instance.CommandList.Add(_command);
            XtraMessageBox.Show("保存成功");
            this.Hide();
            
        } 
        #endregion

        #region 修改命令
        private void btnCommandModify_Click(object sender, EventArgs e)
        {
            if (CommandProvider.Instance.CommandList.Where(x => x.CommandName == _command.CommandName).ToList().Count > 1)
            {
                XtraMessageBox.Show("命令名称不能重复");
                return;
            }
            _command.CommandKey = this.txtCommandKey.Text;//命令key
            _command.CommandName = this.txtCommandName.Text;//命令名称
            _command.CreateTime = DateTime.Now;//命令创建时间
            _command.Port = Convert.ToInt32(this.cmbPort.SelectedText);//命令端口
            _command.CommandDescrption = this.txtCommandDescription.Text;//命令描述
            _command.CommandAssemblyName = txtCommandAssemblyName.Text;//命令程序集名称
            _command.CommandTypeName = txtCommandTypeName.Text;//命令类型名称
            
           //ICommand command=  Activator.CreateInstance(_command.CommandAssemblyName, _command.CommandTypeName) as ICommand;
           // if(command == null)
           // {
           //     MessageBox.Show("当前程序集名称和类型名称找不到该对象");
           //     return;
           // }
           // _command.CommandInstance = command;
            CommandProvider.Instance.CommandList.ModifyCommand(_command);//修改命令
            XtraMessageBox.Show("修改成功");
            this.Hide();
        } 
        #endregion

        #region 命令配置参数
        private void btnCommandArgs_Click(object sender, EventArgs e)
        {
            CommandConfigForm config = new CommandConfigForm(_command);
            config.StartPosition = FormStartPosition.CenterScreen;
            config.Show();
        } 
        #endregion


    }
}