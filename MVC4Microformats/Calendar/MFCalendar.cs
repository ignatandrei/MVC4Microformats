using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using utils;

namespace MVC4Microformats.Calendar
{
    public class MFCalendar:IValidatableObject
    {
        #region required
        public DateTime? DtStart { get; set; }
        public string Summary { get; set; }
        #endregion

        public FormatCalendar FormatCalendarType
        {
            get
            {
                return TheCalendarFormat.Format;
            }
            set
            {
                TheCalendarFormat = new CalendarFormat(value);
            }
        }
        internal CalendarFormat TheCalendarFormat;

        #region optional
        public DateTime? DtEnd { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        #endregion
        #region constructors
        public MFCalendar()
        {
            TheCalendarFormat = CalendarFormat.TagBuilder;
        }
        public MFCalendar(DateTime dtStart, string summary)
            : this(dtStart,summary,CalendarFormat.TagBuilder)
        {
            
        }
        public MFCalendar(DateTime dtStart, string summary, CalendarFormat cf)
            : this()
        {
            this.DtStart = dtStart;
            this.Summary = summary;
            this.TheCalendarFormat = cf;
        }
        #endregion
        /// <summary>
        /// after http://tools.ietf.org/html/rfc2445#page-136
        /// </summary>
        /// <returns></returns>
        public string GenerateICalendar()
        {
            IValidatableObjectHelper.ThrowErrorsForValidation(this);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:http://msprogrammer.serviciipeweb.ro/");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine("DTSTART:" + DtStart.Value.ToString("s"));
            sb.AppendLine("DTSTAMP" + DateTime.Now.ToString("s"));
            sb.AppendLine("SUMMARY:" + Summary);

            sb.AppendLine("UID" + Guid.NewGuid());
            if (DtEnd != null)
            {
                sb.AppendLine("DTEND:" + DtEnd.Value.ToString("s"));
            }
            if (!String.IsNullOrWhiteSpace(Description ))
            {                
                sb.AppendLine("DESCRIPTION:" + Description);
            }
            if (!String.IsNullOrWhiteSpace(Location))
            {
                sb.AppendLine("LOCATION:" + Location);
            }
            if (!String.IsNullOrWhiteSpace(Url))
            {
                sb.AppendLine("URL:" + Url);
            }

            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            return sb.ToString();

        }
        public HtmlString Generate()
        {
            IValidatableObjectHelper.ThrowErrorsForValidation(this);

            switch (TheCalendarFormat.Format)
            {
                case FormatCalendar.eFormatTagBuilder:
                    return GenerateTagBuilder();
                default:                    
                    var str = TheCalendarFormat.FormatHTML.FormatWith(this);
                    return new HtmlString(str);
            }
        }
        private HtmlString GenerateTagBuilder()
        {

            var builder = new TagBuilder(TheCalendarFormat.BeginTag);
            builder.Attributes.Add("id", TagBuilder.CreateSanitizedId(TheCalendarFormat.Id));
            builder.Attributes.Add("class", "vevent");
            if (!TheCalendarFormat.Visible)
            {
                builder.Attributes.Add("style", "display: none");
            }

            var dateStart = new TagBuilder("abbr");
            dateStart.Attributes.Add("title", DtStart.Value.ToString("yyyy-MM-dd"));
            dateStart.Attributes.Add("class", "dtstart");
            dateStart.SetInnerText(DtStart.Value.ToString(TheCalendarFormat.DateTimeFormatCalendar));

            builder.InnerHtml += dateStart.ToString(TagRenderMode.Normal);

            var summary = new TagBuilder("span");
            summary.Attributes.Add("class", "summary");
            summary.SetInnerText(" " + Summary);
            builder.InnerHtml += summary.ToString(TagRenderMode.Normal);
            if (DtEnd != null)
            {
                var dateEnd = new TagBuilder("abbr");
                dateEnd.Attributes.Add("title", DtEnd.Value.ToString("yyyy-MM-dd"));
                //see http://microformats.org/wiki/dtend-issue
                dateEnd.Attributes.Add("class", "dtlast");                
                dateEnd.SetInnerText(" until " + DtEnd.Value.ToString(TheCalendarFormat.DateTimeFormatCalendar));
                builder.InnerHtml += dateEnd.ToString(TagRenderMode.Normal);
            }

            if (!string.IsNullOrEmpty(Location))
            {
                var location = new TagBuilder("span");
                location.Attributes.Add("class", "location");
                
                location.SetInnerText(" at " + Location);
                builder.InnerHtml += location.ToString(TagRenderMode.Normal);
            }

            if (!string.IsNullOrEmpty(Url))
            {
                var url = new TagBuilder("a");
                url.Attributes.Add("href", Url);
                url.Attributes.Add("target", "_blank");
                url.Attributes.Add("class", "url");
                url.InnerHtml = builder.InnerHtml;
                builder.InnerHtml = url.ToString(TagRenderMode.Normal);
            }
            if (!string.IsNullOrEmpty(Description))
            {
                var description = new TagBuilder("div");
                description.Attributes.Add("class", "description");
                description.SetInnerText(Description);
                builder.InnerHtml += description.ToString(TagRenderMode.Normal);

            }

            
            return new HtmlString(builder.ToString(TagRenderMode.Normal));

        }


        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            
            if (string.IsNullOrWhiteSpace(Summary))
                yield return new ValidationResult("Summary", new string[1] { "Summary" });
            if(!DtStart.HasValue)
                yield return new ValidationResult("DtStart", new string[1] { "DtStart" });


        }

        #endregion
    }
}
