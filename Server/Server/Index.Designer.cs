namespace Server
{
    partial class Index
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.Console_rbx = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ClinetCount_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(307, 361);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "启动监听";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Console_rbx
            // 
            this.Console_rbx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Console_rbx.Location = new System.Drawing.Point(12, 12);
            this.Console_rbx.Name = "Console_rbx";
            this.Console_rbx.Size = new System.Drawing.Size(173, 393);
            this.Console_rbx.TabIndex = 1;
            this.Console_rbx.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前连接数 ：";
            // 
            // ClinetCount_label
            // 
            this.ClinetCount_label.AutoSize = true;
            this.ClinetCount_label.Location = new System.Drawing.Point(274, 16);
            this.ClinetCount_label.Name = "ClinetCount_label";
            this.ClinetCount_label.Size = new System.Drawing.Size(11, 12);
            this.ClinetCount_label.TabIndex = 3;
            this.ClinetCount_label.Text = "0";
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 417);
            this.Controls.Add(this.ClinetCount_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Console_rbx);
            this.Controls.Add(this.button1);
            this.Name = "Index";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox Console_rbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ClinetCount_label;
    }
}

