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
    public class Instruction
    {
        public enum SEGPREFIX {ES, CS, SS, DS, FS, GS, None};
        public enum LOCKPREFIX { LOCK, None };
        public enum LOOPPREFIX { REP, REPNE, None };

        public Operand op1;
        public Operand op2;
        public Operand op3;

        public int len;
        public int opcount;
        public List<int> bytes;

        public Instruction () 
        {
            len = 0;
            opcount = 0;
            bytes = new List<int>();
        }

        public List<int> getBytes()
        {
            return bytes;
        }
    }

//- arithmetic ----------------------------------------------------------------

    public class Add : Instruction
    {
        bool carry;

        public Add(Operand _op1, Operand _op2, bool _carry)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
            carry = _carry;
        }

        public override string ToString()
        {
            return (carry ? "ADC" : "ADD");
        }
    }

    public class Subtract : Instruction
    {
        bool borrow;

        public Subtract(Operand _op1, Operand _op2, bool _borrow)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
            borrow = _borrow;
        }

        public override string ToString()
        {
            return (borrow ? "SBB" : "SUB");
        }
    }

    public class Multiply : Instruction
    {
        public Multiply(Operand _op1, Operand _op2)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
        }
    }

    public class Divide : Instruction
    {
        public Divide(Operand _op1, Operand _op2)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
        }
    }

    public class Negate : Instruction
    {
        public Negate(Operand _op1, Operand _op2)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
        }
    }

    public class Increment : Instruction
    {
        public Increment(Operand _op1)
            : base()
        {
            op1 = _op1;            
        }

        public override string ToString()
        {
            return "INC";
        }
    }

    public class Decrement : Instruction
    {
        public Decrement(Operand _op1)
            : base()
        {
            op1 = _op1;
        }

        public override string ToString()
        {
            return "DEC";
        }
    }

//- boolean ----------------------------------------------------------------

    public class Not : Instruction
    {
        public Not(Operand _op1, Operand _op2)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
        }
    }

    public class And : Instruction
    {
        public And(Operand _op1, Operand _op2)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
        }
        public override string ToString()
        {
            return "AND";
        }
    }

    public class Or : Instruction
    {
        public Or(Operand _op1, Operand _op2)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
        }
        public override string ToString()
        {
            return "OR";
        }
    }

    public class Xor : Instruction
    {
        public Xor(Operand _op1, Operand _op2)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
        }
        public override string ToString()
        {
            return "XOR";
        }
    }

//- bit operations ----------------------------------------------------------------

    public class Rotate : Instruction
    {
    }

    public class Shift : Instruction
    {
    }

    public class ConvertSize : Instruction
    {
    }

    public class AsciiAdjust : Instruction
    {
        public enum MODE {AAA, AAS}

        MODE mode;

        public AsciiAdjust(MODE _mode)
            : base() 
        {
            mode = _mode;
        }
        public override string ToString()
        {
            return (mode == MODE.AAA) ? "AAA" : "AAS";
        }
    }

    public class DecimalAdjust : Instruction
    {
        public enum MODE {DAA, DAS}

        MODE mode;

        public DecimalAdjust(MODE _mode)
            : base() 
        {
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.DAA) ? "DAA" : "DAS";
        }
    }

//- stack operations ----------------------------------------------------------

    public class Push : Instruction
    {
        public Push(Operand _op1)
            : base()
        {
            op1 = _op1;
        }

        public override string ToString()
        {
            return "PUSH";
        }
    }

    public class Pop : Instruction
    {
        public Pop(Operand _op1)
            : base()
        {
            op1 = _op1;
        }

        public override string ToString()
        {
            return "POP";
        }
    }

//- comparison ----------------------------------------------------------------

    public class Compare : Instruction
    {
        public Compare(Operand _op1, Operand _op2)
            : base()
        {
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "CMP";
        }
    }

    public class Test : Instruction
    {
    }

//- branching -----------------------------------------------------------------

    public class Jump : Instruction
    {
    }

    public class JumpConditional : Instruction
    {
    }

    public class Loop : Instruction
    {
    }

    public class Call : Instruction
    {
    }

    public class Return : Instruction
    {
    }

    public class Enter : Instruction
    {
    }

    public class Leave : Instruction
    {
    }

    public class Interupt : Instruction
    {
    }

    public class IReturn : Instruction
    {
    }

//- flag operations -----------------------------------------------------------

    public class LoadFlags : Instruction
    {
    }

    public class StoreFlags : Instruction
    {
    }

    public class SetFlag : Instruction
    {
    }

    public class ClearFlag : Instruction
    {
    }

    public class ComplementCarry : Instruction
    {
    }

//- data operations -----------------------------------------------------------

    public class Exchange : Instruction
    {
    }

    public class LoadString : Instruction
    {
    }

    public class MoveString : Instruction
    {
    }

    public class StoreString : Instruction
    {
    }

    public class ScanString : Instruction
    {
    }

    public class XlateString : Instruction
    {
    }

    public class CompareString : Instruction
    {
    }

    public class Input : Instruction
    {
    }

    public class Output : Instruction
    {
    }

//- addressing ----------------------------------------------------------------

    public class LoadPtr : Instruction
    {
    }

    public class LoadEffAddress : Instruction
    {
    }

//- miscellaneous -------------------------------------------------------------

    public class Wait : Instruction
    {
    }

    public class Arpl : Instruction
    {
    }

    public class Bound : Instruction
    {
    }

    public class Lock : Instruction
    {
    }

    public class Halt : Instruction
    {
    }

    public class NoOp : Instruction
    {
    }
}
