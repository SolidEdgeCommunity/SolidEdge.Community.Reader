using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdgeContrib.Reader.Native
{
    public static class MethodExtensions
    {
        #region FILETIME

        public static DateTime ToDateTime(this System.Runtime.InteropServices.ComTypes.FILETIME filetime)
        {
            long highBits = filetime.dwHighDateTime;
            highBits = highBits << 32;
            return DateTime.FromFileTime(highBits + (long)filetime.dwLowDateTime);
        }

        #endregion

        #region IEnumSTATPROPSTG

        internal static void FinalRelease(this IEnumSTATPROPSTG enumerator)
        {
            Marshal.FinalReleaseComObject(enumerator);
        }

        #endregion

        #region IEnumSTATPROPSETSTG

        internal static void FinalRelease(this IEnumSTATPROPSETSTG enumerator)
        {
            Marshal.FinalReleaseComObject(enumerator);
        }

        #endregion

        #region IEnumSTATSTG

        internal static void FinalRelease(this IEnumSTATSTG enumerator)
        {
            Marshal.FinalReleaseComObject(enumerator);
        }

        #endregion

        #region IntPtr

        internal static T ToStructure<T>(this IntPtr p)
        {
            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        #endregion

        #region IPropertySetStorage

        internal static int GetPropertyValue(this IPropertySetStorage propertySetStorage, PIDSI propid, out PROPVARIANT propvar)
        {
            return propertySetStorage.GetPropertyValue(FormatId.SummaryInformation, (uint)propid, out propvar);
        }

        internal static int GetPropertyValue(this IPropertySetStorage propertySetStorage, PIDDSI propid, out PROPVARIANT propvar)
        {
            return propertySetStorage.GetPropertyValue(FormatId.DocSummaryInformation, (uint)propid, out propvar);
        }

        internal static int GetPropertyValue(this IPropertySetStorage propertySetStorage, Guid fmtid, uint propid, out PROPVARIANT propvar)
        {
            HRESULT hr = (int)HRESULT.E_FAIL;

            propvar = default(PROPVARIANT);

            PROPSPEC[] propspec = new PROPSPEC[1];
            PROPVARIANT[] propvars = { propvar };
            IPropertyStorage propertyStorage = null;
            uint grfMode = (uint)(STGM.READ | STGM.SHARE_EXCLUSIVE);

            try
            {
                if (NativeMethods.Succeeded(hr = propertySetStorage.Open(ref fmtid, grfMode, out propertyStorage)))
                {
                    hr = propertyStorage.GetProperty(propid, out propvar);
                }
            }
            catch
            {
            }
            finally
            {
                if (propertyStorage != null)
                {
                    propertyStorage.FinalRelease();
                }
            }

            return hr;
        }

        internal static int GetPropertyName(this IPropertySetStorage propertySetStorage, Guid fmtid, uint propid, out string name)
        {
            HRESULT hr = (int)HRESULT.E_FAIL;

            name = String.Empty;

            IPropertyStorage propertyStorage = null;
            uint grfMode = (uint)(STGM.READ | STGM.SHARE_EXCLUSIVE);

            try
            {
                if (NativeMethods.Succeeded(hr = propertySetStorage.Open(ref fmtid, grfMode, out propertyStorage)))
                {
                    hr = propertyStorage.GetPropertyName(propid, out name);
                }
            }
            catch
            {
            }
            finally
            {
                if (propertyStorage != null)
                {
                    propertyStorage.FinalRelease();
                }
            }

            return hr;
        }

        //internal static PropertySet[] ToArray(this IPropertySetStorage propertySetStorage)
        //{
        //    HRESULT hr = (int)HRESULT.E_FAIL;
        //    IEnumSTATPROPSETSTG enumerator = null;
        //    STATPROPSETSTG[] stat = new STATPROPSETSTG[1];
        //    uint fetched = 0;
        //    List<PropertySet> list = new List<PropertySet>();

        //    try
        //    {
        //        if (NativeMethods.Succeeded(hr = propertySetStorage.Enum(out enumerator)))
        //        {
        //            while (NativeMethods.Succeeded(hr = (enumerator.Next(1, stat, out fetched))) && (fetched > 0))
        //            {
        //                list.Add(new PropertySet(stat[0]));
        //            }
        //        }

        //        // Interestingly, IEnum .Next() does not return FMTID_UserDefinedProperties.
        //        Guid fmtid = FMTID.FMTID_UserDefinedProperties;
        //        uint grfMode = (uint)(STGM.READ | STGM.SHARE_EXCLUSIVE);
        //        IPropertyStorage propertyStorage = null;

        //        try
        //        {
        //            if (NativeMethods.Succeeded(hr = propertySetStorage.Open(fmtid, grfMode, out propertyStorage)))
        //            {
        //                if (NativeMethods.Succeeded(hr = propertyStorage.Stat(out stat[0])))
        //                {
        //                    list.Add(new PropertySet(stat[0]));
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //        finally
        //        {
        //            if (propertyStorage != null)
        //            {
        //                propertyStorage.FinalRelease();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        if (enumerator != null)
        //        {
        //            enumerator.FinalRelease();
        //        }
        //    }

        //    return list.ToArray();
        //}

        #endregion

        #region IPropertyStorage

        internal static void FinalRelease(this IPropertyStorage propertyStorage)
        {
            Marshal.FinalReleaseComObject(propertyStorage);
        }

        internal static int GetProperty(this IPropertyStorage propertyStorage, uint propid, out PROPVARIANT propvar)
        {
            HRESULT hr = (int)HRESULT.E_FAIL;

            propvar = default(PROPVARIANT);

            PROPSPEC[] propspec = new PROPSPEC[1];
            PROPVARIANT[] propvars = { propvar };

            propspec[0].ulKind = (uint)PRSPEC.PROPID;
            propspec[0].u.propId = propid;

            if (NativeMethods.Succeeded(hr = propertyStorage.ReadMultiple(1, propspec, propvars)))
            {
                propvar = propvars[0];
            }

            return hr;
        }

        internal static int GetPropertyName(this IPropertyStorage propertyStorage, uint propid, out string name )
        {
            HRESULT hr = HRESULT.E_FAIL;

            name = String.Empty;
            
            uint[] rgpropid = { propid };
            string[] rglpwstrName = { String.Empty };

            if (NativeMethods.Succeeded(hr = propertyStorage.ReadPropertyNames(1, rgpropid, rglpwstrName)))
            {
                name = rglpwstrName[0];
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                Guid fmtid = propertyStorage.GetFormatId();
                
                if (fmtid.Equals(FormatId.SummaryInformation))
                {
                    name = propertyStorage.GetPropertyName((PIDSI)propid);
                    hr = HRESULT.S_OK;
                }
                else if (fmtid.Equals(FormatId.DocSummaryInformation))
                {
                    name = propertyStorage.GetPropertyName((PIDDSI)propid);
                    hr = HRESULT.S_OK;
                }
            }

            return hr;
        }

        internal static string GetPropertyName(this IPropertyStorage propertyStorage, PIDSI propid)
        {
            switch (propid)
            {
                case PIDSI.APPNAME:
                    return "Name of Creating Application";
                case PIDSI.AUTHOR:
                    return "Author";
                case PIDSI.CHARCOUNT:
                    return "Number of Characters";
                case PIDSI.COMMENTS:
                    return "Comments";
                case PIDSI.CREATE_DTM:
                    return "Created";
                case PIDSI.DOC_SECURITY:
                    return "Security";
                case PIDSI.EDITTIME:
                    return "Total Editing Time";
                case PIDSI.KEYWORDS:
                    return "Keywords";
                case PIDSI.LASTAUTHOR:
                    return "Last Saved By";
                case PIDSI.LASTPRINTED:
                    return "Last Printed";
                case PIDSI.LASTSAVE_DTM:
                    return "Last Saved";
                case PIDSI.PAGECOUNT:
                    return "Number of Pages";
                case PIDSI.REVNUMBER:
                    return "Revision Number";
                case PIDSI.SUBJECT:
                    return "Subject";
                case PIDSI.TEMPLATE:
                    return "Template";
                case PIDSI.THUMBNAIL:
                    return "Thumbnail";
                case PIDSI.TITLE:
                    return "Title";
                case PIDSI.WORDCOUNT:
                    return "Number of Words";
                default:
                    return String.Empty;
            }
        }
        
        internal static string GetPropertyName(this IPropertyStorage propertyStorage, PIDDSI propid)
        {
            switch (propid)
            {
                case PIDDSI.BYTECOUNT:
                    return "Bytes";
                case PIDDSI.CATEGORY:
                    return "Category";
                case PIDDSI.COMPANY:
                    return "Company";
                case PIDDSI.DOCPARTS:
                    return "Titles of Parts";
                case PIDDSI.HEADINGPAIR:
                    return "Heading Pairs";
                case PIDDSI.HIDDENCOUNT:
                    return "Hidden Slides";
                case PIDDSI.LINECOUNT:
                    return "Lines";
                case PIDDSI.LINKSDIRTY:
                    return "Links Up To Date";
                case PIDDSI.MANAGER:
                    return "Manager";
                case PIDDSI.MMCLIPCOUNT:
                    return "MMClips";
                case PIDDSI.NOTECOUNT:
                    return "Notes";
                case PIDDSI.PARCOUNT:
                    return "Paragraphs";
                case PIDDSI.PRESFORMAT:
                    return "Presentation Target";
                case PIDDSI.SCALE:
                    return "Scale Crop";
                case PIDDSI.SLIDECOUNT:
                    return "Slides";
                default:
                    return String.Empty;
            }
        }

        internal static Guid GetFormatId(this IPropertyStorage propertyStorage)
        {
            return propertyStorage.GetStatistics().fmtid;
        }

        internal static STATPROPSETSTG GetStatistics(this IPropertyStorage propertyStorage)
        {
            STATPROPSETSTG statpropsetstg = default(STATPROPSETSTG);
            propertyStorage.Stat(out statpropsetstg);
            return statpropsetstg;
        }

        #endregion

        #region IStorage

        internal static void FinalRelease(this IStorage storage)
        {
            Marshal.FinalReleaseComObject(storage);
        }

        internal static System.Runtime.InteropServices.ComTypes.STATSTG[] GetElements(this IStorage storage)
        {
            HRESULT hr = HRESULT.E_FAIL;
            IEnumSTATSTG enumerator = null;
            List<System.Runtime.InteropServices.ComTypes.STATSTG> list = new List<System.Runtime.InteropServices.ComTypes.STATSTG>();

            try
            {
                if (NativeMethods.Succeeded(hr = storage.EnumElements(0, IntPtr.Zero, 0, out enumerator)))
                {
                    uint celt = 1;
                    System.Runtime.InteropServices.ComTypes.STATSTG[] statstg = new System.Runtime.InteropServices.ComTypes.STATSTG[1];
                    uint fetched = 0;

                    while ((NativeMethods.Succeeded(hr = enumerator.Next(celt, statstg, out fetched))) && (fetched > 0))
                    {
                        list.Add(statstg[0]);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (enumerator != null)
                {
                    enumerator.FinalRelease();
                }
            }

            return list.ToArray();
        }

        internal static int EnumElements(this IStorage storage, out System.Runtime.InteropServices.ComTypes.STATSTG[] elements)
        {
            HRESULT hr = HRESULT.E_FAIL;
            IEnumSTATSTG enumerator =  null;
            elements = new System.Runtime.InteropServices.ComTypes.STATSTG[] { };
            List<System.Runtime.InteropServices.ComTypes.STATSTG> list = new List<System.Runtime.InteropServices.ComTypes.STATSTG>();

            try
            {
                if (NativeMethods.Succeeded(hr = storage.EnumElements(0, IntPtr.Zero, 0, out enumerator)))
                {
                    uint celt = 1;
                    System.Runtime.InteropServices.ComTypes.STATSTG[] statstg = new System.Runtime.InteropServices.ComTypes.STATSTG[1];
                    uint fetched = 0;

                    while ((NativeMethods.Succeeded(hr = enumerator.Next(celt, statstg, out fetched))) && (fetched > 0))
                    {
                        list.Add(statstg[0]);
                    }

                    elements = list.ToArray();
                }
            }
            catch
            {
            }
            finally
            {
                if (enumerator != null)
                {
                    enumerator.FinalRelease();
                }
            }

            return 0;
        }

        internal static System.Runtime.InteropServices.ComTypes.STATSTG GetStatistics(this IStorage storage)
        {
            return storage.GetStatistics(STATFLAG.DEFAULT);
        }

        internal static System.Runtime.InteropServices.ComTypes.STATSTG GetStatistics(this IStorage storage, STATFLAG statflag)
        {
            System.Runtime.InteropServices.ComTypes.STATSTG statstg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
            uint grfStatFlag = (uint)statflag;
            storage.Stat(out statstg, grfStatFlag);
            return statstg;
        }

        internal static IStorage OpenStorage(this IStorage storage, string pwcsName)
        {
            IStorage subStorage = null;
            uint grfMode = (uint)(STGM.READ | STGM.SHARE_EXCLUSIVE);
            Marshal.ThrowExceptionForHR(storage.OpenStorage(pwcsName, null, grfMode, IntPtr.Zero, 0, out subStorage));
            return subStorage;
        }

        internal static int OpenStorage(this IStorage storage, string pwcsName, out IStorage ppstg)
        {
            uint grfMode = (uint)(STGM.READ | STGM.SHARE_EXCLUSIVE);
            return storage.OpenStorage(pwcsName, null, grfMode, IntPtr.Zero, 0, out ppstg);
        }

        internal static int OpenStorage(this IStorage storage, string pwcsName, uint grfMode, out IStorage ppstg)
        {
            return storage.OpenStorage(pwcsName, null, grfMode, IntPtr.Zero, 0, out ppstg);
        }

        internal static IStream OpenStream(this IStorage storage, string pwcsName)
        {
            IStream stream = null;
            uint grfMode = (uint)(STGM.READ | STGM.SHARE_EXCLUSIVE);
            Marshal.ThrowExceptionForHR(storage.OpenStream(pwcsName, 0, grfMode, 0, out stream));
            return stream;

        }

        internal static int OpenStream(this IStorage storage, string pwcsName, out IStream ppstg)
        {
            uint grfMode = (uint)(STGM.READ | STGM.SHARE_EXCLUSIVE);
            return storage.OpenStream(pwcsName, 0, grfMode, 0, out ppstg);
        }

        internal static int OpenStream(this IStorage storage, string pwcsName, uint grfMode, out IStream ppstg)
        {
            return storage.OpenStream(pwcsName, 0, grfMode, 0, out ppstg);
        }

        internal static MemoryStream ReadStreamMemoryStream(this IStorage storage, string streamName)
        {
            HRESULT hr = HRESULT.E_FAIL;
            IStream stream = null;

            try
            {
                if (NativeMethods.Succeeded(hr = storage.OpenStream(streamName, out stream)))
                {
                    return stream.ToMemoryStream();
                }
                else
                {
                    Marshal.ThrowExceptionForHR(hr);
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.FinalRelease();
                }
            }

            return null;
        }

        internal static MemoryStream StreamToMemoryStream(this IStorage storage, string streamName, params string[] subStorageNames)
        {
            List<IStorage> subStorages = new List<IStorage>();
            IStorage targetStorage = storage;
            IStream stream = null;

            try
            {
                foreach (string subStorageName in subStorageNames)
                {
                    targetStorage = targetStorage.OpenStorage(subStorageName);
                    subStorages.Add(targetStorage);
                }

                stream = targetStorage.OpenStream(streamName);
                return stream.ToMemoryStream();
            }
            finally
            {
                if (stream != null)
                {
                    stream.FinalRelease();
                }

                foreach (IStorage subStorage in subStorages)
                {
                    if (subStorage != null)
                    {
                        subStorage.FinalRelease();
                    }
                }
            }
        }

        //internal static BinaryReader ReadMemoryStreamToBinaryReader(this IStorage storage, string name)
        //{
        //    return new BinaryReader(storage.ReadMemoryStream(name));
        //}

        //internal static BinaryReader ReadMemoryStreamToBinaryReader(this IStorage storage, string name, Encoding encoding)
        //{
        //    return new BinaryReader(storage.ReadMemoryStream(name), encoding);
        //}

        internal static int ReadStructure<T>(this IStorage storage, string pwcsName, out T structure)
        {
            HRESULT hr = HRESULT.E_FAIL;
            structure = default(T);
            Native.IStream stream = null;

            try
            {
                if (NativeMethods.Succeeded(storage.OpenStream(pwcsName, out stream)))
                {
                    if (NativeMethods.Succeeded(hr = stream.ReadStructure<T>(out structure)))
                    {
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (stream != null)
                {
                    stream.FinalRelease();
                }
            }

            return hr;
        }

        #endregion

        #region IStream

        internal static void FinalRelease(this IStream stream)
        {
            Marshal.FinalReleaseComObject(stream);
        }

        internal static int Seek(this IStream stream, long dlibMove, STREAM_SEEK dwOrigin)
        {
            IntPtr plibNewPosition = default(IntPtr);
            return stream.Seek(dlibMove, (int)dwOrigin, plibNewPosition);
        }

        internal static int Read(this IStream stream, byte[] pv)
        {
            HRESULT hr = HRESULT.E_FAIL;
            int cbRead = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv, pv.Length, out cbRead)))
            {
                if (cbRead != pv.Length)
                {
                    hr = HRESULT.E_FAIL;
                }
            }

            return (int)hr;
        }

        internal static int ReadByte(this IStream stream, out byte value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(byte)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = pv[0];
            }

            return hr;
        }

        internal static int ReadByte(this IStream stream, int count, out byte[] value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[count];
            value = new byte[] { };

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = pv;
            }

            return hr;
        }

        internal static byte[] ReadBytes(this IStream stream, int count)
        {
            byte[] pv = new byte[count];
            Marshal.ThrowExceptionForHR(stream.Read(pv));
            return pv;
        }

        internal static int ReadDouble(this IStream stream, out double value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(double)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = BitConverter.ToDouble(pv, 0);
            }

            return hr;
        }

        internal static int ReadInt16(this IStream stream, out short value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(short)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = BitConverter.ToInt16(pv, 0);
            }

            return hr;
        }

        internal static int ReadInt32(this IStream stream)
        {
            int value;
            Marshal.ThrowExceptionForHR(stream.ReadInt32(out value));
            return value;
        }

        internal static int ReadInt32(this IStream stream, out int value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(int)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = BitConverter.ToInt32(pv, 0);
            }

            return hr;
        }

        internal static int ReadInt64(this IStream stream, out long value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(long)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = BitConverter.ToInt64(pv, 0);
            }

            return hr;
        }

        //internal static string ReadNextString(this IStream stream, Encoding encoding)
        //{
        //    string value = null;
        //    int nextStringLength = 0;
        //    Marshal.ThrowExceptionForHR(stream.ReadInt32(out nextStringLength));
        //    Marshal.ThrowExceptionForHR(stream.ReadString(encoding, nextStringLength, out value));
        //    return value;
        //}

        internal static int ReadSByte(this IStream stream, out sbyte value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(sbyte)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = (sbyte)pv[0];
            }

            return hr;
        }

        internal static int ReadSingle(this IStream stream, out float value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(float)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = BitConverter.ToSingle(pv, 0);
            }

            return hr;
        }

        internal static int ReadString(this IStream stream, Encoding encoding, int count, out string value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[count];
            value = null;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = encoding.GetString(pv);
                // Remove any trailing null terminating characters.
                value = value.TrimEnd(new char[] { '\0' });
            }

            return hr;
        }

        internal static int ReadStructure<T>(this IStream stream, out T structure)
        {
            HRESULT hr = HRESULT.E_FAIL;
            structure = default(T);
            byte[] pv = new byte[Marshal.SizeOf(typeof(T))];

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                unsafe
                {
                    fixed (byte* ppv = pv)
                    {
                        IntPtr p = new IntPtr(ppv);
                        structure = p.ToStructure<T>();
                    }
                }
            }

            return hr;
        }

        internal static T ReadStructure<T>(this IStream stream)
        {
            T structure = default(T);
            byte[] pv = new byte[Marshal.SizeOf(typeof(T))];

            Marshal.ThrowExceptionForHR(stream.Read(pv));

            unsafe
            {
                fixed (byte* ppv = pv)
                {
                    IntPtr p = new IntPtr(ppv);
                    structure = p.ToStructure<T>();
                }
            }
            

            return structure;
        }

        internal static int ReadUInt16(this IStream stream, out ushort value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(ushort)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = BitConverter.ToUInt16(pv, 0);
            }

            return hr;
        }

        internal static int ReadUInt32(this IStream stream, out uint value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(uint)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = BitConverter.ToUInt32(pv, 0);
            }

            return hr;
        }

        internal static int ReadUInt64(this IStream stream, out ulong value)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] pv = new byte[sizeof(ulong)];
            value = 0;

            if (NativeMethods.Succeeded(hr = stream.Read(pv)))
            {
                value = BitConverter.ToUInt64(pv, 0);
            }

            return hr;
        }

        //public void uncompressFile(string inFile, string outFile)
        //{
        //    int data = 0;
        //    int stopByte = -1;
        //    System.IO.FileStream outFileStream = new System.IO.FileStream(outFile, System.IO.FileMode.Create);
        //    zlib.ZInputStream inZStream = new zlib.ZInputStream(System.IO.File.Open(inFile, System.IO.FileMode.Open, System.IO.FileAccess.Read));
        //    while (stopByte != (data = inZStream.Read()))
        //    {
        //        byte _dataByte = (byte)data;
        //        outFileStream.WriteByte(_dataByte);
        //    }

        //    inZStream.Close();
        //    outFileStream.Close();
        //}

        internal static MemoryStream DecompressZlib(this IStream stream)
        {
            MemoryStream outStream = new MemoryStream();

            using (InflaterInputStream inf = new InflaterInputStream(stream.ToMemoryStream()))
            {
                inf.CopyTo(outStream);
            }

            return outStream;
        }

        internal static BinaryReader ToBinaryReader(this IStream stream)
        {
            return new BinaryReader(stream.ToMemoryStream());
        }

        internal static BinaryReader ToBinaryReader(this IStream stream, Encoding encoding)
        {
            return new BinaryReader(stream.ToMemoryStream(), encoding);
        }

        internal static MemoryStream ToMemoryStream(this IStream stream)
        {
            HRESULT hr = HRESULT.E_FAIL;
            byte[] buffer = new byte[4096];
            int cbRead = 0;
            MemoryStream memoryStream = new MemoryStream();

            while (NativeMethods.Succeeded(hr = stream.Read(buffer, buffer.Length, out cbRead)))
            {
                memoryStream.Write(buffer, 0, cbRead);
                if (cbRead < buffer.Length) break;
                buffer = new byte[buffer.Length];
            }

            stream.Seek(0, STREAM_SEEK.SET);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        #endregion

        #region Type

        internal static PropertySetAttribute GetPropertySetAttribute(this Type type)
        {
            PropertySetAttribute[] attributes = type.GetCustomAttributes(typeof(PropertySetAttribute), false) as PropertySetAttribute[];

            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0];
            }

            return null;
        }

        internal static string GetPropertySetName(this Type type)
        {
            PropertySetAttribute[] attributes = type.GetCustomAttributes(typeof(PropertySetAttribute), false) as PropertySetAttribute[];

            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0].Name;
            }

            return null;
        }

        #endregion
    }
}
