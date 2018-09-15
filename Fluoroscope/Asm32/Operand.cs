/* ----------------------------------------------------------------------------
Origami Asm32 Library
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

namespace Origami.Asm32
{
    public class Operand
    {
       public enum OPSIZE { Byte, SignedByte, Word, DWord, QWord, FWord, TByte, MM, XMM, None };
    }

//- immediate ----------------------------------------------------------------

    public class Immediate : Operand
    {
        public uint val;
        public OPSIZE size;
        public bool isOffset;

        public Immediate(uint _val, OPSIZE _size)
        {
            val = _val;
            size = _size;
            isOffset = false;
        }

        public override string ToString()
        {
            String result = val.ToString("X");
            if (val > 0x09) result = result + "h";
            if (isOffset && size == OPSIZE.Byte)
            {
                uint b = val;
                bool negative = false;
                if ((b > 0x80))
                {
                    b = 0x100 - b;
                    negative = true;
                }
                result = b.ToString("X");
                if (b > 0x09) result = result + "h";
                if (negative)
                {
                    result = "-" + result;
                }
            }

            if (size == OPSIZE.SignedByte)
            {
                if (val >= 0x80)
                {
                    result = "FFFFFF" + result;
                }
            }

            if ((!isOffset) && (Char.IsLetter(result[0])))
            {
                result = "0" + result;
            }

            return result;
        }
    }

    public class Address : Operand
    {
        public uint val;

        public Address(uint _val)
        {
            val = _val;
        }

        public override string ToString()
        {
            return val.ToString("X8");
        }
    }

    public class Absolute : Operand
    {
        public uint seg;
        public uint addr;

        public Absolute(uint _seg, uint _addr)
        {
            seg = _seg;
            addr = _addr;            
        }

        public override string ToString()
        {
            return (seg.ToString("X4") + ':' + addr.ToString("X8"));
        }
    }

//- memory --------------------------------------------------------------------

    public class Memory : Operand
    {
        public Register f1;
        public Register f2;
        public int mult;
        public Immediate f3;
        public OPSIZE size;
        public Segment.SEG seg;

        public Memory(Register _f1, Register _f2, int _mult, Immediate _f3, OPSIZE _size, Segment.SEG _seg)
        {
            f1 = _f1;
            f2 = _f2;
            mult = _mult;
            f3 = _f3;
            if (f3 != null)
            {
                f3.isOffset = true;
            }
            size = _size;
            seg = _seg;
        }

        public String getSizePtrStr(Operand.OPSIZE size)
        {
           //if (operandSizeOverride && (size == Operand.OPSIZE.DWord)) size = Operand.OPSIZE.Word;

            String result = "???";
            if (size == Operand.OPSIZE.Byte) result = "byte ptr ";
            if (size == Operand.OPSIZE.Word) result = "word ptr ";
            if (size == Operand.OPSIZE.DWord) result = "dword ptr ";
            if (size == Operand.OPSIZE.QWord) result = "qword ptr ";
            if (size == Operand.OPSIZE.FWord) result = "fword ptr ";
            if (size == Operand.OPSIZE.TByte) result = "tbyte ptr ";
            //if (size == Operand.OPSIZE.MM) result = "mmword ptr ";
            if (size == Operand.OPSIZE.XMM) result = "xmmword ptr ";
            if (size == Operand.OPSIZE.None) result = "";
            return result;
        }

        public override string ToString()
        {
            String result = "";

            //the address part
            if (f1 != null)
            {
                result = f1.ToString();
            }
            if (f2 != null)
            {
                if (result.Length > 0)
                {
                    result += "+";
                }
                result += f2.ToString();
            }
            if (mult > 1)
            {
                result += ("*" + mult.ToString());
            }
            if ((f3 != null) && (f3.val > 0))
            {
                String immStr = f3.ToString();
                if ((result.Length > 0) && (immStr[0] != '-'))
                {
                    result += "+";
                }
                result += immStr;
            }

            //the decorations
            result = "[" + result + "]";
            if ((seg != Segment.SEG.DS) || (f1 == null && f2 == null))
            {
                result = seg.ToString() + ":" + result;
            }
            result = getSizePtrStr(size) + result;
            return result;
        }
    }

}
