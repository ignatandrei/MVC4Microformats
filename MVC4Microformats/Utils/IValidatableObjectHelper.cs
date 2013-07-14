using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace utils
{
    public static class IValidatableObjectHelper
    {
        public static void ThrowErrorsForValidation(IValidatableObject var)
        {
            ThrowErrorsForValidation(var, null);
        }
        public static void ThrowErrorsForValidation(IValidatableObject var, ValidationContext vc)
        {
            IEnumerable<ValidationResult> val =var.Validate(vc);
            if (val == null)
                return;

            int nr=val.Count();
            if(nr == 0)
                return;
            Exception[] ex=new Exception[nr];
            int i=0;
            foreach (var item in val)
            {
                ArgumentException ax=new ArgumentException(item.ErrorMessage,item.MemberNames.FirstOrDefault());
                ex[i++]=ax;
            }
            AggregateException agg = new AggregateException("errors : " + nr, ex);
            throw agg;
        }
    }
}