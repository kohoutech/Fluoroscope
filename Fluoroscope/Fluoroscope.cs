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
using System.Linq;
using System.Text;

//10/11/16

namespace Fluoroscope
{

    class Fluoroscope
    {
        public SourceFile source;
        public WindowsParser parser;
        public Decoder decoder;

        public Fluoroscope()
        {
            
            parser = new WindowsParser(this);
            decoder = new Decoder(this);
        }

        public void setSourceFile(String filename)
        {
            source = new SourceFile(filename);      //read in file
        }

        public void parseSource()
        {
            parser.parse();                         //parse win hdr + get section list
        }

        public void decodeSource()
        {
            decoder.decode();                       //decode code + data sections
        }
    }
}
