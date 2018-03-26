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

        public Immediate(uint _val, OPSIZE _size)
        {
            val = _val;
            size = _size;
        }
    }

//- register ------------------------------------------------------------------

    public class Register : Operand
    {
        public enum REG8 { AL, CL, DL, BL, AH, CH, DH, BH, None };
        public enum REG16 { AX, CX, DX, BX, SP, BP, SI, DI, None };
        public enum REG32 { EAX, ECX, EDX, EBX, ESP, EBP, ESI, EDI, None };
        public enum REGMM { MM0, MM1, MM2, MM3, MM4, MM5, MM6, MM7, None };
        public enum REGXMM { XMM0, XMM1, XMM2, XMM3, XMM4, XMM5, XMM6, XMM7, None };

        public int size;
        public REG8 reg8;
        public REG16 reg16;
        public REG32 reg32;
        public REGMM regmm;
        public REGXMM regxmm;

        public Register(REG32 r32)
        {
            size = 32;
            reg8 = REG8.None;
            reg16 = REG16.None;
            reg32 = r32;
            regmm = REGMM.None;
            regxmm = REGXMM.None;
        }

        public Register(REG16 r16)
        {
            size = 16;
            reg8 = REG8.None;
            reg16 = r16;
            reg32 = REG32.None;
            regmm = REGMM.None;
            regxmm = REGXMM.None;
        }

        public Register(REG8 r8)
        {
            size = 8;
            reg8 = r8;
            reg16 = REG16.None;
            reg32 = REG32.None;
            regmm = REGMM.None;
            regxmm = REGXMM.None;
        }

        public Register(REGMM rmm)
        {
            size = 1;
            reg8 = REG8.None;
            reg16 = REG16.None;
            reg32 = REG32.None;
            regmm = rmm;
            regxmm = REGXMM.None;
        }

        public Register(REGXMM rxmm)
        {
            size = 2;
            reg8 = REG8.None;
            reg16 = REG16.None;
            reg32 = REG32.None;
            regmm = REGMM.None;
            regxmm = rxmm;
        }

    }

//- register ------------------------------------------------------------------

        //    readonly String[] seg16 = { "es", "cs", "ss", "ds", "fs", "gs", "??", "??" };

    public class Segment : Operand
    {
        public enum SEG { ES, CS, SS, DS, FS, GS, None };

        public SEG seg;

        public Segment(SEG _seg)
        {
            seg = _seg;
        }
    }

//- memory --------------------------------------------------------------------

    public class Memory : Operand
    {
    }

}
