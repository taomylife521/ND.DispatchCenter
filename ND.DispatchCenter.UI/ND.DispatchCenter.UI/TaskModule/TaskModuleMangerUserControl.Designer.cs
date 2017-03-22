namespace ND.DispatchCenter.UI
{
    partial class TaskModuleMangerUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddTaskModule = new DevExpress.XtraEditors.SimpleButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnSaveData = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnAddTaskModule
            // 
            this.btnAddTaskModule.Location = new System.Drawing.Point(28, 16);
            this.btnAddTaskModule.Name = "btnAddTaskModule";
            this.btnAddTaskModule.Size = new System.Drawing.Size(75, 23);
            this.btnAddTaskModule.TabIndex = 1;
            this.btnAddTaskModule.Text = "新增";
            this.btnAddTaskModule.Click += new System.EventHandler(this.btnAddTaskModule_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(63, 58);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(547, 408);
            this.treeView1.TabIndex = 3;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Location = new System.Drawing.Point(152, 16);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(117, 23);
            this.btnSaveData.TabIndex = 4;
            this.btnSaveData.Text = "手动持久化数据";
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // TaskModuleMangerUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSaveData);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.btnAddTaskModule);
            this.Name = "TaskModuleMangerUserControl";
            this.Size = new System.Drawing.Size(826, 510);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAddTaskModule;
        private System.Windows.Forms.TreeView treeView1;
        private DevExpress.XtraEditors.SimpleButton btnSaveData;
    }
}
