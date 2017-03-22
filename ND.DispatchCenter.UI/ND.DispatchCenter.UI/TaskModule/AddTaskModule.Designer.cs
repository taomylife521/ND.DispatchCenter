namespace ND.DispatchCenter.UI.TaskModule
{
    partial class AddTaskModule
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtTaskModuleKey = new System.Windows.Forms.TextBox();
            this.txtTaskModuleName = new System.Windows.Forms.TextBox();
            this.txtTaskModuleDescription = new System.Windows.Forms.TextBox();
            this.txtModuleAddr = new System.Windows.Forms.TextBox();
            this.btnAddTaskModule = new DevExpress.XtraEditors.SimpleButton();
            this.btnModifyTaskModule = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "任务模块名称";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(24, 223);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(72, 14);
            this.labelControl8.TabIndex = 7;
            this.labelControl8.Text = "任务模块地址";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(24, 114);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(72, 14);
            this.labelControl9.TabIndex = 8;
            this.labelControl9.Text = "任务模块描述";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 14);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "任务模块Key";
            // 
            // txtTaskModuleKey
            // 
            this.txtTaskModuleKey.Enabled = false;
            this.txtTaskModuleKey.Location = new System.Drawing.Point(115, 13);
            this.txtTaskModuleKey.Name = "txtTaskModuleKey";
            this.txtTaskModuleKey.Size = new System.Drawing.Size(461, 22);
            this.txtTaskModuleKey.TabIndex = 10;
            // 
            // txtTaskModuleName
            // 
            this.txtTaskModuleName.Location = new System.Drawing.Point(115, 58);
            this.txtTaskModuleName.Name = "txtTaskModuleName";
            this.txtTaskModuleName.Size = new System.Drawing.Size(461, 22);
            this.txtTaskModuleName.TabIndex = 11;
            // 
            // txtTaskModuleDescription
            // 
            this.txtTaskModuleDescription.Location = new System.Drawing.Point(115, 106);
            this.txtTaskModuleDescription.Multiline = true;
            this.txtTaskModuleDescription.Name = "txtTaskModuleDescription";
            this.txtTaskModuleDescription.Size = new System.Drawing.Size(461, 66);
            this.txtTaskModuleDescription.TabIndex = 12;
            // 
            // txtModuleAddr
            // 
            this.txtModuleAddr.Location = new System.Drawing.Point(115, 220);
            this.txtModuleAddr.Name = "txtModuleAddr";
            this.txtModuleAddr.Size = new System.Drawing.Size(461, 22);
            this.txtModuleAddr.TabIndex = 13;
            // 
            // btnAddTaskModule
            // 
            this.btnAddTaskModule.Location = new System.Drawing.Point(145, 281);
            this.btnAddTaskModule.Name = "btnAddTaskModule";
            this.btnAddTaskModule.Size = new System.Drawing.Size(75, 23);
            this.btnAddTaskModule.TabIndex = 14;
            this.btnAddTaskModule.Text = "新增";
            this.btnAddTaskModule.Click += new System.EventHandler(this.btnAddTaskModule_Click);
            // 
            // btnModifyTaskModule
            // 
            this.btnModifyTaskModule.Location = new System.Drawing.Point(243, 281);
            this.btnModifyTaskModule.Name = "btnModifyTaskModule";
            this.btnModifyTaskModule.Size = new System.Drawing.Size(75, 23);
            this.btnModifyTaskModule.TabIndex = 15;
            this.btnModifyTaskModule.Text = "修改";
            this.btnModifyTaskModule.Click += new System.EventHandler(this.btnModifyTaskModule_Click);
            // 
            // AddTaskModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 316);
            this.Controls.Add(this.btnModifyTaskModule);
            this.Controls.Add(this.btnAddTaskModule);
            this.Controls.Add(this.txtModuleAddr);
            this.Controls.Add(this.txtTaskModuleDescription);
            this.Controls.Add(this.txtTaskModuleName);
            this.Controls.Add(this.txtTaskModuleKey);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl1);
            this.Name = "AddTaskModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加任务模块";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.TextBox txtTaskModuleKey;
        private System.Windows.Forms.TextBox txtTaskModuleName;
        private System.Windows.Forms.TextBox txtTaskModuleDescription;
        private System.Windows.Forms.TextBox txtModuleAddr;
        private DevExpress.XtraEditors.SimpleButton btnAddTaskModule;
        private DevExpress.XtraEditors.SimpleButton btnModifyTaskModule;
    }
}