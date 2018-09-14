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
    class InstructionSSE
    {
        //SSEAdd - ADDPS/ADDSS
        public class SSEAdd : Instruction
        {
            bool intop;
            bool pop;

            public SSEAdd(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSESubtract - SUBPS/SUBSS
        public class SSESubtract : Instruction
        {
            bool intop;
            bool pop;

            public SSESubtract(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEMult - MULPS/MULSS
        public class SSEMult : Instruction
        {
            bool intop;
            bool pop;

            public SSEMult(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEDivide - DIVPS/DIVSS
        public class SSEDivide : Instruction
        {
            bool intop;
            bool pop;

            public SSEDivide(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEReciprocal - RCPPS/RCPSS
        public class SSEReciprocal : Instruction
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

        //SSESqrt - SQRTPS/SQRTSS
        public class SSESqrt : Instruction
        {
            bool intop;
            bool pop;

            public SSESqrt(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEReciprocalSqrt - RSQRTPS/RSQRTSS
        public class SSEReciprocalSqrt : Instruction
        {
            bool intop;
            bool pop;

            public SSEReciprocalSqrt(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEMax - MAXPS/MAXSS
        public class SSEMax : Instruction
        {
            bool intop;
            bool pop;

            public SSEMax(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEMin - MINPS/MINSS
        public class SSEMin : Instruction
        {
            bool intop;
            bool pop;

            public SSEMin(Operand _op1, Operand _op2, bool _intop, bool _pop)
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


    }
}
