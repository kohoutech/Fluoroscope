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

//general instructions - section 5.20 Intel Architecture Manual (2018)

namespace Origami.Asm32
{
    //- prefix instruction ---------------------------------------------------------------

    public class Wait : Instruction
    {
        public Wait()
            : base()
        {
        }

        public override string ToString()
        {
            return "WAIT";
        }
    }

    //- data transfer ---------------------------------------------------------------

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

    //ConditionalMove - CMOV#
    public class ConditionalMove : Instruction
    {
        public enum CONDIT
        {
            CMOVO, CMOVNO, CMOVB, CMOVAE, CMOVE, CMOVNE, CMOVBE, CMOVA,
            CMOVS, CMOVNS, CMOVP, CMOVNP, CMOVL, CMOVGE, CMOVLE, CMOVG
        };

        public CONDIT condit;

        public ConditionalMove(Operand _op1, Operand _op2, CONDIT _condit)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            condit = _condit;
        }

        String[] condits = { "CMOVO", "CMOVNO", "CMOVB", "CMOVAE", "CMOVE", "CMOVNE", "CMOVBE", "CMOVA", 
                           "CMOVS", "CMOVNS", "CMOVP", "CMOVNP", "CMOVL", "CMOVGE", "CMOVLE", "CMOVG" };

        public override string ToString()
        {
            return condits[(int)condit];
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

    //ByteSwap - BSWAP
    public class ByteSwap : Instruction
    {
        public ByteSwap(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "BSWAP";
        }
    }

    //ExchangeAdd - XADD
    public class ExchangeAdd : Instruction
    {
        public ExchangeAdd(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "XADD";
        }
    }

    //CompareExchange - CMPXCHG/CMPXCHG8B
    public class CompareExchange : Instruction
    {
        bool wide;

        public CompareExchange(Operand _op1, Operand _op2, bool _wide)
            : base()
        {
            opcount = (_op2 != null) ? 2 : 1;
            op1 = _op1;
            op2 = _op2;
            wide = _wide;
        }

        public override string ToString()
        {
            return wide ? "CMPXCHG8B" : "CMPXCHG";
        }
    }

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

    public class PushRegisters : Instruction
    {
        public PushRegisters()
            : base()
        {
        }

        public override string ToString()
        {
            return "PUSHAD";
        }
    }

    public class PopRegisters : Instruction
    {
        public PopRegisters()
            : base()
        {
        }

        public override string ToString()
        {
            return "POPAD";
        }
    }

    public class ConvertSize : Instruction
    {
        public enum MODE { CWDE, CDQ }

        MODE mode;

        public ConvertSize(MODE _mode)
            : base()
        {
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.CWDE) ? "CWDE" : "CDQ";
        }
    }

    //MoveExtend - MOVSX/MOVZX
    public class MoveExtend : Instruction
    {
        public enum MODE { SIGN, ZERO }
        MODE mode;

