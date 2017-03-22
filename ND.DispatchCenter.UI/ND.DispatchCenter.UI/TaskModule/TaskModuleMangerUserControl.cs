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
using ND.DispatchCenter.UI.TaskModule;
using ND.DispatchCenter.Core.TaskModule;
using Newtonsoft.Json;
using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.TaskModule;
using DevExpress.LookAndFeel;

namespace ND.DispatchCenter.UI
{
    public partial class TaskModuleMangerUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public TaskModuleMangerUserControl()
        {
            InitializeComponent();
            BindTree();
           
        }

        #region 新增任务模块
        private void btnAddTaskModule_Click(object sender, EventArgs e)
        {
            AddTaskModule module = new AddTaskModule(OperatorType.Add,null,this);
            module.Show();
        } 
        #endregion

        #region 绑定树结构
        public void BindTree()
        {
            this.treeView1.Nodes.Clear();
            string dllName = "";
            for(int i = 0;i<TaskModuleProvider.Instance.TaskList.Count;i++)
            {
                //添加顶节点
                TreeNode tempNode = new TreeNode();
                tempNode.Text = TaskModuleProvider.Instance.TaskList[i].TaskModuleName;
                tempNode.Name = TaskModuleProvider.Instance.TaskList[i].TaskModuleName;
                tempNode.Tag = JsonConvert.SerializeObject(TaskModuleProvider.Instance.TaskList[i]);
                for(int j=0;j<TaskModuleProvider.Instance.TaskList[i].AssemblyCollection.Count;j++)
                {
                    TreeNode tempNode2 = new TreeNode();
                     dllName = TaskModuleProvider.Instance.TaskList[i].AssemblyCollection[j].TaskModuleAssemblyName;
                     tempNode2.Text = dllName.Substring(dllName.LastIndexOf("_") + 1);
                    tempNode2.Name = TaskModuleProvider.Instance.TaskList[i].AssemblyCollection[j].TaskModuleAssemblyName;
                    tempNode2.Tag =JsonConvert.SerializeObject(TaskModuleProvider.Instance.TaskList[i].AssemblyCollection[j]);
                     
                    tempNode.Nodes.Add(tempNode2);
                }
                treeView1.Nodes.Add(tempNode);
            }
          
        }
        #endregion

