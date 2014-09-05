namespace TrampolineTimer
{
    partial class CameraInterface
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
            this.cameraPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // cameraPanel
            // 
            this.cameraPanel.BackColor = System.Drawing.Color.Transparent;
            this.cameraPanel.Location = new System.Drawing.Point(-2, 0);
            this.cameraPanel.Name = "cameraPanel";
            this.cameraPanel.Size = new System.Drawing.Size(300, 347);
            this.cameraPanel.TabIndex = 0;
            this.cameraPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // CameraInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(300, 350);
            this.Controls.Add(this.cameraPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "CameraInterface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My WebCam Manager ® Muaz Khan";
            this.Shown += new System.EventHandler(this.CameraInterface_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel cameraPanel;

    }
}

