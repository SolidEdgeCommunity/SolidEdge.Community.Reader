using SolidEdge.Community.Reader.Native;
using System;

namespace SolidEdge.Community.Reader.Assembly
{
    /// <summary>
    /// Solid Edge Synchronous Assembly Document (.asm)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.SynchronousAssemblyDocument, CLSID.SynchronousAssemblyDocument, PROGID.SynchronousAssemblyDocument, "Solid Edge Synchronous Assembly Document", ".asm")]
    public sealed class SynchronousAssemblyDocument : SolidEdgeDocument
    {
        internal SynchronousAssemblyDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static SynchronousAssemblyDocument Open(string path)
        {
            return Open<SynchronousAssemblyDocument>(path);
        }
    }
}
