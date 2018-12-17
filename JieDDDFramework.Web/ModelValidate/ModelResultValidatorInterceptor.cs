using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using JieDDDFramework.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace JieDDDFramework.Web.ModelValidate
{
    public class ModelResultValidatorInterceptor: IValidatorInterceptor
    {
        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            return validationContext;
        }

        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext,
            ValidationResult result)
        {
            if (result.IsValid)
            {
                return result;
            }
            controllerContext.HttpContext.Items[ValidatorAttribute.VALIDATOR_ITEM] = result.Errors;
            return result;
        }
    }
}
