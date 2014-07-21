using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdge.Community.Reader
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct BUILDVERSIONS
    {
        public BUILDVERSION CreatedVersion;
        public BUILDVERSION LastSavedVersion;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BUILDVERSION
    {
        public byte Revision;
        public byte Build;
        public byte Minor;
        public byte Major;

        public Version ToVersion()
        {
            return new Version(Major, Minor, Build, Revision);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct JSITESLIST
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Unknown1;
        public int Count;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct JSITE
    {
        public int Id;
        public string StorageName { get { return String.Format("JSite{0}", Id); } }
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct BYTES_16
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] b;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct BYTES_28
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] b;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct BYTES_30
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 29)]
        public byte[] b;
    }
}
