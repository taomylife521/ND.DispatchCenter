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
using System.IO;
using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using ND.DispatchCenter.Core.ListenerModule;

namespace ND.DispatchCenter.UI.TaskModule
{
    public partial class AddTaskModule : DevExpress.XtraEditors.XtraForm
    {
        private OperatorType _operatorType;
        private TaskModuleDescriptor _taskModule;
        private TaskModuleMangerUserControl _control;
        public AddTaskModule(OperatorType operatorType,TaskModuleDescriptor taskModule,TaskModuleMangerUserControl control)
        {
             InitializeComponent(); 
            _operatorType = operatorType;
            _taskModule = taskModule;
            _control = control;
            BindContent();
        }

        #region 绑定内容
        private void BindContent()
        {
            this.txtTaskModuleKey.Text = System.Guid.NewGuid().ToString();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "Tasks").ToString();
            txtModuleAddr.Text = path;
            switch(_operatorType)
            {
                case OperatorType.Delete:
                case OperatorType.Add:
                    {
                        this.Text = "添加任务模块";
                        _taskModule = new TaskModuleDescriptor();
                        btnAddTaskModule.Visible = true;
                        btnModifyTaskModule.Visible = false;
                    }
                    break;
                case OperatorType.Refresh:
                    {
                        this.Text = "修改任务模块";
                        btnAddTaskModule.Visible = false;
                        btnModifyTaskModule.Visible = true;
                        txtTaskModuleName.Enabled = false;
                        if(_taskModule != null)
                        {
                            txtTaskModuleKey.Text = _taskModule.TaskModuleKey;
                            txtTaskModuleName.Text = _taskModule.TaskModuleName;
                            txtTaskModuleDescription.Text = _taskModule.TaskModuleDecription;
                            txtModuleAddr.Text = _taskModule.TaskModuleFolderAddr;
                        }
                    }
                    break;
                default:
                    break;
            }
        } 
        #endregion

        #region 新增任务模块
        private void btnAddTaskModule_Click(object sender, EventArgs e)
        {
            _taskModule.TaskModuleKey = this.txtTaskModuleKey.Text;//任务模块key
            _taskModule.TaskModuleName = this.txtTaskModuleName.Text;//任务模块名称
            _taskModule.TaskModuleCreateTime = DateTime.Now;//任务模块创建时间
            _taskModule.TaskModuleDecription = this.txtTaskModuleDescription.Text;//任务模块描述
            _taskModule.TaskModuleFolderAddr = this.txtModuleAddr.Text;//任务模块地址
            if (!string.IsNullOrEmpty(_taskModule.TaskModuleFolderAddr))
            {
                if(!Directory.Exists(Path.Combine(_taskModule.TaskModuleFolderAddr, _taskModule.TaskModuleName)))
                {
                    Directory.CreateDirectory(Path.Combine(_taskModule.TaskModuleFolderAddr, _taskModule.TaskModuleName));
                }
            }
            TaskModuleProvider.Instance.TaskList.Add(_taskModule);
            
           
            _control.BindTree();//重新绑定
            UIListener.UpdateTaskModuleUI(OperatorType.RefreshModuleList);
            MessageBox.Show("添加成功");
            
                
            this.Hide();
        } 
        #endregion

        #region 修改任务模块
        private void btnModifyTaskModule_Click(object sender, EventArgs e)
        {
            _taskModule.TaskModuleKey = this.txtTaskModuleKey.Text;//任务模块key
            _taskModule.TaskModuleName = this.txtTaskModuleName.Text;//任务模块名称
            _taskModule.TaskModuleCreateTime = DateTime.Now;//任务模块创建时间
            _taskModule.TaskModuleDecription = this.txtTaskModuleDescription.Text;//任务模块描述
            _taskModule.TaskModuleFolderAddr = this.txtModuleAddr.Text;//任务模块地址
           RunTaskResult res= TaskModuleProvider.Instance.TaskList.ModifyTaskModule(_taskModule);
            if(res.RunStatus == Core.TaskModule.RunStatus.Normal)
            {
                XtraMessageBox.Show("修改成功");
                this.Hide();
                _control.BindTree();//重新绑定
                return;
            }
            XtraMessageBox.Show("修改失败\r\n" + JsonConvert.SerializeObject(res.Ex));
        } 
        #endregion
    }
}