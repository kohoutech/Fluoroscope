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

//- data transfer -------------------------------------------------------------

        //SSEMovePacked - MOVAPS/MOVUPS
        public class SSEMovePacked : Instruction
        {
            bool intop;
            bool pop;

            public SSEMovePacked(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEMoveHigh - MOVHPS/MOVHLPS
        public class SSEMoveHigh : Instruction
        {
            bool intop;
            bool pop;

            public SSEMoveHigh(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEMoveLow - MOVLPS/MOVLHPS
        public class SSEMoveLow : Instruction
        {
            bool intop;
            bool pop;

            public SSEMoveLow(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEExtract - MOVMSKPS
        public class SSEExtract : Instruction
        {
            bool intop;
            bool pop;

            public SSEExtract(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEMoveScalar - MOVSS
        public class SSEMoveScalar : Instruction
        {
            bool intop;
            bool pop;

            public SSEMoveScalar(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- arithmetic -------------------------------------------------------------

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

            public SSEReciprocal(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- comparison ----------------------------------------------------------------

        //SSECompare - CMPPS/CMPSS
        public class SSECompare : Instruction
        {
            bool intop;
            bool pop;

            public SSECompare(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSECompareSetFlags - COMISS/UCOMISS
        public class SSECompareSetFlags : Instruction
        {
            bool intop;
            bool pop;

            public SSECompareSetFlags(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- logical -------------------------------------------------------------------

        //SSEAnd - ANDPS
        public class SSEAnd : Instruction
        {
            bool intop;
            bool pop;

            public SSEAnd(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSENand - ANDNPS
        public class SSENand : Instruction
        {
            bool intop;
            bool pop;

            public SSENand(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEOr - ORPS
        public class SSEOr : Instruction
        {
            bool intop;
            bool pop;

            public SSEOr(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEXor - XORPS
        public class SSEXor : Instruction
        {
            bool intop;
            bool pop;

            public SSEXor(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- shuffle/unpack ------------------------------------------------------------

        //SSEShuffle - SHUFPS
        public class SSEShuffle : Instruction
        {
            bool intop;
            bool pop;

            public SSEShuffle(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEUnpack - UNPCKHPS/UNPCKLPS
        public class SSEUnpack : Instruction
        {
            bool intop;
            bool pop;

            public SSEUnpack(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- conversion ----------------------------------------------------------------

        //SSEConvertFromInt - CVTPI2PS/CVTSI2SS
        public class SSEConvertFromInt : Instruction
        {
            bool intop;
            bool pop;

            public SSEConvertFromInt(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEConvertPackedToInt - CVTPS2PI/CVTTPS2PI
        public class SSEConvertPackedToInt : Instruction
        {
            bool intop;
            bool pop;

            public SSEConvertPackedToInt(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEConvertScalarToInt - CVTSS2SI/CVTTSS2SI
        public class SSEConvertScalarToInt : Instruction
        {
            bool intop;
            bool pop;

            public SSEConvertScalarToInt(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- state mgmt ----------------------------------------------------------------

        //SSELoadState - LDMXCSR
        public class SSELoadState : Instruction
        {
            bool intop;
            bool pop;

            public SSELoadState(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEStoreState - STMXCSR
        public class SSEStoreState : Instruction
        {
            bool intop;
            bool pop;

            public SSEStoreState(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- control -------------------------------------------------------------------

        //SSEStoreQuadBytes - MASKMOVQ
        public class SSEStoreQuadBytes : Instruction
        {
            bool intop;
            bool pop;

            public SSEStoreQuadBytes(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEStoreQuad - MOVNTQ
        public class SSEStoreQuad : Instruction
        {
            bool intop;
            bool pop;

            public SSEStoreQuad(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEStorePacked - MOVNTPS
        public class SSEStorePacked : Instruction
        {
            bool intop;
            bool pop;

            public SSEStorePacked(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEPrefetchData - PREFETCHNTA/PREFETCHT0/PREFETCHT1/PREFETCHT2
        public class SSEPrefetchData : Instruction
        {
            bool intop;
            bool pop;

            public SSEPrefetchData(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

        //SSEStoreFence - SFENCE
        public class SSEStoreFence : Instruction
        {
            bool intop;
            bool pop;

            public SSEStoreFence(Operand _op1, Operand _op2, bool _intop, bool _pop)
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
