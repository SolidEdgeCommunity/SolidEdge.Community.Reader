using SolidEdgeCommunity.Reader.Native;
//using SolidEdge.Community.Reader.Native.zlib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdgeCommunity.Reader
{
    internal static class MethodExtensions
    {
        #region System.Reflection.Assembly

        internal static Type[] GetPropertySetTypes(this System.Reflection.Assembly assembly)
        {
            var baseType = typeof(PropertySet);
            return assembly.GetTypes().Where(t => t.IsSubclassOf(baseType)).ToArray();
        }

        internal static Type GetPropertySetType(this System.Reflection.Assembly assembly, Guid fmtid)
        {
            foreach (Type type in assembly.GetPropertySetTypes())
            {
                PropertySetAttribute attribute = type.GetPropertySetAttribute();

                if (attribute != null)
                {
                    if (attribute.FormatId.Equals(fmtid))
                    {
                        return type;
                    }
                }
            }

            return null;
        }

        internal static Type[] GetSolidEdgeDocumentTypes(this System.Reflection.Assembly assembly)
        {
            var baseType = typeof(SolidEdgeDocument);
            return assembly.GetTypes().Where(t => t.IsSubclassOf(baseType)).ToArray();
        }

        internal static Type[] GetSolidEdgeDocumentTypes(this System.Reflection.Assembly assembly, Guid classId)
        {
            List<Type> list = new List<Type>();

            foreach (Type type in assembly.GetSolidEdgeDocumentTypes())
            {
                SolidEdgeDocumentAttribute attribute = type.GetSolidEdgeDocumentAttribute();

                if (attribute != null)
                {
                    if (attribute.ClassId.Equals(classId))
                    {
                        list.Add(type);
                    }
                }
            }

            return list.ToArray();
        }

        #endregion

        #region MemoryStream

        internal static byte[] ReadBytes(this MemoryStream stream, int count)
        {
            byte[] buffer = new byte[count];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        /// <summary>
        /// Reads the next 16 bytes into a Guid.
        /// </summary>
        internal static Guid ReadGuid(this MemoryStream stream)
        {
            byte[] buffer = new byte[16];
            stream.Read(buffer, 0, buffer.Length);
            return new Guid(buffer);
        }

        /// <summary>
        /// Reads the next sizeof(double) bytes into a double.
        /// </summary>
        internal static double ReadDouble(this MemoryStream stream)
        {
            byte[] buffer = new byte[sizeof(double)];
            stream.Read(buffer, 0, buffer.Length);
            return BitConverter.ToDouble(buffer, 0);
        }

        internal static T ReadEnum<T>(this MemoryStream stream)
        {
            byte[] buffer = new byte[sizeof(int)];
            stream.Read(buffer, 0, buffer.Length);
            return (T)(object)BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Reads the next sizeof(short) bytes into a short.
        /// </summary>
        internal static short ReadInt16(this MemoryStream stream)
        {
            byte[] buffer = new byte[sizeof(short)];
            stream.Read(buffer, 0, buffer.Length);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>
        /// Reads the next sizeof(int) bytes into a int.
        /// </summary>
        internal static int ReadInt32(this MemoryStream stream)
        {
            byte[] buffer = new byte[sizeof(int)];
            stream.Read(buffer, 0, buffer.Length);
            return BitConverter.ToInt32(buffer, 0);
        }

        internal static int[] ReadInt32(this MemoryStream stream, int count)
        {
            int[] ints = new int[count];

            for (int i = 0 ; i < count; i++)
            {
                ints[i] = stream.ReadInt32();
            }

            return ints;
        }

        /// <summary>
        /// Reads the next sizeof(long) bytes into a long.
        /// </summary>
        internal static long ReadInt64(this MemoryStream stream)
        {
            byte[] buffer = new byte[sizeof(long)];
            stream.Read(buffer, 0, buffer.Length);
            return BitConverter.ToInt64(buffer, 0);
        }

        internal static double[] ReadMatrix(this MemoryStream stream, int length)
        {
            double[] matrix = new double[length];
            for (int i = 0; i < 16; i++)
            {
                matrix[i] = stream.ReadDouble();
            }
            return matrix;
        }

        /// <summary>
        /// Assumes the length is defined in the next 4 bytes.
        /// </summary>
        internal static string ReadString(this MemoryStream stream, Encoding encoding)
        {
            int length = stream.ReadInt32();
            return encoding.GetString(stream.ReadBytes(length));
        }

        internal static string ReadString(this MemoryStream stream, Encoding encoding, bool autoTrim)
        {
            int length = stream.ReadInt32();
            return stream.ReadString(encoding, length, autoTrim);
        }

        internal static string ReadString(this MemoryStream stream, Encoding encoding, int length)
        {
            return stream.ReadString(encoding, length, false);
        }

        internal static string ReadString(this MemoryStream stream, Encoding encoding, int length, bool autoTrim)
        {
            //// Remove any trailing null terminating characters.
            //value = value.TrimEnd(new char[] { '\0' });

            string s = encoding.GetString(stream.ReadBytes(length));

            if (autoTrim)
            {
                s = s.Trim(new char[] { (char)ASCII.NUL });
            }

            return s;
        }

        internal static T ReadStructure<T>(this MemoryStream stream)
        {
            T structure = default(T);
            byte[] pv = new byte[Marshal.SizeOf(typeof(T))];

            stream.Read(pv, 0, pv.Length);

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

        internal static ushort ReadUInt16(this MemoryStream stream)
        {
            byte[] pv = new byte[sizeof(ushort)];
            stream.Read(pv, 0, pv.Length);
            return BitConverter.ToUInt16(pv, 0);
        }

        internal static uint ReadUInt32(this MemoryStream stream)
        {
            byte[] pv = new byte[sizeof(uint)];
            stream.Read(pv, 0, pv.Length);
            return BitConverter.ToUInt32(pv, 0);
        }

        internal static ulong ReadUInt64(this MemoryStream stream)
        {
            byte[] pv = new byte[sizeof(ulong)];
            stream.Read(pv, 0, pv.Length);
            return BitConverter.ToUInt32(pv, 0);
        }

        /// <summary>
        /// Reads the next 4 bytes into a Version.
        /// </summary>
        internal static Version ReadVersion(this MemoryStream stream)
        {
            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, buffer.Length);
            return new Version(buffer[3], buffer[2], buffer[1], buffer[0]);
        }

        #endregion

        #region IntPtr

        internal static T ToStructure<T>(this IntPtr p)
        {
            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        #endregion

        #region Type

        internal static SolidEdgeDocumentAttribute GetSolidEdgeDocumentAttribute(this Type type)
        {
            SolidEdgeDocumentAttribute[] attributes = type.GetCustomAttributes(typeof(SolidEdgeDocumentAttribute), false) as SolidEdgeDocumentAttribute[];

            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0];
            }

            return null;
        }

        #endregion
    }
}
