
using System;
using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPINTERFACE_TABLE
    {
        public uint NumEntries;
        public MIB_IPINTERFACE_ROW[] Table;
        public MIB_IPINTERFACE_TABLE(IntPtr pTable)
        {
            NumEntries = (uint)Marshal.ReadInt32(pTable, 0);
            Table = new MIB_IPINTERFACE_ROW[NumEntries];
            for (int i = 0; i < NumEntries; i++)
            {
                // The offset of the array of structures is 8 bytes past the beginning. Then, take the index and multiply it by the number of bytes in the structure.
                // the length of the MIB_IPINTERFACE_ROW structure is 168 bytes - this was determined by doing a sizeof(MIB_IPINTERFACE_ROW) in an unmanaged C++ app.
                IntPtr pItemList = new IntPtr(pTable.ToInt64() + (i * 168) + 8);

                // Construct the MIB_IPINTERFACE_ROW structure, marshal the unmanaged structure into it, then copy it to the array of structures.
                MIB_IPINTERFACE_ROW Row = new MIB_IPINTERFACE_ROW();
                Row = (MIB_IPINTERFACE_ROW)Marshal.PtrToStructure(pItemList, typeof(MIB_IPINTERFACE_ROW));
                Table[i] = Row;
            }
        }
    }
}
