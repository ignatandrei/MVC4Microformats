MVC4 Microformats
This a simple implementation for microformats . Now the calendar format , that are in those forms:

    Microformats site for calendar
    http://microformats.org/wiki/hcalendar
    HTML5 microformat for calendar
    http://microformats.org/wiki/HTML5
    Schema format
    http://schema.org/Event
    iCalendar format
    http://tools.ietf.org/html/rfc2445#page-136


	Use with 

Model.Generate()  - to can show data 

and a form + action to save the calendar in outlook format
<form method="post" action='@Url.Action("ReturnFile", "Home")' target='_blank'>

    <input type="hidden"  name="resultHtml" value="@Model.GenerateICalendar().Replace("\"","'")" />
    <input type="hidden" name="nameFile" value="cal.ics" />
    <input type="hidden" name="eventName" value="MyEvent" />
    <input type="submit" value="Download event" />
</form>
 [HttpPost]
        public ActionResult ReturnFile(string resultHtml, string eventName, string nameFile)
        {
            return File(System.Text.UTF8Encoding.Default.GetBytes(resultHtml), "text/calendar", nameFile);
        }

See demo for the app on http://mvc4microformats.apphb.com/

