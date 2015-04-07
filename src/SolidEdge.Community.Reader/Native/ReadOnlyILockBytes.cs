using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdgeCommunity.Reader.Native
{
    internal class ReadOnlyILockBytes : ILockBytes, IDisposable
    {
        private Stream _stream;

        public ReadOnlyILockBytes(Stream stream)
        {
            _stream = stream;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (_stream != null))
            {
                _stream = null;
            }
        }

        void ILockBytes.ReadAt(
            ulong offset,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            [Out]
            byte[] pv,
            int cb,
            out int pcbRead)
        {
            ThrowIfDisposed();
            checked { _stream.Seek((long)offset, SeekOrigin.Begin); }
            pcbRead = _stream.Read(pv, 0, cb);
        }

        void ILockBytes.WriteAt(ulong offset, byte[] pv, int cb, out int pcbWritten)
        {
            throw new NotImplementedException();
        }

        void ILockBytes.Flush()
        {
            throw new NotImplementedException();
        }

        void ILockBytes.SetSize(ulong cb)
        {
            throw new NotImplementedException();
        }

        void ILockBytes.LockRegion(ulong libOffset, ulong cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        void ILockBytes.UnlockRegion(ulong libOffset, ulong cb, int dwLockType)
        {
            throw new NotImplementedException();
        }

        void ILockBytes.Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
        {
            ThrowIfDisposed();

            pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG()
            {
                grfLocksSupported = 0, // No lock supported
                cbSize = _stream.Length,
                type = (int)STGTY.LOCKBYTES
            };
        }

        private void ThrowIfDisposed()
        {
            if (_stream == null)
            {
                throw new ObjectDisposedException(null);
            }
        }
    }
}
