using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KBS.Portals.Calculator.Logic.Util
{
    public static class Extensions
    {
        public static string GetDescription(this Enum e)
        {
            var fieldInfo = e.GetType().GetRuntimeField(e.ToString());

            var attributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            
            return e.ToString();
        }
    }
}
