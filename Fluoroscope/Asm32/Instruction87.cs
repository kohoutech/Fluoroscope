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

//- arithmetic ----------------------------------------------------------------

    public class FAdd : Instruction
    {
        bool intop;
        bool pop;

        public FAdd(Operand _op1, bool _intop, bool _pop)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            intop = _intop;
            pop = _pop;
        }

        public override string ToString()
        {
            return (intop ? "FIADD" : (pop) ? "FADDP" : "FADD");
        }
    }

    public class FSubtract : Instruction
    {
        bool intop;
        bool pop;
        bool reverse;

        public FSubtract(Operand _op1, bool _intop, bool _pop, bool _rev)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            intop = _intop;
            pop = _pop;
            reverse = _rev;
        }

        public override string ToString()
        {
            String result = (intop ? "FISUB" : (pop) ? "FSUBP" : "FSUB");
            if (reverse)
            {
                result = result + "R";
            }
            return result;
        }
    }

    public class FMulitply : Instruction
    {
        bool intop;
        bool pop;

        public FMulitply(Operand _op1, bool _intop, bool _pop)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            intop = _intop;
            pop = _pop;
        }

        public override string ToString()
        {
            return (intop ? "FIMUL" : (pop) ? "FMULP" : "FMUL");
        }
    }

    public class FDivide : Instruction
    {
        bool intop;
        bool pop;
        bool reverse;

        public FDivide(Operand _op1, bool _intop, bool _pop, bool _rev)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            intop = _intop;
            pop = _pop;
            reverse = _rev;
        }

        public override string ToString()
        {
            String result = (intop ? "FIDIV" : (pop) ? "FDIVP" : "FDIV");
            if (reverse)
            {
                result = result + "R";
            }
            return result;
        }
    }

    public class FIncrement : Instruction
    {
    }

    public class FDecrement : Instruction
    {
    }

    public class FSquareRoot : Instruction
    {
    }

    public class F2XM1 : Instruction
    {
    }

    public class FYL2X : Instruction
    {
    }

    public class FYL2XP1 : Instruction
    {
    }

//- trignometric -------------------------------------------------------------

    public class FSine : Instruction
    {
    }

    public class FCosine : Instruction
    {
    }

    public class FSineCosine : Instruction
    {
    }

    public class FTangent : Instruction
    {
    }

    public class FArcTangent : Instruction
    {
    }

//- numeric ----------------------------------------------------------------

    public class FChangeSign : Instruction
    {
    }

    public class FAbsolute : Instruction
    {
    }

    public class FRound : Instruction
    {
    }

    public class FScale : Instruction
    {
    }

    public class FExtract : Instruction
    {
    }

    public class FRemainder : Instruction
    {
    }

//- comparison ----------------------------------------------------------------

    public class FCompare : Instruction
    {
        bool intop;
        bool pop;
        bool doublepop;
        bool unordered;
        bool setflags;

        public FCompare(Operand _op1, bool _intop, bool _pop, bool _double, bool _unordered, bool _flags)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            intop = _intop;
            pop = _pop;
            doublepop = _double;
            if (doublepop) pop = true;          //you have to pop once before you can pop twice!
            unordered = _unordered;
            setflags = _flags;
        }

        public override string ToString()
        {
            String result = (intop ? "FICOM" : (unordered) ? "FUCOM" : "FCOM");
            if (setflags)
            {
                result = result + "I";
            }
            if (pop)
            {
                result = result + "P";
            }
            if (doublepop)
            {
                result = result + "PP";
            }
            return result;
        }
    }

    public class FCompareInt : Instruction
    {
    }

    public class FCompareFloat : Instruction
    {
    }

    public class FCompareUnordered : Instruction
    {
    }

    public class FTest : Instruction
    {
    }

    public class FExamine : Instruction
    {
    }

//- data operations -----------------------------------------------------------

    public class FExchange : Instruction
    {
    }

    public class FConditionalMove : Instruction
    {
    }

    public class FLoad : Instruction
    {
    }

    public class FLoadInteger : Instruction
    {
    }

    public class FLoadBCD : Instruction
    {
    }

    public class FLoadConstant : Instruction
    {
    }

    public class FStore : Instruction
    {
    }

    public class FStoreInteger : Instruction
    {
    }

    public class FStoreBCD : Instruction
    {
    }

    public class FFreeRegister : Instruction
    {
    }

//- control operations --------------------------------------------------------

    public class FInitialize : Instruction
    {
    }

    public class FClearExceptions : Instruction
    {
    }

    public class FLoadEnvironment : Instruction
    {
    }

    public class FStoreEnvironment : Instruction
    {
    }

    public class FLoadControlWord : Instruction
    {
    }

    public class FStoreControlWord : Instruction
    {
    }

    public class FStatusControlWord : Instruction
    {
    }

    public class FSaveState : Instruction
    {
    }

    public class FRestoreState : Instruction
    {
    }

    public class FNoOp : Instruction
    {
    }
}
