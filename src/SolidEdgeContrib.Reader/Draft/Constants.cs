using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidEdgeContrib.Reader.Draft
{
    internal static class StorageName
    {
        public const string JDraftViewerInfo = "JDraftViewerInfo";
    }

    internal static class StreamName
    {
        public const string JDraftDocumentInfo = "JDraftDocumentInfo";
    }

    public enum SheetOrientation
    {
        Landscape,
        Portrait
    }

    public enum SheetPaperSize
    {
        [SheetPaperSize(0, 0, "Custom")]
        Custom = -2,

        [SheetPaperSize(8.5, 13, "Folio Tall  (8.5\" x 13\")")]
        EngFolioTall = 0,

        [SheetPaperSize(13, 8.5, "Folio Wide  (13\" x 8.5\")")]
        EngFolioWide = 1,

        [SheetPaperSize(8.5, 14, "Legal Tall  (8.5\" x 14\")")]
        EngLegalTall = 2,

        [SheetPaperSize(14, 8.5, "Legal Wide  (14\" x 8.5\")")]
        EngLegalWide = 3,

        [SheetPaperSize(7.5, 10.5, "Executive Tall  (7.5\" x 10.5\")")]
        EngExecutiveTall = 4,

        [SheetPaperSize(10.5, 7.5, "Executive Wide  (10.5\" x 7.5\")")]
        EngExecutiveWide = 5,

        [SheetPaperSize(5.5, 8.5, "Statement Tall  (5.5\" x 8.5\")")]
        EngStatementTall = 6,

        [SheetPaperSize(8.5, 5.5, "Statement Wide  (8.5\" x 5.5\")")]
        EngStatementWide = 7,

        [SheetPaperSize(4.12, 9.5, "Com-10 Tall  (4.125\" x 9.5\")")]
        EngCom10Tall = 8,

        [SheetPaperSize(9.5, 4.12, "Com-10 Wide  (9.5\" x 4.125\")")]
        EngCom10Wide = 9,

        [SheetPaperSize(3.87, 7.5, "Monarch Tall  (3.875\" x 7.5\")")]
        EngMonarchTall = 10,

        [SheetPaperSize(7.5, 3.87, "Monarch Wide  (7.5\" x 3.875\")")]
        EngMonarchWide = 11,

        [SheetPaperSize(8.5, 11, "ANSI A Tall  (8.5\" x 11\")")]
        AnsiATall = 12,

        [SheetPaperSize(11, 8.5, "ANSI A Wide  (11\" x 8.5\")")]
        AnsiAWide = 13,

        [SheetPaperSize(11, 17, "ANSI B Tall  (11\" x 17\")")]
        AnsiBTall = 14,

        [SheetPaperSize(17, 11, "ANSI B Wide  (17\" x 11\")")]
        AnsiBWide = 15,

        [SheetPaperSize(17, 22, "ANSI C Tall  (17\" x 22\")")]
        AnsiCTall = 16,

        [SheetPaperSize(22, 17, "ANSI C Wide  (22\" x 17\")")]
        AnsiCWide = 17,

        [SheetPaperSize(22, 34, "ANSI D Tall  (22\" x 34\")")]
        AnsiDTall = 18,

        [SheetPaperSize(34, 22, "ANSI D Wide  (34\" x 22\")")]
        AnsiDWide = 19,

        [SheetPaperSize(34, 44, "ANSI E Tall  (34\" x 44\")")]
        AnsiETall = 20,

        [SheetPaperSize(44, 34, "ANSI E Wide  (44\" x 34\")")]
        AnsiEWide = 21,

        [SheetPaperSize(24, 36, "ANSI Architectural D Tall  (24\" x 36\")")]
        AnsiArchDTall = 22,

        [SheetPaperSize(36, 24, "ANSI Architectural D Wide  (36\" x 24\")")]
        AnsiArchDWide = 23,

        [SheetPaperSize(36, 48, "ANSI Architectural E Tall  (36\" x 48\")")]
        AnsiArchETall = 24,

        [SheetPaperSize(48, 36, "ANSI Architectural E Wide  (48\" x 36\")")]
        AnsiArchEWide = 25,

        [SheetPaperSize(5.82, 8.26, "ISO A5 Tall  (148mm x 210mm)")]
        IsoA5Tall = 26,

        [SheetPaperSize(8.26, 5.82, "ISO A5 Wide  (210mm x 148mm)")]
        IsoA5Wide = 27,

        [SheetPaperSize(8.26, 11.69, "ISO A4 Tall  (210mm x 297mm)")]
        IsoA4Tall = 28,

        [SheetPaperSize(11.69, 8.26, "ISO A4 Wide  (297mm x 210mm)")]
        IsoA4Wide = 29,

        [SheetPaperSize(11.69, 16.53, "ISO A3 Tall  (297mm x 420mm)")]
        IsoA3Tall = 30,

        [SheetPaperSize(16.53, 11.69, "ISO A3 Wide  (420mm x 297mm)")]
        IsoA3Wide = 31,

        [SheetPaperSize(16.53, 23.38, "ISO A2 Tall  (420mm x 594mm)")]
        IsoA2Tall = 32,

        [SheetPaperSize(23.38, 16.53, "ISO A2 Wide  (594mm x 420mm)")]
        IsoA2Wide = 33,

        [SheetPaperSize(23.38, 33.11, "ISO A1 Tall  (594mm x 841mm)")]
        IsoA1Tall = 34,

        [SheetPaperSize(33.11, 23.38, "ISO A1 Wide  (841mm x 594mm)")]
        IsoA1Wide = 35,

        [SheetPaperSize(33.11, 46.81, "ISO A0 Tall  (841mm x 1189mm)")]
        IsoA0Tall = 36,

        [SheetPaperSize(46.81, 33.11, "ISO A0 Wide  (1189mm x 841mm)")]
        IsoA0Wide = 37,

        [SheetPaperSize(7.16, 10.11, "ISO B5 Tall  (182mm x 257mm)")]
        IsoB5Tall = 38,

        [SheetPaperSize(10.11, 7.16, "ISO B5 Wide  (257mm x 182mm)")]
        IsoB5Wide = 39,

        [SheetPaperSize(9.84, 13.93, "ISO B4 Tall  (250mm x 354mm)")]
        IsoB4Tall = 40,

        [SheetPaperSize(13.93, 9.84, "ISO B4 Wide  (354mm x 250mm)")]
        IsoB4Wide = 41,

        [SheetPaperSize(6.37, 9.01, "ISO C5 Tall  (162mm x 229mm)")]
        IsoC5Tall = 42,

        [SheetPaperSize(9.01, 6.37, "ISO C5 Wide  (229mm x 162mm)")]
        IsoC5Wide = 43,

        [SheetPaperSize(4.33, 8.66, "ISO DL Tall  (110mm x 220mm)")]
        IsoDLTall = 44,

        [SheetPaperSize(8.66, 4.33, "ISO DL Wide  (220mm x 110mm)")]
        IsoDLWide = 45,

        [SheetPaperSize(8.46, 10.82, "ISO Quatro Tall  (215mm x 275mm)")]
        IsoQuatroTall = 46,

        [SheetPaperSize(10.82, 8.46, "ISO Quatro Wide  (275mm x 215mm)")]
        IsoQuatroWide = 47
    }

    public enum SheetPaperUnit : ushort
    {
        Unknown = 0,
        Millimeters = 0x3D,
        Centimeters = 0x3E,
        Inches = 0x40
    }
}
