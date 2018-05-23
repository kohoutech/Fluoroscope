/* ----------------------------------------------------------------------------
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

//10/11/16

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Fluoroscope.UI;
using Origami.Win32;

namespace Fluoroscope
{

    class Fluoroscope
    {
        public FluoroWindow fwindow;
        public Win32Exe winexe;

        public Fluoroscope(FluoroWindow _fwindow)
        {
            fwindow = _fwindow;
            winexe = null;
        }

        public void openSourceFile(String filename)
        {
            winexe = new Win32Exe();
            winexe.readFile(filename);      //read in file            
        }

        public void closeSourceFile()
        {
            winexe = null;
        }

//-----------------------------------------------------------------------------

        public void showExeHeaderInfo()
        {
            InfoWindow infoWin = new InfoWindow();
            infoWin.setTitle("EXE Header");
            String text = winexe.peHeader.getInfo() + "\r\n" + winexe.optHeader.getInfo();
            infoWin.setText(text);
            infoWin.Show(fwindow);
        }

        public void showSectionData(Section section)
        {
            InfoWindow infoWin = new InfoWindow();
            infoWin.setTitle("Section [" + section.secNum + "] " + section.secName + " Data");
            String text = section.displayData();
            infoWin.setText(text);
            infoWin.Show(fwindow);
        }

        public void showSectionCode(Section section)
        {
            if (section is CodeSection)
            {
                CodeSection codeSec = (CodeSection)section;
                InfoWindow infoWin = new InfoWindow();
                infoWin.setTitle("Section [" + section.secNum + "] " + section.secName + " Code");
                List<String> text = codeSec.disasmCode();
                infoWin.setText(text);
                infoWin.Show(fwindow);
            }
        }

        public void writeDumpFile(String outname, List<String> lines)
        {
            StreamWriter outfile = new StreamWriter(outname);

            foreach (String line in lines)
            {
                outfile.Write(line);
            }
            outfile.Close();
        }
    }
}
