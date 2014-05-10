using SolidEdgeContrib.Reader.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidEdgeContrib.Reader
{
    public class SolidEdgePropertySet : PropertySet
    {
        internal SolidEdgePropertySet(STATPROPSETSTG stat, Property[] properties)
            : base(stat, properties)
        {
        }

        internal T GetPropertyValue<T>(ExtendedSummaryInformationConstants propid)
        {
            return GetPropertyValue<T>((uint)propid);
        }

        internal T GetPropertyValue<T>(MechanicalModelingConstants propid)
        {
            return GetPropertyValue<T>((uint)propid);
        }

        internal T GetPropertyValue<T>(ProjectInformationConstants propid)
        {
            return GetPropertyValue<T>((uint)propid);
        }
    }

    [PropertySet(FMTID.ExtendedSummaryInformation, "ExtendedSummaryInformation")]
    public class ExtendedSummaryInformationPropertySet : SolidEdgePropertySet
    {
        internal ExtendedSummaryInformationPropertySet(STATPROPSETSTG stat, Property[] properties)
            : base(stat, properties)
        {
        }

        public string NameOfSavingApplication { get { return GetPropertyValue<string>(ExtendedSummaryInformationConstants.APPNAME); } }
        public int CreationLocale { get { return GetPropertyValue<int>(ExtendedSummaryInformationConstants.CREATION_LOCALE); } }
        //public int DocumentContentType { get { return GetPropertyValue<int>(PIDESI.DOC_CONTENT_TYPE); } }
        public Guid DocumentId { get { return GetPropertyValue<Guid>(ExtendedSummaryInformationConstants.DOCUMENT_ID); } }
        //public bool Hardware { get { return GetPropertyValue<bool>(PIDESI.HARDWARE); } }
        public int Status { get { return GetPropertyValue<int>(ExtendedSummaryInformationConstants.STATUS); } }
        public string Username { get { return GetPropertyValue<string>(ExtendedSummaryInformationConstants.USERNAME); } }
    }

    [PropertySet(FMTID.MechanicalModeling, "MechanicalModeling")]
    public class MechanicalModelingPropertySet : SolidEdgePropertySet
    {
        internal MechanicalModelingPropertySet(STATPROPSETSTG stat, Property[] properties)
            : base(stat, properties)
        {
        }

        public string Material { get { return GetPropertyValue<string>(MechanicalModelingConstants.MATERIAL); } }
    }

    [PropertySet(FMTID.ProjectInformation, "ProjectInformation")]
    public class ProjectInformationPropertySet : SolidEdgePropertySet
    {
        internal ProjectInformationPropertySet(STATPROPSETSTG stat, Property[] properties)
            : base(stat, properties)
        {
        }

        public string DocumentNumber { get { return GetPropertyValue<string>(ProjectInformationConstants.DOCUMENT_NUMBER); } }
        public string Revision { get { return GetPropertyValue<string>(ProjectInformationConstants.REVISION); } }
        public string ProjectName { get { return GetPropertyValue<string>(ProjectInformationConstants.PROJECT_NAME); } }
    }

    [PropertySet(FMTID.VersionInformation, "VersionInformation")]
    public class VersionInformationPropertySet : SolidEdgePropertySet
    {
        internal VersionInformationPropertySet(STATPROPSETSTG stat, Property[] properties)
            : base(stat, properties)
        {
        }
    }
}
