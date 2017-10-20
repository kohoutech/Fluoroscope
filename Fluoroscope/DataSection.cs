﻿/* ----------------------------------------------------------------------------
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

namespace Fluoroscope
{
    class DataSection : Section
    {
        public DataSection(Fluoroscope _fluoro, int _secnum, String _sectionName, uint _memsize, 
                uint _memloc, uint _filesize, uint _fileloc, uint _flags, uint _imageBase)
            : base(_fluoro, _secnum, _sectionName, _memsize, _memloc, _filesize, _fileloc, 
            _flags, _imageBase)
        {
            Console.Out.WriteLine("[" + _secnum + "] is a data section");
        }
    }
}