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
            this.SuspendLayout();
            // 
            // GetImage
            // 
            this.GetImage.Location = new System.Drawing.Point(256, 24);
            this.GetImage.Name = "GetImage";
            this.GetImage.Size = new System.Drawing.Size(91, 23);
            this.GetImage.TabIndex = 0;
            this.GetImage.Text = "获得当前图片";
            this.GetImage.UseVisualStyleBackColor = true;
            this.GetImage.Click += new System.EventHandler(this.GetImage_Click);
            // 
            // OperateCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 298);
            this.Controls.Add(this.GetImage);
            this.Name = "OperateCamera";
            this.Text = "OperateCamera";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetImage;
    }
}