using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidEdgeContrib.Reader.Native
{
    internal static class ASCII
    {
        public const int NUL = 0;    // Null char
        public const int SOH = 1;    // Start of Heading
        public const int STX = 2;    // Start of Text
        public const int ETX = 3;    // End of Text
        public const int EOT = 4;    // End of Transmission
        public const int ENQ = 5;    // Enquiry
        public const int ACK = 6;    // Acknowledgment
        public const int BEL = 7;    // Bell
        public const int BS = 8;     // Back Space
        public const int HT = 9;     // Horizontal Tab
        public const int LF = 10;    // Line Feed
        public const int VT = 11;    // Vertical Tab
    }

    /// <summary>
    /// Constants for the BITMAPINFOHEADER biCompression field.
    /// </summary>
    internal enum BI : uint
    {
        RGB = 0,
        RLE8 = 1,
        RLE4 = 2,
        BITFIELDS = 3,
        JPEG = 4,
        PNG = 5
    }

    /// <summary>
    /// Constants for CreateDIBitmap
    /// </summary>
    internal enum CBM : uint
    {
        /// <summary>
        /// initialize bitmap
        /// </summary>
        INIT = 0x04
    }

    /// <summary>
    /// Predefined Clipboard Formats
    /// </summary>
    internal enum CF : uint
    {
        TEXT = 1,
        BITMAP = 2,
        METAFILEPICT = 3,
        SYLK = 4,
        DIF = 5,
        TIFF = 6,
        OEMTEXT = 7,
        DIB = 8,
        PALETTE = 9,
        PENDATA = 10,
        RIFF = 11,
        WAVE = 12,
        UNICODETEXT = 13,
        ENHMETAFILE = 14
    }

    /// <summary>
    /// DIB color table identifiers
    /// </summary>
    internal enum DIB : uint
    {
        /// <summary>
        /// color table in RGBs
        /// </summary>
        RGB_COLORS = 1,
        /// <summary>
        /// color table in palette indices
        /// </summary>
        PAL_COLORS = 2,
    }

    internal static partial class FMTID
    {
        public const string SummaryInformation = "F29F85E0-4FF9-1068-AB91-08002B27B3D9";
        public const string DocSummaryInformation = "D5CDD502-2E9C-101B-9397-08002B2CF9AE";
        public const string UserDefinedProperties = "D5CDD505-2E9C-101B-9397-08002B2CF9AE";
    }

    internal static partial class FormatId
    {
        public static readonly Guid SummaryInformation = new Guid(FMTID.SummaryInformation);
        public static readonly Guid DocSummaryInformation = new Guid(FMTID.DocSummaryInformation);
        public static readonly Guid UserDefinedProperties = new Guid(FMTID.UserDefinedProperties);
    }

    internal class HRESULT
    {
        private int _hr;

        private HRESULT(int hr)
        {
            _hr = hr;
        }

        public static implicit operator int(HRESULT hr)
        {
            return hr._hr;
        }

        public static implicit operator HRESULT(int hr)
        {
            return new HRESULT(hr);
        }

        #region winerror.h

        public static int CO_E_CANTDETERMINECLASS = unchecked((int)0x800401F2);
        public static int DV_E_CLIPFORMAT = unchecked((int)0x8004006A);
        public static int E_FAIL = unchecked((int)0x80004005);
        public static int ERROR_INVALID_VARIANT = 604;
        public static int S_OK = 0;
        public static int S_FALSE = 1;
        public static int STG_E_INVALIDHANDLE = unchecked((int)0x80030006);
        public static int STG_E_INVALIDHEADER = unchecked((int)0x800300FB);

        #endregion
    }

    internal static partial class IID
    {
        public const string IEnumSTATPROPSETSTG = "0000013B-0000-0000-C000-000000000046";
        public const string IEnumSTATPROPSTG = "00000139-0000-0000-C000-000000000046";
        public const string IEnumSTATSTG = "0000000D-0000-0000-C000-000000000046";
        public const string ILockBytes = "0000000a-0000-0000-C000-000000000046";
        public const string IPropertySetStorage = "0000013A-0000-0000-C000-000000000046";
        public const string IPropertyStorage = "00000138-0000-0000-C000-000000000046";
        public const string IStorage = "0000000b-0000-0000-C000-000000000046";
        public const string IStream = "0000000c-0000-0000-C000-000000000046";
        public const string IUnknown = "00000000-0000-0000-C000-000000000046";

        public static readonly Guid IID_IEnumSTATPROPSETSTG = new Guid(IEnumSTATPROPSETSTG);
        public static readonly Guid IID_IEnumSTATPROPSTG = new Guid(IEnumSTATPROPSTG);
        public static readonly Guid IID_IEnumSTATSTG = new Guid(IEnumSTATSTG);
        public static readonly Guid IID_ILockBytes = new Guid(ILockBytes);
        public static readonly Guid IID_IPropertySetStorage = new Guid(IPropertySetStorage);
        public static readonly Guid IID_IPropertyStorage = new Guid(IPropertyStorage);
        public static readonly Guid IID_IStorage = new Guid(IStorage);
        public static readonly Guid IID_IStream = new Guid(IStream);
        public static readonly Guid IID_IUnknown = new Guid(IUnknown);
    }

    /// <summary>
    /// Property IDs for the DiscardableInformation Property Set.
    /// </summary>
    internal enum PIDDI : uint
    {
        /// <summary>
        /// VT_BLOB
        /// </summary>
        THUMBNAIL = 0x00000002
    }

    /// <summary>
    /// Property IDs for the SummaryInformation Property Set.
    /// </summary>
    internal enum PIDSI : uint
    {
        /// <summary>VT_LPSTR</summary>
        TITLE = 0x00000002,
        /// <summary>VT_LPSTR</summary>
        SUBJECT = 0x00000003,
        /// <summary>VT_LPSTR</summary>
        AUTHOR = 0x00000004,
        /// <summary>VT_LPSTR</summary>
        KEYWORDS = 0x00000005,
        /// <summary>VT_LPSTR</summary>
        COMMENTS = 0x00000006,
        /// <summary>VT_LPSTR</summary>
        TEMPLATE = 0x00000007,
        /// <summary>VT_LPSTR</summary>
        LASTAUTHOR = 0x00000008,
        /// <summary>VT_LPSTR</summary>
        REVNUMBER = 0x00000009,
        /// <summary>VT_FILETIME (UTC)</summary>
        EDITTIME = 0x0000000a,
        /// <summary>VT_FILETIME (UTC)</summary>
        LASTPRINTED = 0x0000000b,
        /// <summary>VT_FILETIME (UTC)</summary>
        CREATE_DTM = 0x0000000c,
        /// <summary>VT_FILETIME (UTC)</summary>
        LASTSAVE_DTM = 0x0000000d,
        /// <summary>VT_I4</summary>
        PAGECOUNT = 0x0000000e,
        /// <summary>VT_I4</summary>
        WORDCOUNT = 0x0000000f,
        /// <summary>VT_I4</summary>
        CHARCOUNT = 0x00000010,
        /// <summary>VT_CF</summary>
        THUMBNAIL = 0x00000011,
        /// <summary>VT_LPSTR</summary>
        APPNAME = 0x00000012,
        /// <summary>VT_I4</summary>
        DOC_SECURITY = 0x00000013
    }

    /// <summary>
    /// Property IDs for the DocSummaryInformation Property Set
    /// </summary>
    internal enum PIDDSI : uint
    {
        /// <summary>VT_LPSTR</summary>
        CATEGORY = 0x00000002,
        /// <summary>VT_LPSTR</summary>
        PRESFORMAT = 0x00000003,
        /// <summary>VT_I4</summary>
        BYTECOUNT = 0x00000004,
        /// <summary>VT_I4</summary>
        LINECOUNT = 0x00000005,
        /// <summary>VT_I4</summary>
        PARCOUNT = 0x00000006,
        /// <summary>VT_I4</summary>
        SLIDECOUNT = 0x00000007,
        /// <summary>VT_I4</summary>
        NOTECOUNT = 0x00000008,
        /// <summary>VT_I4</summary>
        HIDDENCOUNT = 0x00000009,
        /// <summary>VT_I4</summary>
        MMCLIPCOUNT = 0x0000000A,
        /// <summary>VT_BOOL</summary>
        SCALE = 0x0000000B,
        /// <summary>VT_VARIANT | VT_VECTOR</summary>
        HEADINGPAIR = 0x0000000C,
        /// <summary>VT_LPSTR | VT_VECTOR</summary>
        DOCPARTS = 0x0000000D,      // VT_LPSTR | VT_VECTOR
        /// <summary>VT_LPSTR</summary>
        MANAGER = 0x0000000E,
        /// <summary>VT_LPSTR</summary>
        COMPANY = 0x0000000F,
        /// <summary>VT_BOOL</summary>
        LINKSDIRTY = 0x00000010,
    }

    internal enum PRSPEC : uint
    {
        INVALID = 0xffffffff,
        LPWSTR = 0,
        PROPID = 1
    }

    [Flags]
    internal enum STGC : uint
    {
        DEFAULT	= 0,
        OVERWRITE	= 1,
        ONLYIFCURRENT	= 2,
        DANGEROUSLYCOMMITMERELYTODISKCACHE	= 4,
        CONSOLIDATE	= 8
    }

    internal enum STGTY : uint
    {
        STORAGE	= 1,
        STREAM	= 2,
        LOCKBYTES	= 3,
        PROPERTY	= 4
    }

    /* Storage instantiation modes */
    [Flags]
    internal enum STGM : uint
    {
        #region Access

        READ = 0x00000000,
        WRITE = 0x00000001,
        READWRITE = 0x00000002,

        #endregion

        #region Sharing
        
        SHARE_EXCLUSIVE =   0x00000010,
        SHARE_DENY_WRITE =  0x00000020,
        SHARE_DENY_READ =   0x00000030,
        SHARE_DENY_NONE =   0x00000040,
        PRIORITY =          0x00040000,

        #endregion

        #region Creation

        CREATE = 0x00001000,
        CONVERT = 0x00020000,
        FAILIFTHERE = 0x00000000,

        #endregion

        #region Transactioning

        DIRECT = 0x00000000,
        TRANSACTED = 0x00010000,

        #endregion

        #region Transactioning Performance

        NOSCRATCH = 0x00100000,
        NOSNAPSHOT = 0x00200000,

        #endregion

        #region Direct SWMR and Simple

        SIMPLE = 0x08000000,
        DIRECT_SWMR = 0x00400000,

        #endregion

        #region Delete On Release

        DELETEONRELEASE = 0x04000000

        #endregion

    }

    internal enum STGFMT : uint
    {
        STORAGE = 0,
        NATIVE = 1,
        FILE = 3,
        ANY = 4,
        DOCFILE = 5
    }

    [Flags]
    internal enum STATFLAG : uint
    {
        DEFAULT = 0,
        NONAME = 1,
        NOOPEN = 2
    }

    internal enum STREAM_SEEK : uint
    {
        SET	= 0,
        CUR	= 1,
        END	= 2
    }
}
