using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SolidEdge.Community.Reader.Native
{
    internal static class NativeMethods
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteEnhMetaFile(IntPtr hemf);

        [DllImport("ole32.dll")]
        public static extern int FmtIdToPropStgName(
            [In] ref Guid pfmtid,
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder oszName);

        [DllImport("GdiPlus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int GdipCreateBitmapFromGdiDib(IntPtr gdiBitmapInfo, IntPtr gdiBitmapData, out IntPtr bitmap); 

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("ole32.dll")]
        public static extern int OleLoadFromStream(
            IStream pStm,
            [In] ref Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("ole32.dll")]
        public extern static int PropVariantClear(ref PROPVARIANT pvar);

        [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
        public static extern int ReadClassStm(
            IStream pStm,
            ref Guid guid);

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SetEnhMetaFileBits(
            uint cbBuffer,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]
            byte[] lpData
            );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("ole32.dll")]
        public static extern int StgIsStorageFile([MarshalAs(UnmanagedType.LPWStr)]string pwcsName);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("ole32.dll")]
        public static extern int StgOpenStorageEx(
            [MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
            uint grfMode,
            uint stgfmt,
            uint grfAttrs,
            IntPtr pStgOptions,
            IntPtr reserved2,
            [In] ref Guid riid,
            out IStorage ppObjectOpen);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("ole32.dll")]
        public static extern int StgOpenStorageEx(
            [MarshalAs(UnmanagedType.LPWStr)] string pwcsName,
            uint grfMode,
            uint stgfmt,
            uint grfAttrs,
            IntPtr pStgOptions,
            IntPtr reserved2,
            [In] ref Guid riid,
            out IPropertySetStorage ppObjectOpen);

        [DllImport("ole32.dll")]
        public static extern int StgOpenStorageOnILockBytes(
            ILockBytes plkbyt,
            IStorage pStgPriority,
            uint grfMode,
            IntPtr snbEnclude,
            uint reserved,
            out IStorage ppstgOpen);

        public static bool Succeeded(int hr)
        {
            return hr >= 0;
        }

        public static bool Failed(int hr)
        {
            return hr < 0;
        }
    }
}
