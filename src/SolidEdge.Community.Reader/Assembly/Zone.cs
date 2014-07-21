using System;

namespace SolidEdge.Community.Reader.Assembly
{
    public class Zone
    {
        private string _name;

        public Zone(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; } }

        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(_name) == false)
            {
                return _name;
            }

            return base.ToString();
        }
    }
}
