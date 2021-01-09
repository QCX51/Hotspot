using System.Runtime.InteropServices;

namespace Native
{
    //http://msdn.microsoft.com/en-us/library/ms706277%28VS.85%29.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct NDIS_OBJECT_HEADER
    {
        public string Type; //UCHAR
        public string Revision; //UCHAR
        public ushort Size;
    }
}
