using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SolidEdge.Community.Reader.Draft
{
    internal static class MethodExtensions
    {
        #region FieldInfo

        internal static SheetPaperSizeAttribute GetSheetPaperSizeAttribute(this FieldInfo field)
        {
            SheetPaperSizeAttribute[] attributes = field.GetCustomAttributes(typeof(SheetPaperSizeAttribute), false) as SheetPaperSizeAttribute[];

            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0];
            }

            return null;
        }

        #endregion
    }
}
