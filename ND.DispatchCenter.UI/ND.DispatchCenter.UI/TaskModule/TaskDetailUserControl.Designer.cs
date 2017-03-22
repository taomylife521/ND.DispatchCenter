namespace ND.DispatchCenter.UI
{
    partial class TaskDetailUserControl
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTaskDescription = new System.Windows.Forms.Label();
            this.lbxLog = new DevExpress.XtraEditors.ListBoxControl();
            this.btnTaskConfig = new System.Windows.Forms.Button();
            this.lbxTaskStatistics = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTaskKey = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbTaskName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lbxLog)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "立即执行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "任务名称:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "任务描述：";
            // 
            // lbTaskDescription
            // 
            this.lbTaskDescription.AutoSize = true;
            this.lbTaskDescription.Location = new System.Drawing.Point(103, 46);
            this.lbTaskDescription.Name = "lbTaskDescription";
            this.lbTaskDescription.Size = new System.Drawing.Size(38, 14);
            this.lbTaskDescription.TabIndex = 4;
            this.lbTaskDescription.Text = "label3";
            // 
            // lbxLog
            // 
            this.lbxLog.Location = new System.Drawing.Point(424, 120);
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.Size = new System.Drawing.Size(398, 410);
            this.lbxLog.TabIndex = 5;
            // 
            // btnTaskConfig
            // 
            this.btnTaskConfig.Location = new System.Drawing.Point(747, 34);
            this.btnTaskConfig.Name = "btnTaskConfig";
            this.btnTaskConfig.Size = new System.Drawing.Size(75, 23);
            this.btnTaskConfig.TabIndex = 6;
            this.btnTaskConfig.Text = "配置信息";
            this.btnTaskConfig.UseVisualStyleBackColor = true;
            this.btnTaskConfig.Click += new System.EventHandler(this.btnTaskConfig_Click);
            // 
            // lbxTaskStatistics
            // 
            this.lbxTaskStatistics.FormattingEnabled = true;
            this.lbxTaskStatistics.HorizontalScrollbar = true;
            this.lbxTaskStatistics.ItemHeight = 14;
            this.lbxTaskStatistics.Location = new System.Drawing.Point(54, 120);
            this.lbxTaskStatistics.Name = "lbxTaskStatistics";
            this.lbxTaskStatistics.Size = new System.Drawing.Size(341, 410);
            this.lbxTaskStatistics.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "任务统计信息(每5秒自动刷新一次):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(421, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "任务实时信息";
            // 
            // txtTaskKey
            // 
            this.txtTaskKey.Location = new System.Drawing.Point(354, 19);
            this.txtTaskKey.Name = "txtTaskKey";
            this.txtTaskKey.Size = new System.Drawing.Size(254, 22);
            this.txtTaskKey.TabIndex = 10;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbTaskName
            // 
            this.lbTaskName.Location = new System.Drawing.Point(106, 26);
            this.lbTaskName.Name = "lbTaskName";
            this.lbTaskName.Size = new System.Drawing.Size(70, 14);
            this.lbTaskName.TabIndex = 11;
            this.lbTaskName.Text = "labelControl1";
            // 
            // TaskDetailUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTaskName);
            this.Controls.Add(this.txtTaskKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbxTaskStatistics);
            this.Controls.Add(this.btnTaskConfig);
            this.Controls.Add(this.lbxLog);
            this.Controls.Add(this.lbTaskDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "TaskDetailUserControl";
            this.Size = new System.Drawing.Size(942, 568);
            this.Load += new System.EventHandler(this.TaskDetailUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lbxLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTaskDescription;
        private DevExpress.XtraEditors.ListBoxControl lbxLog;
        private System.Windows.Forms.Button btnTaskConfig;
        private System.Windows.Forms.ListBox lbxTaskStatistics;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTaskKey;
        private DevExpress.XtraEditors.LabelControl lbTaskName1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.LabelControl lbTaskName;
    }
}
