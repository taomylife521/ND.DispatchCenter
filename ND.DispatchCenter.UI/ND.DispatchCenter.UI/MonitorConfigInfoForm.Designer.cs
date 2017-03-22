namespace ND.DispatchCenter.UI
{
    partial class MonitorConfigInfoForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddTaskConfig
            // 
            this.btnAddTaskConfig.Location = new System.Drawing.Point(234, 9);
            this.btnAddTaskConfig.Name = "btnAddTaskConfig";
            this.btnAddTaskConfig.Size = new System.Drawing.Size(62, 24);
            this.btnAddTaskConfig.TabIndex = 5;
            this.btnAddTaskConfig.Text = "新增";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.key,
            this.值});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.Location = new System.Drawing.Point(4, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(311, 213);
            this.dataGridView1.TabIndex = 4;
            // 
            // key
            // 
            this.key.HeaderText = "键(key)";
            this.key.Name = "key";
            this.key.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.key.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 值
            // 
            this.值.HeaderText = "值(value)";
            this.值.Name = "值";
            this.值.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.值.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MonitorConfigInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 264);
            this.Controls.Add(this.btnAddTaskConfig);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MonitorConfigInfoForm";
            this.Text = "监控配置";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAddTaskConfig;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn key;
        private System.Windows.Forms.DataGridViewTextBoxColumn 值;
    }
}