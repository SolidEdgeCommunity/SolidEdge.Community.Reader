//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;

//namespace SolidEdgeCommunity.Reader.Assembly
//{
//    internal class Attachments
//    {
//        public static Attachment[] GetAttachments(MemoryStream stream)
//        {
//            if (stream == null) throw new ArgumentNullException("stream");

//            List<Attachment> items = new List<Attachment>();


//            byte[] bJunk;
//            int iJunk = 0;

//            try
//            {
//                byte[] data = stream.ToArray();

//                //Solid Edge GUID
//                Guid guid = stream.ReadGuid();

//                int[] countArray = stream.ReadInt32(5);

//                bJunk = stream.ReadBytes(12);

//                for (int i = 0; i < countArray[1]; i++)
//                {
//                    bJunk = stream.ReadBytes(18);

//                    int stringLength = 0;
//                    string asciiPath = stream.ReadString(Encoding.ASCII);

//                    bJunk = stream.ReadBytes(24);

//                    int structLength = stream.ReadInt32();
//                    stringLength = stream.ReadInt32();
//                    short eot = stream.ReadInt16();
//                    string unicodePath = stream.ReadString(Encoding.Unicode, stringLength);

//                    stream.Seek(22, SeekOrigin.Current);
//                    string asciiFileName = stream.ReadString(Encoding.ASCII);

//                    bJunk = stream.ReadBytes(24);

//                    int iUnknown = stream.ReadInt32();

//                    if (iUnknown > 0)
//                    {
//                        stringLength = stream.ReadInt32();
//                        eot = stream.ReadInt16();

//                        string unicodeFileName = stream.ReadString(Encoding.Unicode, stringLength);
//                    }

//                    //int[] intArray2 = stream.ReadInt32(3);
//                    bJunk = stream.ReadBytes(12);

//                    AttachmentFlags flags = stream.ReadEnum<AttachmentFlags>();

//                    double[] matrix = stream.ReadMatrix(16);

//                    bJunk = stream.ReadBytes(52);
//                    string occurrenceName = stream.ReadString(Encoding.Unicode);

//                    bJunk = stream.ReadBytes(40);

//                    AttachmentFlags flags2 = stream.ReadEnum<AttachmentFlags>();

//                    AttachmentExtendedFlags extendedFlags = stream.ReadEnum<AttachmentExtendedFlags>();

//                    iJunk = stream.ReadInt32();

//                    int userDefinedQuantity = stream.ReadInt32();

//                    long position = stream.Position;
//                    byte[] endOfText = { 3, 3 };

//                    for (int z = 0; z < 100; z++)
//                    {
//                        if (stream.ReadBytes(2).SequenceEqual(endOfText))
//                        {
//                            stream.Seek(-2, SeekOrigin.Current);
//                            break;
//                        }
//                        stream.Seek(-1, SeekOrigin.Current);
//                    }


//                    //iJunk = stream.ReadInt32();
//                    //iJunk = stream.ReadInt32();

//                    items.Add(new Attachment(unicodePath, matrix, flags, extendedFlags));
//                }
//            }
//            catch
//            {
//            }
//            finally
//            {
//                stream.Dispose();
//            }

//            return items.ToArray();
//        }
//    }

//    public class Attachment
//    {
//        private string _filename;
//        private double[] _matrix;
//        private AttachmentFlags _flags;
//        private AttachmentExtendedFlags _extendedFlags;

//        internal Attachment(string filename, double[] matrix, AttachmentFlags flags, AttachmentExtendedFlags extendedFlags)
//        {
//            _filename = filename;
//            _matrix = matrix;
//            _flags = flags;
//            _extendedFlags = extendedFlags;
//        }

//        public string Filename { get { return _filename; } }
//        public double[] Matrix { get { return _matrix; } }
//        public AttachmentFlags Flags { get { return _flags; } }
//        public AttachmentExtendedFlags ExtendedFlags { get { return _extendedFlags; } }

//        #region Attachment flags

