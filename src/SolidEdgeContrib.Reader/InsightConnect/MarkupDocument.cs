using SolidEdgeContrib.Reader.Native;
using System;

namespace SolidEdgeContrib.Reader.InsightConnect
{
    /// <summary>
    /// Insight Connect Markup Document (.pcf)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.MarkupDocument, CLSID.MarkupDocument, PROGID.MarkupDocument, "Insight Connect Markup Document", ".pcf")]
    public sealed class MarkupDocument : SolidEdgeDocument
    {
        internal MarkupDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static MarkupDocument Open(string path)
        {
            return Open<MarkupDocument>(path);
        }
    }
}
