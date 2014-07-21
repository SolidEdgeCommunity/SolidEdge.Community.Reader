using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidEdge.Community.Reader.Draft
{
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal class SheetPaperSizeAttribute : Attribute
    {
        private double _width;
        private double _height;
        private string _description;

        public SheetPaperSizeAttribute(double width, double height, string description)
        {
            _width = width;
            _height = height;
            _description = description;
        }

        public double Width { get { return _width; } }
        public double Height { get { return _height; } }
        public string Description { get { return _description; } }
    }
}
