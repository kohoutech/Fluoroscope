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
    public enum OPSIZE { Byte, SignedByte, Word, DWord, QWord, FWord, TByte, MM, XMM, CR, DR, None };

    public class Operand
    {
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

        public List<byte> getBytes()
        {
            List<byte> result = new List<byte>();
            switch (size)
            {
                case OPSIZE.Byte:
                    result.Add((byte)val);
                    break;

                case OPSIZE.DWord:
                    uint _val = val;
                    int a = (int)_val % 256;
                    _val /= 256;
                    int b = (int)_val % 256;
                    _val /= 256;
                    int c = (int)_val % 256;
                    _val /= 256;
                    int d = (int)_val % 256;
                    result.Add((byte)a);
                    result.Add((byte)b);
                    result.Add((byte)c);
                    result.Add((byte)d);
                    break;
            }
            return result;
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

        public List<byte> getBytes(out int mode, out int rm)
        {
            List<byte> result = null;
            mode = 0;
            rm = 0;
            if (r1 != null)
            {
                if (r2 == null)
                {
                    if (imm == null)
                    {
                        //r1
                        rm = r1.code;       
                    }
                    else
                    {
                        //r1 + imm(8/32)
                    }
                }
                else
                {
                    if (imm == null)        
                    {
                        //r1 + r2
                        rm = 04;
                        result.Add((byte)(05 + (r1.code * 8)));
                        result.AddRange(imm.getBytes());
                    }
                    else
                    {
                        //r1 + r2 + imm(8/32)
                    }
                }
            }
            else
            {
                if (r2 != null)
                {
                    //r2 + imm(32)
                }
                else
                {
                    //imm(32)
                }
            }

            return result;
        }

        public String getSizePtrStr(OPSIZE size)
        {
            //if (operandSizeOverride && (size == OPSIZE.DWord)) size = OPSIZE.Word;

            String result = "";
            switch (size)
            {
                case OPSIZE.Byte:
                    result = "byte ptr ";
                    break;
                case OPSIZE.Word:
                    result = "word ptr ";
                    break;
                case OPSIZE.DWord:
                    result = "dword ptr ";
                    break;
                case OPSIZE.QWord:
                    result = "qword ptr ";
                    break;
                case OPSIZE.FWord:
                    result = "fword ptr ";
                    break;
                case OPSIZE.TByte:
                    result = "tbyte ptr ";
                    break;
                case OPSIZE.MM:
                    result = "mmword ptr ";
                    break;
                case OPSIZE.XMM:
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