        public MoveExtend(Operand _op1, Operand _op2, MODE _mode)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.SIGN) ? "MOVSX" : "MOVZX";
        }
    }

    //- arithmetic ----------------------------------------------------------------

    //Add - ADC/ADD
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

    //Subtract - SBB/SUB
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

    //IntMultiply - IMUL
    public class IntMultiply : Instruction
    {
        public IntMultiply(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public IntMultiply(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

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

    //Multiply - MUL
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

    //IntDivide - IDIV
    public class IntDivide : Instruction
    {
        public IntDivide(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public IntDivide(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "IDIV";
        }
    }

    //Divide - DIV
    public class Divide : Instruction
    {
        public Divide(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "DIV";
        }
    }

    //Increment - INC
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

    //Decrement - DEC
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

    //Negate - NEG
    public class Negate : Instruction
    {
        public Negate(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "NEG";
        }
    }

    //Compare - CMP
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

    //- decimal ---------------------------------------------------------------

    //AsciiAdjust - AAA/AAS/AAM/AAD
    public class AsciiAdjust : Instruction
    {
        public enum MODE { Add, Sub, Mult, Div }

        MODE mode;

        //for mode = add / subtract
        public AsciiAdjust(MODE _mode)
            : base()
        {
            mode = _mode;
        }

        //for mode = multiply / divide, op1 gives the base
        public AsciiAdjust(MODE _mode, Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            mode = _mode;
        }

        String[] modes = { "AAA", "AAS", "AAM", "AAD" };

        public override string ToString()
        {
            String result = modes[(int)mode];
            if ((mode == MODE.Mult || mode == MODE.Div) && (op1 != null))
            {
                result = result + "B";
            }
            return result;
        }
    }

    //DecimalAdjust - DAA/DAS
    public class DecimalAdjust : Instruction
    {
        public enum MODE { Add, Sub }

        MODE mode;

        public DecimalAdjust(MODE _mode)
            : base()
        {
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.Add) ? "DAA" : "DAS";
        }
    }

    //- logical ----------------------------------------------------------------

    //And - AND
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


    //Or - OR
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

    //Xor - XOR
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

    //Not - NOT
    public class Not : Instruction
    {
        public Not(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "NOT";
        }
    }

    //- shift/rotate ----------------------------------------------------------

    //Shift - SAL/SAR/SHL/SHR
    public class Shift : Instruction
    {
        public enum MODE { LEFT, RIGHT }

        MODE mode;
        bool arthimetic;

        public Shift(Operand _op1, Operand _op2, MODE _mode, bool _arthimetic)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            mode = _mode;
            arthimetic = _arthimetic;
        }

        public override string ToString()
        {
            return (arthimetic) ? ((mode == MODE.LEFT) ? "SAL" : "SAR") : ((mode == MODE.LEFT) ? "SHL" : "SHR");
        }
    }

    //DoublePrecShift - SHLD/SHRD
    public class DoublePrecShift : Instruction
    {

        public enum MODE { LEFT, RIGHT }
        MODE mode;

        public DoublePrecShift(Operand _op1, Operand _op2, Operand _op3, MODE _mode)
            : base()
        {
            opcount = 3;
            op1 = _op1;
            op2 = _op2;
            op3 = _op3;
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.LEFT) ? "SHLD" : "SHRD";
        }
    }

    //Rotate - RCL/RCR/ROL/ROR
    public class Rotate : Instruction
    {
        public enum MODE { LEFT, RIGHT }

        MODE mode;
        bool withCarry;

        public Rotate(Operand _op1, Operand _op2, MODE _mode, bool _withCarry)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            mode = _mode;
            withCarry = _withCarry;
        }

        public override string ToString()
        {
            return (withCarry) ? ((mode == MODE.LEFT) ? "RCL" : "RCR") : ((mode == MODE.LEFT) ? "ROL" : "ROR");
        }
    }

    //- bit operations --------------------------------------------------------

    //BitTest - BT/BTC/BTR/BTS
    public class BitTest : Instruction
    {
        public enum MODE { BT, BTS, BTR, BTC }
        MODE mode;

        public BitTest(Operand _op1, Operand _op2, MODE _mode)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.BT) ? "BT" : (mode == MODE.BTC) ? "BTC" : (mode == MODE.BTR) ? "BTR" : "BTS";
        }
    }

    //BitScan - BSF/BSR
    public class BitScan : Instruction
    {
        public enum MODE { BSF, BSR }
        MODE mode;

        public BitScan(Operand _op1, Operand _op2, MODE _mode)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.BSF) ? "BSF" : "BSR";
        }
    }

    //SetByte - SET#
    public class SetByte : Instruction
    {
        public enum CONDIT
        {
            SETO, SETNO, SETB, SETAE, SETE, SETNE, SETBE, SETA,
            SETS, SETNS, SETP, SETNP, SETL, SETGE, SETLE, SETG
        };

        public CONDIT condit;

        public SetByte(Operand _op1, CONDIT _condit)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            condit = _condit;
        }

        String[] condits = { "SETO", "SETNO", "SETB", "SETAE", "SETE", "SETNE", "SETBE", "SETA", 
                           "SETS", "SETNS", "SETP", "SETNP", "SETL", "SETGE", "SETLE", "SETG" };

        public override string ToString()
        {
            return condits[(int)condit];
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
        public Jump(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "JMP";
        }
    }

    public class JumpConditional : Instruction
    {
        public enum CONDIT { JO, JNO, JB, JAE, JE, JNE, JBE, JA, JS, JNS, JP, JNP, JL, JGE, JLE, JG };

        public CONDIT condit;

        public JumpConditional(Operand _op1, CONDIT _condit)
            : base()
        {
            condit = _condit;
            op1 = _op1;
            opcount = 1;
        }

        String[] condits = { "JO", "JNO", "JB", "JAE", "JE", "JNE", "JBE", "JA", 
                           "JS", "JNS", "JP", "JNP", "JL", "JGE", "JLE", "JG" };

        public override string ToString()
        {
            return condits[(int)condit];
        }
    }

    public class Loop : Instruction
    {
        public enum MODE { LOOP, LOOPE, LOOPNE, JECXZ }

        MODE mode;

        public Loop(Operand _op1, MODE _mode)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            mode = _mode;
        }

        String[] modes = { "LOOP", "LOOPE", "LOOPNE", "JECXZ" };

        public override string ToString()
        {
            return modes[(int)mode];
        }
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
        bool far;

        public Return(bool _far)
            : base()
        {
            far = _far;
        }

        public Return(Operand _op1, bool _far)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            far = _far;
        }

        public override string ToString()
        {
            return (far) ? "RETF" : "RET";
        }
    }

    public class IReturn : Instruction
    {
        public IReturn()
            : base()
        {
        }

        public override string ToString()
        {
            return "IRETD";
        }
    }

    public class Interrupt : Instruction
    {
        public Interrupt(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "INT";
        }
    }

    public class InterruptDebug : Instruction
    {
        public InterruptDebug()
            : base()
        {
            opcount = 1;
            op1 = new Immediate(3, Operand.OPSIZE.Byte);
        }

        public override string ToString()
        {
            return "INT";
        }
    }

    public class InterruptOverflow : Instruction
    {
        public InterruptOverflow()
            : base()
        {
        }

        public override string ToString()
        {
            return "INTO";
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

    public class Enter : Instruction
    {
        public Enter(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "ENTER";
        }
    }

    public class Leave : Instruction
    {
        public Leave()
            : base()
        {
        }

        public override string ToString()
        {
            return "LEAVE";
        }
    }

    //- string operations -----------------------------------------------------------

    public class MoveString : Instruction
    {
        LOOPPREFIX prefix;

        public MoveString(Operand _op1, Operand _op2, LOOPPREFIX _prefix)
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
            return prefixStr + "MOVS";
        }
    }

    public class CompareString : Instruction
    {
        LOOPPREFIX prefix;

        public CompareString(Operand _op1, Operand _op2, LOOPPREFIX _prefix)
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
            return prefixStr + "CMPS";
        }
    }

    public class ScanString : Instruction
    {
        LOOPPREFIX prefix;

        public ScanString(Operand _op1, LOOPPREFIX _prefix)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            prefix = _prefix;
        }

        public override string ToString()
        {
            String prefixStr = (prefix == LOOPPREFIX.REP) ? "REPE " : ((prefix == LOOPPREFIX.REPNE) ? "REPNE " : "");
            return prefixStr + "SCAS";
        }
    }

    public class LoadString : Instruction
    {
        LOOPPREFIX prefix;

        public LoadString(Operand _op1, LOOPPREFIX _prefix)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            prefix = _prefix;
        }

        public override string ToString()
        {
            String prefixStr = (prefix == LOOPPREFIX.REP) ? "REP " : ((prefix == LOOPPREFIX.REPNE) ? "REPNE " : "");
            return prefixStr + "LODS";
        }
    }

    public class StoreString : Instruction
    {
        LOOPPREFIX prefix;

        public StoreString(Operand _op1, LOOPPREFIX _prefix)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            prefix = _prefix;
        }

        public override string ToString()
        {
            String prefixStr = (prefix == LOOPPREFIX.REP) ? "REP " : ((prefix == LOOPPREFIX.REPNE) ? "REPNE " : "");
            return prefixStr + "STOS";
        }
    }

    //- i/o operations --------------------------------------------------------

    public class Input : Instruction
    {
        public Input(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "IN";
        }
    }

    public class Output : Instruction
    {
        public Output(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "OUT";
        }
    }

    public class InputString : Instruction
    {
        LOOPPREFIX prefix;

        public InputString(Operand _op1, Operand _op2, LOOPPREFIX _prefix)
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

    public class OutputString : Instruction
    {
        LOOPPREFIX prefix;

        public OutputString(Operand _op1, Operand _op2, LOOPPREFIX _prefix)
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

    //- flag operations -----------------------------------------------------------

    public class SetFlag : Instruction
    {
        public enum FLAG { Carry, Int, Dir }

        FLAG flag;

        public SetFlag(FLAG _flag)
            : base()
        {
            flag = _flag;
        }

        String[] flags = { "STC", "STI", "STD" };

        public override string ToString()
        {
            return flags[(int)flag];
        }
    }

    public class ClearFlag : Instruction
    {
        public enum FLAG { Carry, Int, Dir }

        FLAG flag;

        public ClearFlag(FLAG _flag)
            : base()
        {
            flag = _flag;
        }

        String[] flags = { "CLC", "CLI", "CLD" };

        public override string ToString()
        {
            return flags[(int)flag];
        }
    }

    public class ComplementCarry : Instruction
    {
        public ComplementCarry()
            : base()
        {
        }

        public override string ToString()
        {
            return "CMC";
        }
    }

    public class LoadFlags : Instruction
    {
        public LoadFlags()
            : base()
        {
        }

        public override string ToString()
        {
            return "LAHF";
        }
    }

    public class StoreFlags : Instruction
    {
        public StoreFlags()
            : base()
        {
        }

        public override string ToString()
        {
            return "SAHF";
        }
    }

    public class PushFlags : Instruction
    {
        public PushFlags()
            : base()
        {
        }

        public override string ToString()
        {
            return "PUSHFD";
        }
    }

    public class PopFlags : Instruction
    {
        public PopFlags()
            : base()
        {
        }

        public override string ToString()
        {
            return "POPFD";
        }
    }

    //- segment operations -----------------------------------------------------

    public class LoadPtr : Instruction
    {
        public enum MODE { LDS, LES }

        MODE mode;

        public LoadPtr(Operand _op1, Operand _op2, MODE _mode)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.LDS) ? "LDS" : "LES";
        }
    }

    //LoadFarPointer - LFS/LGS/LSS
    public class LoadFarPointer : Instruction
    {
        public enum SEG { SS, FS, GS }
        SEG seg;

        public LoadFarPointer(Operand _op1, Operand _op2, SEG _seg)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
            seg = _seg;
        }

        public override string ToString()
        {
            return (seg == SEG.FS) ? "LFS" : (seg == SEG.GS) ? "LGS" : "LSS";
        }
    }

    //- miscellaneous -------------------------------------------------------------

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

    public class NoOp : Instruction
    {
        public NoOp()
            : base()
        {
        }

        public override string ToString()
        {
            return "NOP";
        }
    }

    public class XlateString : Instruction
    {
        LOOPPREFIX prefix;

        public XlateString(Operand _op1, LOOPPREFIX _prefix)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            prefix = _prefix;
        }

        public override string ToString()
        {
            String prefixStr = (prefix == LOOPPREFIX.REP) ? "REP " : ((prefix == LOOPPREFIX.REPNE) ? "REPNE " : "");
            return prefixStr + "XLAT";
        }
    }

    //CpuId - CPUID
    public class CpuId : Instruction
    {
        public CpuId()
            : base()
        {
        }

        public override string ToString()
        {
            return "CPUID";
        }
    }

    //CacheFlush - CLFLUSH/CLFLUSHOPT
    public class CacheFlush : Instruction
    {
        bool optimized;

        public CacheFlush(Operand _op1, bool _optimized)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            optimized = _optimized;
        }

        public override string ToString()
        {
            return optimized ? "CLFLUSHOPT" : "CLFLUSH";
        }
    }

    //UndefinedOp - UD0/UD1/UD2
    public class UndefinedOp : Instruction
    {
        int level;

        public UndefinedOp(int _level)
            : base()
        {
            level = _level;
        }

        public override string ToString()
        {
            return ("UD" + level.ToString());
        }
    }

    //for byte sequences that don't match any known instruction
    public class UnknownOp : Instruction
    {
        public UnknownOp()
            : base()
        {
        }

        public override string ToString()
        {
            return "???";
        }
    }
}
