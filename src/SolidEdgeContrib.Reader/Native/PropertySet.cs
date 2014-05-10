using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SolidEdgeContrib.Reader.Native
{
    public class PropertySet : IList<Property>, ICollection<Property>
    {
        private STATPROPSETSTG _stat;
        internal string _name = String.Empty;
        private List<Property> _properties = new List<Property>();
        
        internal PropertySet(STATPROPSETSTG stat, Property[] properties)
        {
            if (properties == null) throw new ArgumentNullException("properties");

            _stat = stat;
            _properties.AddRange(properties);
        }

        public Property this[uint propertyId]
        {
            get
            {
                return this.Where(x => x.PropertyId.Equals(propertyId)).FirstOrDefault();
            }
        }

        public Property this[string name]
        {
            get
            {
                return this[name, StringComparison.Ordinal];
            }
        }

        public Property this[string name, StringComparison comparisonType]
        {
            get
            {
                return this.Where(x => x.Name != null).Where(x => x.Name.Equals(name, comparisonType)).FirstOrDefault();
            }
        }

        internal static PropertySet FromIPropertyStorage(IPropertyStorage propertyStorage)
        {
            if (propertyStorage == null) throw new ArgumentNullException("propertyStorage");

            HRESULT hr = HRESULT.E_FAIL;
            PropertySet propertySet = null;
            STATPROPSETSTG stat;
            propertyStorage.Stat(out stat);

            IEnumSTATPROPSTG enumerator = null;

            List<Property> properties = new List<Property>();

            try
            {
                if (NativeMethods.Succeeded(hr = propertyStorage.Enum(out enumerator)))
                {
                    STATPROPSTG[] sps = new STATPROPSTG[] { default(STATPROPSTG) };
                    uint fetched = 0;

                    while ((enumerator.Next(1, sps, out fetched) == HRESULT.S_OK) && (fetched == 1))
                    {
                        string name;
                        PROPVARIANT propvar = default(PROPVARIANT);

                        try
                        {
                            propertyStorage.GetPropertyName(sps[0].propid, out name);
                            sps[0].lpwstrName = name;
                            propertyStorage.GetProperty(sps[0].propid, out propvar);
                            properties.Add(new Property(sps[0], propvar.Value));
                        }
                        catch
                        {
                        }
                        finally
                        {
                            propvar.Clear();
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

            Type type = System.Reflection.Assembly.GetExecutingAssembly().GetPropertySetType(stat.fmtid);

            if (type == null)
            {
                // In this case, we did not find a strongly typed property set.
                type = typeof(PropertySet);
            }

            propertySet = PropertySet.InvokeInternalConstructor(type, stat, properties.ToArray());
            propertySet.Name = type.GetPropertySetName();

            return propertySet;
        }

        internal static PropertySet InvokeInternalConstructor(Type type, STATPROPSETSTG stat, Property[] properties)
        {
            Type[] types = new[] { typeof(STATPROPSETSTG), typeof(Property[]) };
            ConstructorInfo ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, types, null);

            if (ctor != null)
            {
                return (PropertySet)ctor.Invoke(new object[] { stat, properties });
            }
            else
            {
                throw new System.Reflection.TargetException(String.Format("Unable for find appropriate constructor for type '{0}'", type.FullName));
            }
        }

        internal T GetPropertyValue<T>(PIDDSI propid)
        {
            return GetPropertyValue<T>((uint)propid);
        }

        internal T GetPropertyValue<T>(PIDSI propid)
        {
            return GetPropertyValue<T>((uint)propid);
        }

        internal T GetPropertyValue<T>(uint propid)
        {
            return (T)Convert.ChangeType(GetPropertyValue(propid), typeof(T));
        }

        internal DateTime? GetPropertyValueAsDateTime(PIDSI propid)
        {
            return GetPropertyValueAsDateTime((uint)propid);
        }

        internal DateTime? GetPropertyValueAsDateTime(uint propid)
        {
            DateTime? dt = null;
            object value = GetPropertyValue(propid);

            if (value is DateTime)
            {
                dt = (DateTime)value;
            }

            return dt;
        }

        internal object GetPropertyValue(uint propid)
        {
            Property property = _properties.FirstOrDefault(x => x.PropertyId == propid);

            if (property != null)
            {
                return property.Value;
            }

            return null;
        }

        public Guid FormatId { get { return _stat.fmtid; } }

        public string Name
        {
            get { return _name; }
            internal set { _name = value; }
        }

        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(_name) == false)
            {
                return _name;
            }

            return _stat.fmtid.ToString("B");
        }


        #region System.Collections.Generic.IList

        int IList<Property>.IndexOf(Property item)
        {
            return _properties.IndexOf(item);
        }

        void IList<Property>.Insert(int index, Property item)
        {
            throw new NotSupportedException();
        }

        void IList<Property>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public Property this[int index]
        {
            get
            {
                return _properties[index];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region System.Collections.Generic.ICollection<T>

        void ICollection<Property>.Add(Property item)
        {
            throw new NotSupportedException();
        }

        void ICollection<Property>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<Property>.Contains(Property item)
        {
            return _properties.Contains(item);
        }

        void ICollection<Property>.CopyTo(Property[] array, int arrayIndex)
        {
            _properties.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _properties.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        bool ICollection<Property>.Remove(Property item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region System.Collections.Generic.IEnumerable<T>

        public IEnumerator<Property> GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        #endregion

        #region System.Collections.IEnumerator

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        #endregion
    }

    [PropertySet(FMTID.UserDefinedProperties, "Custom")]
    public class CustomPropertySet : PropertySet
    {
        internal CustomPropertySet(STATPROPSETSTG stat, Property[] properties)
            : base(stat, properties)
        {
        }
    }

    [PropertySet(FMTID.DocSummaryInformation, "DocumentSummaryInformation")]
    public class DocumentSummaryInformationPropertySet : PropertySet
    {
        internal DocumentSummaryInformationPropertySet(STATPROPSETSTG stat, Property[] properties)
            : base(stat, properties)
        {
        }

        // Remarked out properties are also excluded via the Solid Edge API.
        public int ByteCount { get { return GetPropertyValue<int>(PIDDSI.BYTECOUNT); } }
        public string Category { get { return GetPropertyValue<string>(PIDDSI.CATEGORY); } }
        public string Company { get { return GetPropertyValue<string>(PIDDSI.COMPANY); } }
        public int HiddenObjects { get { return GetPropertyValue<int>(PIDDSI.HIDDENCOUNT); } }
        public int Lines { get { return GetPropertyValue<int>(PIDDSI.LINECOUNT); } }
        public string Manager { get { return GetPropertyValue<string>(PIDDSI.MANAGER); } }
        public int MultimediaClips { get { return GetPropertyValue<int>(PIDDSI.MMCLIPCOUNT); } }
        //public bool LinksUpToDate { get { return (bool?)GetProperty<bool>(PIDDSI.LINKSDIRTY); } }
        public int Notes { get { return GetPropertyValue<int>(PIDDSI.NOTECOUNT); } }
        public int Paragraphs { get { return GetPropertyValue<int>(PIDDSI.PARCOUNT); } }
        public string PresentationFormat { get { return GetPropertyValue<string>(PIDDSI.PRESFORMAT); } }
        //public bool ScaleCrop { get { return GetProperty<bool>(PIDDSI.SCALE); } }
        public int Slides { get { return GetPropertyValue<int>(PIDDSI.SLIDECOUNT); } }
    }

    [PropertySet(FMTID.SummaryInformation, "SummaryInformation")]
    public class SummaryInformationPropertySet : PropertySet
    {
        internal SummaryInformationPropertySet(STATPROPSETSTG stat, Property[] properties)
            : base(stat, properties)
        {
        }

        // Remarked out properties are also excluded via the Solid Edge API.
        public string ApplicationName { get { return GetPropertyValue<string>(PIDSI.APPNAME); } }
        public string Author { get { return GetPropertyValue<string>(PIDSI.AUTHOR); } }
        public string Comments { get { return GetPropertyValue<string>(PIDSI.COMMENTS); } }
        public DateTime? CreatedDate { get { return GetPropertyValueAsDateTime(PIDSI.CREATE_DTM); } }
        public string Keywords { get { return GetPropertyValue<string>(PIDSI.KEYWORDS); } }
        public string LastAuthor { get { return GetPropertyValue<string>(PIDSI.LASTAUTHOR); } }
        public DateTime? LastPrintDate { get { return GetPropertyValueAsDateTime(PIDSI.LASTPRINTED); } }
        public DateTime? LastSavedDate { get { return GetPropertyValueAsDateTime(PIDSI.LASTSAVE_DTM); } }
        public int NumberOfPages { get { return GetPropertyValue<int>(PIDSI.PAGECOUNT); } }
        public int NumberOfWords { get { return GetPropertyValue<int>(PIDSI.WORDCOUNT); } }
        public int NumberOfCharacters { get { return GetPropertyValue<int>(PIDSI.CHARCOUNT); } }
        public string RevisionNumber { get { return GetPropertyValue<string>(PIDSI.REVNUMBER); } }
        public string Subject { get { return GetPropertyValue<string>(PIDSI.SUBJECT); } }
        public string Template { get { return GetPropertyValue<string>(PIDSI.TEMPLATE); } }
        //public object Thumbnail { get { return GetProperty<object>(PIDSI.THUMBNAIL); } }
        public string Title { get { return GetPropertyValue<string>(PIDSI.TITLE); } }
        public DateTime? TotalEditingTime { get { return GetPropertyValueAsDateTime(PIDSI.EDITTIME); } }
        public int Security { get { return GetPropertyValue<int>(PIDSI.DOC_SECURITY); } }
    }
}
