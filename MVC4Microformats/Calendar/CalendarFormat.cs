using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC4Microformats.Calendar
{
    public class CalendarFormat
    {
        public static explicit operator CalendarFormat(string definition)
        {
            int i;
            if (int.TryParse(definition, out i))
                return new CalendarFormat((FormatCalendar)i);

            FormatCalendar f;
            if (Enum.TryParse<FormatCalendar>(definition, out f))
                return new CalendarFormat(f);

            return new CalendarFormat();
        }
        
        public CalendarFormat()
            : this(FormatCalendar.eFormatCustom)
        {
        }
        public CalendarFormat(FormatCalendar cf)
        {
            this.Format = cf;
            switch (cf)
            {
                case FormatCalendar.eFormatHTML:
                    this.FormatHTML = ResCalendar.eFormatHTML;
                    break;
                case FormatCalendar.eFormatHTML5:
                    this.FormatHTML = ResCalendar.eFormatHTML5;
                    break;
                case FormatCalendar.eFormatSchemaOrg:
                    this.FormatHTML = ResCalendar.eFormatSchemaOrg;
                    break;
                default:
                    break;
            }
        }
        public string DateTimeFormatCalendar = "dd MMM yyyy";
        public string BeginTag = "div";
        public string Id = "hcalendar";
        public bool Visible = true;

        internal FormatCalendar Format;
        public string FormatHTML = "please enter some format in your Calendar instance.FormatHtml or use CalendarFormat.TagBuilder, CalendarFormat.Classic or CalendarFormat.Html5";

        public static CalendarFormat TagBuilder = new CalendarFormat(FormatCalendar.eFormatTagBuilder);
        public static CalendarFormat HtmlClassic = new CalendarFormat(FormatCalendar.eFormatHTML);
        public static CalendarFormat Html5 = new CalendarFormat(FormatCalendar.eFormatHTML5);
        public static CalendarFormat HtmlSchemaOrg = new CalendarFormat(FormatCalendar.eFormatSchemaOrg);

    }

    
}
