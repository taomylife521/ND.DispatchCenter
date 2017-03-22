namespace ND.DispatchCenter.UI.ListenerModule
{
    partial class AddCommandConfigForm
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
            this.txtCommandConfigValue = new DevExpress.XtraEditors.TextEdit();
            this.txtCommandConfigKey = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddCommandConfig = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandConfigValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandConfigKey.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCommandConfigValue
            // 
            this.txtCommandConfigValue.Location = new System.Drawing.Point(237, 21);
            this.txtCommandConfigValue.Name = "txtCommandConfigValue";
            this.txtCommandConfigValue.Size = new System.Drawing.Size(100, 20);
            this.txtCommandConfigValue.TabIndex = 9;
            // 
            // txtCommandConfigKey
            // 
            this.txtCommandConfigKey.Location = new System.Drawing.Point(65, 19);
            this.txtCommandConfigKey.Name = "txtCommandConfigKey";
            this.txtCommandConfigKey.Size = new System.Drawing.Size(100, 20);
            this.txtCommandConfigKey.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "值(value):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "键(key):";
            // 
            // btnAddCommandConfig
            // 
            this.btnAddCommandConfig.Location = new System.Drawing.Point(137, 48);
            this.btnAddCommandConfig.Name = "btnAddCommandConfig";
            this.btnAddCommandConfig.Size = new System.Drawing.Size(75, 23);
            this.btnAddCommandConfig.TabIndex = 5;
            this.btnAddCommandConfig.Text = "新增";
            this.btnAddCommandConfig.Click += new System.EventHandler(this.btnAddCommandConfig_Click);
            // 
            // AddCommandConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 81);
            this.Controls.Add(this.txtCommandConfigValue);
            this.Controls.Add(this.txtCommandConfigKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddCommandConfig);
            this.Name = "AddCommandConfigForm";
            this.Text = "AddCommandConfigForm";
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandConfigValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommandConfigKey.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtCommandConfigValue;
        private DevExpress.XtraEditors.TextEdit txtCommandConfigKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnAddCommandConfig;
    }
}