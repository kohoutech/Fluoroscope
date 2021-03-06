﻿/* ----------------------------------------------------------------------------
Fluoroscope : an executable file viewer
Copyright (C) 1998-2018  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Fluoroscope.UI;
using Origami.Win32;

namespace Fluoroscope
{
    public partial class FluoroWindow : Form
    {
        Fluoroscope fscope;
        List<SectionPanel> sections;
        SectionPanel selectedSection;
        String currentPath;

        public FluoroWindow()
        {
            fscope = new Fluoroscope(this);

            InitializeComponent();

            sections = new List<SectionPanel>();
            selectedSection = null;
            currentPath = null;
        }

//-----------------------------------------------------------------------------

        public void openFile(String filename)
        {
            fluoroStatusLabel.Text = "Loading...";
            closeFile();
            try
            {
                fscope.openSourceFile(filename);
            }
            catch (Win32ReadException e)
            {
                String msg = "unable to load " + filename + "\n" + e.Message;
                MessageBox.Show(msg, "read error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fluoroStatusLabel.Text = "";
                return;
            }

            Point panelPos = new Point(0, 0);
            foreach (Section section in fscope.winexe.sections)
            {
                SectionPanel panel = new SectionPanel(this, section);
                sections.Add(panel);
                panel.Location = panelPos;
                fCanvas.Controls.Add(panel);
                panel.Show();
                panelPos.Offset(0, panel.Height);
            }
            this.ClientSize = new Size(fCanvas.Width + 20, 
                fCanvas.Height + fluoroMenu.Height + fluoroToolStrip.Height + fluoroStatus.Height + 20);
            Text = "Fluoroscope [" + filename + "]";
            fluoroStatusLabel.Text = "";
            exeHdrViewMenuItem.Enabled = true;
            exehdrToolStripButton.Enabled = true;
        }

        public void setSelectedSections(SectionPanel selected)
        {
            if (selected != selectedSection)
            {
                if (selectedSection != null)
                {
                    selectedSection.setSelcted(false);
                }
                selectedSection = selected;
                selectedSection.setSelcted(true);
                dataToolStripButton.Enabled = true;
                dataViewMenuItem.Enabled = true;
                bool codesec = selectedSection.section.isCode();
                codeViewMenuItem.Enabled = codesec;
                codeToolStripButton.Enabled = codesec;
            }
        }

        public void closeFile()
        {
            fCanvas.Controls.Clear();
            fscope.closeSourceFile();
            Text = "Fluoroscope";
            exeHdrViewMenuItem.Enabled = false;
            exehdrToolStripButton.Enabled = false;
            dataToolStripButton.Enabled = false;
            dataViewMenuItem.Enabled = false;
            codeViewMenuItem.Enabled = false;
            codeToolStripButton.Enabled = false;
        }

        private void showOpenFileDialog()
        {
            String filename = "";
            //if (currentPath != null)
            //{
            //    fluoroOpenFileDialog.InitialDirectory = currentPath;
            //}
            //else
            //{
            //    fluoroOpenFileDialog.InitialDirectory = Application.StartupPath;                
            //}
            //fluoroOpenFileDialog.FileName = "";
            //fluoroOpenFileDialog.DefaultExt = "*.exe";
            //fluoroOpenFileDialog.Filter = "Executable files|*.exe|DLL files|*.dll|All files|*.*";
            //fluoroOpenFileDialog.ShowDialog();
            //filename = fluoroOpenFileDialog.FileName;
            filename = "test.dll";
            if (filename.Length != 0)
            {
                currentPath = Path.GetDirectoryName(filename);
                openFile(filename);
            }
        }

//-----------------------------------------------------------------------------

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            showOpenFileDialog();
        }

        private void closeFileMenuItem_Click(object sender, EventArgs e)
        {
            closeFile();
        }

        private void exitFileMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

//-----------------------------------------------------------------------------

        private void exeHdrViewMenuItem_Click(object sender, EventArgs e)
        {
            fscope.showExeHeaderInfo();
        }

        private void selSecDataViewMenuItem_Click(object sender, EventArgs e)
        {
            fscope.showSectionData(selectedSection.section);
        }

        private void disasmCodeViewMenuItem_Click(object sender, EventArgs e)
        {
            fscope.showSectionCode(selectedSection.section);
        }

//-----------------------------------------------------------------------------

        private void aboutHelpMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Fluoroscope\nversion 1.1.0\n" + "\xA9 Origami Software 1998-2018\n" + "http://origami.kohoutech.com";
            MessageBox.Show(msg, "About");
        }

//- tool strip ------------------------------------------------------------

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            showOpenFileDialog();
        }

        private void exehdrToolStripButton_Click(object sender, EventArgs e)
        {
            fscope.showExeHeaderInfo();
        }

        private void dataToolStripButton_Click(object sender, EventArgs e)
        {
            Cursor prevCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            fscope.showSectionData(selectedSection.section);
            this.Cursor = prevCursor;
        }

        private void codeToolStripButton_Click(object sender, EventArgs e)
        {
            fscope.showSectionCode(selectedSection.section);
        }

    }
}
