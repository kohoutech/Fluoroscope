/* ----------------------------------------------------------------------------
Fluoroscope : an executable file viewer
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

//10/11/16

//https://docs.microsoft.com/en-us/windows/desktop/debug/pe-format

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Fluoroscope.UI;
using Origami.Win32;
using Origami.Asm32;

namespace Fluoroscope
{

    class Fluoroscope
    {
        public FluoroWindow fwindow;
        public Win32Exe winexe;

        public Fluoroscope(FluoroWindow _fwindow)
        {
            fwindow = _fwindow;
            winexe = null;
        }

        public void openSourceFile(String filename)
        {
            winexe = new Win32Exe();
            winexe.readFile(filename);      //read in file            
        }

        public void closeSourceFile()
        {
            winexe = null;
        }

//- displaying ---------------------------------------------------------------

        public void showExeHeaderInfo()
        {
            InfoWindow infoWin = new InfoWindow();
            infoWin.setTitle("EXE Header");

            String coffHdr =
               "machine = " + winexe.machine + "\r\n" +
               "sectioncount = " + winexe.sectionCount + "\r\n" +
               "timestamp = " + winexe.timeStamp + "\r\n" +
               "symbol tbl ptr = " + winexe.symbolTblAddr + "\r\n" +
               "symbol count = " + winexe.symbolCount + "\r\n" +
               "optional header size = " + winexe.optionalHdrSize + "\r\n" +
               "characteristics = " + winexe.characteristics;

            String optionalHdr =
                "magic number = " + winexe.magicNum + "\r\n" +
                "linker version = " + winexe.majorLinkerVersion + "." + winexe.minorLinkerVersion + "\r\n" +
                "size of code = " + winexe.sizeOfCode + "\r\n" +
                "size of initializedData = " + winexe.sizeOfInitializedData + "\r\n" +
                "size of uninitializedData = " + winexe.sizeOfUninitializedData + "\r\n" +
                "entry point = " + winexe.addressOfEntryPoint + "\r\n" +
                "base of code = " + winexe.baseOfCode + "\r\n" +
                "base of data = " + winexe.baseOfData + "\r\n" +
                "image base = " + winexe.imageBase.ToString("X") + "\r\n" +
                "memory alignment = " + winexe.sectionAlignment + "\r\n" +
                "file alignment = " + winexe.fileAlignment + "\r\n" +
                "OS version = " + winexe.majorOSVersion + "." + winexe.minorOSVersion + "\r\n" +
                "image version = " + winexe.majorImageVersion + "." + winexe.minorImageVersion + "\r\n" +
                "subsystem version = " + winexe.majorSubsystemVersion + "." + winexe.minorSubsystemVersion + "\r\n" +
                "win32 version = " + winexe.win32VersionValue + "\r\n" +
                "size of image = " + winexe.sizeOfImage + "\r\n" +
                "size of headers = " + winexe.sizeOfHeaders + "\r\n" +
                "checksum = " + winexe.checksum + "\r\n" +
                "subsystem = " + winexe.subsystem + "\r\n" +
                "DLL characteristics = " + winexe.dLLCharacteristics + "\r\n" +
                "size of stack reserve = " + winexe.sizeOfStackReserve + "\r\n" +
                "size of stack commit = " + winexe.sizeOfStackCommit + "\r\n" +
                "size of heap reserve = " + winexe.sizeOfHeapReserve + "\r\n" +
                "size of heap commit = " + winexe.sizeOfHeapCommit + "\r\n" +
                "loader flags = " + winexe.loaderFlags;

            String dataDir =
                "export table = " + winexe.dExportTable.rva + " : " + winexe.dExportTable.size + "\r\n" +
                "import table = " + winexe.dImportTable.rva + " : " + winexe.dImportTable.size + "\r\n" +
                "resource table = " + winexe.dResourceTable.rva + " : " + winexe.dResourceTable.size + "\r\n" +
                "exception table = " + winexe.exceptionTable.rva + " : " + winexe.exceptionTable.size + "\r\n" +
                "certificate table = " + winexe.certificatesTable.rva + " : " + winexe.certificatesTable.size + "\r\n" +
                "base relocation table = " + winexe.baseRelocationTable.rva + " : " + winexe.baseRelocationTable.size + "\r\n" +
                "debug data = " + winexe.debugTable.rva + " : " + winexe.debugTable.size + "\r\n" +
                "architecture = " + winexe.architecture.rva + " : " + winexe.architecture.size + "\r\n" +
                "global pointer = " + winexe.globalPtr.rva + " : " + winexe.globalPtr.size + "\r\n" +
                "thread local storage table = " + winexe.threadLocalStorageTable.rva + " : " + winexe.threadLocalStorageTable.size + "\r\n" +
                "load configuration table = " + winexe.loadConfigurationTable.rva + " : " + winexe.loadConfigurationTable.size + "\r\n" +
                " bound import table = " + winexe.boundImportTable.rva + " : " + winexe.boundImportTable.size + "\r\n" +
                "import address table = " + winexe.importAddressTable.rva + " : " + winexe.importAddressTable.size + "\r\n" +
                "delay import descriptor = " + winexe.delayImportDescriptor.rva + " : " + winexe.delayImportDescriptor.size + "\r\n" +
                "CLR runtime header = " + winexe.CLRRuntimeHeader.rva + " : " + winexe.CLRRuntimeHeader.size + "\r\n" +
                "reserved = " + winexe.reserved.rva + " : " + winexe.reserved.size;

            String text = coffHdr + "\r\n\r\n" + optionalHdr + "\r\n\r\n" + dataDir;
            infoWin.setText(text);
            infoWin.Show(fwindow);
        }


        public void showSectionData(Section section)
        {
            StringBuilder dataStr = new StringBuilder();       //the whole thing as one LONG string
            StringBuilder ascii = new StringBuilder();      //the ascii representation of the bytes on one line

            InfoWindow infoWin = new InfoWindow();
            infoWin.setTitle("Section [" + section.secNum + "] " + section.secName + " Data");

            int bpos = 0;
            uint loc = section.memloc;
            for (; bpos < section.data.Length; )
            {
                if (bpos % 16 == 0)
                {
                    dataStr.Append(loc.ToString("X8") + ": ");         //address field if at start of line
                }

                uint b = section.data[bpos];
                dataStr.Append(b.ToString("X2"));                                              //single byte value in hex
                dataStr.Append(" ");
                ascii.Append((b >= 0x20 && b <= 0x7E) ? ((char)b).ToString() : ".");        //and its ascii equivalent
                bpos++;
                loc++;

                if (bpos % 16 == 0)
                {
                    dataStr.AppendLine(ascii.ToString());      //ascii field if at end of line
                    ascii.Clear();
                }
            }
            if (bpos % 16 != 0)                                 //fill out partial last line
            {
                int remainder = (bpos % 16);
                for (; remainder < 16; remainder++)
                {
                    dataStr.Append("   ");                  //space over to line up ascii field

                }
                dataStr.AppendLine(ascii.ToString());
            }
            String text = dataStr.ToString();

            infoWin.setText(text);
            infoWin.Show(fwindow);
        }

        const int BYTESFIELDWIDTH = 6;              //in bytes = each byte takes up 3 spaces
        const int OPCODEFIELDWIDTH = 12;            //in actual spaces

        //format addr, instruction bytes & asm ops into a list of strings for all bytes in code section
        public void showSectionCode(Section section)
        {
            if (section.isCode())
            {
                InfoWindow infoWin = new InfoWindow();
                infoWin.setTitle("Section [" + section.secNum + "] " + section.secName + " Code");

                uint srcpos = 0;
                List<String> codeList = new List<String>();
                StringBuilder asmLine = new StringBuilder();
                i32Disasm disasm = new i32Disasm(section.data, srcpos);
                Instruction instr;
                uint instrlen = 0;
                List<byte> instrBytes;

                uint codeaddr = section.memloc;         //starting pos of code in mem, used for instr addrs

                while (srcpos < (section.data.Length - disasm.MAXINSTRLEN))
                {
                    instr = disasm.getInstr(codeaddr);          //disasm bytes at cur source pos into next instruction
                    instrBytes = instr.getBytes();              //the instruction's bytes
                    instrlen = (uint)instrBytes.Count;          //determines how many bytes to format in line

                    asmLine.Clear();

                    //address field
                    asmLine.Append("  " + codeaddr.ToString("X8") + ": ");

                    //bytes field
                    for (int i = 0; i < BYTESFIELDWIDTH; i++)
                    {
                        if (i < instrlen)
                        {
                            asmLine.Append(instrBytes[i].ToString("X2") + " ");
                        }
                        else
                        {
                            asmLine.Append("   ");          //extra space in field
                        }
                    }
                    asmLine.Append(" ");                    //space over to opcode field

                    //opcode field
                    String opcode = instr.ToString();
                    if (instr.lockprefix)
                    {
                        opcode = "LOCK " + opcode;
                    }
                    asmLine.Append(opcode);

                    //operands field
                    String spacer = (opcode.Length < OPCODEFIELDWIDTH) ?
                        "            ".Substring(0, OPCODEFIELDWIDTH - opcode.Length) : "";

                    if (instr.opcount > 0)
                    {
                        asmLine.Append(spacer + instr.op1.ToString());
                    }
                    if (instr.opcount > 1)
                    {
                        asmLine.Append("," + instr.op2.ToString());
                    }
                    if (instr.opcount > 2)
                    {
                        asmLine.Append("," + instr.op3.ToString());
                    }

                    //if all of instructions bytes were too long for one line, put the extra bytes on the next line
                    if (instrlen > 6)
                    {
                        asmLine.AppendLine();
                        asmLine.Append("            ");                 //blank addr field
                        for (int i = 6; i < instrlen; i++)
                        {
                            asmLine.Append(instrBytes[i].ToString("X2"));    //extra bytes
                            if (i < (instrlen - 1))
                            {
                                asmLine.Append(" ");
                            }
                        }
                    }

                    codeList.Add(asmLine.ToString());

                    srcpos += instrlen;
                    codeaddr += instrlen;
                }

                String text = String.Join("\r\n", codeList);

                infoWin.setText(text);
                infoWin.Show(fwindow);
            }
        }

        public void writeDumpFile(String outname, List<String> lines)
        {
            File.WriteAllLines(outname, lines);
        }
    }
}
