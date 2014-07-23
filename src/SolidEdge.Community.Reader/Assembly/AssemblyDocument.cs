using SolidEdgeCommunity.Reader.Native;
using System;

namespace SolidEdgeCommunity.Reader.Assembly
{
    /// <summary>
    /// Solid Edge Assembly Document (.asm)
    /// </summary>
    [SolidEdgeDocumentAttribute(DocumentType.AssemblyDocument, CLSID.AssemblyDocument, PROGID.AssemblyDocument, "Solid Edge Assembly Document", ".asm")]
    public class AssemblyDocument : SolidEdgeDocument
    {
        //private Attachment[] _attachments;

        internal AssemblyDocument(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
            //LoadAttachments();
        }

        //protected virtual void LoadAttachments()
        //{
        //    HRESULT hr = HRESULT.E_FAIL;
        //    SolidEdge.Community.Reader.Native.IStream stream = null;

        //    if (NativeMethods.Succeeded(hr = _rootStorage.OpenStream(StreamName.Attachments, out stream)))
        //    {
        //        _attachments = SolidEdge.Community.Reader.Assembly.Attachments.GetAttachments(stream.ToMemoryStream());
        //    }
        //}

        public new static AssemblyDocument Open(string path)
        {
            return Open<AssemblyDocument>(path);
        }

        //public Attachment[] Attachments { get { return _attachments; } }
        //        private void LoadDocumentLinksOld()
        //        {
        //            HRESULT hr = HRESULT.E_FAIL;
        //            JSITESLIST sitesList = default(JSITESLIST);
        //            SolidEdge.Community.Reader.Native.IStream sitesStream = null;
        //            IStorage siteStorage = null;
        //            //SolidEdge.Community.Reader.Native.IStream oleStream = null;
        //            SolidEdge.Community.Reader.Native.IStream propertiesStream = null;

        //            string ole = new string(new char[] { (char)StreamName.SOH, 'O', 'l', 'e' });

        //            try
        //            {
        //                if (NativeMethods.Succeeded(hr = _rootStorage.OpenStream(StreamName.JSitesList, out sitesStream)))
        //                {
        //                    sitesList = sitesStream.ReadStructure<JSITESLIST>();

        //                    for (int i = 0; i < sitesList.Count; i++)
        //                    {
        //                        JSITE site = sitesStream.ReadStructure<JSITE>();

        //                        try
        //                        {
        //                            if (NativeMethods.Succeeded(hr = _rootStorage.OpenStorage(site.StorageName, out siteStorage)))
        //                            {
        //                                string[] names = siteStorage.GetSubStorageNames();
        //                                //using (BinaryReader reader = _rootStorage.ReadMemoryStreamToBinaryReader(ole))
        //                                //{
        //                                //}

        //                                byte[] oleBytes = null;
        //                                byte[] propertyBytes = null;

        //                                using (MemoryStream memoryStream = siteStorage.ReadMemoryStream(ole))
        //                                {
        //                                    oleBytes = memoryStream.ToArray();
        //                                    //int startIndex = 30;

        //                                    //int stringLength = BitConverter.ToInt32(bytes, startIndex);
        //                                    //startIndex += sizeof(int);


        //                                    ////Encoding.ASCII.GetString(bytes, 34, 84)
        //                                    //string ascii = Encoding.ASCII.GetString(bytes, startIndex, stringLength);
        //                                    //startIndex += stringLength;
        //                                    //startIndex += 28;


        //                                    ////BitConverter.ToInt32(bytes, 146)	168	int
        //                                    //stringLength = BitConverter.ToInt32(bytes, startIndex);
        //                                    //startIndex += sizeof(int);

        //                                    //// End of text check. 2 bytes
        //                                    //if (BitConverter.ToUInt16(bytes, startIndex) == StreamName.EOT)
        //                                    //{
        //                                    //    startIndex += sizeof(ushort);
        //                                    //}

        //                                    //string unicode = Encoding.Unicode.GetString(bytes, startIndex, stringLength);

        //                                    //_links.Add(unicode);
        //                                }

        //                                using (MemoryStream memoryStream = siteStorage.ReadMemoryStream("JProperties"))
        //                                {
        //                                    propertyBytes = memoryStream.ToArray();
        //                                }

        //                                var aa = oleBytes.Where(x => x.Equals(22)).ToArray();
        //                                var ab = oleBytes.Where(x => x.Equals(160)).ToArray();

        //                                //if (NativeMethods.Succeeded(hr = siteStorage.OpenStream(ole, out oleStream)))
        //                                //{
        //                                //    using (MemoryStream memoryStream = oleStream.ToMemoryStream())
        //                                //    {
        //                                //        byte[] bytes = memoryStream.ToArray();
        //                                //        int startIndex = 30;

        //                                //        int stringLength = BitConverter.ToInt32(bytes, startIndex);
        //                                //        startIndex += sizeof(int);


        //                                //        //Encoding.ASCII.GetString(bytes, 34, 84)
        //                                //        string ascii = Encoding.ASCII.GetString(bytes, startIndex, stringLength);
        //                                //        startIndex += stringLength;
        //                                //        startIndex += 28;


        //                                //        //BitConverter.ToInt32(bytes, 146)	168	int
        //                                //        stringLength = BitConverter.ToInt32(bytes, startIndex);
        //                                //        startIndex += sizeof(int);

        //                                //        // End of text check. 2 bytes
        //                                //        if (BitConverter.ToUInt16(bytes, startIndex) == StreamName.EOT)
        //                                //        {
        //                                //            startIndex += sizeof(ushort);
        //                                //        }

        //                                //        string unicode = Encoding.Unicode.GetString(bytes, startIndex, stringLength);

        //                                //        _links.Add(unicode);
        //                                //    }
        //                                //}
        //                            }
        //                        }
        //                        catch (System.Exception ex)
        //                        {
        //#if DEBUG
        //                            System.Diagnostics.Debugger.Break();
        //#endif
        //                        }
        //                        finally
        //                        {
        //                            if (siteStorage != null)
        //                            {
        //                                siteStorage.FinalRelease();
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            catch
        //            {
        //            }
        //            finally
        //            {
        //                if (sitesStream != null)
        //                {
        //                    sitesStream.FinalRelease();
        //                }
        //            }
        //        }
    }
}
