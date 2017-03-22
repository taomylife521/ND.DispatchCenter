namespace ND.DispatchCenter.UI
{
    partial class AddTaskConfigInfoForm
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
            this.btnAddTaskConfig = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTaskConfigKey = new DevExpress.XtraEditors.TextEdit();
            this.txtTaskConfigValue = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskConfigKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskConfigValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddTaskConfig
            // 
            this.btnAddTaskConfig.Location = new System.Drawing.Point(143, 49);
            this.btnAddTaskConfig.Name = "btnAddTaskConfig";
            this.btnAddTaskConfig.Size = new System.Drawing.Size(75, 23);
            this.btnAddTaskConfig.TabIndex = 0;
            this.btnAddTaskConfig.Text = "新增";
            this.btnAddTaskConfig.Click += new System.EventHandler(this.btnAddTaskConfig_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "键(key):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "值(value):";
            // 
            // txtTaskConfigKey
            // 
            this.txtTaskConfigKey.Location = new System.Drawing.Point(71, 23);
            this.txtTaskConfigKey.Name = "txtTaskConfigKey";
            this.txtTaskConfigKey.Size = new System.Drawing.Size(100, 20);
            this.txtTaskConfigKey.TabIndex = 3;
            // 
            // txtTaskConfigValue
            // 
            this.txtTaskConfigValue.Location = new System.Drawing.Point(243, 25);
            this.txtTaskConfigValue.Name = "txtTaskConfigValue";
            this.txtTaskConfigValue.Size = new System.Drawing.Size(100, 20);
            this.txtTaskConfigValue.TabIndex = 4;
            // 
            // AddTaskConfigInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 80);
            this.Controls.Add(this.txtTaskConfigValue);
            this.Controls.Add(this.txtTaskConfigKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddTaskConfig);
            this.Name = "AddTaskConfigInfoForm";
            this.Text = "添加配置信息";
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskConfigKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskConfigValue.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAddTaskConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtTaskConfigKey;
        private DevExpress.XtraEditors.TextEdit txtTaskConfigValue;
    }
}