using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using MVC4Microformats.Calendar;

namespace MVC4Microformats
{
    public static class LinkExtensionCalendar
    {
        
        public static MFCalendar Calendar(this HtmlHelper html, CalendarFormat cf, DateTime dtStart, string summary)
        {
            return Calendar(html, cf, dtStart, summary, null, null, null, null);
        }
        public static MFCalendar Calendar(this HtmlHelper html, CalendarFormat cf, DateTime dtStart, string summary, DateTime? DateEnd, string Location)
        {
            return Calendar(html, cf, dtStart, summary, DateEnd, Location, null, null);
        }

        public static MFCalendar Calendar(this HtmlHelper html, CalendarFormat cf, DateTime dtStart, string summary, DateTime? DateEnd, string Location, string URL, string Description)
        {
            var c = new MFCalendar(dtStart, summary);
            c.TheCalendarFormat = cf;
            c.Location = Location;
            c.DtEnd = DateEnd;
            c.Location = Location;
            c.Url = URL;
            c.Summary = summary;
            c.Description = Description;
            return c;
        }
    }
}
