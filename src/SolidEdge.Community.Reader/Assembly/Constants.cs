using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidEdgeCommunity.Reader.Assembly
{
    static class StorageName
    {
        public const string Configs = "Configs";
        public const string Zones = "Zones";
    }

    internal static class StreamName
    {
        public const string Attachments = "Attachments";
        public const string JSitesList = "JSitesList";
    }

    //[Flags]
    //internal enum AttachmentFlags
    //{
    //    ExcludeFromBOM = 1,
    //    IsDisplayable = 2,
    //    IsLocatable = 4,
    //    IsTopLevel = 8,
    //    IsPart = 16,
    //    IsAssembly = 32,
    //    IsReferenceOnly = 64,
    //    LocateControl = 128,
    //    HasAlteredMatrix = 256,
    //    IsBeingPlaced = 512,
    //    IsMarked = 1024,
    //    IsForeign = 2048,
    //    HasDeletedConstraint = 4096,
    //    ReferencePlanesOn = 8192,
    //    IsCollapsed = 16384,
    //    HasUserDefinedName = 32768,
    //    ConstrainedSequentially = 0x00010000,
    //    HideInDrawing = 0x00020000,
    //    HideInSubAssembly = 0x00040000,
    //    ExcludeFromPhysProps = 0x00080000,
    //    ExplodeAsSingleEntity = 0x00100000,
    //    IsMasterPart = 0x00200000,
    //    IsSlavePart = 0x00400000,
    //    IsLitePart = 0x00800000,
    //    UseSimplified = 0x01000000,
    //    IgnoreViewFilter = 0x02000000,
    //    IsTransparentInDraft = 0x04000000,
    //    CoordSysOn = 0x08000000,
    //    SketchesOn = 0x10000000,
    //    ConstructionsOff = 0x20000000,
    //    ReferenceAxesOn = 0x40000000,
    //    ConstructionCurvesOff = unchecked((int)0x80000000)
    //}

    //internal enum AttachmentExtendedFlags
    //{
    //    IsTubePart = 1,
    //    IsDirectedPart = 2,
    //    TubePathIsSick = 4,
    //    IsEditPlacement = 8,
    //    DoNotUnload = 16,
    //    ExExcludeReference = 64,
    //    SuppressConstructions = 128,
    //    IsWirePart = 256,
    //    WireIsSick = 512,
    //    UseDisplayInComponent = 1024,
    //    IsPipeFitting = 2048,
    //    IsFlexiblePipe = 4096,
    //    IsFlexibleAssembly = 8192,
    //    IsFramePart = 16384,
    //    IsAdjustPart = 32768,
    //    IsBoltedConnection = 0x00010000,
    //    IsSlaveBoltedConnPart = 0x00020000,
    //    TreatAsSoftReference = 0x00040000,
    //    ExcludeFromInterference = 0x00080000,
    //    IsGenericAssemblyBody = 0x00100000,
    //    IsWeldBead = 0x00200000,
    //    IsHarnessWire = 0x00400000,
    //    IsHarnessCable = 0x00800000,
    //    IsHarnessBundle = 0x01000000
    //}
}