//        public bool ExcludeFromBOM { get { return _flags.HasFlag(AttachmentFlags.ExcludeFromBOM); } }
//        public bool IsDisplayable { get { return _flags.HasFlag(AttachmentFlags.IsDisplayable); } }
//        public bool IsLocatable { get { return _flags.HasFlag(AttachmentFlags.IsLocatable); } }
//        public bool IsTopLevel { get { return _flags.HasFlag(AttachmentFlags.IsTopLevel); } }
//        public bool IsPart { get { return _flags.HasFlag(AttachmentFlags.IsPart); } }
//        public bool IsAssembly { get { return _flags.HasFlag(AttachmentFlags.IsAssembly); } }
//        public bool IsReferenceOnly { get { return _flags.HasFlag(AttachmentFlags.IsReferenceOnly); } }
//        public bool LocateControl { get { return _flags.HasFlag(AttachmentFlags.LocateControl); } }
//        public bool HasAlteredMatrix { get { return _flags.HasFlag(AttachmentFlags.HasAlteredMatrix); } }
//        public bool IsBeingPlaced { get { return _flags.HasFlag(AttachmentFlags.IsBeingPlaced); } }
//        public bool IsMarked { get { return _flags.HasFlag(AttachmentFlags.IsMarked); } }
//        public bool IsForeign { get { return _flags.HasFlag(AttachmentFlags.IsForeign); } }
//        public bool HasDeletedConstraint { get { return _flags.HasFlag(AttachmentFlags.HasDeletedConstraint); } }
//        public bool ReferencePlanesOn { get { return _flags.HasFlag(AttachmentFlags.ReferencePlanesOn); } }
//        public bool IsCollapsed { get { return _flags.HasFlag(AttachmentFlags.IsCollapsed); } }
//        public bool HasUserDefinedName { get { return _flags.HasFlag(AttachmentFlags.HasUserDefinedName); } }
//        public bool ConstrainedSequentially { get { return _flags.HasFlag(AttachmentFlags.ConstrainedSequentially); } }
//        public bool HideInDrawing { get { return _flags.HasFlag(AttachmentFlags.HideInDrawing); } }
//        public bool HideInSubAssembly { get { return _flags.HasFlag(AttachmentFlags.HideInSubAssembly); } }
//        public bool ExcludeFromPhysicalProperties { get { return _flags.HasFlag(AttachmentFlags.ExcludeFromPhysProps); } }
//        public bool ExplodeAsSingleEntity { get { return _flags.HasFlag(AttachmentFlags.ExplodeAsSingleEntity); } }
//        public bool IsMasterPart { get { return _flags.HasFlag(AttachmentFlags.IsMasterPart); } }
//        public bool IsSlavePart { get { return _flags.HasFlag(AttachmentFlags.IsSlavePart); } }
//        public bool IsLitePart { get { return _flags.HasFlag(AttachmentFlags.IsLitePart); } }
//        public bool UseSimplified { get { return _flags.HasFlag(AttachmentFlags.UseSimplified); } }
//        public bool IgnoreViewFilter { get { return _flags.HasFlag(AttachmentFlags.IgnoreViewFilter); } }
//        public bool IsTransparentInDraft { get { return _flags.HasFlag(AttachmentFlags.IsTransparentInDraft); } }
//        public bool CoordSysOn { get { return _flags.HasFlag(AttachmentFlags.CoordSysOn); } }
//        public bool SketchesOn { get { return _flags.HasFlag(AttachmentFlags.SketchesOn); } }
//        public bool ConstructionsOff { get { return _flags.HasFlag(AttachmentFlags.ConstructionsOff); } }
//        public bool ReferenceAxesOn { get { return _flags.HasFlag(AttachmentFlags.ReferenceAxesOn); } }
//        public bool ConstructionCurvesOff { get { return _flags.HasFlag(AttachmentFlags.ConstructionCurvesOff); } }

//        #endregion

//        #region Attachment extended flags

//        public bool IsTubePart { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsTubePart); } }
//        public bool IsDirectedPart { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsDirectedPart); } }
//        public bool TubePathIsSick { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.TubePathIsSick); } }
//        public bool IsEditPlacement { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsEditPlacement); } }
//        public bool DoNotUnload { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.DoNotUnload); } }
//        public bool ExcludeReference { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.ExExcludeReference); } }
//        public bool SuppressConstructions { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.SuppressConstructions); } }
//        public bool IsWirePart { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsWirePart); } }
//        public bool WireIsSick { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.WireIsSick); } }
//        public bool UseDisplayInComponent { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.UseDisplayInComponent); } }
//        public bool IsPipeFitting { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsPipeFitting); } }
//        public bool IsFlexiblePipe { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsFlexiblePipe); } }
//        public bool IsFlexibleAssembly { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsFlexibleAssembly); } }
//        public bool IsFramePart { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsFramePart); } }
//        public bool IsAdjustPart { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsAdjustPart); } }
//        public bool IsBoltedConnection { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsBoltedConnection); } }
//        public bool IsSlaveBoltedConnPart { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsSlaveBoltedConnPart); } }
//        public bool TreatAsSoftReference { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.TreatAsSoftReference); } }
//        public bool ExcludeFromInterference { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.ExcludeFromInterference); } }
//        public bool IsGenericAssemblyBody { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsGenericAssemblyBody); } }
//        public bool IsWeldBead { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsWeldBead); } }
//        public bool IsHarnessWire { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsHarnessWire); } }
//        public bool IsHarnessCable { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsHarnessCable); } }
//        public bool IsHarnessBundle { get { return _extendedFlags.HasFlag(AttachmentExtendedFlags.IsHarnessBundle); } }

//        #endregion
//    }
//}