        #region 树节点单击的时候
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (e.Node.Level == 0)
            {
                TreeNode currentNode = this.treeView1.GetNodeAt(new Point(e.X, e.Y));
                ContextMenuStrip cms = new ContextMenuStrip();
                if (currentNode != null)
                {
                    ToolStripMenuItem editModify = new ToolStripMenuItem("修改");
                    ToolStripMenuItem editDelete = new ToolStripMenuItem("删除");
                    ToolStripMenuItem editRefreshModule = new ToolStripMenuItem("刷新");
                    editModify.Click += new EventHandler(editModify_Click);
                    editDelete.Click += new EventHandler(editDelete_Click);
                    editRefreshModule.Click += new EventHandler(editRefreshModule_Click);
                    cms.Items.Add(editModify);
                    cms.Items.Add(editDelete);
                    cms.Items.Add(editRefreshModule);
                    cms.Show(this.treeView1, e.X, e.Y);
                    this.treeView1.SelectedNode = currentNode;
                }
            }
            else
            {
                TreeNode currentNode = this.treeView1.GetNodeAt(new Point(e.X, e.Y));
                ContextMenuStrip cms = new ContextMenuStrip();
                if (currentNode != null)
                {
                    ToolStripMenuItem editRefresh = new ToolStripMenuItem("刷新");
                    editRefresh.Click += new EventHandler(editRefresh_Click);
                    cms.Items.Add(editRefresh);
                    cms.Show(this.treeView1, e.X, e.Y);
                    this.treeView1.SelectedNode = currentNode;
                }
            }

           }

        /// <summary>
        /// 刷新dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void editRefresh_Click(object sender, EventArgs e)
        {
            TreeNode tn = this.treeView1.SelectedNode;
         TaskModuleAssemblyDescriptor assemblyDescriptor= JsonConvert.DeserializeObject<TaskModuleAssemblyDescriptor>(tn.Tag.ToString());
            int count =TaskProvider.Instance.TaskList.Where(x => x.TaskStatus == TaskWorkStatus.Running).ToList().Count();
            if(count > 0)
            {
                XtraMessageBox.Show("当前此dll中有正在执行的任务,暂不能刷新");
                return;
            }
            List<TaskDescriptor> lstTasks = TaskProvider.Instance.TaskList.Where(x => x.TaskModuleAssemblyKey == assemblyDescriptor.TaskModuleAssemblyKey).ToList();
          lstTasks.ForEach(x =>
          {
              TaskProvider.Instance.TaskList.Remove(x);
          });
          TaskProvider.Initialize(assemblyDescriptor.TaskModuleAssemblyName, assemblyDescriptor.TaskModuleKey, assemblyDescriptor.TaskModuleName, assemblyDescriptor.TaskModuleAssemblyKey, assemblyDescriptor.TaskModuleAssemblyName);//重新初始化
          TaskModuleProvider.RefreshTaskInfo(true);
          BindTree();
          this.treeView1.ExpandAll();
          XtraMessageBox.Show("刷新成功！");
         
        }

        /// <summary>
        /// 刷新任务模块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void editRefreshModule_Click(object sender, EventArgs e)
        {
            TreeNode tn = this.treeView1.SelectedNode;
            TaskModuleDescriptor moduleDescriptor = JsonConvert.DeserializeObject<TaskModuleDescriptor>(tn.Tag.ToString());
            int count = TaskProvider.Instance.TaskList.Where(x => x.TaskStatus == TaskWorkStatus.Running && x.TaskModuleKey == tn.Tag).ToList().Count();
            if (count > 0)
            {
                XtraMessageBox.Show("当前此模块中有正在执行的任务,暂不能刷新");
                return;
            }
            TaskModuleProvider.Initialize(moduleDescriptor);
           // TaskProvider.Initialize(moduleDescriptor.TaskModuleAssemblyAddr, moduleDescriptor.TaskModuleKey, moduleDescriptor.TaskModuleName, moduleDescriptor.TaskModuleAssemblyKey, moduleDescriptor.TaskModuleAssemblyName);//重新初始化
            BindTree();
            this.treeView1.ExpandAll();
            XtraMessageBox.Show("刷新成功！");
          
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void editDelete_Click(object sender, EventArgs e)
        {
            TreeNode tn = this.treeView1.SelectedNode;
           DialogResult res=  XtraMessageBox.Show("如果删除该模块当前模块子任务会一并删除,确定要删除吗?", "提示", MessageBoxButtons.OKCancel);
            if(res == DialogResult.Cancel)
            {
                return;
            }
            TaskModuleDescriptor dr = JsonConvert.DeserializeObject<TaskModuleDescriptor>(tn.Tag.ToString());
            int count = TaskProvider.Instance.TaskList.Where(x => x.TaskStatus == TaskWorkStatus.Running).ToList().Count();
            if (count > 0)
            {
                XtraMessageBox.Show("当前此dll中有正在执行的任务,暂不能删除");
                return;
            }
            RunTaskResult result = TaskModuleProvider.Instance.TaskList.DeleteTaskModule(dr);
            if (result.RunStatus != Core.TaskModule.RunStatus.Normal)
            {
                XtraMessageBox.Show("删除失败！\r\n"+JsonConvert.SerializeObject(result.Ex));
                return;
            }
            BindTree();//重新绑定树
            this.treeView1.ExpandAll();
            XtraMessageBox.Show("删除成功！");
           
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void editModify_Click(object sender, EventArgs e)
        {
            TreeNode tn = this.treeView1.SelectedNode;
            TaskModuleDescriptor dr = JsonConvert.DeserializeObject<TaskModuleDescriptor>(tn.Tag.ToString());
            AddTaskModule module = new AddTaskModule(OperatorType.Refresh, dr,this);
            module.Show();
            this.treeView1.ExpandAll();
          
        }  
        #endregion

        #region 手动持久化
        private void btnSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                TaskModuleProvider.Instance.TaskList.PersistenceTaskModule();
                TaskProvider.Instance.TaskList.PersistenceTask();
            }
            catch (Exception ex)
            {
                MessageBox.Show("持久化失败:" + JsonConvert.SerializeObject(ex));
            }
            MessageBox.Show("持久化成功");
        } 
        #endregion
    }
}
