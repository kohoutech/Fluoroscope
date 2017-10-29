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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FluoroWindow));
            this.fluoroStatus = new System.Windows.Forms.StatusStrip();
            this.fluoroMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exeHdrViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fluoroOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fCanvas = new System.Windows.Forms.Panel();
            this.fluoroToolStrip = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fluoroStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.exehdrToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dataToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.codeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fluoroToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selSecDataViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disasmCodeViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fluoroStatus.SuspendLayout();
            this.fluoroMenu.SuspendLayout();
            this.fluoroToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // fluoroStatus
            // 
            this.fluoroStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fluoroStatusLabel});
            this.fluoroStatus.Location = new System.Drawing.Point(0, 439);
            this.fluoroStatus.Name = "fluoroStatus";
            this.fluoroStatus.Size = new System.Drawing.Size(484, 22);
            this.fluoroStatus.TabIndex = 0;
            this.fluoroStatus.Text = "statusStrip1";
            // 
            // fluoroMenu
            // 
            this.fluoroMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.fluoroMenu.Location = new System.Drawing.Point(0, 0);
            this.fluoroMenu.Name = "fluoroMenu";
            this.fluoroMenu.Size = new System.Drawing.Size(484, 24);
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
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exeHdrViewMenuItem,
            this.selSecDataViewMenuItem,
            this.disasmCodeViewMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // exeHdrViewMenuItem
            // 
            this.exeHdrViewMenuItem.Name = "exeHdrViewMenuItem";
            this.exeHdrViewMenuItem.Size = new System.Drawing.Size(212, 22);
            this.exeHdrViewMenuItem.Text = "Exe Header";
            this.exeHdrViewMenuItem.Click += new System.EventHandler(this.exeHdrViewMenuItem_Click);
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
            this.aboutHelpMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutHelpMenuItem.Text = "&About...";
            this.aboutHelpMenuItem.Click += new System.EventHandler(this.aboutHelpMenuItem_Click);
            // 
            // fluoroOpenFileDialog
            // 
            this.fluoroOpenFileDialog.FileName = "openFileDialog1";
            // 
            // fCanvas
            // 
            this.fCanvas.AutoScroll = true;
            this.fCanvas.BackColor = System.Drawing.Color.DodgerBlue;
            this.fCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fCanvas.Location = new System.Drawing.Point(0, 49);
            this.fCanvas.Name = "fCanvas";
            this.fCanvas.Size = new System.Drawing.Size(484, 390);
            this.fCanvas.TabIndex = 2;
            // 
            // fluoroToolStrip
            // 
            this.fluoroToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.toolStripSeparator1,
            this.exehdrToolStripButton,
            this.dataToolStripButton,
            this.codeToolStripButton});
            this.fluoroToolStrip.Location = new System.Drawing.Point(0, 24);
            this.fluoroToolStrip.Name = "fluoroToolStrip";
            this.fluoroToolStrip.Size = new System.Drawing.Size(484, 25);
            this.fluoroToolStrip.TabIndex = 3;
            this.fluoroToolStrip.Text = "toolStrip1";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.ToolTipText = "open EXE file";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // fluoroStatusLabel
            // 
            this.fluoroStatusLabel.Name = "fluoroStatusLabel";
            this.fluoroStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // exehdrToolStripButton
            // 
            this.exehdrToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exehdrToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exehdrToolStripButton.Image")));
            this.exehdrToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exehdrToolStripButton.Name = "exehdrToolStripButton";
            this.exehdrToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.exehdrToolStripButton.Text = "H";
            this.exehdrToolStripButton.ToolTipText = "view EXE header";
            this.exehdrToolStripButton.Click += new System.EventHandler(this.exehdrToolStripButton_Click);
            // 
            // dataToolStripButton
            // 
            this.dataToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dataToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("dataToolStripButton.Image")));
            this.dataToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dataToolStripButton.Name = "dataToolStripButton";
            this.dataToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.dataToolStripButton.Text = "D";
            this.dataToolStripButton.ToolTipText = "view section data";
            this.dataToolStripButton.Click += new System.EventHandler(this.dataToolStripButton_Click);
            // 
            // codeToolStripButton
            // 
            this.codeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.codeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("codeToolStripButton.Image")));
            this.codeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.codeToolStripButton.Name = "codeToolStripButton";
            this.codeToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.codeToolStripButton.Text = "C";
            this.codeToolStripButton.ToolTipText = "disassemble code section";
            this.codeToolStripButton.Click += new System.EventHandler(this.codeToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // selSecDataViewMenuItem
            // 
            this.selSecDataViewMenuItem.Name = "selSecDataViewMenuItem";
            this.selSecDataViewMenuItem.Size = new System.Drawing.Size(212, 22);
            this.selSecDataViewMenuItem.Text = "Selected Section Data";
            this.selSecDataViewMenuItem.Click += new System.EventHandler(this.selSecDataViewMenuItem_Click);
            // 
            // disasmCodeViewMenuItem
            // 
            this.disasmCodeViewMenuItem.Name = "disasmCodeViewMenuItem";
            this.disasmCodeViewMenuItem.Size = new System.Drawing.Size(212, 22);
            this.disasmCodeViewMenuItem.Text = "Disassemble Code Section";
            this.disasmCodeViewMenuItem.Click += new System.EventHandler(this.disasmCodeViewMenuItem_Click);
            // 
            // FluoroWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.fCanvas);
            this.Controls.Add(this.fluoroStatus);
            this.Controls.Add(this.fluoroToolStrip);
            this.Controls.Add(this.fluoroMenu);
            this.MainMenuStrip = this.fluoroMenu;
            this.Name = "FluoroWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fluoroscope";
            this.fluoroStatus.ResumeLayout(false);
            this.fluoroStatus.PerformLayout();
            this.fluoroMenu.ResumeLayout(false);
            this.fluoroMenu.PerformLayout();
            this.fluoroToolStrip.ResumeLayout(false);
            this.fluoroToolStrip.PerformLayout();
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
        private System.Windows.Forms.OpenFileDialog fluoroOpenFileDialog;
        private System.Windows.Forms.Panel fCanvas;
        private System.Windows.Forms.ToolStrip fluoroToolStrip;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exeHdrViewMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel fluoroStatusLabel;
        private System.Windows.Forms.ToolStripButton exehdrToolStripButton;
        private System.Windows.Forms.ToolStripButton dataToolStripButton;
        private System.Windows.Forms.ToolStripButton codeToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolTip fluoroToolTip;
        private System.Windows.Forms.ToolStripMenuItem selSecDataViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disasmCodeViewMenuItem;

    }
}

