using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SolidEdgeContrib.Reader
{
    /// <summary>
    /// Class IDs
    /// </summary>
    public static partial class CLSID
    {
        public const string AssemblyDocument = "00C6BF00-483B-11CE-951A-08003601BE52";
        public const string ConfigFileExtension = "00000000-0000-0000-0000-000000000000";
        public const string FamilyOfAssembliesDocument = "04D613A0-A322-40B5-A2A4-36CA0DE6F5D9";
        public const string SynchronousAssemblyDocument = "213A04B0-815B-4ADE-97D6-859BDA683413";
        public const string DraftDocument = "016B11FB-CDC0-11CE-A035-08003601E53B";
        public const string MarkupDocument = "64E909E5-4ACC-496C-8E4B-A660DC6A56EC";
        public const string PartDocument = "23C52E80-4698-11CE-B307-0800363A1E02";
        public const string SheetMetalDocument = "DD8522E0-2375-11D0-AC05-080036FD1802";
        public const string SynchronousPartDocument = "5854E4B9-DA16-415B-AED5-F6BA2EC08C9B";
        public const string SynchronousSheetMetalDocument = "F779EDBF-EC73-4D27-9CA9-F96F58267AD1";
        public const string WeldmentDocument = "98CCDF9C-213B-11D4-B64C-00C04F79B2BF";

        //public static readonly Guid CLSID_AssemblyDocument = new Guid(AssemblyDocument);
        //public static readonly Guid CLSID_ConfigFileExtension = new Guid(ConfigFileExtension);
        //public static readonly Guid CLSID_FamilyOfAssembliesDocument = new Guid(FamilyOfAssembliesDocument);
        //public static readonly Guid CLSID_SynchronousAssemblyDocument = new Guid(SynchronousAssemblyDocument);
        //public static readonly Guid CLSID_DraftDocument = new Guid(DraftDocument);
        //public static readonly Guid CLSID_MarkupDocument = new Guid(MarkupDocument);
        //public static readonly Guid CLSID_PartDocument = new Guid(PartDocument);
        //public static readonly Guid CLSID_SheetMetalDocument = new Guid(SheetMetalDocument);
        //public static readonly Guid CLSID_SynchronousPartDocument = new Guid(SynchronousPartDocument);
        //public static readonly Guid CLSID_SynchronousSheetMetalDocument = new Guid(SynchronousSheetMetalDocument);
        //public static readonly Guid CLSID_WeldmentDocument = new Guid(WeldmentDocument);
    }

    public enum DocumentType
    {
        AssemblyDocument,
        ConfigFileExtension,
        DraftDocument,
        FamilyOfAssembliesDocument,
        PartDocument,
        SheetMetalDocument,
        SynchronousAssemblyDocument,
        SynchronousPartDocument,
        SynchronousSheetMetalDocument,
        WeldmentDocument,
        MarkupDocument,
        Unknown
    }

    public enum DocumentStatus
    {
		Available = 0,
		InWork = 1,
		InReview = 2,
		Released = 3,
		Baselined = 4,
		Obsolete = 5,
		Unknown = 6
    }

    /// <summary>
    /// Format identifiers
    /// </summary>
    internal static partial class FMTID
    {
        public const string ExtendedSummaryInformation = "cc024fa2-6eb5-11ce-8aa2-08003601e988";
        public const string MechanicalModeling = "cc024fca-6eb5-11ce-8aa2-08003601e988";
        public const string ProjectInformation = "f0d6d0b1-a0d8-11ce-8aa2-08003601e988";
        public const string VersionInformation = "1D60E652-7833-11D3-B83D-00C04F79B2C2";

        public static readonly Guid FMTID_ExtendedSummaryInformation = new Guid(ExtendedSummaryInformation);
        public static readonly Guid FMTID_MechanicalModeling = new Guid(MechanicalModeling);
        public static readonly Guid FMTID_ProjectInformation = new Guid(ProjectInformation);
        public static readonly Guid FMTID_VersionInformation = new Guid(VersionInformation);
    }

    /// <summary>
    /// Property IDs for the ExtendedSummaryInformation Property Set.
    /// </summary>
    internal enum ExtendedSummaryInformationConstants : uint
    {
        APPNAME = 2,
        DOCUMENT_ID = 6,
        STATUS = 7,
        USERNAME = 8,
        CREATION_LOCALE = 9,
        LARGE_DIB = 10,
        SMALL_DIB = 11,
        UNKNOWN_13 = 13,
        HARDWARE = 14,
        DOC_CONTENT_TYPE = 16,
        UNKNOWN_18 = 18
    }

    /// <summary>
    /// Property IDs for the MechanicalModeling Property Set.
    /// </summary>
    internal enum MechanicalModelingConstants : uint
    {
        MATERIAL = 3
    }

    /// <summary>
    /// Property IDs for the ProjectInformation Property Set.
    /// </summary>
    internal enum ProjectInformationConstants : uint
    {
        DOCUMENT_NUMBER = 2,
        REVISION = 3,
        PROJECT_NAME = 4
    }

    ///// <summary>
    ///// Property IDs for the VersionInformation Property Set.
    ///// </summary>
    //internal enum PIDVI : uint
    //{
    //    UNKNOWN_14 = 14
    //}

    public static partial class PROGID
    {
        public const string AssemblyDocument = "SolidEdge.AssemblyDocument";
        public const string ConfigFileExtension = "SolidEdge.ConfigFileExtension";
        public const string FamilyOfAssembliesDocument = "SolidEdge.FamilyOfAssembliesDocument";
        public const string SynchronousAssemblyDocument = "SolidEdge.DMAssemblyDocument";
        public const string DraftDocument = "SolidEdge.DraftDocument";
        public const string MarkupDocument = "SmartView.SEMarkupDocument";
        public const string PartDocument = "SolidEdge.PartDocument";
        public const string SheetMetalDocument = "SolidEdge.SheetMetalDocument";
        public const string SynchronousPartDocument = "SolidEdge.DMPartDocument";
        public const string SynchronousSheetMetalDocument = "SolidEdge.DMSheetMetalDocument";
        public const string WeldmentDocument = "SolidEdge.WeldmentDocument";
    }

    internal static class StreamName
    {
        public const string Attachments = "Attachments";
        public const string BuildVersions = "BuildVersions";
        public const string JSitesList = "JSitesList";
    }
}
