using SolidEdgeContrib.Reader.Native;
using System;

namespace SolidEdgeContrib.Reader.Part
{
    /// <summary>
    /// Solid Edge Sheet Metal Document (.psm)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.SheetMetalDocument, CLSID.SheetMetalDocument, PROGID.SheetMetalDocument, "Solid Edge Sheet Metal Document", ".psm")]
    public sealed class SheetMetalDocument : SolidEdgeDocument
    {
        internal SheetMetalDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static SheetMetalDocument Open(string path)
        {
            return Open<SheetMetalDocument>(path);
        }
    }
}
