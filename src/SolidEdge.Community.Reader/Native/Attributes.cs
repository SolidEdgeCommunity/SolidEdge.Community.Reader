using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidEdgeCommunity.Reader.Native
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class PropertySetAttribute : Attribute
    {
        private Guid _fmtid;
        private string _name;

        public PropertySetAttribute(string fmtid, string name)
        {
            _fmtid = Guid.Parse(fmtid);
            _name = name;
        }

        public Guid FormatId { get { return _fmtid; } }
        public string Name { get { return _name; } }
    }
}
