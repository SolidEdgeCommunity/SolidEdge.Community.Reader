using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SolidEdgeContrib.Reader.Native
{
    internal class ReadOnlyILockBytes : ILockBytes, IDisposable
    {
        private Stream _stream;
        private STATSTG _statstg;
        private bool _disposed;

        public ReadOnlyILockBytes(Stream stream)
        {
            _stream = stream;

            _statstg = new STATSTG()
            {
                grfLocksSupported = 0,
                cbSize = _stream.Length,
                type = (int)STGTY.LOCKBYTES
            };

        }

        #region IDisposable

        void IDisposable.Dispose()
        {
            Close();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                try
                {
                    if (_stream != null)
                    {
                        _stream.Close();
                        _stream = null;
                    }
                }
                catch
                {
#if DEBUG
                    System.Diagnostics.Debugger.Break();
#endif
                }
            }

            _disposed = true;
        }

        

        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region ILockBytes

        public void ReadAt(ulong offset, byte[] pv, int cb, out int pcbRead)
        {
            unsafe
            {
                _stream.Seek((checked((long)offset)), SeekOrigin.Begin);
            }

            pcbRead = _stream.Read(pv, 0, cb);
        }

        public void WriteAt(ulong offset, byte[] pv, int cb, out int pcbWritten)
        {
            throw new NotSupportedException();
        }

        public void Flush()
        {
            throw new NotSupportedException();
        }

        public void SetSize(ulong cb)
        {
            throw new NotSupportedException();
        }

        public void LockRegion(ulong libOffset, ulong cb, int dwLockType)
        {
            throw new NotSupportedException();
        }

        public void UnlockRegion(ulong libOffset, ulong cb, int dwLockType)
        {
            throw new NotSupportedException();
        }

        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
        {
            pstatstg = _statstg;
        }

        #endregion
    }
}
