using SolidEdgeContrib.Reader.Native;
using System;

namespace SolidEdgeContrib.Reader.Part
{
    /// <summary>
    /// Solid Edge Part Document (.par)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.PartDocument, CLSID.PartDocument, PROGID.PartDocument, "Solid Edge Part Document", ".par")]
    public sealed class PartDocument : SolidEdgeDocument
    {
        internal PartDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static PartDocument Open(string path)
        {
            return Open<PartDocument>(path);
        }
    }
}
