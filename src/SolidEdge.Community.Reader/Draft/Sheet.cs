using SolidEdgeCommunity.Reader.Native;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace SolidEdgeCommunity.Reader.Draft
{
    public sealed class Sheet
    {
        private DraftDocument _draftFile;
        private int _index;
        private string _name;
        private SheetPaperUnit _units = default(SheetPaperUnit);
        private SHEETINFO _info;
        
        internal Sheet(DraftDocument draftFile, int index, string name, SheetPaperUnit units, SHEETINFO info)
        {
            _draftFile = draftFile;
            _index = index;
            _name = name;
            _units = units;
            _info = info;
        }

        public int Index { get { return _index; } }
        internal string StreamName { get { return _index.ToString(); } }
        internal uint MetafileSize { get { return _info.emfSize; } }
        internal uint MetafileCompressedSize { get { return _info.emfCompressedSize; } }
        public string Name { get { return _name; } }
        public double Width { get { return _info.width; } }
        public double Height { get { return _info.height; } }
        public SheetPaperUnit PaperUnit { get { return _units; } }

        public SheetOrientation Orientation
        {
            get
            {
                return (Height / Width) > 1 ? SheetOrientation.Landscape : SheetOrientation.Portrait;
            }
        }

        public SheetPaperSizeInfo PaperSizeInfo
        {
            get
            {
                double factor = 1;

                switch (_draftFile._documentInfo.Units)
                {
                    case SheetPaperUnit.Inches:
                        factor = 1;
                        break;
                    case SheetPaperUnit.Millimeters:
                        factor = 25.4;
                        break;
                    case SheetPaperUnit.Centimeters:
                        factor = 2.54;
                        break;
                }

                double width = Width / factor;
                double height = Height / factor;

                for (int i = 0; i < SheetPaperSizes.Length; i++)
                {
                    if (SheetPaperSizes[i].PaperSize == SheetPaperSize.Custom) continue;

                    if ((Math.Abs(width - SheetPaperSizes[i].Width) <= 0.01) && (Math.Abs(height - SheetPaperSizes[i].Height) <= 0.01))
                    {
                        return SheetPaperSizes[i];
                    }
                }

                return SheetPaperSizes[0];
            }
        }

        /// <summary>
        /// Returns an IntPtr of type HENHMETAFILE by calling SetEnhMetaFileBits().
        /// </summary>
        /// <remarks>
        /// The returned handle must be freed using DeleteEnhMetaFile() either directly or indirectly or you will leak memory.
        /// </remarks>
        public IntPtr GetHENHMETAFILE()
        {
            using (MemoryStream emfMemoryStream = _draftFile.GetEmfMemoryStream(this))
            {
                byte[] data = emfMemoryStream.ToArray();
                return NativeMethods.SetEnhMetaFileBits((uint)data.Length, data);
            }
        }

        /// <summary>
        /// Returns a System.Drawing.Imaging.Metafile.
        /// </summary>
        /// <remarks>
        /// Be sure to Dispose() the returned metafile when it is no longer needed or you will leak memory.
        /// </remarks>
        public Metafile GetMetafile()
        {
            var pEmf = GetHENHMETAFILE();
            return new Metafile(pEmf, true);
        }

        /// <summary>
        /// Returns a byte array containing the EMF data.
        /// </summary>
        /// <returns></returns>
        public byte[] GetMetafileBytes()
        {
            using (MemoryStream emfMemoryStream = _draftFile.GetEmfMemoryStream(this))
            {
                return emfMemoryStream.ToArray();
            }
        }

        /// <summary>
        /// Writes the EMF bytes directly to a file.
        /// </summary>
        /// <param name="path"></param>
        public void SaveAsEmf(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            // Ensure the folder path exists.
            fileInfo.Directory.Create();

            using (MemoryStream emfMemoryStream = _draftFile.GetEmfMemoryStream(this))
            {
                using (System.IO.FileStream outFileStream = new System.IO.FileStream(path, System.IO.FileMode.Create))
                {
                    emfMemoryStream.WriteTo(outFileStream);
                }
            }
        }

        internal static SheetPaperSizeInfo[] SheetPaperSizes = new SheetPaperSizeInfo[]
        {
            new SheetPaperSizeInfo(SheetPaperSize.Custom),
            new SheetPaperSizeInfo(SheetPaperSize.EngFolioTall),
            new SheetPaperSizeInfo(SheetPaperSize.EngFolioWide),
            new SheetPaperSizeInfo(SheetPaperSize.EngLegalTall),
            new SheetPaperSizeInfo(SheetPaperSize.EngLegalWide),
            new SheetPaperSizeInfo(SheetPaperSize.EngExecutiveTall),
            new SheetPaperSizeInfo(SheetPaperSize.EngExecutiveWide),
            new SheetPaperSizeInfo(SheetPaperSize.EngStatementTall),
            new SheetPaperSizeInfo(SheetPaperSize.EngStatementWide),
            new SheetPaperSizeInfo(SheetPaperSize.EngCom10Tall),
            new SheetPaperSizeInfo(SheetPaperSize.EngCom10Wide),
            new SheetPaperSizeInfo(SheetPaperSize.EngMonarchTall),
            new SheetPaperSizeInfo(SheetPaperSize.EngMonarchWide),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiATall),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiAWide),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiBTall),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiBWide),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiCTall),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiCWide),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiDTall),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiDWide),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiETall),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiEWide),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiArchDTall),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiArchDWide),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiArchETall),
            new SheetPaperSizeInfo(SheetPaperSize.AnsiArchEWide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA5Tall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA5Wide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA4Tall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA4Wide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA3Tall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA3Wide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA2Tall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA2Wide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA1Tall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA1Wide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA0Tall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoA0Wide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoB5Tall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoB5Wide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoB4Tall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoB4Wide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoC5Tall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoC5Wide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoDLTall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoDLWide),
            new SheetPaperSizeInfo(SheetPaperSize.IsoQuatroTall),
            new SheetPaperSizeInfo(SheetPaperSize.IsoQuatroWide)
        };
    }
}
