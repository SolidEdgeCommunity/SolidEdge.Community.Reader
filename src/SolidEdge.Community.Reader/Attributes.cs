using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidEdge.Community.Reader
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal sealed class SolidEdgeDocumentAttribute : Attribute
    {
        private DocumentType _documentType;
        private Guid _classId;
        private string _progId;
        private string _description;
        private string _defaultExtension;

        public SolidEdgeDocumentAttribute(DocumentType documentType, string classId, string progId, string description, string defaultExtension)
        {
            _documentType = documentType;
            _classId = Guid.Parse(classId);
            _progId = progId;
            _description = description;
            _defaultExtension = defaultExtension;
        }

        public DocumentType DocumentType { get { return _documentType; } }
        public Guid ClassId { get { return _classId; } }
        public string ProgId { get { return _progId; } }
        public string Description { get { return _description; } }
        public string DefaultExtension { get { return _defaultExtension; } }
    }
}
