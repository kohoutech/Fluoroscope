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
        public enum LOCKPREFIX { LOCK, None };
        public enum LOOPPREFIX { REP, REPNE, None };

        public Operand op1;
        public Operand op2;
        public Operand op3;

        public int opcount;
        public List<int> bytes;

        public Instruction () 
        {
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
            opcount = 2;
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
            opcount = 2;
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
            opcount = 2;
            op1 = _op1;
            op2 = _op2;        
        }

        public override string ToString()
        {
            return "MUL";
        }
    }

    public class IntMultiply : Instruction
    {
        public IntMultiply(Operand _op1, Operand _op2, Operand _op3)
            : base()
        {
            opcount = 3;
            op1 = _op1;
            op2 = _op2;
            op3 = _op3;
        }

        public override string ToString()
        {
            return "IMUL";
        }
    }

    public class Divide : Instruction
    {
        public Divide(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
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
            opcount = 1;
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
            opcount = 1;
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
            opcount = 2;
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
            opcount = 2;
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
            opcount = 2;
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
        public enum MODE { CWDE, CDQ }

        MODE mode;

        public ConvertSize(MODE _mode)
            : base() 
        {
            opcount = 0;
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.CWDE) ? "CWDE" : "CDQ";
        }

    }

    public class AsciiAdjust : Instruction
    {
        public enum MODE {AAA, AAS}

        MODE mode;

        public AsciiAdjust(MODE _mode)
            : base() 
        {
            opcount = 0;
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
            opcount = 0;
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
            opcount = 1;
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
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "POP";
        }
    }

    public class Pushad : Instruction
    {
        public Pushad()
            : base()
        {
            opcount = 0;
            
        }

        public override string ToString()
        {
            return "PUSHAD";
        }
    }

    public class Popad : Instruction
    {
        public Popad()
            : base()
        {
            opcount = 0;

        }

        public override string ToString()
        {
            return "POPAD";
        }
    }

    public class Pushfd : Instruction
    {
        public Pushfd()
            : base()
        {
            opcount = 0;

        }

        public override string ToString()
        {
            return "PUSHFD";
        }
    }

    public class Popfd : Instruction
    {
        public Popfd()
            : base()
        {
            opcount = 0;

        }

        public override string ToString()
        {
            return "POPFD";
        }
    }

//- comparison ----------------------------------------------------------------

    public class Compare : Instruction
    {
        public Compare(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
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
        public Test(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "TEST";
        }
    }

//- branching -----------------------------------------------------------------

    public class Jump : Instruction
    {
    }

    public class JumpConditional : Instruction
    {
        public enum CONDIT { JO, JNO, JB, JAE, JE, JNE, JBE, JA, JS, JNS, JP, JNP, JL, JGE, JLE, JG};

        public CONDIT test;

        public JumpConditional(CONDIT _test, Operand _op1)
            : base()
        {
            test = _test;
            op1 = _op1;
            opcount = 1;
        }

        String[] tests = { "JO", "JNO", "JB", "JAE", "JE", "JNE", "JBE", "JA", 
                           "JS", "JNS", "JP", "JNP", "JL", "JGE", "JLE", "JG" };

        public override string ToString()
        {
            return tests[(int)test];
        }
    }

    public class Loop : Instruction
    {
    }

    public class Call : Instruction
    {
        public Call(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;            
        }

        public override string ToString()
        {
            return "CALL";
        }
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

    public class Move : Instruction
    {
        public Move(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "MOV";
        }
    }

    public class Exchange : Instruction
    {
        public Exchange(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "XCHG";
        }
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
        LOOPPREFIX prefix;

        public Input(Operand _op1, Operand _op2, LOOPPREFIX _prefix)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            prefix = _prefix;
        }

        public override string ToString()
        {
            String prefixStr = (prefix == LOOPPREFIX.REP) ? "REP " : ((prefix == LOOPPREFIX.REPNE) ? "REPNE " : "");
            return prefixStr + "INS";
        }
    }

    public class Output : Instruction
    {
        LOOPPREFIX prefix;

        public Output(Operand _op1, Operand _op2, LOOPPREFIX _prefix)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            prefix = _prefix;
        }

        public override string ToString()
        {
            String prefixStr = (prefix == LOOPPREFIX.REP) ? "REP " : ((prefix == LOOPPREFIX.REPNE) ? "REPNE " : "");
            return prefixStr + "OUTS";
        }
    }

//- addressing ----------------------------------------------------------------

    public class LoadPtr : Instruction
    {
    }

    public class LoadEffAddress : Instruction
    {
        public LoadEffAddress(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "LEA";
        }
    }

//- miscellaneous -------------------------------------------------------------

    public class Wait : Instruction
    {
    }

    public class Arpl : Instruction
    {
        public Arpl(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "ARPL";
        }
    }

    public class Bound : Instruction
    {
        public Bound(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "BOUND";
        }
    }

    public class Halt : Instruction
    {
    }

    public class NoOp : Instruction
    {
    }

    public class UnknownOp : Instruction
    {
        public UnknownOp()
            : base()
        {
            opcount = 0;            
        }

        public override string ToString()
        {
            return "???";
        }
    }

}
