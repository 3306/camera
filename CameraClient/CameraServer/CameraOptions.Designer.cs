namespace CameraServer
{
    partial class CameraOptions
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
            this.StartListenBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // StartListenBtn
            // 
            this.StartListenBtn.Location = new System.Drawing.Point(391, 12);
            this.StartListenBtn.Name = "StartListenBtn";
            this.StartListenBtn.Size = new System.Drawing.Size(75, 23);
            this.StartListenBtn.TabIndex = 0;
            this.StartListenBtn.Text = "开始监听";
            this.StartListenBtn.UseVisualStyleBackColor = true;
            this.StartListenBtn.Click += new System.EventHandler(this.StartListenBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(373, 134);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // CameraOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 434);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.StartListenBtn);
            this.Name = "CameraOptions";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartListenBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;

    }
}

