using SolidEdgeCommunity.Reader.Native;
using System;

namespace SolidEdgeCommunity.Reader.Assembly
{
    /// <summary>
    /// Solid Edge Family of Assemblies Document (.asm)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.FamilyOfAssembliesDocument, CLSID.FamilyOfAssembliesDocument, PROGID.FamilyOfAssembliesDocument, "Solid Edge Family of Assemblies Document", ".asm")]
    public sealed class FamilyOfAssembliesDocument : SolidEdgeDocument
    {
        internal FamilyOfAssembliesDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static FamilyOfAssembliesDocument Open(string path)
        {
            return Open<FamilyOfAssembliesDocument>(path);
        }
    }
}
