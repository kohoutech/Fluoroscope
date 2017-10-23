/* ----------------------------------------------------------------------------
Fluoroscope : an executable file decoder
Copyright (C) 1998-2017  George E Greaney

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

namespace Fluoroscope
{
    public partial class FluoroWindow : Form
    {
        Fluoroscope fscope;

        public FluoroWindow()
        {
            fscope = new Fluoroscope();

            InitializeComponent();
        }

//-----------------------------------------------------------------------------

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            //call get new graphic filename dialog box
            String filename = "";
            fluoroOpenFileDialog.InitialDirectory = Application.StartupPath;
            fluoroOpenFileDialog.FileName = "";
            fluoroOpenFileDialog.DefaultExt = "*.exe";
            fluoroOpenFileDialog.Filter = "Executable files|*.exe|DLL files|*.dll|All files|*.*";
            fluoroOpenFileDialog.ShowDialog();
            filename = fluoroOpenFileDialog.FileName;
            if (filename.Length != 0)
            {
                fscope.setSourceFile(filename);
                fscope.parseSource();
            }
        }

        private void exitFileMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

//-----------------------------------------------------------------------------

        private void aboutHelpMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Fluoroscope\nversion 1.0.0\n" + "\xA9 Origami Software 1998-2017\n" + "http://origami.kohoutech.com";
            MessageBox.Show(msg, "About");
        }

    }
}
