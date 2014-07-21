using System;

namespace SolidEdge.Community.Reader.Assembly
{
    public class Configuration
    {
        private string _name;

        public Configuration(string name)
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
