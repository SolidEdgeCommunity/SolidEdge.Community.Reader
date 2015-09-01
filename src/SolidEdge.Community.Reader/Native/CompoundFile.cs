using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdgeCommunity.Reader.Native
{
    public class CompoundFile : IDisposable
    {
        private IStorage _rootStorage;
        protected System.Runtime.InteropServices.ComTypes.STATSTG _statstg;
        protected bool _disposed;
        private PropertySets _propertySets;

        internal CompoundFile(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
        {
            if (storage == null) { throw new ArgumentNullException("storage"); }

            _rootStorage = storage;
            _statstg = statstg;
        }

        #region IDisposable implementation

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
                    if (_rootStorage != null)
                    {

                        if (Marshal.IsComObject(_rootStorage))
                        {
                            _rootStorage.FinalRelease();
                        }
                        else if (_rootStorage is ReadOnlyIStorage)
                        {
                            ((IDisposable)_rootStorage).Dispose();
                        }

                        _rootStorage = null;
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

        #endregion

        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static CompoundFile Open(string path)
        {
            IStorage storage = OpenStorage(path);
            System.Runtime.InteropServices.ComTypes.STATSTG statstg = storage.GetStatistics();
            return new CompoundFile(storage, statstg);
        }

        internal static IStorage OpenStorage(Stream stream)
        {
            IStorage storage = null;
            uint grfMode = (uint)(STGM.READ | STGM.SHARE_EXCLUSIVE);

            ReadOnlyILockBytes readOnlyLockBytes = new ReadOnlyILockBytes(stream);            
            Marshal.ThrowExceptionForHR(NativeMethods.StgOpenStorageOnILockBytes(readOnlyLockBytes, null, grfMode, IntPtr.Zero, 0, out storage));

            return new ReadOnlyIStorage(storage, readOnlyLockBytes);
        }

        internal static IStorage OpenStorage(string path)
        {
            IStorage storage = null;
            //uint grfMode = (uint)(STGM.DIRECT | STGM.READ | STGM.SHARE_DENY_WRITE); // Old
            uint grfMode = (uint)(STGM.READ | STGM.TRANSACTED | STGM.SHARE_DENY_NONE);

            uint stgfmt = (uint)STGFMT.DOCFILE; //Indicates that the file must be a compound file.
            Guid iid = IID.IID_IStorage;
            
            if (NativeMethods.StgIsStorageFile(path) == HRESULT.S_OK)
            {
                Marshal.ThrowExceptionForHR(NativeMethods.StgOpenStorageEx(path, grfMode, stgfmt, 0, IntPtr.Zero, IntPtr.Zero, ref iid, out storage));
            }
            else
            {
                Marshal.ThrowExceptionForHR(HRESULT.STG_E_INVALIDHEADER);
            }

            return storage;
        }

        internal int GetPropertyValue(Guid fmtid, uint propid, out PROPVARIANT propvar)
        {
            IPropertySetStorage propertySetStorage = (IPropertySetStorage)_rootStorage;
            return propertySetStorage.GetPropertyValue(fmtid, propid, out propvar);
        }

        public Guid ClassId { get { return _statstg.clsid; } }
        public DateTime Created { get { return _statstg.ctime.ToDateTime(); } }
        public string FileName { get { return _statstg.pwcsName; } }
        public DateTime LastAccessed { get { return _statstg.atime.ToDateTime(); } }
        public DateTime LastModified { get { return _statstg.mtime.ToDateTime(); } }
        internal IStorage RootStorage { get { return _rootStorage; } }

        public PropertySets PropertySets
        {
            get
            {
                // Note that property sets are delay loaded.
                if (_propertySets == null)
                {
                    _propertySets = new PropertySets((IPropertySetStorage)_rootStorage);
                }
                
                return _propertySets; }
        }

        public DocumentSummaryInformationPropertySet DocumentSummaryInformation { get { return PropertySets.OfType<DocumentSummaryInformationPropertySet>().FirstOrDefault(); } }
        public SummaryInformationPropertySet SummaryInformation { get { return PropertySets.OfType<SummaryInformationPropertySet>().FirstOrDefault(); } }
        public CustomPropertySet CustomProperties { get { return PropertySets.OfType<CustomPropertySet>().FirstOrDefault(); } }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(_statstg.pwcsName) == false)
            {
                return _statstg.pwcsName;
            }

            return base.ToString();
        }
    }
}
