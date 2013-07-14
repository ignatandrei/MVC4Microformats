using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Web.UI;

namespace utils
{
    /// <summary>
    /// http://www.hanselman.com/blog/ASmarterOrPureEvilToStringWithExtensionMethods.aspx
    /// read also 
    /// http://haacked.com/archive/2009/01/14/named-formats-redux.aspx
    /// http://james.newtonking.com/archive/2008/03/29/formatwith-2-0-string-formatting-with-named-variables.aspx
    /// </summary>
    public static class FormattableObject
    {
        

        public static string FormatWith(this string format, object source)
        {

            return FormatWith(format, null, source);

        }



        public static string FormatWith(this string format, IFormatProvider provider, object source)
        {

            if (format == null)

                throw new ArgumentNullException("format");



            Regex r = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",

              RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);



            List<object> values = new List<object>();

            string rewrittenFormat = r.Replace(format, delegate(Match m)
            {

                Group startGroup = m.Groups["start"];

                Group propertyGroup = m.Groups["property"];

                Group formatGroup = m.Groups["format"];

                Group endGroup = m.Groups["end"];



                values.Add((propertyGroup.Value == "0")

                  ? source

                  : DataBinder.Eval(source, propertyGroup.Value));



                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value

                  + new string('}', endGroup.Captures.Count);

            });



            return string.Format(provider, rewrittenFormat, values.ToArray());

        }

       


    }
}
