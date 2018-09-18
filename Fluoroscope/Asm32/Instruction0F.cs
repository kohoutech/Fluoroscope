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

    //SetByte - SET#
    public class SetByte : Instruction
    {
        public enum CONDIT { SETO, SETNO, SETB, SETAE, SETE, SETNE, SETBE, SETA, 
            SETS, SETNS, SETP, SETNP, SETL, SETGE, SETLE, SETG};

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

//- data moving ---------------------------------------------------------------

    //ConditionalMove - CMOV#
    public class ConditionalMove : Instruction
    {
        public enum CONDIT { CMOVO, CMOVNO, CMOVB, CMOVAE, CMOVE, CMOVNE, CMOVBE, CMOVA, 
                             CMOVS, CMOVNS, CMOVP, CMOVNP, CMOVL, CMOVGE, CMOVLE, CMOVG };

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

//- loading/storing status ----------------------------------------------------

    //LoadFarPointer - LFS/LGS/LSS
    public class LoadFarPointer : Instruction
    {
        public enum SEG {SS, FS, GS}
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
        public StoreMMXState(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "FXSAVE";
        }
    }

    //RestoreMMXState - FXRSTOR
    public class RestoreMMXState : Instruction
    {
        public RestoreMMXState(Operand _op1)
            : base()
        {
            opcount = 1;
            op1 = _op1;
        }

        public override string ToString()
        {
            return "FXRSTOR";
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

        public ReadModelSpecReg()
            : base()
        {
            opcount = 0;
        }

        public override string ToString()
        {
            return "RDMSR";
        }
    }

    //WriteModelSpecReg - WRMSR
    public class WriteModelSpecReg : Instruction
    {

        public WriteModelSpecReg()
            : base()
        {
            opcount = 0;
        }

        public override string ToString()
        {
            return "WRMSR";
        }
    }

    //ReadCounters - RDPMC/RDTSC
    public class ReadCounters : Instruction
    {
        public enum MODE { PERFORMANCE, TIMESTAMP };

        MODE mode;

        public ReadCounters(MODE _mode)
            : base()
        {
            opcount = 0;
            mode = _mode;
        }

        public override string ToString()
        {
            return (mode == MODE.PERFORMANCE) ? "RDPMC" : "RDTSC";
        }
    }

//- system management ---------------------------------------------------------

    //ResumeFromSysMgt - RSM
    public class ResumeFromSysMgt : Instruction
    {
        public ResumeFromSysMgt()
            : base()
        {
        }

        public override string ToString()
        {
            return "RSM";
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
}
