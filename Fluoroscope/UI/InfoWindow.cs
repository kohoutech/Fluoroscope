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

namespace Fluoroscope.UI
{
    class InfoWindow : Form
    {
        public TextBox infoText;

        public InfoWindow()
        {
            this.ShowInTaskbar = false;
            this.BackColor = Color.Red;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(200, 200);

            infoText = new TextBox();
            infoText.Font = new Font("Courier New", 10.0f);
            infoText.BackColor = Color.PowderBlue;
            infoText.Multiline = true;
            infoText.WordWrap = false;
            infoText.ScrollBars = ScrollBars.Both;
            infoText.ShortcutsEnabled = true;
            this.Controls.Add(infoText);
            infoText.Size = this.ClientRectangle.Size;

            this.Size = new Size(650, 600);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            infoText.Size = this.ClientRectangle.Size;
        }

        public void setText(String text)
        {
            infoText.Text = text;
            infoText.Select(0, 0);            
        }

        public void setText(List<String> text)
        {
            setText(String.Join(Environment.NewLine, text));
        }

        public void setTitle(String title)
        {
            this.Text = title;
        }
    }
}
