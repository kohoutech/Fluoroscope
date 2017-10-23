namespace Fluoroscope
{
    partial class FluoroWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FluoroWindow));
            this.fluoroStatus = new System.Windows.Forms.StatusStrip();
            this.fluoroMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fluoroCanvas = new System.Windows.Forms.Panel();
            this.fluoroOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fluoroMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fluoroStatus
            // 
            this.fluoroStatus.Location = new System.Drawing.Point(0, 339);
            this.fluoroStatus.Name = "fluoroStatus";
            this.fluoroStatus.Size = new System.Drawing.Size(384, 22);
            this.fluoroStatus.TabIndex = 0;
            this.fluoroStatus.Text = "statusStrip1";
            // 
            // fluoroMenu
            // 
            this.fluoroMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.fluoroMenu.Location = new System.Drawing.Point(0, 0);
            this.fluoroMenu.Name = "fluoroMenu";
            this.fluoroMenu.Size = new System.Drawing.Size(384, 24);
            this.fluoroMenu.TabIndex = 1;
            this.fluoroMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileMenuItem,
            this.toolStripSeparator2,
            this.exitFileMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openFileMenuItem.Image")));
            this.openFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileMenuItem.Size = new System.Drawing.Size(185, 22);
            this.openFileMenuItem.Text = "&Open Exe file";
            this.openFileMenuItem.Click += new System.EventHandler(this.openFileMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(182, 6);
            // 
            // exitFileMenuItem
            // 
            this.exitFileMenuItem.Name = "exitFileMenuItem";
            this.exitFileMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exitFileMenuItem.Text = "E&xit";
            this.exitFileMenuItem.Click += new System.EventHandler(this.exitFileMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutHelpMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutHelpMenuItem
            // 
            this.aboutHelpMenuItem.Name = "aboutHelpMenuItem";
            this.aboutHelpMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutHelpMenuItem.Text = "&About...";
            this.aboutHelpMenuItem.Click += new System.EventHandler(this.aboutHelpMenuItem_Click);
            // 
            // fluoroCanvas
            // 
            this.fluoroCanvas.BackColor = System.Drawing.Color.DodgerBlue;
            this.fluoroCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fluoroCanvas.Location = new System.Drawing.Point(0, 24);
            this.fluoroCanvas.Name = "fluoroCanvas";
            this.fluoroCanvas.Size = new System.Drawing.Size(384, 315);
            this.fluoroCanvas.TabIndex = 2;
            // 
            // fluoroOpenFileDialog
            // 
            this.fluoroOpenFileDialog.FileName = "openFileDialog1";
            // 
            // FluoroWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.fluoroCanvas);
            this.Controls.Add(this.fluoroStatus);
            this.Controls.Add(this.fluoroMenu);
            this.MainMenuStrip = this.fluoroMenu;
            this.Name = "FluoroWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fluoroscope";
            this.fluoroMenu.ResumeLayout(false);
            this.fluoroMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip fluoroStatus;
        private System.Windows.Forms.MenuStrip fluoroMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHelpMenuItem;
        private System.Windows.Forms.Panel fluoroCanvas;
        private System.Windows.Forms.OpenFileDialog fluoroOpenFileDialog;

    }
}

