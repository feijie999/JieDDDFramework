using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.AspNetCore;

namespace JieDDDFramework.Web.ModelValidate
{
    public class ValidatorAttribute : CustomizeValidatorAttribute
    {
        public const string VALIDATOR_ITEM= "VALIDATOR_ITEM";
        public ValidatorAttribute()
        {
            Interceptor = typeof(ModelResultValidatorInterceptor);
        }
    }
}
