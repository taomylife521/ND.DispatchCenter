namespace CommandClient
{
    partial class CommandClientForm
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
            this.cmbCommandName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtResponseParam = new System.Windows.Forms.TextBox();
            this.txtRequestParam = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtCommandParam = new System.Windows.Forms.TextBox();
            this.lbCmdDicription = new DevExpress.XtraEditors.LabelControl();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetTaskList = new System.Windows.Forms.Button();
            this.btnExcuteTask = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbCommandName
            // 
            this.cmbCommandName.FormattingEnabled = true;
            this.cmbCommandName.Items.AddRange(new object[] {
            "help",
            "taskconfig",
            "taskdetail",
            "tasklist",
            "taskstatus",
            "excutetaskconfigandrun"});
            this.cmbCommandName.Location = new System.Drawing.Point(83, 12);
            this.cmbCommandName.Name = "cmbCommandName";
            this.cmbCommandName.Size = new System.Drawing.Size(280, 20);
            this.cmbCommandName.TabIndex = 0;
            this.cmbCommandName.SelectedIndexChanged += new System.EventHandler(this.cmbCommandName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "命令名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "命令参数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 352);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "返回结果(自动赋值)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 203);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "请求参数(自动赋值)";
            // 
            // txtResponseParam
            // 
            this.txtResponseParam.Location = new System.Drawing.Point(76, 373);
            this.txtResponseParam.Multiline = true;
            this.txtResponseParam.Name = "txtResponseParam";
            this.txtResponseParam.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponseParam.Size = new System.Drawing.Size(583, 125);
            this.txtResponseParam.TabIndex = 8;
            // 
            // txtRequestParam
            // 
            this.txtRequestParam.Location = new System.Drawing.Point(83, 219);
            this.txtRequestParam.Multiline = true;
            this.txtRequestParam.Name = "txtRequestParam";
            this.txtRequestParam.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequestParam.Size = new System.Drawing.Size(576, 125);
            this.txtRequestParam.TabIndex = 9;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(262, 516);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtCommandParam
            // 
            this.txtCommandParam.Location = new System.Drawing.Point(83, 72);
            this.txtCommandParam.Multiline = true;
            this.txtCommandParam.Name = "txtCommandParam";
            this.txtCommandParam.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCommandParam.Size = new System.Drawing.Size(576, 125);
            this.txtCommandParam.TabIndex = 11;
            // 
            // lbCmdDicription
            // 
            this.lbCmdDicription.Location = new System.Drawing.Point(83, 38);
            this.lbCmdDicription.Name = "lbCmdDicription";
            this.lbCmdDicription.Size = new System.Drawing.Size(0, 14);
            this.lbCmdDicription.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "命令描述";
            // 
            // btnGetTaskList
            // 
            this.btnGetTaskList.Location = new System.Drawing.Point(394, 516);
            this.btnGetTaskList.Name = "btnGetTaskList";
            this.btnGetTaskList.Size = new System.Drawing.Size(99, 23);
            this.btnGetTaskList.TabIndex = 14;
            this.btnGetTaskList.Text = "获取任务列表";
            this.btnGetTaskList.UseVisualStyleBackColor = true;
            this.btnGetTaskList.Click += new System.EventHandler(this.btnGetTaskList_Click);
            // 
            // btnExcuteTask
            // 
            this.btnExcuteTask.Location = new System.Drawing.Point(520, 516);
            this.btnExcuteTask.Name = "btnExcuteTask";
            this.btnExcuteTask.Size = new System.Drawing.Size(104, 23);
            this.btnExcuteTask.TabIndex = 15;
            this.btnExcuteTask.Text = "给第三方下单";
            this.btnExcuteTask.UseVisualStyleBackColor = true;
            this.btnExcuteTask.Click += new System.EventHandler(this.btnExcuteTask_Click);
            // 
            // CommandClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 554);
            this.Controls.Add(this.btnExcuteTask);
            this.Controls.Add(this.btnGetTaskList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbCmdDicription);
            this.Controls.Add(this.txtCommandParam);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtRequestParam);
            this.Controls.Add(this.txtResponseParam);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCommandName);
            this.Name = "CommandClientForm";
            this.Text = "CommandClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCommandName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtResponseParam;
        private System.Windows.Forms.TextBox txtRequestParam;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtCommandParam;
        private DevExpress.XtraEditors.LabelControl lbCmdDicription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGetTaskList;
        private System.Windows.Forms.Button btnExcuteTask;
    }
}