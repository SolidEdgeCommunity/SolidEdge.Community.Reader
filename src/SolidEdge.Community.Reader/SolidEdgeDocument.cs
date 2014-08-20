using SolidEdgeCommunity.Reader.Assembly;
using SolidEdgeCommunity.Reader.Draft;
using SolidEdgeCommunity.Reader.Native;
using SolidEdgeCommunity.Reader.Part;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SolidEdgeCommunity.Reader
{
    public abstract partial class SolidEdgeDocument : CompoundFile
    {
        [DllImport("gdi32.dll", EntryPoint = "GetDIBits")]
        static extern int GetDIBits([In] IntPtr hdc, [In] IntPtr hbmp, uint uStartScan, uint cScanLines, [Out] byte[] lpvBits, ref BITMAPINFO lpbi, int uUsage);

        private Version _createdVersion;
        private Version _lastSavedVersion;

        internal SolidEdgeDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
            //HRESULT hr = HRESULT.S_OK;
            //System.Runtime.InteropServices.ComTypes.STATSTG[] elements;
            //if (NativeMethods.Succeeded(hr = storage.EnumElements(out elements)))
            //{
            //    foreach (System.Runtime.InteropServices.ComTypes.STATSTG element in elements)
            //    {
            //        Console.WriteLine("{0} - {1}", element.pwcsName, element.clsid);
            //    }
            //}
        }

        internal static SolidEdgeDocument InvokeConstructor(Type type, IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
        {
            ConstructorInfo ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(IStorage), typeof(System.Runtime.InteropServices.ComTypes.STATSTG) }, null);

            if (ctor != null)
            {
                return (SolidEdgeDocument)ctor.Invoke(new object[] { storage, statstg });
            }
            else
            {
                Marshal.ThrowExceptionForHR(HRESULT.CO_E_CANTDETERMINECLASS);
            }

            return null;
        }

        internal static SolidEdgeDocument FromIStorage(IStorage storage)
        {
            if (storage == null) throw new ArgumentNullException("storage");

            SolidEdgeDocument document = null;
            System.Runtime.InteropServices.ComTypes.STATSTG statstg = statstg = storage.GetStatistics();

            // For now there is a 1 - 1 mapping.
            Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetSolidEdgeDocumentTypes(statstg.clsid);

            if (types.Length == 0)
            {
                Marshal.ThrowExceptionForHR(HRESULT.CO_E_CANTDETERMINECLASS);
            }
            else if (types.Length == 1)
            {
                document = InvokeConstructor(types[0], storage, statstg);
            }
            else // Future possibility for multiple types. i.e Guid.Empty
            {
                Marshal.ThrowExceptionForHR(HRESULT.CO_E_CANTDETERMINECLASS);
            }

            return document;
        }

        protected virtual void LoadBuildVersions()
        {
            BUILDVERSIONS buildVersions = default(BUILDVERSIONS);

            if (NativeMethods.Succeeded(RootStorage.ReadStructure<BUILDVERSIONS>(StreamName.BuildVersions, out buildVersions)))
            {
                _createdVersion = buildVersions.CreatedVersion.ToVersion();
                _lastSavedVersion = buildVersions.LastSavedVersion.ToVersion();
            }
        }

        protected virtual DocumentStatus GetDocumentStatus()
        {
            if (ExtendedSummaryInformation != null)
            {
                return (DocumentStatus)ExtendedSummaryInformation.Status;
            }
            else
            {
                return DocumentStatus.Unknown;
            }
        }

        protected virtual DocumentType GetDocumentType()
        {
            Type type = this.GetType();
            SolidEdgeDocumentAttribute attribute = type.GetSolidEdgeDocumentAttribute();

            if (attribute != null)
            {
                return attribute.DocumentType;
            }

            return DocumentType.Unknown;
        }

        public static Version GetCreatedVersion(string path)
        {
            using (SolidEdgeDocument document = SolidEdgeDocument.Open(path))
            {
                return document.CreatedVersion;
            }
        }

        public static Version GetLastSavedVersion(string path)
        {
            using (SolidEdgeDocument document = SolidEdgeDocument.Open(path))
            {
                return document.LastSavedVersion;
            }
        }

        public static DocumentStatus GetStatus(string path)
        {
            using (SolidEdgeDocument document = SolidEdgeDocument.Open(path))
            {
                return document.Status;
            }
        }

        // Getting inconsistent results. Need to work on code.
        public Bitmap GetThumbnail()
        {
            Bitmap bitmap = null;
            PROPVARIANT propvar = default(PROPVARIANT);

            try
            {
                Marshal.ThrowExceptionForHR(GetPropertyValue(FMTID.FMTID_ExtendedSummaryInformation, (uint)ExtendedSummaryInformationConstants.LARGE_DIB, out propvar));

                if (propvar.Type == VarEnum.VT_CF)
                {
                    // Get CLIPDATA.
                    CLIPDATA clipdata = propvar.GetCLIPDATA();

                    // built-in Windows format
                    if (clipdata.ulClipFmt == -1)
                    {
                        // Pointer to BITMAPINFOHEADER.
                        IntPtr pBIH = clipdata.pClipData;
                        bitmap = DibToBitmap.Convert(pBIH);
                    }
                    else
                    {
                        Console.WriteLine("CLIP FORMAT ERROR");
                        Marshal.ThrowExceptionForHR(HRESULT.DV_E_CLIPFORMAT);
                    }
                }
                else
                {
                    Console.WriteLine("INVALID VARIANT ERROR");
                    Marshal.ThrowExceptionForHR(HRESULT.ERROR_INVALID_VARIANT);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                propvar.Clear();
            }

            return bitmap;
        }

        public new static SolidEdgeDocument Open(string path)
        {
            SolidEdgeDocument document = null;
            IStorage storage = null;

            try
            {
                storage = CompoundFile.OpenStorage(path);
                document = FromIStorage(storage);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (document == null)
                {
                    if (storage != null)
                    {
                        storage.FinalRelease();
                    }
                }
            }

            return document;
        }

        public static SolidEdgeDocument Open(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            IStorage storage = CompoundFile.OpenStorage(stream);
            bool flag = false;

            try
            {
                return FromIStorage(storage);
            }
            catch
            {
                flag = true;
                throw;
            }
            finally
            {
                if ((flag) && (storage != null))
                {
                    storage.FinalRelease();
                }
            }
        }

        internal static T Open<T>(string path)
        {
            SolidEdgeDocument document = null;
            bool flag = false;

            try
            {
                document = Open(path);
                return (T)Convert.ChangeType(document, typeof(T));
            }
            catch
            {
                flag = true;
                throw;
            }
            finally
            {
                if ((flag) && (document != null))
                {
                    document.Close();
                }
            }
        }

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Version CreatedVersion
        {
            get
            {
                // Note that build versions are delay loaded.
                if (_createdVersion == null)
                {
                    LoadBuildVersions();
                }

                return _createdVersion;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DocumentType DocumentType { get { return GetDocumentType(); } }

        /// <summary>
        /// 
        /// </summary>
        public Version LastSavedVersion
        {
            get
            {
                // Note that build versions are delay loaded.
                if (_lastSavedVersion == null)
                {
                    LoadBuildVersions();
                }

                return _lastSavedVersion;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DocumentStatus Status { get { return GetDocumentStatus(); } }

        /// <summary>
        /// 
        /// </summary>
        public ExtendedSummaryInformationPropertySet ExtendedSummaryInformation { get { return PropertySets.OfType<ExtendedSummaryInformationPropertySet>().FirstOrDefault(); } }

        /// <summary>
        /// 
        /// </summary>
        public MechanicalModelingPropertySet MechanicalModeling { get { return PropertySets.OfType<MechanicalModelingPropertySet>().FirstOrDefault(); } }

        /// <summary>
        /// 
        /// </summary>
        public ProjectInformationPropertySet ProjectInformation { get { return PropertySets.OfType<ProjectInformationPropertySet>().FirstOrDefault(); } }

        #endregion
    }
}
