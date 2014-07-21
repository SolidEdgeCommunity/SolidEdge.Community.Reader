using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdge.Community.Reader.Native
{
    internal static class IUnknownHelper
    {
        /// <summary>
        /// Returns an array of Guids by QueryInterface()'ing all IIDs known to this system.
        /// </summary>
        public static Guid[] QueryInterfaces(object o)
        {
            List<Guid> list = new List<Guid>();

            if (Marshal.IsComObject(o))
            {
                IntPtr pUnk = IntPtr.Zero;
                try
                {
                    pUnk = Marshal.GetIUnknownForObject(o);

                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default))
                    {
                        using (RegistryKey interfaceKey = baseKey.OpenSubKey("Interface"))
                        {
                            foreach (string iid in interfaceKey.GetSubKeyNames())
                            {
                                try
                                {
                                    Guid guid = Guid.Empty;

                                    if (Guid.TryParse(iid, out guid))
                                    {
                                        IntPtr ppv = IntPtr.Zero;

                                        if (Marshal.QueryInterface(pUnk, ref guid, out ppv) == 0)
                                        {
                                            list.Add(guid);
                                            Marshal.Release(ppv);
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    if (pUnk.Equals(IntPtr.Zero) == false)
                    {
                        Marshal.Release(pUnk);
                    }
                }
            }

            return list.ToArray();
        }
    }
}
