using SolidEdge.Community.Reader.Native;
using System;

namespace SolidEdge.Community.Reader.Part
{
    /// <summary>
    /// Solid Edge Synchronous Sheet Metal Document (.psm)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.SynchronousSheetMetalDocument, CLSID.SynchronousSheetMetalDocument, PROGID.SynchronousSheetMetalDocument, "Solid Edge Synchronous Sheet Metal Document", ".psm")]
    public sealed class SynchronousSheetMetalDocument : SolidEdgeDocument
    {
        internal SynchronousSheetMetalDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static SynchronousSheetMetalDocument Open(string path)
        {
            return Open<SynchronousSheetMetalDocument>(path);
        }
    }
}
