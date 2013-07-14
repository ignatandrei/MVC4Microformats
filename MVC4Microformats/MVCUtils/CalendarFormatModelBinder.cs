using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace MVC4Microformats.MVCUtils
{
    public class CalendarFormatModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var data=base.BindModel(controllerContext, bindingContext);
            return data;
        }
        
        //protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor, object value)
        //{
        //    if(propertyDescriptor.Name == "TheCalendarFormat")
        //    base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        //}
        
    }
}
