using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;

namespace SolidEdgeContrib.Reader.Native
{
    [SuppressUnmanagedCodeSecurity]
    [ComImport]
    [Guid(IID.IEnumSTATPROPSETSTG)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumSTATPROPSETSTG
    {
        [PreserveSig]
        int Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] STATPROPSETSTG[] rgelt, out uint pceltFetched);

        [PreserveSig]
        int Skip([In] uint celt);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone(out IEnumSTATPROPSETSTG ppEnum);
    }

    [SuppressUnmanagedCodeSecurity]
    [ComImport]
    [Guid(IID.IEnumSTATPROPSTG)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumSTATPROPSTG
    {
        [PreserveSig]
        int Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] STATPROPSTG[] rgelt, out uint pceltFetched);

        [PreserveSig]
        int Skip([In] uint celt);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        void Clone(out IEnumSTATPROPSTG ppEnum);
    }

    [SuppressUnmanagedCodeSecurity]
    [ComImport]
    [Guid(IID.IEnumSTATSTG)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumSTATSTG
    {
        [PreserveSig]
        int Next(
            [In]
            uint celt,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]
            [Out]
            System.Runtime.InteropServices.ComTypes.STATSTG[] rgelt,
            out uint pceltFetched);

        [PreserveSig]
        int Skip([In] uint celt);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone(out IEnumSTATSTG ppEnum);
    }

