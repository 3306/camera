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
            this.facenum_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CurrentClassroom = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LessonName = new System.Windows.Forms.Label();
            this.NumShouldBe = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GetImage
            // 
            this.GetImage.Location = new System.Drawing.Point(28, 48);
            this.GetImage.Name = "GetImage";
            this.GetImage.Size = new System.Drawing.Size(91, 23);
            this.GetImage.TabIndex = 0;
            this.GetImage.Text = "获得当前图片";
            this.GetImage.UseVisualStyleBackColor = true;
            this.GetImage.Click += new System.EventHandler(this.GetImage_Click);
            // 
            // CurrentImage
            // 
            this.CurrentImage.Location = new System.Drawing.Point(315, 14);
            this.CurrentImage.Name = "CurrentImage";
            this.CurrentImage.Size = new System.Drawing.Size(320, 240);
            this.CurrentImage.TabIndex = 1;
            this.CurrentImage.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(159, 101);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(43, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "更改摄像头拍摄速度为";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "秒/张";
            // 
            // SetCameraSpeed
            // 
            this.SetCameraSpeed.Location = new System.Drawing.Point(28, 155);
            this.SetCameraSpeed.Name = "SetCameraSpeed";
            this.SetCameraSpeed.Size = new System.Drawing.Size(46, 23);
            this.SetCameraSpeed.TabIndex = 5;
            this.SetCameraSpeed.Text = "更改";
            this.SetCameraSpeed.UseVisualStyleBackColor = true;
            this.SetCameraSpeed.Click += new System.EventHandler(this.SetCameraSpeed_Click);
            // 
            // ExitToIndex
            // 
            this.ExitToIndex.Location = new System.Drawing.Point(572, 447);
            this.ExitToIndex.Name = "ExitToIndex";
            this.ExitToIndex.Size = new System.Drawing.Size(75, 23);
            this.ExitToIndex.TabIndex = 6;
            this.ExitToIndex.Text = "关闭";
            this.ExitToIndex.UseVisualStyleBackColor = true;
            this.ExitToIndex.Click += new System.EventHandler(this.ExitToIndex_Click);
            // 
            // facenum_label
            // 
            this.facenum_label.AutoSize = true;
            this.facenum_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.facenum_label.Location = new System.Drawing.Point(490, 59);
            this.facenum_label.Name = "facenum_label";
            this.facenum_label.Size = new System.Drawing.Size(20, 22);
            this.facenum_label.TabIndex = 7;
            this.facenum_label.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(29, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 8;
            this.label4.Text = "当前教室：";
            // 
            // CurrentClassroom
            // 
            this.CurrentClassroom.AutoSize = true;
            this.CurrentClassroom.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurrentClassroom.Location = new System.Drawing.Point(125, 27);
            this.CurrentClassroom.Name = "CurrentClassroom";
            this.CurrentClassroom.Size = new System.Drawing.Size(20, 22);
            this.CurrentClassroom.TabIndex = 8;
            this.CurrentClassroom.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(29, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 22);
            this.label7.TabIndex = 8;
            this.label7.Text = "应到人数：";
            // 
            // LessonName
            // 
            this.LessonName.AutoSize = true;
            this.LessonName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LessonName.Location = new System.Drawing.Point(394, 27);
            this.LessonName.Name = "LessonName";
            this.LessonName.Size = new System.Drawing.Size(90, 22);
            this.LessonName.TabIndex = 8;
            this.LessonName.Text = "不知道什么";
            // 
            // NumShouldBe
            // 
            this.NumShouldBe.AutoSize = true;
            this.NumShouldBe.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NumShouldBe.Location = new System.Drawing.Point(125, 59);
            this.NumShouldBe.Name = "NumShouldBe";
            this.NumShouldBe.Size = new System.Drawing.Size(20, 22);
            this.NumShouldBe.TabIndex = 8;
            this.NumShouldBe.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(394, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 22);
            this.label11.TabIndex = 8;
            this.label11.Text = "实到人数：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.LessonName);
            this.groupBox1.Controls.Add(this.facenum_label);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.CurrentClassroom);
            this.groupBox1.Controls.Add(this.NumShouldBe);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 101);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "教室基本信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.GetImage);
            this.groupBox2.Controls.Add(this.CurrentImage);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.SetCameraSpeed);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(641, 261);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "摄像头设置";
            // 
            // OperateCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(665, 489);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ExitToIndex);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OperateCamera";
            this.Text = "form";
            this.Load += new System.EventHandler(this.OperateCamera_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetImage;
        private System.Windows.Forms.PictureBox CurrentImage;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SetCameraSpeed;
        private System.Windows.Forms.Button ExitToIndex;
        private System.Windows.Forms.Label facenum_label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label CurrentClassroom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LessonName;
        private System.Windows.Forms.Label NumShouldBe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}