using SolidEdgeCommunity.Reader.Native;
using System;
using System.Collections.Generic;

namespace SolidEdgeCommunity.Reader.Assembly
{
    [SolidEdgeDocumentAttribute(DocumentType.ConfigFileExtension, CLSID.ConfigFileExtension, PROGID.ConfigFileExtension, "Solid Edge Assembly Configuration File", ".cfg")]
    public sealed class ConfigFileExtension : SolidEdgeDocument
    {
        private Configuration[] _configurations = null;
        private Zone[] _zones = null;

        internal ConfigFileExtension(IStorage storage, System.Runtime.InteropServices.ComTypes.STATSTG statstg)
            : base(storage, statstg)
        {
        }

        public new static ConfigFileExtension Open(string path)
        {
            return Open<ConfigFileExtension>(path);
        }

        public Configuration[] Configurations
        {
            get
            {
                if (_configurations == null)
                {
                    LoadConfigurations();
                }

                return _configurations;
            }
        }

        public Zone[] Zones
        {
            get
            {
                if (_zones == null)
                {
                    LoadZones();
                }

                return _zones;
            }
        }

        private void LoadConfigurations()
        {
            HRESULT hr = HRESULT.S_OK;
            List<Configuration> list = new List<Configuration>();
            IStorage storage = null;

            try
            {
                if (NativeMethods.Succeeded(hr = RootStorage.OpenStorage(StorageName.Configs, out storage)))
                {
                    System.Runtime.InteropServices.ComTypes.STATSTG[] elements;
                    if (NativeMethods.Succeeded(hr = storage.EnumElements(out elements)))
                    {
                        foreach (System.Runtime.InteropServices.ComTypes.STATSTG element in elements)
                        {
                            if ((STGTY)element.type == STGTY.STREAM)
                            {
                                list.Add(new Configuration(element.pwcsName));
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
                if (storage != null)
                {
                    storage.FinalRelease();
                }
            }

            _configurations = list.ToArray();
        }

        private void LoadZones()
        {
            HRESULT hr = HRESULT.S_OK;
            List<Zone> list = new List<Zone>();
            IStorage storage = null;

            try
            {
                if (NativeMethods.Succeeded(hr = RootStorage.OpenStorage(StorageName.Zones, out storage)))
                {
                    System.Runtime.InteropServices.ComTypes.STATSTG[] elements;
                    if (NativeMethods.Succeeded(hr = storage.EnumElements(out elements)))
                    {
                        foreach (System.Runtime.InteropServices.ComTypes.STATSTG element in elements)
                        {
                            if ((STGTY)element.type == STGTY.STREAM)
                            {
                                list.Add(new Zone(element.pwcsName));
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
                if (storage != null)
                {
                    storage.FinalRelease();
                }
            }

            _zones = list.ToArray();
        }
    }
}
