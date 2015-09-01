using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdgeCommunity.Reader.Native
{
    internal class ReadOnlyIStorage : IStorage, IPropertySetStorage, IDisposable
    {
        private IStorage _storage;
        private ReadOnlyILockBytes _readOnlyILockBytes;

        internal ReadOnlyIStorage(IStorage storage)
                : this(storage, null)
        {
        }

        internal ReadOnlyIStorage(IStorage storage, ReadOnlyILockBytes lockBytesStream)
        {
            if (storage == null)
            {
                throw new ArgumentNullException("storage");
            }

            _storage = storage;
            _readOnlyILockBytes = lockBytesStream;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (_storage != null))
                {
                    Marshal.FinalReleaseComObject(_storage);

                    if (_readOnlyILockBytes != null)
                    {
                        _readOnlyILockBytes.Dispose();
                    }
                }
            }
            finally
            {
                _storage = null;
                _readOnlyILockBytes = null;
            }
        }

        #region IStorage

        int IStorage.CreateStream(string pwcsName, uint grfMode, uint reserved1, uint reserved2, out System.Runtime.InteropServices.ComTypes.IStream ppstm)
        {
            throw new NotImplementedException();
        }

        int IStorage.OpenStream(string pwcsName, int reserved1, uint grfMode, uint reserved2, out IStream ppstm)
        {
            return _storage.OpenStream(pwcsName, reserved1, grfMode, reserved2, out ppstm);
        }

        int IStorage.CreateStorage(string pwcsName, uint grfMode, uint reserved1, uint reserved2, out IStorage ppstg)
        {
            throw new NotImplementedException();
        }

        int IStorage.OpenStorage(string pwcsName, IStorage pstgPriority, uint grfMode, IntPtr snbExclude, uint reserved, out IStorage ppstg)
        {
            return _storage.OpenStorage(pwcsName, pstgPriority, grfMode, snbExclude, reserved, out ppstg);
        }

        int IStorage.CopyTo(uint ciidExclude, Guid[] rgiidExclude, IntPtr snbExclude, IStorage pstgDest)
        {
            return _storage.CopyTo(ciidExclude, rgiidExclude, snbExclude, pstgDest);
        }

        int IStorage.MoveElementTo(string pwcsName, IStorage pstgDest, string pwcsNewName, uint grfFlags)
        {
            return _storage.MoveElementTo(pwcsName, pstgDest, pwcsNewName, grfFlags);
        }

        int IStorage.Commit(uint grfCommitFlags)
        {
            throw new NotImplementedException();
        }

        int IStorage.Revert()
        {
            throw new NotImplementedException();
        }

        int IStorage.EnumElements(uint reserved1, IntPtr reserved2, uint reserved3, out IEnumSTATSTG ppEnum)
        {
            return _storage.EnumElements(reserved1, reserved2, reserved3, out ppEnum);
        }

        int IStorage.DestroyElement(string pwcsName)
        {
            throw new NotImplementedException();
        }

        int IStorage.RenameElement(string pwcsOldName, string pwcsNewName)
        {
            throw new NotImplementedException();
        }

        int IStorage.SetElementTimes(string pwcsName, System.Runtime.InteropServices.ComTypes.FILETIME[] pctime, System.Runtime.InteropServices.ComTypes.FILETIME[] patime, System.Runtime.InteropServices.ComTypes.FILETIME[] pmtime)
        {
            throw new NotImplementedException();
        }

        int IStorage.SetClass(ref Guid clsid)
        {
            throw new NotImplementedException();
        }

        int IStorage.SetStateBits(uint grfStateBits, uint grfMask)
        {
            throw new NotImplementedException();
        }

        int IStorage.Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, uint grfStatFlag)
        {
            return _storage.Stat(out pstatstg, grfStatFlag);
        }

        #endregion

        #region IPropertySetStorage

        int IPropertySetStorage.Create(ref Guid rfmtid, ref Guid pClsid, uint grfFlags, uint grfMode, out IPropertyStorage ppprstg)
        {
            throw new NotImplementedException();
        }

        int IPropertySetStorage.Open(ref Guid rfmtid, uint grfMode, out IPropertyStorage ppprstg)
        {
            return ((IPropertySetStorage)_storage).Open(rfmtid, grfMode, out ppprstg);
        }

        int IPropertySetStorage.Delete(ref Guid rfmtid)
        {
            throw new NotImplementedException();
        }

        int IPropertySetStorage.Enum(out IEnumSTATPROPSETSTG ppEnum)
        {
            return ((IPropertySetStorage)_storage).Enum(out ppEnum);
        }

        #endregion
    }
}
