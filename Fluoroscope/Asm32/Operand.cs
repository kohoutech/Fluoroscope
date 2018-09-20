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
       public enum OPSIZE { Byte, SignedByte, Word, DWord, QWord, FWord, TByte, MM, XMM, CR, DR, None };
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

    //general format: <size> <seg>:[<r1> + <r2> * <mult) + <imm>]
    //any of these terms is optional, as long as there's at least one
    public class Memory : Operand
    {
        public Register r1;
        public Register r2;
        public int mult;
        public Immediate imm;
        public OPSIZE size;
        public Segment.SEG seg;

        public Memory(Register _r1, Register _r2, int _mult, Immediate _imm, OPSIZE _size, Segment.SEG _seg)
        {
            r1 = _r1;
            r2 = _r2;
            mult = _mult;
            imm = _imm;
            if (imm != null)
            {
                imm.isOffset = true;
            }
            size = _size;
            seg = _seg;
        }

        public String getSizePtrStr(Operand.OPSIZE size)
        {
           //if (operandSizeOverride && (size == Operand.OPSIZE.DWord)) size = Operand.OPSIZE.Word;

            String result = "";
            switch (size)
            {
                case Operand.OPSIZE.Byte:
                    result = "byte ptr ";
                    break;
                case Operand.OPSIZE.Word:
                    result = "word ptr ";
                    break;
                case Operand.OPSIZE.DWord:
                    result = "dword ptr ";
                    break;
                case Operand.OPSIZE.QWord:
                    result = "qword ptr ";
                    break;
                case Operand.OPSIZE.FWord:
                    result = "fword ptr ";
                    break;
                case Operand.OPSIZE.TByte:
                    result = "tbyte ptr ";
                    break;
                case Operand.OPSIZE.MM:
                    result = "mmword ptr ";
                    break;
                case Operand.OPSIZE.XMM:
                    result = "xmmword ptr ";
                    break;
            }
            return result;
        }

        public override string ToString()
        {
            String result = "";

            //the address part
            if (r1 != null)
            {
                result = r1.ToString();
            }
            if (r2 != null)
            {
                if (result.Length > 0)
                {
                    result += "+";
                }
                result += r2.ToString();
            }
            if (mult > 1)
            {
                result += ("*" + mult.ToString());
            }
            if ((imm != null) && (imm.val > 0))
            {
                String immStr = imm.ToString();
                if ((result.Length > 0) && (immStr[0] != '-'))
                {
                    result += "+";
                }
                result += immStr;
            }

            //the decorations
            result = "[" + result + "]";
            if ((seg != Segment.SEG.DS) || (r1 == null && r2 == null))
            {
                result = seg.ToString() + ":" + result;
            }
            result = getSizePtrStr(size) + result;
            return result;
        }
    }

}
