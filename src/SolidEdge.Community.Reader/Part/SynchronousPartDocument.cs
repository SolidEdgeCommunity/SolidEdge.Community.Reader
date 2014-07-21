using SolidEdge.Community.Reader.Native;
using System;

namespace SolidEdge.Community.Reader.Part
{
    /// <summary>
    /// Solid Edge Synchronous Part Document (.par)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.SynchronousPartDocument, CLSID.SynchronousPartDocument, PROGID.SynchronousPartDocument, "Solid Edge Synchronous Part Document", ".par")]
    public sealed class SynchronousPartDocument : SolidEdgeDocument
    {
        internal SynchronousPartDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static SynchronousPartDocument Open(string path)
        {
            return Open<SynchronousPartDocument>(path);
        }
    }
}
