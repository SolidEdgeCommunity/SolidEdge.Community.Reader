using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SolidEdge.Community.Reader.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RGBQUAD
    {
        public byte rgbBlue;        //BYTE
        public byte rgbGreen;       //BYTE
        public byte rgbRed;         //BYTE
        public byte rgbReserved;    //BYTE
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct BITMAPFILEHEADER
    {
        public ushort bfType;
        public uint bfSize;
        public ushort bfReserved1;
        public ushort bfReserved2;
        public uint bfOffBits;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.LPStruct, SizeConst = 1)]
        public RGBQUAD[] bmiColors;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BITMAPINFOHEADER
    {
        public uint biSize;         //DWORD
        public int biWidth;         //LONG
        public int biHeight;        //LONG
        public ushort biPlanes;     //WORD
        public ushort biBitCount;   //WORD
        public BI biCompression;    //DWORD
        public uint biSizeImage;    //DWORD
        public int biXPelsPerMeter; //LONG
        public int biYPelsPerMeter; //LONG
        public uint biClrUsed;      //DWORD
        public uint biClrImportant; //DWORD

        public void Init()
        {
            biSize = (uint)Marshal.SizeOf(this);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BLOB
    {
        public uint cbSize;
        public IntPtr pBlobData;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BSTRBLOB
    {
        public uint cbSize;
        public IntPtr pData;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CArray
    {
        public uint cElems;
        public IntPtr pElems;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CLIPDATA
    {
        public uint cbSize;         //ULONG
        public int ulClipFmt;       //long
        public IntPtr pClipData;    //BYTE*
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CY
    {
        public uint Lo;
        public int Hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PROPSPEC
    {
        public uint ulKind;         //ULONG
        public PROPSPECUNION u;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct PROPSPECUNION
    {
        [FieldOffset(0)]
        internal uint propId;   //PROPID(ULONG)

        [FieldOffset(0)]
        internal IntPtr name;   //LPOLESTR
    }

    // Credit: http://blogs.msdn.com/b/adamroot/archive/2008/04/11/interop-with-propvariants-in-net.aspx
    /// <summary>
    /// Represents the OLE struct PROPVARIANT.
    /// </summary>
    /// <remarks>
    /// Must call Clear when finished to avoid memory leaks. If you get the value of
    /// a VT_UNKNOWN prop, an implicit AddRef is called, thus your reference will
    /// be active even after the PropVariant struct is cleared.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct PROPVARIANT
    {
        #region struct fields

        // The layout of these elements needs to be maintained.
        //
        // NOTE: We could use LayoutKind.Explicit, but we want
        //       to maintain that the IntPtr may be 8 bytes on
        //       64-bit architectures, so we'll let the CLR keep
        //       us aligned.
        //
        // NOTE: In order to allow x64 compat, we need to allow for
        //       expansion of the IntPtr. However, the BLOB struct
        //       uses a 4-byte int, followed by an IntPtr, so
        //       although the p field catches most pointer values,
        //       we need an additional 4-bytes to get the BLOB
        //       pointer. The p2 field provides this, as well as
        //       the last 4-bytes of an 8-byte value on 32-bit
        //       architectures.

        // This is actually a VarEnum value, but the VarEnum type
        // shifts the layout of the struct by 4 bytes instead of the
        // expected 2.
        ushort vt;
        ushort wReserved1;
        ushort wReserved2;
        ushort wReserved3;
        public IntPtr p;
        int p2;

        #endregion // struct fields

        #region union members

        sbyte cVal // CHAR cVal;
        {
            get { return (sbyte)GetDataBytes()[0]; }
        }

        byte bVal // UCHAR bVal;
        {
            get { return GetDataBytes()[0]; }
        }

        short iVal // SHORT iVal;
        {
            get { return BitConverter.ToInt16(GetDataBytes(), 0); }
        }

        ushort uiVal // USHORT uiVal;
        {
            get { return BitConverter.ToUInt16(GetDataBytes(), 0); }
        }

        int lVal // LONG lVal;
        {
            get { return BitConverter.ToInt32(GetDataBytes(), 0); }
        }

        uint ulVal // ULONG ulVal;
        {
            get { return BitConverter.ToUInt32(GetDataBytes(), 0); }
        }

        long hVal // LARGE_INTEGER hVal;
        {
            get { return BitConverter.ToInt64(GetDataBytes(), 0); }
        }

        ulong uhVal // ULARGE_INTEGER uhVal;
        {
            get { return BitConverter.ToUInt64(GetDataBytes(), 0); }
        }

        float fltVal // FLOAT fltVal;
        {
            get { return BitConverter.ToSingle(GetDataBytes(), 0); }
        }

        double dblVal // DOUBLE dblVal;
        {
            get { return BitConverter.ToDouble(GetDataBytes(), 0); }
        }

        bool boolVal // VARIANT_BOOL boolVal;
        {
            get { return (iVal == 0 ? false : true); }
        }

        int scode // SCODE scode;
        {
            get { return lVal; }
        }

        decimal cyVal // CY cyVal;
        {
            get { return decimal.FromOACurrency(hVal); }
        }

        DateTime date // DATE date;
        {
            get { return DateTime.FromOADate(dblVal); }
        }

        #endregion // union members

        private byte[] GetBlobData()
        {
            var blobData = new byte[lVal];
            IntPtr pBlobData;

            try
            {
                switch (IntPtr.Size)
                {
                    case 4:
                        pBlobData = new IntPtr(p2);
                        break;

                    case 8:
                        pBlobData = new IntPtr(BitConverter.ToInt64(GetDataBytes(), sizeof(int)));
                        break;

                    default:
                        throw new NotSupportedException();
                }

                Marshal.Copy(pBlobData, blobData, 0, lVal);
            }
            catch
            {
                return null;
            }

            return blobData;
        }

        internal CLIPDATA GetCLIPDATA()
        {
            return p.ToStructure<CLIPDATA>();
        }

        /// <summary>
        /// Gets a byte array containing the data bits of the struct.
        /// </summary>
        /// <returns>A byte array that is the combined size of the data bits.</returns>
        private byte[] GetDataBytes()
        {
            var ret = new byte[IntPtr.Size + sizeof(int)];
            
            if (IntPtr.Size == 4)
            {
                BitConverter.GetBytes(p.ToInt32()).CopyTo(ret, 0);
            }
            else if (IntPtr.Size == 8)
            {
                BitConverter.GetBytes(p2).CopyTo(ret, IntPtr.Size);
            }

            return ret;
        }

        /// <summary>
        /// Called to clear the PropVariant's referenced and local memory.
        /// </summary>
        /// <remarks>
        /// You must call Clear to avoid memory leaks.
        /// </remarks>
        public void Clear()
        {
            // Can't pass "this" by ref, so make a copy to call PropVariantClear with
            PROPVARIANT var = this;
            NativeMethods.PropVariantClear(ref var);

            // Since we couldn't pass "this" by ref, we need to clear the member fields manually
            // NOTE: PropVariantClear already freed heap data for us, so we are just setting
            //       our references to null.
            vt = (ushort)VarEnum.VT_EMPTY;
            wReserved1 = wReserved2 = wReserved3 = 0;
            p = IntPtr.Zero;
            p2 = 0;
        }

        /// <summary>
        /// Gets the variant type.
        /// </summary>
        public VarEnum Type
        {
            get { return (VarEnum)vt; }
        }

        /// <summary>
        /// Gets the variant value.
        /// </summary>
        public object Value
        {
            get
            {
                switch ((VarEnum)vt)
                {
                    case VarEnum.VT_I1:
                        return cVal;
                    case VarEnum.VT_UI1:
                        return bVal;
                    case VarEnum.VT_I2:
                        return iVal;
                    case VarEnum.VT_UI2:
                        return uiVal;
                    case VarEnum.VT_I4:
                    case VarEnum.VT_INT:
                        return lVal;
                    case VarEnum.VT_UI4:
                    case VarEnum.VT_UINT:
                        return ulVal;
                    case VarEnum.VT_I8:
                        return hVal;
                    case VarEnum.VT_UI8:
                        return uhVal;
                    case VarEnum.VT_R4:
                        return fltVal;
                    case VarEnum.VT_R8:
                        return dblVal;
                    case VarEnum.VT_BOOL:
                        return boolVal;
                    case VarEnum.VT_ERROR:
                        return scode;
                    case VarEnum.VT_CY:
                        return cyVal;
                    case VarEnum.VT_DATE:
                        return date;
                    case VarEnum.VT_FILETIME:
                        if (hVal > 0)
                        {
                            return DateTime.FromFileTime(hVal);
                        }
                        else
                        {
                            return null;
                        }
                    case VarEnum.VT_BSTR:
                        return Marshal.PtrToStringBSTR(p);
                    case VarEnum.VT_LPSTR:
                        return Marshal.PtrToStringAnsi(p);
                    case VarEnum.VT_LPWSTR:
                        return Marshal.PtrToStringUni(p);
                    case VarEnum.VT_UNKNOWN:
                        return Marshal.GetObjectForIUnknown(p);
                    case VarEnum.VT_DISPATCH:
                        return p;
                    case VarEnum.VT_CLSID:
                        return Marshal.PtrToStructure(p, typeof(Guid));
                    //default:
                    //    throw new NotSupportedException("The type of this variable is not support ('" + vt.ToString() + "')");
                }

                return null;
            }
        }
    }

    internal struct STATPROPSETSTG
    {
#pragma warning disable 0649
        public Guid fmtid;                                              //FMTID
        public Guid clsid;                                              //CLSID
        public uint grfFlags;                                           //DWORD
        public System.Runtime.InteropServices.ComTypes.FILETIME mtime;  //FILETIME
        public System.Runtime.InteropServices.ComTypes.FILETIME ctime;  //FILETIME
        public System.Runtime.InteropServices.ComTypes.FILETIME atime;  //FILETIME
        public uint dwOSVersion;                                        //DWORD
#pragma warning restore 0649
    }

    public struct STATPROPSTG
    {
#pragma warning disable 0649
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpwstrName;   //LPOLESTR
        public uint propid;         //PROPID(ULONG)
        public ushort vt;           //VARTYPE(unsigned short)
#pragma warning restore 0649
    }
}
