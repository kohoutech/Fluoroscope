﻿/* ----------------------------------------------------------------------------
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

using Origami.Asm32;

//http://ref.x86asm.net/coder32.html - opcode table
//http://wiki.osdev.org/X86-64_Instruction_Encoding - prefix information

namespace Origami.Asm32
{
    class i32Disasm
    {
        public readonly int MAXINSTRLEN = 20;

        public byte[] srcBuf;           //the bytes being disassembled
        public uint srcpos;             //cur pos in source buf
        public uint codeaddr;           //cur addr of instr in mem
        public List<int> instrBytes;    //the bytes that have been decoded for this instruction        
        
        public String opcode;
        public int opcount;
        public Operand op1;
        public Operand op2;
        public Operand op3;
        
        public Segment.SEG segprefix;
        public Instruction.LOOPPREFIX loopprefix;
        public Instruction.LOCKPREFIX lockprefix;
        public bool operandSizeOverride;
        public bool addressSizeOverride;
        
        private bool useModrm32;

        public i32Disasm(byte[] _source, uint _srcpos)
        {
            srcBuf = _source;           //set source buf + pos in buf when we start disassembling
            srcpos = _srcpos;           //source pos can be changed later if we need to skip over some bytes

            instrBytes = new List<int>();            

            codeaddr = 0;
            opcount = 0;
            opcode = "";

            segprefix = Segment.SEG.DS;
            loopprefix = Instruction.LOOPPREFIX.None;
            lockprefix = Instruction.LOCKPREFIX.None;
            operandSizeOverride = false;
            addressSizeOverride = false;
            useModrm32 = false;            
        }

        public Instruction getInstr(uint _codepos)
        {
            Instruction instr = null;
            instrBytes = new List<int>();

            codeaddr = _codepos;
            opcount = 0;
            segprefix = Segment.SEG.DS;
            loopprefix = Instruction.LOOPPREFIX.None;
            lockprefix = Instruction.LOCKPREFIX.None;
            operandSizeOverride = false;
            addressSizeOverride = false;
            useModrm32 = false;

            uint b = getNextByte();

            //prefix byte
            while ((b == 0x26) || (b == 0x2e) || (b == 0x36) || (b == 0x3e) ||
                   (b == 0x64) || (b == 0x65) || (b == 0x66) || (b == 0x67) ||
                   (b == 0xf0) || (b == 0xf2) || (b == 0xf3)
                )
            {
                setPrefix(b);
                b = getNextByte();
            }

            if (b == 0x0f)
            {
                uint nb = getNextByte();
                //op0f(nb);
            }
            else if ((b >= 0x00) && (b <= 0x3f))
            {
                instr = op03x(b);
            }
            else if ((b >= 0x40) && (b <= 0x5f))
            {
                instr = op45x(b);
            }
            else if ((b >= 0x60) && (b <= 0x6f))
            {
                instr = op6x(b);
            }
            else if ((b >= 0x70) && (b <= 0x7f))
            {
                instr = op7x(b);
            }
            else if ((b >= 0x80) && (b <= 0x8f))
            {
                instr = op8x(b);
            }
            else if ((b >= 0x90) && (b <= 0x9f))
            {
                instr = op9x(b);
            }
            else if ((b >= 0xa0) && (b <= 0xaf))
            {
                instr = opax(b);
            }
            else if ((b >= 0xb0) && (b <= 0xbf))
            {
                instr = opbx(b);
            }
            else if ((b >= 0xc0) && (b <= 0xcf))
            {
                //opcx(b);
            }
            else if ((b >= 0xd0) && (b <= 0xd7))
            {
                //opd7(b);
            }
            else if ((b >= 0xd8) && (b <= 0xdf))
            {
                //op8087(b);
            }
            else if ((b >= 0xe0) && (b <= 0xef))
            {
                //opex(b);
            }
            else if ((b >= 0xf0) && (b <= 0xff))
            {
                //opfx(b);
            }
            if ("lock ".Equals(lockprefix)) opcode = lockprefix + opcode;

            if (instr == null)
            {
                instr = new UnknownOp();
            }

            instr.bytes = instrBytes;

            return instr;
        }

//- source bytes --------------------------------------------------------------

        public uint getNextByte()
        {
            uint b = (uint)srcBuf[srcpos++];
            instrBytes.Add((int)b);
            codeaddr++;
            return b;
        }

//- prefixing -----------------------------------------------------------------

        //3 prefix groups, successive bytes in the same group will overwrite prev prefix byte
        public void setPrefix(uint b)
        {
            if (b == 0x26) segprefix = Segment.SEG.ES;    //group1
            if (b == 0x2e) segprefix = Segment.SEG.CS;
            if (b == 0x36) segprefix = Segment.SEG.SS;
            if (b == 0x3e) segprefix = Segment.SEG.DS;
            if (b == 0x64) segprefix = Segment.SEG.FS;
            if (b == 0x65) segprefix = Segment.SEG.GS;
                
            if (b == 0xf0) lockprefix = Instruction.LOCKPREFIX.LOCK;             //group2

            if (b == 0xf2) loopprefix = Instruction.LOOPPREFIX.REPNE;            //group3
            if (b == 0xf3) loopprefix = Instruction.LOOPPREFIX.REP;
            
            if (b == 0x66) operandSizeOverride = true;
            if (b == 0x67) addressSizeOverride = true;            
        }

//- addressing ----------------------------------------------------------------

        readonly int[] sibscale = { 1, 2, 4, 8 };

        public Memory getSib(uint mode, uint rm, Operand.OPSIZE size)
        {
            Memory result = null;
            Immediate imm = null;
            switch (rm)
            {
                case 0x00:
                case 0x01:
                case 0x02:
                case 0x03:
                case 0x06:
                case 0x07:
                    result = new Memory(getReg(Operand.OPSIZE.DWord, rm), null, 1, null, size, segprefix);
                    break;
                case 0x04:
                    uint sib = getNextByte();               
                    uint scale = (sib / 0x40) % 0x04;       //xx------
                    uint siba = (sib % 0x40) / 0x08;        //--xxx---
                    uint sibb = (sib % 0x08);               //-----xxx
                    if (siba != 0x04)                       //--100---
                    {
                        if ((sibb == 0x05) && (mode == 00))   //-----101
                        {
                            imm = new Immediate(addr32(), Operand.OPSIZE.DWord);
                            result = new Memory(null, getReg(Operand.OPSIZE.DWord, siba), sibscale[scale], imm, size, segprefix);
                        }
                        else
                        {
                            result = new Memory(getReg(Operand.OPSIZE.DWord, sibb), getReg(Operand.OPSIZE.DWord, siba),
                                sibscale[scale], null, size, segprefix);
                        }
                    }
                    else
                    {
                        if ((sibb == 0x05) && (mode == 00))  //-----101
                        {
                            imm = new Immediate(addr32(), Operand.OPSIZE.DWord);
                            result = new Memory(null, null, 1, imm, size, segprefix);
                        }
                        else
                        {
                            result = new Memory(getReg(Operand.OPSIZE.DWord, sibb), null, 1, null, size, segprefix);
                        }
                    }
                    break;
                case 0x05:
                    if (mode == 0x00)
                    {
                        imm = new Immediate(addr32(), Operand.OPSIZE.DWord);
                        result = new Memory(null, null, 1, imm, size, segprefix);
                    }
                    else
                    {
                        result = new Memory(getReg(Operand.OPSIZE.DWord, rm), null, 1, null, size, segprefix);
                    }
                    break;
            }

            if (mode == 0x01)
            {
                imm = new Immediate(getOfs(Operand.OPSIZE.Byte), Operand.OPSIZE.Byte);
                imm.isOffset = true;
                result.f3 = imm;
            }
            if (mode == 0x02)
            {
                imm = new Immediate(getOfs(Operand.OPSIZE.DWord), Operand.OPSIZE.DWord);
                imm.isOffset = true;
                result.f3 = imm;
            }

            return result;
        }

        public Operand getModrm(uint modrm, Operand.OPSIZE size)
        {
            Operand result = null;
            uint mode = (modrm / 0x40) % 0x04;
            uint rm = (modrm % 0x08);
            if (mode != 0x03)
            {
                result = getSib(mode, rm, size);
            }
            else
            {
                result = getReg(size, rm);
            }
            return result;
        }

//- opcodes -------------------------------------------------------------------

        public Instruction ArithmeticOps(uint op, Operand op1, Operand op2)
        {
            Instruction instr = null;
            switch (op)
            {
                case 0x00:
                    instr = new Add(op1, op2, false);
                    break;
                case 0x01:
                    instr = new Or(op1, op2);
                    break;
                case 0x02:
                    instr = new Add(op1, op2, true);
                    break;
                case 0x03:
                    instr = new Subtract(op1, op2, true);
                    break;
                case 0x04:
                    instr = new And(op1, op2);
                    break;
                case 0x05:
                    instr = new Subtract(op1, op2, false);
                    break;
                case 0x06:
                    instr = new Xor(op1, op2);
                    break;
                case 0x07:
                    instr = new Compare(op1, op2);
                    break;
            }
            return instr;
        }

        //0x0f starts two byte opcodes and should never get here
        //0X26, 0x2e, 0x36, 0x3e are prefix bytes and should never get here            
        public Instruction op03x(uint b)
        {
            Instruction instr = null;
            uint bhi = (b / 0x08) % 0x08;   //--bb b--- (top two bits should = 0)
            uint blo = b % 0x08;            //---- -bbb
            uint modrm = 0;

            if (blo < 6)
            {
                switch (blo)
                {
                    case 0x00:
                        modrm = getNextByte();
                        op1 = getModrm(modrm, Operand.OPSIZE.Byte);
                        op2 = getReg(Operand.OPSIZE.Byte, (modrm % 0x40) / 0x08);
                        break;

                    case 0x01:
                        modrm = getNextByte();
                        op1 = getModrm(modrm, Operand.OPSIZE.DWord);
                        op2 = getReg(Operand.OPSIZE.DWord, (modrm % 0x40) / 0x08);
                        break;

                    case 0x02:
                        modrm = getNextByte();
                        op1 = getReg(Operand.OPSIZE.Byte, (modrm % 0x40) / 0x08);
                        op2 = getModrm(modrm, Operand.OPSIZE.Byte);
                        break;

                    case 0x03:
                        modrm = getNextByte();
                        op1 = getReg(Operand.OPSIZE.DWord, (modrm % 0x40) / 0x08);
                        op2 = getModrm(modrm, Operand.OPSIZE.DWord);
                        break;

                    case 0x04:
                        op1 = getReg(Operand.OPSIZE.Byte, 0);
                        op2 = getImm(Operand.OPSIZE.Byte);
                        break;

                    case 0x05:
                        op1 = getReg(Operand.OPSIZE.DWord, 0);
                        op2 = getImm(Operand.OPSIZE.DWord);
                        break;
                }

                instr = ArithmeticOps(bhi, op1, op2);
            }

            if (blo == 0x06)        //0x06, 0x0e, 0x16, 0x1e
            {
                op1 = new Segment((Segment.SEG)bhi);
                instr = new Push(op1);            
                
            }

            if ((blo == 0x07))
            {
                if (bhi < 0x03) {                                   //0x07, 0x0f, 0x17, 0x1f
                        op1 = new Segment((Segment.SEG)bhi);
                        instr  = new Pop(op1);
                }
                else if (bhi < 0x06)
                {
                    instr = new DecimalAdjust((DecimalAdjust.MODE)(bhi - 4));       //0x27, 0x2f
                }
                else
                {
                    instr = new AsciiAdjust((AsciiAdjust.MODE)(bhi - 6));           //0x37, 0x3f
                }
            }
            return instr;
        }

        public Instruction op45x(uint b)
        {
            Instruction instr = null;
            op1 = getReg(Operand.OPSIZE.DWord, (b % 0x08));
            uint bhi = (b / 0x08) % 0x04;
            switch (bhi)
            {
                case 0x00:
                    instr = new Increment(op1);
                    break;
                case 0x01:
                    instr = new Decrement(op1);
                    break;
                case 0x02:
                    instr = new Push(op1);
                    break;
                case 0x03:
                    instr = new Pop(op1);
                    break;
            }
            return instr;            
        }

        //0X64, 0x65, 0x66, 0x67 are prefix bytes and should never get here
        readonly String[] opcode6x = { "pushad", "popad", "bound", "arpl", "---", "---", "---", "---",
                                       "push", "imul", "push", "imul", "ins", "ins", "outs", "outs"};
        readonly int[] size6x = { 0, 2, 1, 3 };

        public Instruction op6x(uint b)
        {
            Instruction instr = null;
            opcode = opcode6x[(b % 0x10)];
            opcount = 0;
            uint bhi = (b / 0x08) % 0x08;   //--bb b--- (top two bits should = 0)
            uint blo = b % 0x08;            //---- -bbb
            uint modrm = 0;
            switch (b)
            {
                case 0x60:
                    instr = new PushRegisters();
                    break;
                case 0x61:
                    instr = new PopRegisters();
                    break;

                case 0x62:
                    modrm = getNextByte();
                    uint mode = (modrm / 0x40) % 0x04;      //62 0c - 62 ff undefined
                    if (mode < 0x03)
                    {
                        op1 = getReg(Operand.OPSIZE.DWord, (modrm % 0x40) / 0x08);
                        op2 = getModrm(modrm, Operand.OPSIZE.QWord);
                        instr = new Bound(op1, op2);
                    }
                    break;

                case 0x63:
                    bhi = (b / 0x08) % 0x08;   //--bb b--- (top two bits should = 0)
                    blo = b % 0x08;            //---- -bbb
                    modrm = getNextByte();
                    op1 = getModrm(modrm, Operand.OPSIZE.Word);
                    op2 = getReg(Operand.OPSIZE.Word, (modrm % 0x40) / 0x08);
                    instr = new Arpl(op1, op2);
                    break;

                case 0x68:
                    op1 = getImm(Operand.OPSIZE.DWord);
                    instr = new Push(op1);
                    break;

                case 0x69:
                    bhi = (b / 0x08) % 0x08;   //--bb b--- (top two bits should = 0)
                    blo = b % 0x08;            //---- -bbb
                    modrm = getNextByte();
                    op1 = getReg(Operand.OPSIZE.DWord, (modrm % 0x40) / 0x08);
                    op2 = getModrm(modrm, Operand.OPSIZE.DWord);
                    op3 = getImm(Operand.OPSIZE.DWord);
                    instr = new IntMultiply(op1, op2, op3);
                    break;

                case 0x6a:
                    op1 = getImm(Operand.OPSIZE.SignedByte);
                    instr = new Push(op1);
                    break;

                case 0x6b:
                    bhi = (b / 0x08) % 0x08;   //--bb b--- (top two bits should = 0)
                    blo = b % 0x08;            //---- -bbb
                    modrm = getNextByte();
                    op1 = getReg(Operand.OPSIZE.DWord, (modrm % 0x40) / 0x08);
                    op2 = getModrm(modrm, Operand.OPSIZE.DWord);
                    op3 = getImm(Operand.OPSIZE.SignedByte);
                    instr = new IntMultiply(op1, op2, op3);
                    break;

                case 0x6c:
                case 0x6d:
                    op1 = new Memory(new Register(Register.REG32.EDI), null, 1, null,
                        (b == 0x6c) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.ES);
                    op2 = new Register(Register.REG16.DX);
                    instr = new Input(op1, op2, loopprefix);
                    break;

                case 0x6e:
                case 0x6f:
                    op1 = new Register(Register.REG16.DX);
                    op2 = new Memory(new Register(Register.REG32.ESI), null, 1, null,
                        (b == 0x6c) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.DS);
                    instr = new Output(op1, op2, loopprefix);
                    break;

            }
            return instr;
        }

        public Instruction op7x(uint b)
        {
            JumpConditional.CONDIT condit = (JumpConditional.CONDIT)(b % 0x10);           
            op1 = rel8();
            return new JumpConditional(condit, op1);            
        }

        //readonly String[] opcode84 = { "add", "or", "adc", "sbb", "and", "sub", "xor", "cmp"};
        readonly String[] opcode88 = { "test", "test", "xchg", "xchg", "mov", "mov", "mov", "mov" };

        public Instruction op8x(uint b)
        {
            Instruction instr = null;
            uint modrm = getNextByte();
            uint bhi = (modrm % 0x40) / 0x08;       //--bb b---
            switch (b)
            {
                case 0x80:
                case 0x82:                      //0x82 is the same as 0x80?
                    op1 = getModrm(modrm, Operand.OPSIZE.Byte);
                    op2 = getImm(Operand.OPSIZE.Byte);
                    instr = ArithmeticOps(bhi, op1, op2);
                    break;

                case 0x81:
                    op1 = getModrm(modrm, Operand.OPSIZE.DWord);
                    op2 = getImm(Operand.OPSIZE.DWord);
                    instr = ArithmeticOps(bhi, op1, op2);
                    break;

                case 0x83:
                    op1 = getModrm(modrm, Operand.OPSIZE.DWord);
                    op2 = getImm(Operand.OPSIZE.SignedByte);
                    instr = ArithmeticOps(bhi, op1, op2);
                    break;

                case 0x84:
                case 0x85:
                    op1 = getModrm(modrm, (b == 0x84) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord);
                    op2 = getReg((b == 0x84) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, bhi);
                    instr = new Test(op1, op2);
                    break;

                case 0x86:
                case 0x87:
                    op1 = getReg((b == 0x86) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, bhi);
                    op2 = getModrm(modrm, (b == 0x86) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord);
                    instr = new Exchange(op1, op2);
                    break;

                case 0x88:
                case 0x89:
                    op1 = getModrm(modrm, (b == 0x88) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord);
                    op2 = getReg((b == 0x88) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, bhi);
                    instr = new Move(op1, op2);
                    break;

                case 0x8a:
                case 0x8b:
                    op1 = getReg((b == 0x8a) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, bhi);
                    op2 = getModrm(modrm, (b == 0x8a) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord);
                    instr = new Move(op1, op2);
                    break;

                case 0x8c:
                    useModrm32 = true;              //kludge to fix dumpbin bug
                    op1 = getModrm(modrm, Operand.OPSIZE.Word);
                    op2 = new Segment((Segment.SEG)bhi);
                    instr = new Move(op1, op2);
                    break;

                case 0x8e:
                    op1 = new Segment((Segment.SEG)bhi);
                    op2 = getModrm(modrm, Operand.OPSIZE.Word);
                    instr = new Move(op1, op2);
                    break;

                case 0x8d:
                    if (modrm < 0xc0)       //8d 0c - 8d ff undefined
                    {
                        op1 = getReg(Operand.OPSIZE.DWord, (modrm % 0x40) / 0x08);
                        op2 = getModrm(modrm, Operand.OPSIZE.None);          //no "byte/dword ptr " prefix
                        instr = new LoadEffAddress(op1, op2);
                    }
                    break;

                case 0x8f:
                    if (bhi == 0)                               //8f 08 - 8f 3f, 8f 48 - 8f 7f \ undefined 
                    {                                           //8f 88 - 88 bf, 88 c8 - 88 ff /
                        op1 = getModrm(modrm, Operand.OPSIZE.DWord);
                        instr = new Pop(op1);
                    }
                    break;

            }
            return instr;
        }

        public Instruction op9x(uint b)
        {
            Instruction instr = null;
            switch (b)
            {
                case 0x90:
                    instr = new NoOp();
                    break;

                case 0x91:
                case 0x92:
                case 0x93:
                case 0x94:
                case 0x95:
                case 0x96:
                case 0x97:
                    op1 = getReg(Operand.OPSIZE.DWord, 0);
                    op2 = getReg(Operand.OPSIZE.DWord, b % 0x8);
                    instr = new Exchange(op1, op2);
                    break;

                case 0x98:
                    instr = new ConvertSize(ConvertSize.MODE.CWDE);
                    break;

                case 0x99:
                    instr = new ConvertSize(ConvertSize.MODE.CDQ);
                    break;

                case 0x9a:
                    op1 = absolute();
                    instr = new Call(op1);
                    break;

                case 0x9b:
                    instr = new Wait();
                    break;

                case 0x9c:
                    instr = new PushFlags();
                    break;

                case 0x9d:
                    instr = new PopFlags();
                    break;

                case 0x9e:
                    instr = new StoreFlags();
                    break;

                case 0x9f:
                    instr = new LoadFlags();
                    break;
            }
            return instr;
        }

        public Instruction opax(uint b)
        {
            Instruction instr = null;
            Immediate imm = null;
            switch (b) {
                case 0xa0:
                case 0xa1:
                    op1 = getReg((b == 0xa0) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, 0);
                    imm = new Immediate(addr32(), Operand.OPSIZE.DWord);
                    op2 = new Memory(null, null, 1, imm, (b == 0xa0) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.DS);
                    instr = new Move(op1, op2);
                    break;

                case 0xa2:
                case 0xa3:
                    imm = new Immediate(addr32(), Operand.OPSIZE.DWord);
                    op1 = new Memory(null, null, 1, imm, (b == 0xa2) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.DS);
                    op2 = getReg((b == 0xa2) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, 0);
                    instr = new Move(op1, op2);
                    break;
                
                case 0xa4:                
                case 0xa5:
                    op1 = new Memory(new Register(Register.REG32.EDI), null, 1, null,
                        (b == 0xa4) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.ES);
                    op2 = new Memory(new Register(Register.REG32.ESI), null, 1, null,
                        (b == 0xa4) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.DS);
                    instr = new MoveString(op1, op2, loopprefix);
                    break;

                case 0xa6:
                case 0xa7:
                    op1 = new Memory(new Register(Register.REG32.ESI), null, 1, null,
                        (b == 0xa6) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.DS);
                    op2 = new Memory(new Register(Register.REG32.EDI), null, 1, null,
                        (b == 0xa6) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.ES);
                    instr = new CompareString(op1, op2, loopprefix);
                    break;
                
                case 0xa8:
                case 0xa9:
                    op1 = getReg((b == 0xa8) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, 0);
                    op2 = getImm((b == 0xa8) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord);
                    instr = new Test(op1, op2);
                    break;

                case 0xaa:
                case 0xab:
                    op1 = new Memory(new Register(Register.REG32.EDI), null, 1, null,
                                (b == 0xaa) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.ES);
                    instr = new StoreString(op1, loopprefix);
                    break;

                case 0xac:
                case 0xad:
                    op2 = new Memory(new Register(Register.REG32.ESI), null, 1, null,
                        (b == 0xac) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.DS);
                    instr = new LoadString(op1, loopprefix);
                    break;

                case 0xae:
                case 0xaf:
                    op1 = new Memory(new Register(Register.REG32.EDI), null, 1, null,
                            (b == 0xae) ? Operand.OPSIZE.Byte : Operand.OPSIZE.DWord, Segment.SEG.ES);
                    instr = new ScanString(op1, loopprefix);
                    break;                
            }
            return instr;
        }

        public Instruction opbx(uint b)
        {
            if (b <= 0xb7)
            {
                op1 = getReg(Operand.OPSIZE.Byte, b % 0x8);
                op2 = getImm(Operand.OPSIZE.Byte);
            }
            else
            {
                op1 = getReg(Operand.OPSIZE.DWord, b % 0x8);
                op2 = getImm(Operand.OPSIZE.DWord);
            }
            return new Move(op1, op2);
        }

        readonly String[] opcodecd = { "rol", "ror", "rcl", "rcr", "shl", "shr", "sal", "sar" };

        public void opcx(uint b)
        {
        //    uint modrm = 0;
        //    switch (b)
        //    {
        //        case 0xc0:
        //            modrm = getNextByte();
        //            opcode = opcodecd[(modrm % 0x40) / 0x08];
        //            op1 = getModrm(modrm, OPSIZE.Byte);
        //            op2 = getImm(OPSIZE.Byte);
        //            opcount = 2;
        //            break;

        //        case 0xc1:
        //            modrm = getNextByte();
        //            opcode = opcodecd[(modrm % 0x40) / 0x08];
        //            op1 = getModrm(modrm, OPSIZE.DWord);
        //            op2 = getImm(OPSIZE.Byte);
        //            opcount = 2;
        //            break;

        //        case 0xc2:
        //            opcode = "ret";
        //            op1 = getImm(OPSIZE.Word);
        //            opcount = 1;
        //            break;

        //        case 0xc3:
        //            opcode = "ret";
        //            opcount = 0;
        //            break;

        //        case 0xc4:
        //        case 0xc5:
        //            modrm = getNextByte();
        //            opcode = (b == 0xc4) ? "les" : "lds";
        //            op1 = getReg(OPSIZE.DWord, (modrm % 0x40) / 0x08);
        //            op2 = getModrm(modrm, OPSIZE.FWord);
        //            opcount = 2;
        //            break;

        //        case 0xc6:
        //        case 0xc7:
        //            modrm = getNextByte();
        //            uint mode = (modrm % 0x40) / 0x08;
        //            if (mode == 0)
        //            {
        //                opcode = "mov";
        //                OPSIZE size = (b == 0xc6) ? OPSIZE.Byte : OPSIZE.DWord;
        //                op1 = getModrm(modrm, size);
        //                op2 = (b == 0xc6) ? getImm(OPSIZE.Byte) : getImm(OPSIZE.DWord); 
        //                opcount = 2;                    
        //            }
        //            else
        //            {
        //                opcode = "???";     //08 - 3f, 48 - 7f \ undefined 
        //                opcount = 0;        //88 - bf, c8 - ff /
        //            }
        //            break;

        //        case 0xc8:
        //            opcode = "enter";
        //            op1 = getImm(OPSIZE.Word);
        //            op2 = getImm(OPSIZE.Byte);
        //            opcount = 2;
        //            break;

        //        case 0xc9:
        //            opcode = "leave";
        //            opcount = 0;
        //            break;

        //        case 0xca:
        //        case 0xcb:
        //            opcode = "retf";
        //            op1 = (b == 0xca) ? getImm(OPSIZE.Word) : "";
        //            opcount = (b == 0xca) ? 1 : 0;
        //            break;

        //        case 0xcc:
        //        case 0xcd:
        //            opcode = "int";
        //            op1 = (b == 0xcc) ? "3" : getImm(OPSIZE.Byte);
        //            opcount = 1;
        //            break;

        //        case 0xce:
        //            opcode = "into";
        //            opcount = 0;
        //            break;

        //        case 0xcf:
        //            opcode = "iretd";
        //            opcount = 0;
        //            break;

        //    }
        }
        
        public void opd7(uint b)
        {
            uint modrm = 0;
            switch (b)
            {
                case 0xd0:
                case 0xd2:
        //            modrm = getNextByte();
        //            opcode = opcodecd[(modrm % 0x40) / 0x08];
        //            op1 = getModrm(modrm, OPSIZE.Byte);
        //            op2 = (b == 0xd0) ? "1" : "cl";
        //            opcount = 2;
                    break;

                case 0xd1:
                case 0xd3:
        //            modrm = getNextByte();
        //            opcode = opcodecd[(modrm % 0x40) / 0x08];
        //            op1 = getModrm(modrm, OPSIZE.DWord);
        //            op2 = (b == 0xd1) ? "1" : "cl";
        //            opcount = 2;
                    break;

                case 0xd4:
                case 0xd5:                    
        //            opcode = (b == 0xd4) ? "aamb" : "aadb";
        //            op1 = getImm(OPSIZE.Byte);
        //            opcount = 1;
        //            if ("0Ah".Equals(op1))
        //            {
        //                opcode = (b == 0xd4) ? "aam" : "aad";
        //                opcount = 0;
        //            }
                    break;

                case 0xd6:              //0xd6 is undefined
        //            opcode = "???";
        //            opcount = 0;
                    break;

                case 0xd7:
        //            opcode = "xlat";
        //            op1 = "byte ptr [ebx]";
        //            opcount = 1;
                    break;
            }
        }

        readonly String[] opcodeex = { "loopne", "loope", "loop", "jecxz", "in", "in", "out", "out",
                                        "call", "jmp", "jmp", "jmp", "in", "in", "out", "out"};

        public void opex(uint b)
        {
            //opcode = opcodeex[(b % 0x10)];
            //opcount = 0;
            //switch (b)
            //{
            //    case 0xe0:
            //    case 0xe1:
            //    case 0xe2:
            //    case 0xe3:
            //    case 0xeb:
            //        op1 = rel8();
            //        opcount = 1;
            //        break;

            //    case 0xe4:
            //    case 0xe5:
            //        op1 = (b == 0xe4) ? "al" : "eax";
            //        op2 = getImm(OPSIZE.Byte);
            //        opcount = 2;
            //        break;

            //    case 0xe6:
            //    case 0xe7:
            //        op1 = getImm(OPSIZE.Byte);
            //        op2 = (b == 0xe6) ? "al" : "eax";
            //        opcount = 2;
            //        break;

            //    case 0xe8:
            //    case 0xe9:
            //        op1 = rel32();
            //        opcount = 1;
            //        break;

            //    case 0xea:
            //        op1 = absolute();
            //        opcount = 1;
            //        break;

            //    case 0xec:
            //    case 0xed:
            //        op1 = (b == 0xec) ? "al" : "eax";
            //        op2 = "dx";
            //        opcount = 2;
            //        break;

            //    case 0xee:
            //    case 0xef:
            //        op1 = "dx";
            //        op2 = (b == 0xee) ? "al" : "eax";
            //        opcount = 2;
            //        break;

                
            //}
        }

        //0Xf0, 0xf2, 0xf3 are prefix bytes and should never get here
        readonly String[] opcodefx = { "???", "???", "???", "???", "hlt", "cmc", "???", "???",
                                        "clc", "stc", "cli", "sti", "cld", "std", "???", "???"};
        readonly String[] opcodef67 = { "test", "???", "not", "neg", "mul", "imul", "div", "idiv" };
        readonly String[] opcodeff = { "inc", "dec", "call", "call", "jmp", "jmp", "push", "???" };
        readonly Operand.OPSIZE[] sizeff = { Operand.OPSIZE.DWord, Operand.OPSIZE.DWord, 
                                       Operand.OPSIZE.DWord, Operand.OPSIZE.FWord, 
                                     Operand.OPSIZE.DWord, Operand.OPSIZE.FWord, 
                                     Operand.OPSIZE.DWord, Operand.OPSIZE.Byte };
        
        public void opfx(uint b)
        {
            //opcode = opcodefx[(b % 0x10)];
            //opcount = 0;
            //uint bhi = (b / 0x08) % 0x08;   //--bb b--- (top two bits should = 0)
            //uint blo = b % 0x08;            //---- -bbb
            //uint modrm = 0;
            //uint mode = 0;
            //switch (b)
            //{
            //    case 0xf1:              //0xf1 is undefined
            //        opcode = "???";
            //        opcount = 0;
            //        break;

            //    case 0xf6:
            //    case 0xf7:
            //        modrm = getNextByte();
            //        mode = (modrm % 0x40) / 0x08;
            //        opcode = opcodef67[mode];
            //        switch (mode)
            //        {
            //            case 0 :
            //                op1 = getModrm(modrm, (b == 0xf6) ? OPSIZE.Byte : OPSIZE.DWord);
            //                op2 = (b == 0xf6) ? getImm(OPSIZE.Byte) : getImm(OPSIZE.DWord);
            //                opcount = 2;
            //                break;

            //            case 4:
            //            case 6:
            //                op1 = (b == 0xf6) ? "al" : "eax";
            //                op2 = getModrm(modrm, (b == 0xf6) ? OPSIZE.Byte : OPSIZE.DWord);
            //                opcount = 2;
            //                break;

            //            case 1:
            //                opcode = "???";
            //                opcount = 0;   
            //                break;

            //            case 7:
            //                if (b == 0xf6)
            //                {
            //                    op1 = getModrm(modrm, OPSIZE.Byte);
            //                    opcount = 1;
            //                }
            //                else
            //                {
            //                    op1 = "eax";
            //                    op2 = getModrm(modrm, OPSIZE.DWord);
            //                    opcount = 2;
            //                }
            //                break;


            //            default:
            //                op1 = getModrm(modrm, (b == 0xf6) ? OPSIZE.Byte : OPSIZE.DWord);
            //                opcount = 1;
            //                break;
            //        }
            //        break;

            //    case 0xfe:
            //        modrm = getNextByte();
            //        mode = (modrm % 0x40) / 0x08;
            //        if (mode <= 1)
            //        {
            //            opcode = (mode == 0) ? "inc" : "dec";
            //            op1 = getModrm(modrm, OPSIZE.Byte);
            //            opcount = 1;
            //        }
            //        else
            //        {
            //            opcode = "???";     //08 - 3f, 48 - 7f \ undefined 
            //            opcount = 0;        //88 - bf, c8 - ff /
            //        }
            //        break;

            //    case 0xff:
            //        modrm = getNextByte();
            //        mode = (modrm / 0x40) % 0x04;
            //        uint range = (modrm % 0x40) / 0x08;
            //        if ((range <= 6) || ((mode == 3) && ((range == 3) || (range == 5)))) 
            //        {
            //            opcode = opcodeff[range];
            //            op1 = getModrm(modrm, sizeff[range]);
            //            opcount = 1;
            //        }
            //        else
            //        {
            //            opcode = "???";     //38 - 3f, 78 - 7f \ undefined 
            //            opcount = 0;        //b8 - bf, f8 - ff /
            //        }
            //        break;
            //}
        }

//- 80x87 instructions --------------------------------------------------------

        //readonly String[] floatreg = {"st(0)", "st(1)", "st(2)", "st(3)", "st(4)", "st(5)", "st(6)", "st(7)"};
        //readonly String[] opcoded8 = { "fadd", "fmul", "fcom", "fcomp", "fsub", "fsubr", "fdiv", "fdivr" };
        //readonly String[] opcoded9 = { "fld", "???", "fst", "fstp", "fldenv", "fldcw", "fnstenv", "fnstcw" };
        //readonly Operand.OPSIZE[] sized9 = { Operand.OPSIZE.DWord, Operand.OPSIZE.None, Operand.OPSIZE.DWord, 
        //                                           Operand.OPSIZE.DWord, Operand.OPSIZE.None, Instruction.
        //                                           OPSIZE.Word, Operand.OPSIZE.None, Operand.OPSIZE.Word };
        //readonly String[] opcoded9c0 = { "fld", "fxch", "???", "fstp1" };
        //readonly String[] opcoded9e0 = {"fchs", "fabs", "???", "???", "ftst", "fxam", "???", "???", 
        //                                "fld1", "fldl2t", "fldl2e", "fldpi", "fldlg2", "fldln2", "fldz", "???",
        //                                "f2xm1", "fyl2x", "fptan", "fpatan", "fxtract", "fprem1", "fdecstp", "fincstp",
        //                                "fprem", "fyl2xp1", "fsqrt", "fsincos", "frndint", "fscale", "fsin", "fcos"};
        //readonly String[] opcodeda = { "fiadd", "fimul", "ficom", "ficomp", "fisub", "fisubr", "fidiv", "fidivr" };
        //readonly String[] opcodedac0 = { "fcmovb", "fcmove", "fcmovbe", "fcmovu" };
        //readonly String[] opcodedb = { "fild", "fisttp", "fist", "fistp", "???", "fld", "???", "fstp" };
        //readonly Operand.OPSIZE[] sizedb = { Operand.OPSIZE.DWord, Operand.OPSIZE.DWord, Operand.OPSIZE.DWord, 
        //                                           Operand.OPSIZE.DWord, 
        //                             Operand.OPSIZE.Byte, Operand.OPSIZE.TByte, Operand.OPSIZE.Byte, Operand.OPSIZE.TByte };
        //readonly String[] opcodedbc0 = { "fcmovnb", "fcmovne", "fcmovnbe", "fcmovnu", "???", "fucomi", "fcomi", "???" };
        //readonly String[] opcodedbe0 = { "feni", "fdisi", "fnclex", "fninit", "fsetpm" };
        //readonly String[] opcodedcc0 = { "fadd", "fmul", "fcom2", "fcomp3", "fsubr", "fsub", "fdivr", "fdiv" };
        //readonly String[] opcodedd = { "fld", "fisttp", "fst", "fstp", "frstor", "???", "fnsave", "fnstsw" };
        //readonly OPSIZE[] sizedd = { OPSIZE.QWord, OPSIZE.QWord, OPSIZE.QWord, OPSIZE.QWord, 
        //                             OPSIZE.None, OPSIZE.Byte, OPSIZE.None, OPSIZE.Word };
        //readonly String[] opcodeddc0 = { "ffree", "fxch4", "fst", "fstp", "fucom", "fucomp", "???", "???" };
        //readonly String[] opcodedec0 = { "faddp", "fmulp", "fcomp5", "???", "fsubrp", "fsubp", "fdivrp", "fdivp" };
        //readonly String[] opcodedf = { "fild", "fisttp", "fist", "fistp", "fbld", "fild", "fbstp", "fistp" };
        //readonly OPSIZE[] sizedf = { OPSIZE.Word, OPSIZE.Word, OPSIZE.Word, OPSIZE.Word, 
        //                             OPSIZE.TByte, OPSIZE.QWord, OPSIZE.TByte, OPSIZE.QWord };
        //readonly String[] opcodedfc0 = { "ffreep", "fxch7", "fstp8", "fstp9", "???", "fucomip", "fcomip", "???" };
        
        
//        public void op8087(uint b)
//        {
//            //all 80x87 opcodes are modr/m instrs
//            uint modrm = getNextByte();
//            uint mode = (modrm / 0x40) % 0x04;
//            uint range = (modrm % 0x40) / 0x08;
//            uint rm = (modrm % 0x08);
//            opcode = "80x87 instr";
//            opcount = 0;
//            if (mode < 3)        //modrm = 0x00 - 0xbf
//            {
//                switch (b)
//                {
//                    case 0xd8:
//                        opcode = opcoded8[range];
//                        op1 = getModrm(modrm, OPSIZE.DWord);
//                        opcount = 1;
//                        break;

//                    case 0xd9:
//                        opcode = opcoded9[range];
//                        if (range != 1)
//                        {
//                            op1 = getModrm(modrm, sized9[range]);
//                            opcount = 1;
//                        }
//                        break;

//                    case 0xda:
//                        opcode = opcodeda[range];
//                        op1 = getModrm(modrm, OPSIZE.DWord);
//                        opcount = 1;
//                        break;

//                    case 0xdb:
//                        opcode = opcodedb[range];
//                        if ((range != 4) || (range != 6))
//                        {
//                            op1 = getModrm(modrm, sizedb[range]);
//                            opcount = 1;
//                        }
//                        break;

//                    case 0xdc:
//                        opcode = opcoded8[range];       //same opcodes as 0xd8
//                        op1 = getModrm(modrm, OPSIZE.QWord);
//                        opcount = 1;
//                        break;

//                    case 0xdd:
//                        opcode = opcodedd[range];
//                        if (range != 5)
//                        {
//                            op1 = getModrm(modrm, sizedd[range]);
//                            opcount = 1;
//                        }
//                        break;

//                    case 0xde:
//                        opcode = opcodeda[range];       //same opcodes as 0xda
//                        op1 = getModrm(modrm, OPSIZE.Word);
//                        opcount = 1;
//                        break;

//                    case 0xdf:
//                        opcode = opcodedf[range];
//                        op1 = getModrm(modrm, sizedf[range]);
//                        opcount = 1;
//                        break;
//                }
//            }
//            else       // modrm  = 0xc0 - 0xff
//            {
//                switch (b)
//                {
//                    case 0xd8:
//                        opcode = opcoded8[range];
//                        if ((range == 2) || (range == 3)) 
//                        {
//                            op1 = floatreg[rm];
//                            opcount = 1;
//                        }
//                        else 
//                        {
//                            op1 = "st";
//                            op2 = floatreg[rm];
//                            opcount = 2;
//                        }
//                        break;

//                    case 0xd9:
//                        if (modrm < 0xe0)
//                        {
//                            if (range != 2)
//                            {
//                                opcode = opcoded9c0[range];
//                                op1 = floatreg[rm];
//                                opcount = 1;
//                            }
//                            else
//                            {
//                                opcode = (modrm == 0xd0) ? "fnop" : "???";
//                                opcount = 0;
//                            }
//                        }
//                        else
//                        {
//                            opcode = opcoded9e0[modrm - 0xe0];
//                            opcount = 0;
//                        }
//                        break;

//                    case 0xda:
//                        if (modrm < 0xe0)
//                        {
//                            opcode = opcodedac0[range];
//                            op1 = "st";
//                            op2 = floatreg[rm];
//                            opcount = 2;                            
//                        }
//                        else
//                        {
//                            opcode = (modrm == 0xe9) ? "fucompp" : "???";
//                            opcount = 0;
//                        }
//                        break;

//                    case 0xdb:
//                        if ((range != 4) && (range != 7))
//                        {
//                            opcode = opcodedbc0[range];
//                            op1 = "st";
//                            op2 = floatreg[rm];
//                            opcount = 2;
//                        }
//                        else
//                        {
//                            opcode = ((modrm >= 0xe0) && (modrm <= 0xe4)) ? opcodedbe0[modrm - 0xe0] : "???";
//                            opcount = 0;                            
//                        }
//                        break;

//                    case 0xdc:
//                        opcode = opcodedcc0[range];
//                        if ((range == 2) || (range == 3))
//                        {
//                            op1 = floatreg[rm];
//                            opcount = 1;
//                        }
//                        else
//                        {
//                            op1 = floatreg[rm];
//                            op2 = "st";
//                            opcount = 2;
//                        }
//                        break;

//                    case 0xdd:
//                        if (modrm < 0xf0)
//                        {
//                            opcode = opcodeddc0[range];
//                            op1 = floatreg[rm];
//                            opcount = 1;
//                        }
//                        else
//                        {
//                            opcode = "???";
//                            opcount = 0;
//                        }
//                        break;

//                    case 0xde:
//                        if (range != 3)
//                        {
//                            opcode = opcodedec0[range];                                
//                            if (range != 2)
//                            {
//                                op1 = floatreg[rm];
//                                op2 = "st";
//                                opcount = 2;
//                            }
//                            else
//                            {
//                                op1 = floatreg[rm];
//                                opcount = 1;

//                            }
//                        }
//                        else
//                        {
//                            opcode = (modrm == 0xd9) ? "fcompp" : "???";
//                            opcount = 0;
//                        }
//                        break;

//                    case 0xdf:
//                        if ((range != 4) && (range != 7))
//                        {
//                            opcode = opcodedfc0[range];
//                            if (range >= 5)
//                            {
//                                op1 = "st";
//                                op2 = floatreg[rm];
//                                opcount = 2;
//                            }
//                            else
//                            {
//                                op1 = floatreg[rm];
//                                opcount = 1;

//                            }
//                        }
//                        else
//                        {
//                            if (modrm == 0xe0)
//                            {
//                                opcode = "fnstsw";
//                                op1 = "ax";
//                                opcount = 1;
//                            }
//                            else
//                            {
//                                opcode = "???";
//                                opcount = 0;
//                            }                            
//                        }
//                        break;
//                }
//            }

//        }

//- two byte instructions -----------------------------------------------------

//        public void op0f(uint b)
//        {
//            if ((b >= 0x00) && (b <= 0x0f))
//            {
//                op0f0x(b);
//            }
//            else if ((b >= 0x10) && (b <= 0x1f))
//            {
//                op0f1x(b);
//            }
//            else if ((b >= 0x20) && (b <= 0x2f))
//            {
//                op0f2x(b);
//            }
//            else if ((b >= 0x30) && (b <= 0x3f))
//            {
//                op0f3x(b);                      //not implemented yet
//            }
//            else if ((b >= 0x40) && (b <= 0x4f))
//            {
//                op0f4x(b);                      //not implemented yet
//            }
//            else if ((b >= 0x50) && (b <= 0x5f))
//            {
//                op0f5x(b);                      //not implemented yet
//            }
//            else if ((b >= 0x60) && (b <= 0x6f))
//            {
//                op0f6x(b);                      //not implemented yet
//            }
//            else if ((b >= 0x70) && (b <= 0x7f))
//            {
//                op0f7x(b);                      //not implemented yet
//            }
//            else if ((b >= 0x80) && (b <= 0x8f))
//            {
//                op0f8x(b);
//            }
//            else if ((b >= 0x90) && (b <= 0x9f))
//            {
//                op0f9x(b);
//            }
//            else if ((b >= 0xa0) && (b <= 0xaf))
//            {
//                op0fax(b);
//            }
//            else if ((b >= 0xb0) && (b <= 0xbf))
//            {
//                op0fbx(b);
//            }
//            else if ((b >= 0xc0) && (b <= 0xcf))
//            {
//                op0fcx(b);                      //not implemented yet
//            }
//            else if ((b >= 0xd0) && (b <= 0xdf))
//            {
//                op0fdx(b);                      //not implemented yet
//            }
//            else if ((b >= 0xe0) && (b <= 0xef))
//            {
//                op0fex(b);                      //not implemented yet
//            }
//            else if ((b >= 0xf0) && (b <= 0xff))
//            {
//                op0ffx(b);                      //not implemented yet
//            }
//        }

//        readonly String[] opcode0f0x = { "???", "???", "lar", "lsl", "???", "syscall", "clts", "sysret",
//                                         "invd", "wbinvd", "???", "ud2", "???", "nop", "femms", "???"};
//        readonly String[] opcode0f00 = { "sldt", "str", "lldt", "ltr", "verr", "verw", "jmpe", "???" };
//        readonly String[] opcode0f01 = { "sgdt", "sidt", "lgdt", "lidt", "smsw", "???", "lmsw", "invlpg" };
//        readonly OPSIZE[] size0f01 = { OPSIZE.FWord, OPSIZE.FWord, OPSIZE.FWord, OPSIZE.FWord, 
//                                       OPSIZE.Word, OPSIZE.None, OPSIZE.Word, OPSIZE.None };
//        readonly String[] opcode0f01c0 = { "???", "vmcall", "vmlaunch", "vmresume", "vmxoff", "???", "???", "???" };
//        readonly String[] opcode0f01c8 = { "monitor", "mwait", "???", "???", "???", "???", "???", "???" };
//        readonly String[] opcode0f01d8 = { "vmrun", "vmmcall", "vmload", "vmsave", "stgi", "clgi", "skinit", "invlpga"};
//        readonly int[] opcount0fd8 = { 1, 0, 1, 1, 0, 0, 1, 2 };
//        readonly String[] opcode0f01f8 = { "swapgs", "rdtscp", "???", "???", "???", "???", "???", "???" };

//        public void op0f0x(uint b)
//        {
//            opcode = opcode0f0x[(b % 0x10)];
//            opcount = 0;
//            uint bhi = (b / 0x08) % 0x08;   //--bb b--- 
//            uint blo = b % 0x08;            //---- -bbb
//            uint modrm = 0;
//            uint mode = 0;
//            uint range = 0;
//            switch (b)
//            {
//                case 0x00:
//                    modrm = getNextByte();
//                    mode = (modrm / 0x40) % 0x04;                        
//                    range = (modrm % 0x40) / 0x08;
//                    if (range <= 6)
//                    {
//                        opcode = opcode0f00[range];
//                        op1 = getModrm(modrm, ((range == 6) ? OPSIZE.DWord : (((mode == 3) && (range < 2)) ? OPSIZE.DWord : OPSIZE.Word)));
//                        opcount = 1;
//                    }
//                    else
//                    {
//                        opcode = "???";
//                        opcount = 0;   
//                    }
//                    break;

//                case 0x01:
//                    modrm = getNextByte();
//                    mode = (modrm / 0x40) % 0x04;                        
//                    range = (modrm % 0x40) / 0x08;
//                    if (mode < 3) {
//                        if (range != 5)
//                        {
//                            opcode = opcode0f01[range];                        
//                            op1 = getModrm(modrm, size0f01[range]);
//                            opcount = 1;
//                        }
//                        else
//                        {
//                            opcode = "???";
//                            opcount = 0;   
//                        } 
//                    } else {
//                        switch (range)
//                        {
//                            case 0x00:
//                                opcode = opcode0f01c0[modrm - 0xc0];
//                                opcount = 0;
//                                break;

//                            case 0x01:
//                                opcode = opcode0f01c8[modrm - 0xc8];
//                                opcount = 0;
//                                if (modrm == 0xc8)
//                                {
//                                    op1 = "eax";
//                                    op2 = "ecx";
//                                    op3 = "edx";
//                                    opcount = 3;
//                                }
//                                if (modrm == 0xc9)
//                                {
//                                    op1 = "eax";
//                                    op2 = "ecx";
//                                    opcount = 2;
//                                }
//                                break;

//                            case 0x03:
//                                uint d8ofs = modrm - 0xd8;
//                                opcode = opcode0f01d8[d8ofs];
//                                op1 = "eax";
//                                op2 = "ecx";
//                                opcount = opcount0fd8[d8ofs];
//                                break;

//                            case 0x04:
//                            case 0x06:
//                                opcode = opcode0f01[range];
//                                op1 = getModrm(modrm, (range == 4 ? OPSIZE.DWord : OPSIZE.Word));
//                                opcount = 1;
//                                break;

//                            case 0x07:
//                                opcode = opcode0f01f8[modrm - 0xf8];
//                                opcount = 0;
//                                break;

//                            default:
//                                opcode = "???";
//                                opcount = 0;
//                                break;
//                        }

//                    }
//                    break;

//                case 0x02:
//                case 0x03:
//                    modrm = getNextByte();
//                    op1 = getReg(OPSIZE.DWord, (modrm % 0x40) / 0x08);
//                    op2 = getModrm(modrm, OPSIZE.DWord);
//                    opcount = 2;
//                    break;
//            }
//        }

//        readonly String[] opcode0f1x = { "movups", "movups", "movlps", "movlps", "unpcklps", "unpckhps", "movhps", "movhps",
//                                         "???", "???", "???", "???", "???", "???", "???", "???"};
//        readonly String[] opcode0f18 = { "prefetchnta", "prefetcht0", "prefetcht1", "prefetcht2", "???", "???", "???", "???" };

        
//        public void op0f1x(uint b)
//        {
//            opcode = opcode0f1x[(b % 0x10)];
//            opcount = 0;
//            uint bhi = (b / 0x08) % 0x08;   //--bb b--- (top two bits should = 0)
//            uint blo = b % 0x08;            //---- -bbb
//            uint modrm = 0;
//            uint mode = 0;
//            switch (b)
//            {
//                case 0x10:
//                case 0x14:
//                case 0x15:                
//                    modrm = getNextByte();
//                    op1 = regxmm[(modrm % 0x40) / 0x08];
//                    op2 = getModrm(modrm, OPSIZE.XMM);
//                    opcount = 2;
//                    break;

//                case 0x11:
//                    modrm = getNextByte();
//                    op1 = getModrm(modrm, OPSIZE.XMM);
//                    op2 = regxmm[(modrm % 0x40) / 0x08];
//                    opcount = 2;
//                    break;

//                case 0x12:
//                case 0x16:
//                    modrm = getNextByte();
//                    mode = (modrm / 0x40) % 0x04;
//                    if (mode == 3) opcode = (b == 0x02) ? "movhlps" : "movlhps";
//                    op1 = regxmm[(modrm % 0x40) / 0x08];
//                    OPSIZE arg2 = (mode < 3) ? OPSIZE.QWord : OPSIZE.XMM;
//                    op2 = getModrm(modrm, arg2);
//                    opcount = 2;
//                    break;

//                case 0x13:
//                case 0x17:
//                    modrm = getNextByte();
//                    mode = (modrm / 0x40) % 0x04;
//                    if (mode < 3)
//                    {
//                        op1 = getModrm(modrm, OPSIZE.QWord);
//                        op2 = regxmm[(modrm % 0x40) / 0x08];
//                        opcount = 2;
//                    }
//                    else
//                    {
//                        opcode = "???";
//                        opcount = 0;
//                    }
//                    break;

//                case 0x18:
//                    modrm = getNextByte();
//                    mode = (modrm / 0x40) % 0x04;
//                    uint range = (modrm % 0x40) / 0x08;
//                    if ((mode < 3) && (range <= 3))
//                    {
//                        opcode = opcode0f18[range];
//                        op1 = getModrm(modrm, OPSIZE.None);
//                        opcount = 1;
//                    }
//                    else
//                    {
//                        opcode = "???";
//                        opcount = 0;
//                    }
//                    break;

//            }
//        }

//        readonly String[] opcode0f2x = { "???", "???", "???", "???", "???", "???", "???", "???",
//                                         "movaps", "movaps", "cvtpi2ps", "movntps", "cvttps2pi", "cvtps2pi", "ucomiss", "comiss"};
                                   

//        public void op0f2x(uint b)
//        {
//            opcode = opcode0f2x[(b % 0x10)];
//            opcount = 0;
//            uint bhi = (b / 0x08) % 0x08;   //--bb b--- (top two bits should = 0)
//            uint blo = b % 0x08;            //---- -bbb
//            uint modrm = 0;
//            uint mode = 0;
//            switch (b)
//            {
//                case 0x28:
//                case 0x2a:
//                    modrm = getNextByte();
//                    op1 = regxmm[(modrm % 0x40) / 0x08];
//                    op2 = getModrm(modrm, (b == 0x28 ? OPSIZE.XMM : OPSIZE.MM));
//                    opcount = 2;
//                    break;

//                case 0x29:
//                    modrm = getNextByte();
//                    op1 = getModrm(modrm, OPSIZE.XMM);
//                    op2 = regxmm[(modrm % 0x40) / 0x08];
//                    opcount = 2;
//                    break;

//                case 0x2b:
//                    modrm = getNextByte();
//                    mode = (modrm / 0x40) % 0x04;
//                    if (mode < 3)
//                    {
//                        op1 = getModrm(modrm, OPSIZE.XMM);
//                        op2 = regxmm[(modrm % 0x40) / 0x08];
//                        opcount = 2;
//                    }
//                    else
//                    {
//                        opcode = "???";
//                        opcount = 0;
//                    }
//                    break;

//                case 0x2c:
//                case 0x2d:
//                    modrm = getNextByte();
//                    mode = (modrm / 0x40) % 0x04;
//                    op1 = regmm[(modrm % 0x40) / 0x08];
//                    op2 = getModrm(modrm, (mode < 3) ? OPSIZE.MM : OPSIZE.XMM);
//                    opcount = 2;
//                    break;

//                case 0x2e:
//                case 0x2f:
//                    modrm = getNextByte();
//                    mode = (modrm / 0x40) % 0x04;
//                    op1 = regxmm[(modrm % 0x40) / 0x08];
//                    op2 = getModrm(modrm, (mode < 3) ? OPSIZE.DWord : OPSIZE.XMM);
//                    opcount = 2;
//                    break;

//            }
//        }

//        public void op0f3x(uint b)
//        {
//            opcode = "0F instr";
//            opcount = 0;
//        }
//        public void op0f4x(uint b)
//        {
//            opcode = "0F instr";
//            opcount = 0;
//        }
//        public void op0f5x(uint b)
//        {
//            opcode = "0F instr";
//            opcount = 0;
//        }
//        public void op0f6x(uint b)
//        {
//            opcode = "0F instr";
//            opcount = 0;
//        }
//        public void op0f7x(uint b)
//        {
//            opcode = "0F instr";
//            opcount = 0;
//        }

//        readonly String[] opcode0f8x = { "jo", "jno", "jb", "jae", "je", "jne", "jbe", "ja", "js", "jns", "jp", "jnp", "jl", "jge", "jle", "jg" };

//        public void op0f8x(uint b)
//        {
//            opcode = opcode0f8x[(b % 0x10)];
//            op1 = rel32();
//            opcount = 1;
//        }

//        readonly String[] opcode0f9x = { "seto", "setno", "setb", "setae", "sete", "setne", "setbe", "seta", 
//                                         "sets", "setns", "setp", "setnp", "setl", "setge", "setle", "setg" };

//        public void op0f9x(uint b)
//        {
//            opcode = opcode0f9x[(b % 0x10)];
//            uint modrm = getNextByte();
//            op1 = getModrm(modrm, OPSIZE.Byte);
//            opcount = 1;
//        }

//        readonly String[] opcode0fax = { "push", "pop", "cpuid", "bt", "shld", "shld", "xbts", "ibts", 
//                                         "push", "pop", "rsm", "bts", "shrd", "shrd", "???", "imul" };
//        readonly String[] opcode0fae = { "fxsave", "fxrstor", "ldmxcsr", "stmxcsr", "???", "???", "???", "clflush" };
//        readonly OPSIZE[] size0fae = { OPSIZE.None, OPSIZE.None, OPSIZE.DWord, OPSIZE.DWord, OPSIZE.None, OPSIZE.None, OPSIZE.None, OPSIZE.None };
        

//        public void op0fax(uint b)
//        {
//            opcode = opcode0fax[(b % 0x10)];
//            opcount = 0;
//            uint modrm = 0;
//            switch (b)
//            {
//                case 0xa0:
//                case 0xa1:
//                case 0xa8:
//                case 0xa9:
//                    op1 = (b <= 0xa1) ? "fs" : "gs";
//                    opcount = 1;
//                    break;

//                case 0xa3:
//                case 0xa7:
//                case 0xab:
//                    modrm = getNextByte();
//                    op1 = getModrm(modrm, OPSIZE.DWord);
//                    op2 = getReg(OPSIZE.DWord, (modrm % 0x40) / 0x08);
//                    opcount = 2;
//                    break;

//                case 0xa4:
//                case 0xac:
//                    modrm = getNextByte();
//                    op1 = getModrm(modrm, OPSIZE.DWord);
//                    op2 = getReg(OPSIZE.DWord, (modrm % 0x40) / 0x08);
//                    op3 = getImm(OPSIZE.Byte);
//                    opcount = 3;
//                    break;

//                case 0xa5:
//                case 0xad:
//                    modrm = getNextByte();
//                    op1 = getModrm(modrm, OPSIZE.DWord);
//                    op2 = getReg(OPSIZE.DWord, (modrm % 0x40) / 0x08);
//                    op3 = "cl";
//                    opcount = 3;
//                    break;

//                case 0xa6:
//                case 0xaf:
//                    modrm = getNextByte();
//                    op1 = getReg(OPSIZE.DWord, (modrm % 0x40) / 0x08);
//                    op2 = getModrm(modrm, OPSIZE.DWord);
//                    opcount = 2;
//                    break;

//                case 0xae:
//                    modrm = getNextByte();
//                    uint mode = (modrm / 0x40) % 0x04;
//                    uint range = (modrm % 0x40) / 0x08;
//                    if ((mode < 3) && ((range <= 3) || (range == 7)))
//                    {
//                        opcode = opcode0fae[range];
//                        op1 = getModrm(modrm, size0fae[range]);
//                        opcount = 1;
//                    }
//                    else
//                    {
//                        opcode = "???";
//                        opcount = 0;
//                    }
//                    break;

//            }
//        }

//        readonly String[] opcode0fbx = { "cmpxchg", "cmpxchg", "lss", "btr", "lfs", "lgs", "movzx", "movzx", 
//                                         "jmpe", "???", "???", "btc", "bsf", "bsr", "movsx", "movsx" };
//        readonly OPSIZE[] size0fbx = { OPSIZE.None, OPSIZE.None, OPSIZE.None, OPSIZE.None, OPSIZE.None, OPSIZE.None, OPSIZE.Byte, OPSIZE.Word, 
//                                       OPSIZE.None, OPSIZE.None, OPSIZE.None, OPSIZE.None, OPSIZE.DWord, OPSIZE.DWord, OPSIZE.Byte, OPSIZE.Word };

//        public void op0fbx(uint b)
//        {
//            opcode = opcode0fbx[(b % 0x10)];
//            opcount = 0;
//            uint modrm = 0;
//            switch (b)
//            {
//                case 0xb0:
//                    modrm = getNextByte();
//                    op1 = getModrm(modrm, OPSIZE.Byte);
//                    op2 = getReg(OPSIZE.Byte, (modrm % 0x40) / 0x08);
//                    opcount = 2;
//                    break;

//                case 0xb1:
//                case 0xb3:
//                case 0xbb:
//                    modrm = getNextByte();
//                    op1 = getModrm(modrm, OPSIZE.DWord);
//                    op2 = getReg(OPSIZE.DWord, (modrm % 0x40) / 0x08);
//                    opcount = 2;
//                    break;

//                case 0xb6:
//                case 0xb7:
//                case 0xbc:
//                case 0xbd:
//                case 0xbe:
//                case 0xbf:
//                    modrm = getNextByte();
//                    op1 = getReg(OPSIZE.DWord, (modrm % 0x40) / 0x08);
//                    op2 = getModrm(modrm, size0fbx[(b % 0x10)]);
//                    opcount = 2;
//                    break;

//                case 0xb2:
//                case 0xb4:
//                case 0xb5:
//                    modrm = getNextByte();
//                    uint mode = (modrm / 0x40) % 0x04;
//                    if (mode < 3)
//                    {
//                        op1 = getReg(OPSIZE.DWord, (modrm % 0x40) / 0x08);
//                        op2 = getModrm(modrm, OPSIZE.FWord);
//                        opcount = 2;
//                    }
//                    else
//                    {
//                        opcode = "???";
//                        opcount = 0;
//                    }
//                    break;

//                case 0xb8:
//                    op1 = rel32();
//                    opcount = 1;
//                    break;
                
//            }
//        }
//        public void op0fcx(uint b)
//        {
//            opcode = "0F instr";
//            opcount = 0;
//        }
//        public void op0fdx(uint b)
//        {
//            opcode = "0F instr";
//            opcount = 0;
//        }
//        public void op0fex(uint b)
//        {
//            opcode = "0F instr";
//            opcount = 0;
//        }
//        public void op0ffx(uint b)
//        {
//            opcode = "0F instr";
//            opcount = 0;
//        }

//- registers -----------------------------------------------------------------
       
        public Register getReg(Operand.OPSIZE size, uint reg)
        {
            if (operandSizeOverride && (size == Operand.OPSIZE.DWord)) size = Operand.OPSIZE.Word;
            if (useModrm32 && (size == Operand.OPSIZE.Word)) size = Operand.OPSIZE.DWord;

            Register result = null;
            switch (size)
            {
                case Operand.OPSIZE.Byte:
                    result = new Register((Register.REG8)reg);
                    break;
                case Operand.OPSIZE.Word:
                    result = new Register((Register.REG16)reg);
                    break;
                case Operand.OPSIZE.DWord:
                    result = new Register((Register.REG32)reg);
                    break;
                case Operand.OPSIZE.MM:
                    result = new Register((Register.REGMM)reg);
                    break;
                case Operand.OPSIZE.XMM:
                    result = new Register((Register.REGXMM)reg);
                    break;
            }

            return result;
        }

//- immediates ----------------------------------------------------------------

        public uint getNextWord()
        {
            uint b = getNextByte();
            uint a = getNextByte();
            return (a * 256 + b);
        }

        public uint getNextDWord()
        {
            uint d = getNextByte();
            uint c = getNextByte();
            uint b = getNextByte();
            uint a = getNextByte();
            return (((a * 256 + b) * 256 + c) * 256 + d);            
        }

        public Operand getImm(Operand.OPSIZE size)
        {
            if (operandSizeOverride && (size == Operand.OPSIZE.DWord)) size = Operand.OPSIZE.Word;

            uint val = 0;
            switch (size)
            {
                case Operand.OPSIZE.Byte:
                    val = getNextByte();
                    break;
                case Operand.OPSIZE.SignedByte:
                    val = getNextByte();
                    break;
                case Operand.OPSIZE.Word:
                    val = getNextWord();
                    break;
                case Operand.OPSIZE.DWord:
                    val = getNextDWord();
                    break;
            }

            Immediate imm = new Immediate(val, size);

            return imm;
        }

        public uint getOfs(Operand.OPSIZE size)
        {
            uint result = 0;
            if (size == Operand.OPSIZE.Byte) result = ofs8();
            if (size == Operand.OPSIZE.DWord) result = ofs32();
            return result;
        }

        public uint ofs8()   
        {
            return getNextByte();            
        }

        public uint ofs32()
        {
            return getNextDWord();
        }

        public Operand rel8()
        {
            uint b = getNextByte();
            uint target = b + codeaddr;
            if (b >= 0x80)
            {
                target -= 0x100;
            }

            Immediate imm = new Immediate(target, Operand.OPSIZE.DWord);
            return imm;
        }

        public Operand rel32()
        {
            uint sum = getNextDWord();
            sum += codeaddr;
            Immediate imm = new Immediate(sum, Operand.OPSIZE.DWord);
            return imm;
        }

        public uint addr32()          //mem addrs aren't prefixed with '0'
        {
            uint sum = getNextDWord();
            return sum;
        }

        public Absolute absolute()   //6 bytes -> ssss:aaaaaaaa
        {
            uint addr = getNextDWord();
            uint seq = getNextWord();
            Absolute abs = new Absolute(seq, addr);
            return abs;
        }
    }
}
