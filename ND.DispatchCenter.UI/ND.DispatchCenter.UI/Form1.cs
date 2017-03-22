using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using ND.DispatchCenter.Core.ScanModule;
using ND.DispatchCenter.Core.Extentions;
using ND.DispatchCenter.Core.TaskModule;
using System.Collections.Concurrent;
using System.Reflection;
using DevExpress.XtraNavBar;
using DevExpress.XtraTab;
using ND.DispatchCenter.Core.ListenerModule;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Config;
using ND.DispatchCenter.Core.Helper;
using ND.DispatchCenter.Core.LogModule;
using ND.DispatchCenter.Core.InitializeModule;
using Newtonsoft.Json;
using ND.DispatchCenter.UI.ListenerModule;
using ND.DispatchCenter.Core.ListenerModule.Command;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Threading;
using System.IO;



namespace ND.DispatchCenter.UI
{
    public partial class Form1 : RibbonForm
    {

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        //封装一个用于向LISTBOX控件显示信息的方法
        Action<string> showMsg = null;
        public List<string> lstFiles = new List<string>();

        event EventHandler<string> onListBoxItemAdded;
      
        public Form1()
        {
            try
            {
                
               // CheckForIllegalCrossThreadCalls = false;
                InitializeComponent();
               
            }
            catch(Exception ex)
            {
                MessageBox.Show("平台启动失败:"+JsonConvert.SerializeObject(ex));
            }
          

        }
        ~Form1()
        {
            this.Dispose();
        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitSkinGallery();
            this.StartPosition = FormStartPosition.CenterScreen;
            xtraTabPage1.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            xtraTabCommandManger.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            xtraTabPage2.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            showMsg = new Action<string>((txt) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (this.lbxListenerLog.Items.Count >= 100) this.lbxListenerLog.Items.Remove(this.lbxListenerLog.Items[0]);
                    //更新 
                    this.lbxListenerLog.Items.Add(txt);
                }));
            });


            this.onListBoxItemAdded += (obj, arg) =>
            {
                try
                {
                    LogHelper.WriteListenerCommandLog(null, arg);
                }
                catch { }
            };

            this.navBarControl.Groups.Where(x => x.Name == "taskCollectionGrp").ToList().Clear();

            PlatformManger manger = new PlatformManger();
            manger.onListenerInitializeComplete += manger_onListenerInitializeComplete;
            manger.onTaskInitializeComplete += manger_onTaskInitializeComplete;
            manger.onTaskModuleInitializeComplete += manger_onTaskModuleInitializeComplete;
            manger.onCommandInitializeComplete += manger_onCommandInitializeComplete;
            manger.Initialize();
           // this.timer1.Tick += timer1_Tick;
           // this.timer1.Start();

            BindModuleAndFile();
          
           
           
          
        }

        #region 绑定模块列表和模块名称
        private void BindModuleAndFile()
        {
            this.cbModuleList.Items.Clear();
            this.cbReadModuleName.Items.Clear();
            TaskModuleProvider.Instance.TaskList.ToList().ForEach(x =>
            {
                this.cbModuleList.Items.Add(x.TaskModuleName);
                this.cbReadModuleName.Items.Add(x.TaskModuleName);
            });
            if (TaskModuleProvider.Instance.TaskList.ToList().Count > 0)
            {
                this.cbModuleList.SelectedIndex = 0;
                this.cbReadModuleName.SelectedIndex = 0;
            }
            BindFileListByModuleName(this.cbReadModuleName.Text);
        }
        #endregion

        #region 根据moduelName绑定文件列表
        private void BindFileListByModuleName(string moduleName)
        {
            this.cbReadFileList.Items.Clear();
            this.cbReadFileList.Text = "";
            if(string.IsNullOrEmpty(moduleName))
            {
                return;
            }
           TaskModuleDescriptor module= TaskModuleProvider.Instance.GetTaskModuleDescriptor(x => x.TaskModuleName == moduleName);
           module.AssemblyCollection.ToList().ForEach(x =>
           {
               this.cbReadFileList.Items.Add(x.TaskModuleAssemblyAddr.Substring(x.TaskModuleAssemblyAddr.LastIndexOf("\\")+1));
           });
            if( this.cbReadFileList.Items.Count>0)
            {
                this.cbReadFileList.SelectedIndex = 0;
            }
        }

        #endregion

        #region 定时监控系统资源
        void timer1_Tick(object sender, EventArgs e)
        {
           // SystemInfo helper = new SystemInfo();
          // Process cur= helper.cur;
           //string str = string.Format("{0}:{1}  {2:N}KB CPU使用率：{3}", cur.ProcessName, "工作集        ", helper.curpc.NextValue() / 1024, helper.value);
          // Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3}", cur.ProcessName, "工作集(进程类)", cur.WorkingSet64 / 1024, helper.value);//这个工作集只是在一开始初始化，后期不变
          // Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3}", cur.ProcessName, "工作集        ", helper.curpc.NextValue() / 1024, helper.value);//这个工作集是动态更新的
              //第二种计算CPU使用率的方法
          // Console.WriteLine("{0}:{1}  {2:N}KB CPU使用率：{3}%", cur.ProcessName, "私有工作集    ", helper.curpcp.NextValue() / 1024, helper.curtime.NextValue() / Environment.ProcessorCount);
              //Thread.Sleep(interval);

              //第一种方法获取系统CPU使用情况
             // Console.Write("\r系统CPU使用率：{0}%", helper.totalcpu.NextValue());
              //Thread.Sleep(interval);

              //第二章方法获取系统CPU和内存使用情况
             // Console.Write("\r系统CPU使用率：{0}%，系统内存使用大小：{1}MB({2}GB)", helper.sys.CpuLoad, (helper.sys.PhysicalMemory - helper.sys.MemoryAvailable) / MB_DIV, (helper.sys.PhysicalMemory - helper.sys.MemoryAvailable) / (double)GB_DIV);
           // StringBuilder strMsg =new StringBuilder();
           // strMsg.Append("CPU数量:"+helper.ProcessorCount+"CPU占有率:" + helper.CpuLoad.ToString("0.0") + "% ,可用内存:" + helper.MemoryAvailable + ",物理内存:" + helper.PhysicalMemory + "");
           // CommonSystemListener.Caption = strMsg.ToString();

            Process pr = Process.GetCurrentProcess();
            //Process[] p = Process.GetProcessesByName(cur.ProcessName);//获取指定进程信息
            //// Process[] p = Process.GetProcesses();//获取所有进程信息
            //string cpu = string.Empty;
            //string info = string.Empty;

            //PerformanceCounter pp = new PerformanceCounter();//性能计数器
            //pp.CategoryName = "Process";//指定获取计算机进程信息  如果传Processor参数代表查询计算机CPU 
            //pp.CounterName = "% Processor Time";//占有率
            ////如果pp.CategoryName="Processor",那么你这里赋值这个参数 pp.InstanceName = "_Total"代表查询本计算机的总CPU。
            //pp.InstanceName = cur.ProcessName;//指定进程 
            //pp.MachineName = ".";
            //if (p.Length > 0)
            //{
                //foreach (Process pr in p)
                //{
                   // while (true)//1秒钟读取一次CPU占有率。
                   // {
                       string info = pr.ProcessName + "内存：" +
                                                (pr.PrivateMemorySize64/1024).ToString();//得到进程内存
                       CommonSystemListener.Caption = info; //+ "    CPU使用情况：" + Math.Round(pp.NextValue(), 2).ToString() + "%";
                       
                  //  }
               // }
           // }
           
        } 
        #endregion

       
        #region 平台管理初始化事件方法

        #region 命令初始化完毕
        /// <summary>
        /// 命令初始化完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void manger_onCommandInitializeComplete(object sender, Core.ListenerModule.Command.CommandEventArgs e)
        {
            #region 根据命令更新任务状态

            BindCommandTree();
            UpdateCommandStausBar();
            #endregion
        }
        
        #endregion

        #region 绑定命令树
        public void BindCommandTree()
        {
            this.CommandTreeView1.Nodes.Clear();
            for (int i = 0; i < CommandProvider.Instance.CommandList.Count; i++)
            {
                //添加顶节点
                TreeNode tempNode = new TreeNode();
                tempNode.Text = CommandProvider.Instance.CommandList[i].CommandName;
                tempNode.Name = CommandProvider.Instance.CommandList[i].CommandName;
                tempNode.Tag = CommandProvider.Instance.CommandList[i].CommandKey;

                CommandTreeView1.Nodes.Add(tempNode);
            }

        } 
        #endregion

        #region 任务模块更新消息
        /// <summary>
        /// 任务模块更新消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void manger_onTaskModuleInitializeComplete(object sender, TaskModuleEventArgs e)
        {
            #region 根据命令类型更新状态
            switch (e.OpreatorType)
            {
                case OperatorType.Add:
                    {
                        NavBarGroup navGrp = new NavBarGroup();
                        navGrp.Caption = e.TaskModule.TaskModuleName;
                        navGrp.Tag = e.TaskModule.TaskModuleKey;
                        navGrp.Name = "taskCollectionGrp";
                        navGrp.SmallImage = ((System.Drawing.Image)(resources.GetObject("taskCollectionGrp.SmallImage")));
                        this.navBarControl.Groups.Add(navGrp);
                    }
                    break;
                case OperatorType.Reset://重置任务模块
                    {
                        this.navBarControl.Groups.Where(x => x.Name == "taskCollectionGrp").ToList().Clear();
                        for (int i = 0; i < TaskModuleProvider.Instance.TaskList.Count; i++)
                        {
                            NavBarGroup barGrp = new NavBarGroup()
                            {
                                Caption = TaskModuleProvider.Instance.TaskList[i].TaskModuleName,
                                Tag = TaskModuleProvider.Instance.TaskList[i].TaskModuleKey,
                                SmallImage = ((System.Drawing.Image)(resources.GetObject("taskCollectionGrp.SmallImage")))
                            };
                            this.navBarControl.Groups.Add(barGrp);
                        }
                    }
                    break;
                case OperatorType.Clear:
                    {
                        this.navBarControl.Groups.Where(x => x.Name == "taskCollectionGrp").ToList().ForEach(m =>
                        {
                            this.navBarControl.Groups.Remove(m);
                        });
                    }
                    break;
                case OperatorType.Delete:
                    {
                        NavBarGroup navGrp = GetNavBarGroup(e.TaskModule.TaskModuleKey, e.TaskModule.TaskModuleName);
                        if (navGrp != null)
                        {
                            this.navBarControl.Groups.Remove(navGrp);
                        }
                    }
                    break;
                case OperatorType.Refresh://先移除后添加
                    {
                        NavBarGroup navGrp = new NavBarGroup();

                        navGrp = GetNavBarGroup(e.TaskModule.TaskModuleKey, e.TaskModule.TaskModuleName);
                        if (navGrp != null)
                        {
                            this.navBarControl.Groups.Remove(navGrp);
                        }
                        navGrp = new NavBarGroup();
                        navGrp.Caption = e.TaskModule.TaskModuleName;
                        navGrp.Tag = e.TaskModule.TaskModuleKey;
                        navGrp.SmallImage = ((System.Drawing.Image)(resources.GetObject("taskCollectionGrp.SmallImage")));
                        this.navBarControl.Groups.Add(navGrp);
                        //加载任务
                        TaskProvider.Instance.TaskList.Where(x => x.TaskModuleKey == e.TaskModule.TaskModuleKey).ToList().ForEach(x =>
                        {
                            TaskProvider.Instance.TaskList.OnTaskOperationMessage(new TaskEventArgs() { OpreatorType = OperatorType.Refresh, Task = x });
                        });

                    }
                    break;
                case OperatorType.RefreshModuleList:
                    BindModuleAndFile();
                    break;
                default:
                    break;
            }
            UpdateStausBar();
            #endregion
        } 
        #endregion

        #region 任务更新
        /// <summary>
        /// 任务更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void manger_onTaskInitializeComplete(object sender, TaskEventArgs e)
        {
            #region 根据命令更新任务状态
            switch (e.OpreatorType)
            {

                case OperatorType.Add:
                    {
                        NavBarGroup barGrp = GetNavBarGroup(e.Task.TaskModuleKey, e.Task.TaskModuleName);
                        NavBarItem barItem = new NavBarItem()
                        {
                            Caption = e.Task.ImplementationInstance.TaskName(),
                            Tag = e.Task.TaskKey, //JsonConvert.SerializeObject(e.Task, new JsonSerializerSettings() { TypeNameHandling= TypeNameHandling.Auto}),
                            Hint = "任务名称:" + e.Task.ImplementationInstance.TaskName() + "\r\n任务描述:" + e.Task.ImplementationInstance.TaskDescription()
                            //SmallImage = ((System.Drawing.Image)(resources.GetObject("taskCollectionGrp.SmallImage")))
                        };
                        barItem.LinkClicked += Item_LinkClicked;
                        barGrp.ItemLinks.Add(barItem);
                    }
                    break;
                case OperatorType.Reset:
                    {

                        for (int i = 0; i < navBarControl.Groups.Count; i++)
                        {
                            NavBarGroup barGrp = navBarControl.Groups[i];
                            if (barGrp.Name != "taskCollectionGrp") continue;
                            barGrp.ItemLinks.Clear();
                            List<TaskDescriptor> tasks = TaskProvider.Instance.TaskList.Where(m => m.TaskModuleKey == navBarControl.Groups[i].Tag).ToList();
                            tasks.ForEach(n =>
                            {
                                string taskName = n.ImplementationInstance.TaskName();
                                string taskDescription = n.ImplementationInstance.TaskDescription();
                                NavBarItem barItem = new NavBarItem()
                                {
                                    Caption = taskName,
                                    Tag = n.TaskKey,//JsonConvert.SerializeObject(n),
                                    Hint = "任务名称:" + taskName + "\r\n任务描述:" + taskDescription
                                    //SmallImage = ((System.Drawing.Image)(resources.GetObject("taskCollectionGrp.SmallImage")))
                                };
                                barItem.LinkClicked += Item_LinkClicked;
                                barGrp.ItemLinks.Add(barItem);
                            });

                        }
                    }
                    break;
                case OperatorType.Clear:
                    {
                        NavBarGroup barGrp = GetNavBarGroup(e.Task.TaskModuleKey, e.Task.TaskModuleName);
                        barGrp.ItemLinks.Clear();
                    }
                    break;
                case OperatorType.Delete:
                    {
                        NavBarGroup barGrp = GetNavBarGroup(e.Task.TaskModuleKey, e.Task.TaskModuleName);
                        NavBarItemLink itemLink = GetNavBarItemLink(barGrp, e.Task.TaskKey);
                        if (itemLink != null)
                        {
                            barGrp.ItemLinks.Remove(itemLink);//先移除后添加
                        }

                    }
                    break;
                case OperatorType.Refresh:
                    {
                        NavBarGroup barGrp = GetNavBarGroup(e.Task.TaskModuleKey, e.Task.TaskModuleName);
                        NavBarItemLink itemLink = GetNavBarItemLink(barGrp, e.Task.TaskKey,e.Task.ImplementationInstance.TaskName());
                        if (itemLink != null)
                        {
                            barGrp.ItemLinks.Remove(itemLink);//先移除后添加
                        }
                        NavBarItem barItem = new NavBarItem()
                        {
                            Caption = e.Task.ImplementationInstance.TaskName(),
                            Tag = e.Task.TaskKey,//JsonConvert.SerializeObject(e.Task),
                            Hint = "任务名称:" + e.Task.ImplementationInstance.TaskName() + "\r\n任务描述:" + e.Task.ImplementationInstance.TaskDescription()
                            //SmallImage = ((System.Drawing.Image)(resources.GetObject("taskCollectionGrp.SmallImage")))
                        };
                        barItem.LinkClicked += Item_LinkClicked;
                        barGrp.ItemLinks.Add(barItem);
                    }
                    break;

                default:
                    break;
            }
            UpdateStausBar();
            #endregion
        } 
        #endregion

        #region 监听初始化
        /// <summary>
        /// 监听初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void manger_onListenerInitializeComplete(object sender, string e)
        {
           // lbxListenerLog.Items.Add(DateTime.Now + ":" + e);
            showMsg(DateTime.Now + ":" + e);
            onListBoxItemAdded(this, DateTime.Now + ":" + e);//频繁读写报异常
        }  
        #endregion

        #endregion




        #region 更新状态栏信息
        private void UpdateStausBar()
        {
            int taskModulaCount = TaskModuleProvider.Instance.TaskList.Count;
            int taskCount = TaskProvider.Instance.TaskList.Count;
            int runningTaskCount = TaskProvider.Instance.TaskList.Where(x => x.TaskStatus == TaskWorkStatus.Running).ToList().Count;
            siStatus.Caption = "当前总任务模块数量:" + taskModulaCount + "\r\n当前总任务数量:" + taskCount + "当前正在运行的任务数量:" + runningTaskCount;
        } 
        private  void UpdateCommandStausBar()
        {
            CommandStatusBar.Caption = "当前监听命令个数:" + CommandProvider.Instance.CommandList.Count;
        }
        #endregion
      


        #region 左边列表单机某项时
        void Item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            // MessageBox.Show(e.Link.Caption);
          //  TaskDescriptor task = JsonConvert.DeserializeObject<TaskDescriptor>(e.Link.Item.Tag.ToString());
            int count = taskTabCollection.TabPages.Where(x => e.Link.Item.Tag == x.Tag).ToList().Count;
            if (count <= 0)
            {
                XtraTabPage tabPage = new DevExpress.XtraTab.XtraTabPage()
                {
                    Text = e.Link.Caption,
                    Tag = e.Link.Item.Tag,
                    ShowCloseButton = DevExpress.Utils.DefaultBoolean.True
                };
               TaskDescriptor task= TaskProvider.Instance.TaskList.SingleOrDefault(x => x.TaskKey == e.Link.Item.Tag);
               var aaa = new TaskDetailUserControl(task);
               tabPage.Controls.Add(aaa);
                taskTabCollection.TabPages.Add(tabPage);
                taskTabCollection.SelectedTabPage = tabPage;
            }
            else
            {
                //taskTabCollection.TabPages.SingleOrDefault(x => e.Link.Item.Tag == x.Tag).Focus();
                taskTabCollection.SelectedTabPage = taskTabCollection.TabPages.SingleOrDefault(x => e.Link.Item.Tag == x.Tag);
            }
        } 
        #endregion

        #region Tab选项卡关闭时
        private void taskTabCollection_CloseButtonClick(object sender, EventArgs e)
        {
            DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs EArg = (DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs)e;
            string name = EArg.Page.Text;//得到关闭的选项卡的text
            foreach (XtraTabPage page in taskTabCollection.TabPages)//遍历得到和关闭的选项卡一样的Text
            {
                if (page.Text == name)
                {
                    taskTabCollection.TabPages.Remove(page);
                    page.Dispose();
                    return;
                }
            }
        }
        
        #endregion


        #region 任务模块管理单击
        private void taskModuleManger_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            int count = taskTabCollection.TabPages.Where(x => e.Link.Item.Tag == x.Tag).ToList().Count;
            if (count <= 0)
            {
                XtraTabPage tabPage = new DevExpress.XtraTab.XtraTabPage()
                {
                    Text = e.Link.Caption,
                    Tag = e.Link.Item.Tag,
                    ShowCloseButton = DevExpress.Utils.DefaultBoolean.True
                };
                tabPage.Controls.Add(new TaskModuleMangerUserControl());
                taskTabCollection.TabPages.Add(tabPage);
                taskTabCollection.SelectedTabPage = tabPage;

            }
            else
            {
                //taskTabCollection.TabPages.SingleOrDefault(x => e.Link.Item.Tag == x.Tag).Focus();
                taskTabCollection.SelectedTabPage = taskTabCollection.TabPages.SingleOrDefault(x => e.Link.Item.Tag == x.Tag);
            }
        }
          #endregion

        #region 获取单个任务模块
        private NavBarGroup GetNavBarGroup(string taskModuleKey, string taskModuleName)
        {
            NavBarGroup navGrp = null;
           // NavBarGroup navGrp = this.navBarControl.Groups.FirstOrDefault(x =>x.Caption == taskModuleName|| x.Tag == taskModuleKey );
            for (int i = 0; i < this.navBarControl.Groups.Count;i++ )
            {
                if (this.navBarControl.Groups[i].Name != "grpDllManger")
                {
                    if (this.navBarControl.Groups[i].Tag != null)
                    {
                        if (this.navBarControl.Groups[i].Tag.ToString() == taskModuleKey)
                        {
                            navGrp = this.navBarControl.Groups[i];
                        }
                    }
                }
            }
                return navGrp;
        }
        #endregion

        #region 获取单个任务模块下的任务
        private NavBarItemLink GetNavBarItemLink(NavBarGroup barGrp, string taskKey,string taskName="")
        {
            NavBarItemLink itemLink = null;
            for (int i = 0; i < barGrp.ItemLinks.Count; i++)
            {
                if (barGrp.ItemLinks[i].Item.Tag != null)
                {
                    if (barGrp.ItemLinks[i].Item.Caption ==taskName || barGrp.ItemLinks[i].Item.Tag.ToString() == taskKey)
                   // if (barGrp.ItemLinks[i].Item.Tag.ToString() == taskTypeName )
                    {
                        itemLink = barGrp.ItemLinks[i];
                    }
                }
            }
         // NavBarItemLink itemLink=  barGrp.ItemLinks.FirstOrDefault(x => x.Item.Tag.ToString() == taskKey);
          return itemLink;
        }
        #endregion

        #region 新增命令
        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            AddCommandForm addCommand = new AddCommandForm(OperatorType.Add);
            addCommand.Owner = this;
            addCommand.StartPosition = FormStartPosition.CenterScreen;
            addCommand.Show();
        } 
        #endregion

        #region 命令树单击的时候

        private void CommandTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
                TreeNode currentNode = this.CommandTreeView1.GetNodeAt(new Point(e.X, e.Y));
                ContextMenuStrip cms = new ContextMenuStrip();
                if (currentNode != null)
                {
                    ToolStripMenuItem editModify = new ToolStripMenuItem("修改");
                   // ToolStripMenuItem editDelete = new ToolStripMenuItem("删除");
                 
                    editModify.Click += new EventHandler(editModify_Click);
                   // editDelete.Click += new EventHandler(editDelete_Click);
                   // editRefreshModule.Click += new EventHandler(editRefreshModule_Click);
                    cms.Items.Add(editModify);
                   // cms.Items.Add(editDelete);
                   // cms.Items.Add(editRefreshModule);
                    cms.Show(this.CommandTreeView1, e.X, e.Y);
                    this.CommandTreeView1.SelectedNode = currentNode;
                } 
        }
       

        /// <summary>
        /// 刷新命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void editRefreshModule_Click(object sender, EventArgs e)
        {
            TreeNode tn = this.CommandTreeView1.SelectedNode;
            TaskModuleDescriptor moduleDescriptor = JsonConvert.DeserializeObject<TaskModuleDescriptor>(tn.Tag.ToString());
           
            TaskModuleProvider.Initialize(moduleDescriptor);
            // TaskProvider.Initialize(moduleDescriptor.TaskModuleAssemblyAddr, moduleDescriptor.TaskModuleKey, moduleDescriptor.TaskModuleName, moduleDescriptor.TaskModuleAssemblyKey, moduleDescriptor.TaskModuleAssemblyName);//重新初始化
            BindCommandTree();
            this.CommandTreeView1.ExpandAll();
            XtraMessageBox.Show("刷新成功！");

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void editDelete_Click(object sender, EventArgs e)
        {
            TreeNode tn = this.CommandTreeView1.SelectedNode;
           
           
            BindCommandTree();//重新绑定树
            this.CommandTreeView1.ExpandAll();
            XtraMessageBox.Show("删除成功！");

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void editModify_Click(object sender, EventArgs e)
        {
            TreeNode tn = this.CommandTreeView1.SelectedNode;
            string commandKey = tn.Tag.ToString();
          CommandDescriptor command=  CommandProvider.Instance.CommandList.FirstOrDefault(x => x.CommandKey == commandKey);
          AddCommandForm module = new AddCommandForm(OperatorType.Refresh, command);
            module.StartPosition = FormStartPosition.CenterScreen;
            module.Show();
            this.CommandTreeView1.ExpandAll();

        }
        #endregion

        #region 刷新命令
        private void btnRefreshCommand_Click(object sender, EventArgs e)
        {
            CommandProvider.Instance.CommandList.Clear(false);
            CommandProvider.Refresh();//重新刷新
            XtraMessageBox.Show("刷新成功");
        } 
        #endregion


        #region 托盘代码
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    this.ShowInTaskbar = true;  //显示在系统任务栏
            //    this.WindowState = FormWindowState.Normal;  //还原窗体
            //    this.notifyIcon1.Visible = false;  //托盘图标隐藏
            //}
            this.Visible = true; //恢复主窗体
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; //取消窗体关闭事件
            this.Visible = false; //隐藏主窗体
            //if (this.WindowState == FormWindowState.Minimized)  //判断是否最小化
            //{
            //    this.ShowInTaskbar = false;  //不显示在系统任务栏
            //    this.notifyIcon1.Visible = true;  //托盘图标可见
            //}
        }

        private void MenuItem_Open_Click(object sender, EventArgs e)
        {
            this.Visible = true; //恢复主窗体
        }

        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        #endregion

        #region 打开文件
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();  
            fileDialog.Multiselect = true;  
            fileDialog.Title = "请选择文件";  
            fileDialog.Filter="所有文件(*.*)|*.dll";  
            if (fileDialog.ShowDialog() == DialogResult.OK)  
            {  
                this.txtFileName.Text=fileDialog.FileName;
              //  lstFiles.Add(fileDialog.FileName);
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dgvFileList);
                dr.Cells[0].Value = fileDialog.FileName;
                dgvFileList.Rows.Insert(dgvFileList.Rows.Count - 1, dr);　
            }
           　
        } 
        #endregion


        #region 批量上传
        private void btnBulkUpload_Click(object sender, EventArgs e)
        {
            string moduelName = this.cbModuleList.Text;
            string fielName = "";
            string x = "";
            if(this.dgvFileList.Rows.Count <=0 )
            {
                MessageBox.Show("请先选择要上传的文件");
                return;
            }
            List<string> errList = new List<string>();
            TaskModuleDescriptor module = TaskModuleProvider.Instance.GetTaskModuleDescriptor(m => m.TaskModuleName == moduelName);
            if (module != null && module.AssemblyCollection.Count > 0)
            {
               // module.AssemblyCollection.Clear();
            }
            for (int i = 0; i < this.dgvFileList.Rows.Count-1;i++ )
            {
                 x= this.dgvFileList.Rows[i].Cells[0].Value.ToString();
                byte[] bytes = File.ReadAllBytes(x);
                fielName = x.Substring(x.LastIndexOf("\\") + 1);
              bool r=  MongoFileHelper.Instance.UploadFile(moduelName, fielName, bytes);
                if(!r)
                {
                    errList.Add(fielName);
                }
                else//上传成功，更新到TaskModule集合中
                {
                    module.AssemblyCollection.Where(k => k.TaskModuleAssemblyName == "ND.DispatchCenter_" + moduelName + "_" + fielName).ToList().ForEach(n =>
                    {
                        module.AssemblyCollection.Remove(n);
                    });
                  module.AssemblyCollection.Add(new TaskModuleAssemblyDescriptor()
                  {
                      TaskModuleAssemblyAddr =x,
                      TaskModuleAssemblyKey=Guid.NewGuid().ToString(),
                      TaskModuleAssemblyName = "ND.DispatchCenter_" + moduelName+"_"+fielName,
                      TaskModuleAssemblyUpdateTime = DateTime.Now.ToString(),
                      TaskModuleKey=module.TaskModuleKey,
                      TaskModuleName=module.TaskModuleName
                  });
                }
            }
            TaskModuleProvider.Instance.TaskList.PersistenceTaskModule();//持久化TaskModule
            BindModuleAndFile();
            TaskModuleProvider.Initialize(module);
            module = null;
            if(errList.Count <= 0)
            {
                MessageBox.Show("上传成功");
                return;
            }
            MessageBox.Show("上传失败："+string.Join(",",errList.ToArray()));
                
                   
             
        } 
        #endregion

        #region 读取文件
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            if (this.cbReadModuleName.Items.Count <= 0 || this.cbReadFileList.Text.Length <= 0)
           {
               MessageBox.Show("请先选择要读取的模块名称");
               return;
           }
           if (this.cbReadFileList.Items.Count <= 0 || this.cbReadFileList.Text.Length <= 0)
           {
               MessageBox.Show("请先选择要读取的模块名称");
               return;
           }
           dataGridView1.Rows.Clear();
           byte[] bytes = MongoFileHelper.Instance.ReadFile(this.cbReadModuleName.Text, cbReadFileList.Text);
            Assembly assem= Assembly.Load(bytes);
            StringBuilder str = new StringBuilder();
            assem.GetTypes().ToList().ForEach(x =>
            {
           
                DataGridViewRow dr = new DataGridViewRow();
                //添加的行作为第一行
                int index=dataGridView1.Rows.Add(dr);
                dataGridView1.Rows[index].Cells[0].Value = x.FullName;   
            });
     
        } 
        #endregion

        #region 模块选择变了以后
        private void cbReadModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFileListByModuleName(this.cbReadModuleName.Text);
        }
        
        #endregion

        #region 上传命令dll
        private void btnUploadCommand_Click(object sender, EventArgs e)
        {
         string x= this.txtFileName.Text;
            if(x.Length <= 0)
            {
                MessageBox.Show("未知的命令地址");
                return;
            }
            if (x.IndexOf("Command") < -1)
            {
                MessageBox.Show("请先打开要上传的命令地址");
                return;
            }
            byte[] bytes = File.ReadAllBytes(x);
            bool r = MongoFileHelper.Instance.UploadCommandFile(bytes);
            btnRefreshCommand.PerformClick();//刷新命令
            if(r)
            {
                MessageBox.Show("上传成功");
                return;
            }
            MessageBox.Show("上传失败");
        } 
        #endregion

        #region 读取命令dll中的类型
        private void btnReadCommandDll_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            byte[] bytes = MongoFileHelper.Instance.ReadCommandFile();
            Assembly assem = Assembly.Load(bytes);
            StringBuilder str = new StringBuilder();
            assem.GetTypes().ToList().ForEach(x =>
            {

                DataGridViewRow dr = new DataGridViewRow();
                //添加的行作为第一行
                int index = dataGridView1.Rows.Add(dr);
                dataGridView1.Rows[index].Cells[0].Value = x.FullName;
            });
        } 
        #endregion

        #region 自动编号
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
              //自动编号与数据库无关
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView1.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridView1.RowHeadersDefaultCellStyle.Font, rectangle, dataGridView1.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        private void dgvFileList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvFileList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvFileList.RowHeadersDefaultCellStyle.Font, rectangle, dgvFileList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        #endregion

        #region 删除文件
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (this.cbReadModuleName.Items.Count <= 0 || this.cbReadModuleName.Text.Length <= 0)
            {
                MessageBox.Show("请先选择要读取的模块名称");
                return;
            }
            if (this.cbReadFileList.Items.Count <= 0 || this.cbReadFileList.Text.Length <= 0)
            {
                MessageBox.Show("请先选择要读取的模块名称");
                return;
            }
            TaskModuleDescriptor module = TaskModuleProvider.Instance.GetTaskModuleDescriptor(m => m.TaskModuleName == this.cbReadModuleName.Text);
            bool r=MongoFileHelper.Instance.DeleteFile(this.cbReadModuleName.Text, cbReadFileList.Text);
            if(r)
            {
              TaskModuleAssemblyDescriptor assembly=  module.AssemblyCollection.FirstOrDefault(x => x.TaskModuleAssemblyName == "ND.DispatchCenter_" + this.cbReadModuleName.Text + "_" + cbReadFileList.Text);
                if(assembly!=null)
                {
                    module.AssemblyCollection.Remove(assembly);
                }
            }
            BindModuleAndFile();
        }
        
        #endregion

        #region 清空dataGridView
        private void btnClearDataGridView_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dgvFileList.Rows.Clear();
        } 
        #endregion
       





    }
}