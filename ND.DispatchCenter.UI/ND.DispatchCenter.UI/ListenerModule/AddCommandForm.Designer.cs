namespace ND.DispatchCenter.UI.ListenerModule
{
    partial class AddCommandForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCommandKey = new DevExpress.XtraEditors.TextEdit();
            this.txtCommandTypeName = new DevExpress.XtraEditors.TextEdit();
            this.txtCommandAssemblyName = new DevExpress.XtraEditors.TextEdit();
            this.txtCommandName = new DevExpress.XtraEditors.TextEdit();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.txtCommandDescription = new System.Windows.Forms.TextBox();
            this.btnCommandAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnCommandModify = new DevExpress.XtraEditors.SimpleButton();
            this.btnCommandArgs = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandTypeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandAssemblyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "命令Key";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "命令类型名称";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 211);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 14);
            this.label9.TabIndex = 8;
            this.label9.Text = "命令程序集名称";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 14);
            this.label10.TabIndex = 9;
            this.label10.Text = "命令描述";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 14);
            this.label11.TabIndex = 10;
            this.label11.Text = "所属监听端口";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 14);
            this.label12.TabIndex = 11;
            this.label12.Text = "命令名称(英文)";
            // 
            // txtCommandKey
            // 
            this.txtCommandKey.Location = new System.Drawing.Point(109, 42);
            this.txtCommandKey.Name = "txtCommandKey";
            this.txtCommandKey.Size = new System.Drawing.Size(307, 20);
            this.txtCommandKey.TabIndex = 12;
            // 
            // txtCommandTypeName
            // 
            this.txtCommandTypeName.Location = new System.Drawing.Point(109, 252);
            this.txtCommandTypeName.Name = "txtCommandTypeName";
            this.txtCommandTypeName.Size = new System.Drawing.Size(307, 20);
            this.txtCommandTypeName.TabIndex = 14;
            // 
            // txtCommandAssemblyName
            // 
            this.txtCommandAssemblyName.EditValue = "ND.DispatchCenter.Core";
            this.txtCommandAssemblyName.Location = new System.Drawing.Point(109, 208);
            this.txtCommandAssemblyName.Name = "txtCommandAssemblyName";
            this.txtCommandAssemblyName.Size = new System.Drawing.Size(307, 20);
            this.txtCommandAssemblyName.TabIndex = 15;
            // 
            // txtCommandName
            // 
            this.txtCommandName.Location = new System.Drawing.Point(109, 77);
            this.txtCommandName.Name = "txtCommandName";
            this.txtCommandName.Size = new System.Drawing.Size(307, 20);
            this.txtCommandName.TabIndex = 17;
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Items.AddRange(new object[] {
            "2000",
            "2001"});
            this.cmbPort.Location = new System.Drawing.Point(109, 119);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(307, 22);
            this.cmbPort.TabIndex = 18;
            // 
            // txtCommandDescription
            // 
            this.txtCommandDescription.Location = new System.Drawing.Point(109, 148);
            this.txtCommandDescription.Multiline = true;
            this.txtCommandDescription.Name = "txtCommandDescription";
            this.txtCommandDescription.Size = new System.Drawing.Size(307, 54);
            this.txtCommandDescription.TabIndex = 19;
            // 
            // btnCommandAdd
            // 
            this.btnCommandAdd.Location = new System.Drawing.Point(127, 293);
            this.btnCommandAdd.Name = "btnCommandAdd";
            this.btnCommandAdd.Size = new System.Drawing.Size(75, 23);
            this.btnCommandAdd.TabIndex = 20;
            this.btnCommandAdd.Text = "保存";
            this.btnCommandAdd.Click += new System.EventHandler(this.btnCommandAdd_Click);
            // 
            // btnCommandModify
            // 
            this.btnCommandModify.Location = new System.Drawing.Point(249, 293);
            this.btnCommandModify.Name = "btnCommandModify";
            this.btnCommandModify.Size = new System.Drawing.Size(75, 23);
            this.btnCommandModify.TabIndex = 21;
            this.btnCommandModify.Text = "修改";
            this.btnCommandModify.Click += new System.EventHandler(this.btnCommandModify_Click);
            // 
            // btnCommandArgs
            // 
            this.btnCommandArgs.Location = new System.Drawing.Point(315, 12);
            this.btnCommandArgs.Name = "btnCommandArgs";
            this.btnCommandArgs.Size = new System.Drawing.Size(90, 23);
            this.btnCommandArgs.TabIndex = 22;
            this.btnCommandArgs.Text = "命令参数配置";
            this.btnCommandArgs.Click += new System.EventHandler(this.btnCommandArgs_Click);
            // 
            // AddCommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 344);
            this.Controls.Add(this.btnCommandArgs);
            this.Controls.Add(this.btnCommandModify);
            this.Controls.Add(this.btnCommandAdd);
            this.Controls.Add(this.txtCommandDescription);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.txtCommandName);
            this.Controls.Add(this.txtCommandAssemblyName);
            this.Controls.Add(this.txtCommandTypeName);
            this.Controls.Add(this.txtCommandKey);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Name = "AddCommandForm";
            this.Text = "AddCommandForm";
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandTypeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandAssemblyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.TextEdit txtCommandKey;
        private DevExpress.XtraEditors.TextEdit txtCommandTypeName;
        private DevExpress.XtraEditors.TextEdit txtCommandAssemblyName;
        private DevExpress.XtraEditors.TextEdit txtCommandName;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.TextBox txtCommandDescription;
        private DevExpress.XtraEditors.SimpleButton btnCommandAdd;
        private DevExpress.XtraEditors.SimpleButton btnCommandModify;
        private DevExpress.XtraEditors.SimpleButton btnCommandArgs;
    }
}