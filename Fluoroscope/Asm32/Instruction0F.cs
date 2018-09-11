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

//- bit operations ------------------------------------------------------------

    //BitScan - BSF/BSR
    public class BitScan : Instruction
    {
        bool intop;
        bool pop;

        public BitScan(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //BitTest - BT/BTC/BTR/BTS
    public class BitTest : Instruction
    {
        bool intop;
        bool pop;

        public BitTest(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //ByteSwap - BSWAP
    public class ByteSwap : Instruction
    {
        bool intop;
        bool pop;

        public ByteSwap(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //SetByte - SET#
    public class SetByte : Instruction
    {
        bool intop;
        bool pop;

        public SetByte(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //DoublePrecShift - SHLD/SHRD
    public class DoublePrecShift : Instruction
    {
        bool intop;
        bool pop;

        public DoublePrecShift(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- data moving ---------------------------------------------------------------

    //ConditionalMove - CMOV#
    public class ConditionalMove : Instruction
    {
        bool intop;
        bool pop;

        public ConditionalMove(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //MoveExtend - MOVSX/MOVZX
    public class MoveExtend : Instruction
    {
        bool intop;
        bool pop;

        public MoveExtend(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //CompareExchange - CMPXCHG/CMPXCHG8B
    public class CompareExchange : Instruction
    {
        bool intop;
        bool pop;

        public CompareExchange(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //ExchangeAdd - XADD
    public class ExchangeAdd : Instruction
    {
        bool intop;
        bool pop;

        public ExchangeAdd(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- loading/storing status ----------------------------------------------------

    //LoadFarPointer - LFS/LGS/LSS
    public class LoadFarPointer : Instruction
    {
        bool intop;
        bool pop;

        public LoadFarPointer(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //LoadDescriptor - LLDT/LGDT/LIDT
    public class LoadDescriptor : Instruction
    {
        public enum MODE { LLDT, LGDT, LIDT }

        MODE mode;

        public LoadDescriptor(Operand _op1, MODE _mode)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            mode = _mode;
        }

        public override string ToString()
        {
            return ((mode == MODE.LLDT) ? "LLDT" : (mode == MODE.LGDT) ? "LGDT" : "LIDT");
        }
    }

    //StoreDescriptor - SLDT/SGDT/SIDT
    public class StoreDescriptor : Instruction
    {
        public enum MODE { SLDT, SGDT, SIDT }

        MODE mode;

        public StoreDescriptor(Operand _op1, MODE _mode)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            mode = _mode;
        }

        public override string ToString()
        {
            return ((mode == MODE.SLDT) ? "SLDT" : (mode == MODE.SGDT) ? "SGDT" : "SIDT");
        }
    }

    //LoadAccessRights - LAR
    public class LoadAccessRights : Instruction
    {
        public LoadAccessRights(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "LAR";
        }
    }

    //LoadSegementLimit - LSL
    public class LoadSegementLimit : Instruction
    {
        public LoadSegementLimit(Operand _op1, Operand _op2)
            : base()
        {
            opcount = 2;
            op1 = _op1;
            op2 = _op2;
        }

        public override string ToString()
        {
            return "LSL";
        }
    }

    //StoreMMXState - FXSAVE
    public class StoreMMXState : Instruction
    {
        bool intop;
        bool pop;

        public StoreMMXState(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //RestoreMMXState - FXRSTOR
    public class RestoreMMXState : Instruction
    {
        bool intop;
        bool pop;

        public RestoreMMXState(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //LoadSMachinetatusWord - LMSW
    public class LoadSMachinetatusWord : Instruction
    {
        public LoadSMachinetatusWord(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "LMSW";
        }
    }

    //StoreMachineStatusWord - SMSW
    public class StoreMachineStatusWord : Instruction
    {
        public StoreMachineStatusWord(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "SMSW";
        }
    }

    //LoadTaskRegister - LTR
    public class LoadTaskRegister : Instruction
    {
        public LoadTaskRegister(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "LTR";
        }
    }

    //StoreaskRegister - STR
    public class StoreTaskRegister : Instruction
    {
        public StoreTaskRegister(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            
        }

        public override string ToString()
        {
            return "STR";
        }
    }

    //ReadModelSpecReg - RDMSR
    public class ReadModelSpecReg : Instruction
    {
        bool intop;
        bool pop;

        public ReadModelSpecReg(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //WriteModelSpecReg - WRMSR
    public class WriteModelSpecReg : Instruction
    {
        bool intop;
        bool pop;

        public WriteModelSpecReg(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //ReadCounters - RDPMC/RDTSC
    public class ReadCounters : Instruction
    {
        bool intop;
        bool pop;

        public ReadCounters(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

//- system management ---------------------------------------------------------

    //ResumeFromSysMgt - RSM
    public class ResumeFromSysMgt : Instruction
    {
        bool intop;
        bool pop;

        public ResumeFromSysMgt(Operand _op1, Operand _op2, bool _intop, bool _pop)
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

    //SystemCall - SYSCALL/SYSENTER
    public class SystemCall : Instruction
    {
        public enum MODE { SYSCALL, SYSENTER }

        MODE mode;

        public SystemCall(MODE _mode)
            : base()
        {
            mode = _mode;
        }

        public override string ToString()
        {
            return ((mode == MODE.SYSCALL ? "SYSCALL" : "SYSENTER"));
        }
    }

    //SystemRet - SYSRET/SYSEXIT
    public class SystemRet : Instruction
    {
        public enum MODE { SYSRET, SYSEXIT }

        MODE mode;

        public SystemRet(MODE _mode)
            : base()
        {
            mode = _mode;
        }

        public override string ToString()
        {
            return ((mode == MODE.SYSRET ? "SYSRET" : "SYSEXIT"));
        }
    }

    //ClearTaskFlag - CLTS
    public class ClearTaskFlag : Instruction
    {
        public ClearTaskFlag()
            : base()
        {
        }

        public override string ToString()
        {
            return "CLTS";
        }
    }

    //InvalidateCache - INVD/WBINVD
    public class InvalidateCache : Instruction
    {
        public enum MODE { INVD, WBINVD }

        MODE mode;

        public InvalidateCache(MODE _mode)
            : base()
        {
            mode = _mode;
        }

        public override string ToString()
        {
            return ((mode == MODE.INVD ? "INVD" : "WBINVD"));
        }
    }

    //InvalidateTLB - INVLPG
    public class InvalidateTLB : Instruction
    {
        bool intop;
        bool pop;

        public InvalidateTLB(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "INVLPG";
        }
    }

    //VerifySegment - VERR/VERW
    public class VerifySegment : Instruction
    {
        public enum MODE { VERR, VERW }

        MODE mode;

        public VerifySegment(Operand _op1, MODE _mode)
            : base()
        {
            opcount = 1;
            op1 = _op1;
            mode = _mode;
        }

        public override string ToString()
        {
            return ((mode == MODE.VERR ? "VERR" : "VERW"));
        }
    }

    //CpuId - CPUID
    public class CpuId : Instruction
    {
        bool intop;
        bool pop;

        public CpuId(Operand _op1, Operand _op2, bool _intop, bool _pop)
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
}
