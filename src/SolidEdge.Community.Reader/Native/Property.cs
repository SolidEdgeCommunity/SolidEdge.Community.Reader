using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdge.Community.Reader.Native
{
    public sealed class Property
    {
        private STATPROPSTG _statpropstg;
        private object _value;

        internal Property(STATPROPSTG statpropstg, object value)
        {
            _statpropstg = statpropstg;
            _value = value;
        }

        public uint PropertyId { get { return _statpropstg.propid; } }
        public System.Runtime.InteropServices.VarEnum VariantType { get { return (VarEnum)_statpropstg.vt; } }
        public string Name { get { return _statpropstg.lpwstrName; } }
        public object Value { get { return _value; } }

        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(_statpropstg.lpwstrName) == false)
            {
                return _statpropstg.lpwstrName;
            }

            return _statpropstg.propid.ToString();
        }
    }
}
