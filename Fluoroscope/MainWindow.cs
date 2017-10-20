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
    public partial class MainWindow : Form
    {
        Fluoroscope fluoroscope;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "loading...";

            String spath = "C:\\kohoutech\\DECODER RING\\Fluoroscope\\";
            String srcname = spath + "ProSoloVst.dll";
            //String spath = "C:\\kohoutech\\DECODER RING\\Fluoroscope\\FluoroTester\\";
            //String srcname = spath + "test.dll";
            String outname = spath + "out.code.txt";
            fluoroscope = new Fluoroscope(srcname, outname);
            lblStatus.Text = "done...";
        }
    }
}
