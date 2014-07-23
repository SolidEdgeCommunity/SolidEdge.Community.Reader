using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdgeCommunity.Reader.Native
{
    public class PropertySets : IList<PropertySet>, ICollection<PropertySet>
    {
        private IPropertySetStorage _propertySetStorage;
        private List<PropertySet> _propertySets = new List<PropertySet>();

        internal PropertySets(IPropertySetStorage propertySetStorage)
        {
            _propertySetStorage = propertySetStorage;
            LoadPropertySets();
        }

        public PropertySet this[Guid formatId]
        {
            get
            {
                return this.Where(x => x.FormatId.Equals(formatId)).FirstOrDefault();
            }
        }

        public PropertySet this[string name]
        {
            get
            {
                return this[name, StringComparison.Ordinal];
            }
        }

        public PropertySet this[string name, StringComparison comparisonType]
        {
            get
            {
                return this.Where(x => x.Name != null).Where(x => x.Name.Equals(name, comparisonType)).FirstOrDefault();
            }
        }


        #region System.Collections.Generic.IList

        int System.Collections.Generic.IList<PropertySet>.IndexOf(PropertySet item)
        {
            return _propertySets.IndexOf(item);
        }

        void System.Collections.Generic.IList<PropertySet>.Insert(int index, PropertySet item)
        {
            throw new NotImplementedException();
        }

        void System.Collections.Generic.IList<PropertySet>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public PropertySet this[int index]
        {
            get
            {
                return _propertySets[index];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region System.Collections.Generic.ICollection<T>

        void System.Collections.Generic.ICollection<PropertySet>.Add(PropertySet item)
        {
            throw new NotSupportedException();
        }

        void System.Collections.Generic.ICollection<PropertySet>.Clear()
        {
            throw new NotSupportedException();
        }

        bool System.Collections.Generic.ICollection<PropertySet>.Contains(PropertySet item)
        {
            return _propertySets.Contains(item);
        }

        void System.Collections.Generic.ICollection<PropertySet>.CopyTo(PropertySet[] array, int arrayIndex)
        {
            _propertySets.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _propertySets.Count; }
        }

        bool System.Collections.Generic.ICollection<PropertySet>.IsReadOnly
        {
            get { return true; }
        }

        bool System.Collections.Generic.ICollection<PropertySet>.Remove(PropertySet item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region System.Collections.Generic.IEnumerable<T>

        public IEnumerator<PropertySet> GetEnumerator()
        {
            return _propertySets.GetEnumerator();
        }

        #endregion

        #region System.Collections.IEnumerator

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _propertySets.GetEnumerator();
        }

        #endregion

        private void LoadPropertySets()
        {
            HRESULT hr = (int)HRESULT.E_FAIL;
            IEnumSTATPROPSETSTG enumerator = null;
            STATPROPSETSTG[] stat = new STATPROPSETSTG[1];
            uint fetched = 0;
            List<Guid> fmtids = new List<Guid>();

            // Build list of FMTID's.
            try
            {
                if (NativeMethods.Succeeded(hr = _propertySetStorage.Enum(out enumerator)))
                {
                    while (NativeMethods.Succeeded(hr = (enumerator.Next(1, stat, out fetched))) && (fetched == 1))
                    {
                        fmtids.Add(stat[0].fmtid);
                    }

                    // IPropertySetStorage.Enum() does not enumerate FMTID_UserDefinedProperties.
                    // Note that FMTID_UserDefinedProperties may not exist.
                    fmtids.Add(FormatId.UserDefinedProperties);
                }

                foreach (Guid fmtid in fmtids)
                {
                    Guid rfmtid = fmtid;
                    uint grfMode = (uint)(STGM.READ | STGM.SHARE_EXCLUSIVE);
                    //uint grfMode = (uint)(STGM.DIRECT | STGM.READ | STGM.SHARE_DENY_WRITE);
                    IPropertyStorage propertyStorage = null;

                    try
                    {
                        if (NativeMethods.Succeeded(hr = _propertySetStorage.Open(ref rfmtid, grfMode, out propertyStorage)))
                        {
                            _propertySets.Add(PropertySet.FromIPropertyStorage(propertyStorage));
                        }
                    }
                    catch
                    {
#if DEBUG
                        System.Diagnostics.Debugger.Break();
#endif
                    }
                    finally
                    {
                        if (propertyStorage != null)
                        {
                            propertyStorage.FinalRelease();
                        }
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
        }
    }
}
