/* ----------------------------------------------------------------------------
Fluoroscope : an executable file decoder
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using Fluoroscope;
using Origami.Win32;

namespace Fluoroscope.UI
{
    public class SectionPanel : UserControl
    {
        public FluoroWindow fwindow;
        public Section section;
        public bool selected;

        public TextBox secText;

        public SectionPanel(FluoroWindow _fwindow, Section _section)
        {
            fwindow = _fwindow;
            section = _section;
            
            //this.BackColor = Color.White;
            this.Height = 70;
            this.Width = 400;
            selected = false;

            secText = new TextBox();
            secText.Font = new Font("Courier", 10.0f);
            secText.BackColor = Color.White;
            secText.Multiline = true;
            secText.WordWrap = false;
            secText.ReadOnly = true;
            secText.Click += new EventHandler(secText_Click);
            this.Controls.Add(secText);
            secText.Size = this.ClientRectangle.Size;
            setSectionData();
        }

        private String getSectionFlagData()
        {
            String flags = "      ";
            if (section.isCode()) flags += "code / ";
            if (section.isInitializedData()) flags += "initialized data / ";
            if (section.isUninitializedData()) flags += "uninitialized data / ";
            if (section.isDiscardable()) flags += "discardable / ";
            if (section.isExecutable()) flags += "executable / ";
            if (section.isReadable()) flags += "read / ";
            if (section.isWritable()) flags += "write / ";
            flags = flags.Remove(flags.Length - 2);
            return flags;
        }

        private void setSectionData()
        {
            //section text
            String secdata = "[" + section.secNum + "] " + section.secName + "\r\n";
            secdata += "      memory size: " + section.memsize.ToString("X") +
                "   address: " + section.memloc.ToString("X") + " to " + (section.memloc + section.memsize - 1).ToString("X")
                 + "\r\n";
            secdata += "      file size: " + section.filesize.ToString("X") +
                "   address: " + section.fileloc.ToString("X") + " to " + (section.fileloc + section.filesize - 1).ToString("X")
                 + "\r\n";
            secdata += getSectionFlagData();                
            secText.Text = secdata;
        }

        protected void secText_Click(object sender, System.EventArgs e)
        {
            secText.Select(0, 0);            
            fwindow.setSelectedSections(this);
        }

        public void setSelcted(bool isSelected)
        {
            selected = isSelected;
            secText.BackColor = (isSelected ? Color.PowderBlue : Color.White);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Graphics g = e.Graphics;
            //g.SmoothingMode = SmoothingMode.AntiAlias;


            ////section border
            //if (!selected)
            //{
            //    g.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
            //}
            //else
            //{
            //    Pen borderPen = new Pen(Color.Red, 2.0f);
            //    borderPen.Alignment = PenAlignment.Inset;
            //    g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
            //    borderPen.Dispose();
            //}
        }

    }
}
