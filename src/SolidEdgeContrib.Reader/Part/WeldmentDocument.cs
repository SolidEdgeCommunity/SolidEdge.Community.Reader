using SolidEdgeContrib.Reader.Native;
using System;

namespace SolidEdgeContrib.Reader.Part
{
    /// <summary>
    /// Solid Edge Weldment Document (.pwd)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.WeldmentDocument, CLSID.WeldmentDocument, PROGID.WeldmentDocument, "Solid Edge Weldment Document", ".pwd")]
    public sealed class WeldmentDocument : SolidEdgeDocument
    {
        internal WeldmentDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static WeldmentDocument Open(string path)
        {
            return Open<WeldmentDocument>(path);
        }
    }
}
