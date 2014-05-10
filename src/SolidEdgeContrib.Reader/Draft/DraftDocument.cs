//using SolidEdgeContrib.Reader.Native.zlib.Zip.Compression.Streams;
using SolidEdgeContrib.Reader.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdgeContrib.Reader.Draft
{
    /// <summary>
    /// Solid Edge Draft Document (.dft)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.DraftDocument, CLSID.DraftDocument, PROGID.DraftDocument, "Solid Edge Draft Document", ".dft")]
    public sealed class DraftDocument : SolidEdgeDocument
    {
        private Sheet[] _sheets = null;
        internal DRAFTDOCUMENTINFO _documentInfo;

        internal DraftDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static DraftDocument Open(string path)
        {
            return Open<DraftDocument>(path);
        }

        internal MemoryStream GetEmfMemoryStream(Sheet sheet)
        {
            HRESULT hr = HRESULT.E_FAIL;
            IStorage viewerStorage = null;
            IStream emzStream = null;
            MemoryStream memoryStream = null;

            try
            {
                Marshal.ThrowExceptionForHR(hr = RootStorage.OpenStorage(StorageName.JDraftViewerInfo, out viewerStorage));
                Marshal.ThrowExceptionForHR(hr = viewerStorage.OpenStream(sheet.StreamName, out emzStream));
                memoryStream = emzStream.DecompressZlib();
            }
            catch
            {
            }
            finally
            {
                if (emzStream != null)
                {
                    emzStream.FinalRelease();
                }
                if (viewerStorage != null)
                {
                    viewerStorage.FinalRelease();
                }
            }

            return memoryStream;
        }

        private void LoadSheets()
        {
            List<Sheet> list = new List<Sheet>();
            MemoryStream memoryStream = null;

            try
            {
                memoryStream = RootStorage.StreamToMemoryStream(StreamName.JDraftDocumentInfo, StorageName.JDraftViewerInfo);

                _documentInfo = memoryStream.ReadStructure<DRAFTDOCUMENTINFO>();
                
                for (int i = 0; i < _documentInfo.NumberOfSheets; i++)
                {
                    int sheetNameLength;
                    string name;

                    sheetNameLength = memoryStream.ReadInt32();
                    sheetNameLength *= 2;

                    name = memoryStream.ReadString(Encoding.Unicode, sheetNameLength, true);

                    SHEETINFO sheetInfo = memoryStream.ReadStructure<SHEETINFO>();

                    list.Add(new Sheet(this, i + 1, name, _documentInfo.Units, sheetInfo));
                }
            }
            catch
            {
                // Swallow exception.
#if DEBUG
                System.Diagnostics.Debugger.Break();
#endif
            }
            finally
            {
                if (memoryStream != null)
                {
                    memoryStream.Close();
                }
            }

            _sheets = list.ToArray();
        }

        public Sheet[] Sheets
        {
            get
            {
                if (_sheets == null)
                {
                    LoadSheets();
                }

                return _sheets;
            }
        }
    }
}
