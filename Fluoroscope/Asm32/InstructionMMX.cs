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
    public class InstructionMMX
    {
        //MMXEmptyState  - EMMS
        public class MMXEmptyState : Instruction
        {
            bool intop;
            bool pop;

            public MMXEmptyState(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return "EMMS";
            }
        }

        //MMXPack - PACKSSDW/PACKSSWB/PACKUSWB
        public class MMXPack : Instruction
        {
            bool intop;
            bool pop;

            public MMXPack(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXUnpack - PUNPCKHBW/PUNPCKHDQ/PUNPCKHWD/PUNPCKLBW/PUNPCKLDQ/PUNPCKLWD
        public class MMXUnpack : Instruction
        {
            bool intop;
            bool pop;

            public MMXUnpack(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXMoveWord - MOVD/MOVQ
        public class MMXMoveWord : Instruction
        {
            bool intop;
            bool pop;

            public MMXMoveWord(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXAdd - PADDB/PADDW/PADDD/PADDSB/PADDSW/PADDUSB/PADDUSW
        public class MMXAdd : Instruction
        {
            bool intop;
            bool pop;

            public MMXAdd(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXSubtract - PSUBB/PSUBD/PSUBW/PSUBSB/PSUBSW/PSUBUSB/PSUBUSW
        public class MMXSubtract : Instruction
        {
            bool intop;
            bool pop;

            public MMXSubtract(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXMult - PMULHW/PMULLW
        public class MMXMult : Instruction
        {
            bool intop;
            bool pop;

            public MMXMult(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXMultAdd - PMADDWD
        public class MMXMultAdd : Instruction
        {
            bool intop;
            bool pop;

            public MMXMultAdd(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXShift - PSLLD/PSLLQ/PSLLW/PSRAD/PSRAW/PSRLD/PSRLQ/PSRLW
        public class MMXShift : Instruction
        {
            bool intop;
            bool pop;

            public MMXShift(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXAnd - PAND
        public class MMXAnd : Instruction
        {
            bool intop;
            bool pop;

            public MMXAnd(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXAddNot - PANDN
        public class MMXAddNot : Instruction
        {
            bool intop;
            bool pop;

            public MMXAddNot(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXOr - POR
        public class MMXOr : Instruction
        {
            bool intop;
            bool pop;

            public MMXOr(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXXor - PXOR
        public class MMXXor : Instruction
        {
            bool intop;
            bool pop;

            public MMXXor(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXCompEqual - PCMPEQB/PCMPEQD/PCMPEQW
        public class MMXCompEqual : Instruction
        {
            bool intop;
            bool pop;

            public MMXCompEqual(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }

        //MMXCompGtrThn - PCMPGTB/PCMPGTD/PCMPGTW
        public class MMXCompGtrThn : Instruction
        {
            bool intop;
            bool pop;

            public MMXCompGtrThn(Operand _op1, Operand _op2, bool _intop, bool _pop)
                : base()
            {
                opcount = (_op2 != null) ? 2 : 1;
                op1 = _op1;
                op2 = _op2;
                intop = _intop;
                pop = _pop;
            }

            public override string ToString()
            {
                return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
            }
        }
    }
}
