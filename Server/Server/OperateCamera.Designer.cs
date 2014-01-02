namespace Server
{
    partial class OperateCamera
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
            this.GetImage = new System.Windows.Forms.Button();
            this.CurrentImage = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SetCameraSpeed = new System.Windows.Forms.Button();
            this.ExitToIndex = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentImage)).BeginInit();
            this.SuspendLayout();
            // 
            // GetImage
            // 
            this.GetImage.Location = new System.Drawing.Point(12, 12);
            this.GetImage.Name = "GetImage";
            this.GetImage.Size = new System.Drawing.Size(91, 23);
            this.GetImage.TabIndex = 0;
            this.GetImage.Text = "获得当前图片";
            this.GetImage.UseVisualStyleBackColor = true;
            this.GetImage.Click += new System.EventHandler(this.GetImage_Click);
            // 
            // CurrentImage
            // 
            this.CurrentImage.Location = new System.Drawing.Point(12, 41);
            this.CurrentImage.Name = "CurrentImage";
            this.CurrentImage.Size = new System.Drawing.Size(640, 480);
            this.CurrentImage.TabIndex = 1;
            this.CurrentImage.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(266, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(43, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "更改摄像头拍摄速度为";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "秒/张";
            // 
            // SetCameraSpeed
            // 
            this.SetCameraSpeed.Location = new System.Drawing.Point(353, 12);
            this.SetCameraSpeed.Name = "SetCameraSpeed";
            this.SetCameraSpeed.Size = new System.Drawing.Size(46, 23);
            this.SetCameraSpeed.TabIndex = 5;
            this.SetCameraSpeed.Text = "更改";
            this.SetCameraSpeed.UseVisualStyleBackColor = true;
            this.SetCameraSpeed.Click += new System.EventHandler(this.SetCameraSpeed_Click);
            // 
            // ExitToIndex
            // 
            this.ExitToIndex.Location = new System.Drawing.Point(577, 12);
            this.ExitToIndex.Name = "ExitToIndex";
            this.ExitToIndex.Size = new System.Drawing.Size(75, 23);
            this.ExitToIndex.TabIndex = 6;
            this.ExitToIndex.Text = "关闭";
            this.ExitToIndex.UseVisualStyleBackColor = true;
            this.ExitToIndex.Click += new System.EventHandler(this.ExitToIndex_Click);
            // 
            // OperateCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(665, 530);
            this.ControlBox = false;
            this.Controls.Add(this.ExitToIndex);
            this.Controls.Add(this.SetCameraSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.CurrentImage);
            this.Controls.Add(this.GetImage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OperateCamera";
            this.ShowInTaskbar = false;
            this.Text = "OperateCamera";
            this.Load += new System.EventHandler(this.OperateCamera_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetImage;
        private System.Windows.Forms.PictureBox CurrentImage;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SetCameraSpeed;
        private System.Windows.Forms.Button ExitToIndex;
    }
}