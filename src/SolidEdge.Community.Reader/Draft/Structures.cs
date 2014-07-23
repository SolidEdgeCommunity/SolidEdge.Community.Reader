using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SolidEdgeCommunity.Reader.Draft
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DRAFTDOCUMENTINFO
    {
#pragma warning disable 0649
        public uint ViewerInfoVersion;
        public int NumberOfSheets;
        public int ActiveSheetIndex;
        public uint GeometricVersion;
        public SheetPaperUnit Units;
#pragma warning restore 0649
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct SHEETINFO
    {
        public double width;
        public double height;
        public uint emfSize;
        public uint emfCompressedSize;
    };

    public struct SheetPaperSizeInfo
    {
        public SheetPaperSize PaperSize;
        public double Width;
        public double Height;
        public string Name;
        public string Description;

        public SheetPaperSizeInfo(SheetPaperSize paperSize)
        {
            PaperSize = paperSize;
            Width = 0;
            Height = 0;
            Name = paperSize.ToString();
            Description = String.Empty;

            FieldInfo field = typeof(SheetPaperSize).GetField(paperSize.ToString());

            if (field != null)
            {
                SheetPaperSizeAttribute attribute = field.GetSheetPaperSizeAttribute();

                if (attribute != null)
                {
                    Description = attribute.Description;
                    Width = attribute.Width;
                    Height = attribute.Height;
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
