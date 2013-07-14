using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC4Microformats.Calendar
{
    public enum FormatCalendar
    {
        /// <summary>
        /// http://microformats.org/code/hcalendar/creator
        /// </summary>
        eFormatTagBuilder = 0,
        /// <summary>
        /// http://microformats.org/wiki/hcalendar
        /// </summary>
        eFormatHTML,
        /// <summary>
        /// http://microformats.org/wiki/HTML5
        /// </summary>
        eFormatHTML5,
        /// <summary>
        /// http://schema.org/Event
        /// </summary>
        eFormatSchemaOrg,
        /// <summary>
        /// specify (CalendarFormat).FormatHTML to your data...
        /// </summary>
        eFormatCustom

    }
}