//MIDL_INTERFACE("0000000a-0000-0000-C000-000000000046")
//    ILockBytes : public IUnknown
//    {
    [ComImport]
    [Guid(IID.IPropertySetStorage)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ILockBytes
    {
        //    public:
        //        virtual /* [local] */ HRESULT STDMETHODCALLTYPE ReadAt( 
        //            /* [in] */ ULARGE_INTEGER ulOffset,
        //            /* [annotation][length_is][size_is][out] */ 
        //            _Out_writes_bytes_to_(cb, *pcbRead)  void *pv,
        //            /* [in] */ ULONG cb,
        //            /* [annotation][out] */ 
        //            _Out_opt_  ULONG *pcbRead) = 0;

        //void ReadAt(long ulOffset, System.IntPtr pv, int cb, out UIntPtr pcbRead);
        void ReadAt(
            ulong offset,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            [Out]
            byte[] pv,
            int cb,
            out int pcbRead);

        //        virtual /* [local] */ HRESULT STDMETHODCALLTYPE WriteAt( 
        //            /* [in] */ ULARGE_INTEGER ulOffset,
        //            /* [annotation][size_is][in] */ 
        //            _In_reads_bytes_(cb)  const void *pv,
        //            /* [in] */ ULONG cb,
        //            /* [annotation][out] */ 
        //            _Out_opt_  ULONG *pcbWritten) = 0;

        //void WriteAt(long ulOffset, System.IntPtr pv, int cb, out UIntPtr pcbWritten);
        void WriteAt(ulong offset, byte[] pv, int cb, out int pcbWritten);

        //        virtual HRESULT STDMETHODCALLTYPE Flush( void) = 0;

        void Flush();

        //        virtual HRESULT STDMETHODCALLTYPE SetSize( 
        //            /* [in] */ ULARGE_INTEGER cb) = 0;

        //void SetSize(long cb);
        void SetSize(ulong cb);

        //        virtual HRESULT STDMETHODCALLTYPE LockRegion( 
        //            /* [in] */ ULARGE_INTEGER libOffset,
        //            /* [in] */ ULARGE_INTEGER cb,
        //            /* [in] */ DWORD dwLockType) = 0;

        //void LockRegion(long libOffset, long cb, int dwLockType);
        void LockRegion(ulong libOffset, ulong cb, int dwLockType);

        //        virtual HRESULT STDMETHODCALLTYPE UnlockRegion( 
        //            /* [in] */ ULARGE_INTEGER libOffset,
        //            /* [in] */ ULARGE_INTEGER cb,
        //            /* [in] */ DWORD dwLockType) = 0;
        //void UnlockRegion(long libOffset, long cb, int dwLockType);
        void UnlockRegion(ulong libOffset, ulong cb, int dwLockType);

        //        virtual HRESULT STDMETHODCALLTYPE Stat( 
        //            /* [out] */ __RPC__out STATSTG *pstatstg,
        //            /* [in] */ DWORD grfStatFlag) = 0;
        void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);
        //    };
    }

    [SuppressUnmanagedCodeSecurity]
    [ComImport]
    [Guid(IID.IPropertySetStorage)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertySetStorage
    {
        [PreserveSig]
        int Create([In] ref Guid rfmtid, [In] ref Guid pClsid, [In] uint grfFlags, [In] uint grfMode, out IPropertyStorage ppprstg);

        [PreserveSig]
        int Open([In] ref Guid rfmtid, [In] uint grfMode, out IPropertyStorage ppprstg);

        [PreserveSig]
        int Delete([In] ref Guid rfmtid);

        [PreserveSig]
        int Enum(out IEnumSTATPROPSETSTG ppEnum);
    }

    [SuppressUnmanagedCodeSecurity]
    [ComImport]
    [Guid(IID.IPropertyStorage)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyStorage
    {
        [PreserveSig]
        int ReadMultiple([In] uint cpspec, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] PROPSPEC[] rgpspec, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] PROPVARIANT[] rgpropvar);

        [PreserveSig]
        int WriteMultiple([In] uint cpspec, [In] PROPSPEC[] rgpspec, [In] PROPVARIANT[] rgpropvar, [In] uint propidNameFirst);

        [PreserveSig]
        int DeleteMultiple([In] uint cpspec, [In] PROPSPEC[] rgpspec);

        [PreserveSig]
        int ReadPropertyNames(
            [In]
            uint cpropid,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]
            [In]
            uint[] rgpropid,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType=UnmanagedType.LPWStr, SizeParamIndex = 0)]
            [In, Out]
            string[] rglpwstrName);

        [PreserveSig]
        int WritePropertyNames([In] uint cpropid, [In] uint[] rgpropid, [In] string[] rglpwstrName);

        [PreserveSig]
        int DeletePropertyNames([In] uint cpropid, [In] uint[] rgpropid);

        [PreserveSig]
        int Commit([In] uint grfCommitFlags);

        [PreserveSig]
        int Revert();

        [PreserveSig]
        int Enum(out IEnumSTATPROPSTG ppEnum);

        [PreserveSig]
        int SetTimes([In] System.Runtime.InteropServices.ComTypes.FILETIME[] pctime, [In] System.Runtime.InteropServices.ComTypes.FILETIME[] patime, [In] System.Runtime.InteropServices.ComTypes.FILETIME[] pmtime);

        [PreserveSig]
        int SetClass([In] ref Guid clsid);

        [PreserveSig]
        int Stat([Out] out STATPROPSETSTG pstatpsstg);
    }

    [SuppressUnmanagedCodeSecurity]
    [ComImport]
    [Guid(IID.IStorage)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IStorage
    {
        [PreserveSig]
        int CreateStream([In] string pwcsName, [In] uint grfMode, [In] uint reserved1, [In] uint reserved2, out System.Runtime.InteropServices.ComTypes.IStream ppstm);
        
        [PreserveSig]
        int OpenStream([In] string pwcsName, [In] int reserved1, [In] uint grfMode, [In] uint reserved2, out IStream ppstm);

        [PreserveSig]
        int CreateStorage([In] string pwcsName, [In] uint grfMode, [In] uint reserved1, [In] uint reserved2, out IStorage ppstg);

        [PreserveSig]
        int OpenStorage([In] string pwcsName, [In] IStorage pstgPriority, [In] uint grfMode, [In] IntPtr snbExclude, [In] uint reserved, out IStorage ppstg);

        [PreserveSig]
        int CopyTo([In] uint ciidExclude, [In] Guid[] rgiidExclude, [In] IntPtr snbExclude, [In] IStorage pstgDest);

        [PreserveSig]
        int MoveElementTo([In] string pwcsName, [In] IStorage pstgDest, [In] string pwcsNewName, [In] uint grfFlags);

        [PreserveSig]
        int Commit([In] uint grfCommitFlags);

        [PreserveSig]
        int Revert();

        [PreserveSig]
        int EnumElements([In] uint reserved1, [In] IntPtr reserved2, [In] uint reserved3, out IEnumSTATSTG ppEnum);

        [PreserveSig]
        int DestroyElement([In] string pwcsName);

        [PreserveSig]
        int RenameElement([In] string pwcsOldName, [In] string pwcsNewName);

        [PreserveSig]
        int SetElementTimes([In] string pwcsName, [In] System.Runtime.InteropServices.ComTypes.FILETIME[] pctime, [In] System.Runtime.InteropServices.ComTypes.FILETIME[] patime, [In] System.Runtime.InteropServices.ComTypes.FILETIME[] pmtime);

        [PreserveSig]
        int SetClass([In] ref Guid clsid);

        [PreserveSig]
        int SetStateBits([In] uint grfStateBits, [In] uint grfMask);

        [PreserveSig]
        int Stat([Out] out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, [In] uint grfStatFlag);
    }

    [SuppressUnmanagedCodeSecurity]
    [ComImport]
    [Guid(IID.IStream)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IStream
    {
        #region ISequentialStream

        [PreserveSig]
        int Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, out int pcbRead);

        [PreserveSig]
        int Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, out int pcbWritten);

        #endregion

        [PreserveSig]
        int Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

        [PreserveSig]
        int SetSize(long libNewSize);

        [PreserveSig]
        int CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

        [PreserveSig]
        int Commit(int grfCommitFlags);

        [PreserveSig]
        int Revert();

        [PreserveSig]
        int LockRegion(long libOffset, long cb, int dwLockType);

        [PreserveSig]
        int UnlockRegion(long libOffset, long cb, int dwLockType);

        [PreserveSig]
        int Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);

        [PreserveSig]
        int Clone(out IStream ppstm);
    }
}
