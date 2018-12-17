using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using JieDDDFramework.Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JieDDDFramework.Web.Models
{
    public class ModelErrorResult : ApiResult
    {
        public ModelErrorResult()
        {

        }
        public ModelErrorResult(ModelStateDictionary modelStateDictionary)
        {
            Success = false;
            Code = -1;
            Message = modelStateDictionary.Keys.SelectMany(x=>modelStateDictionary[x].Errors)
                .Select(x=>x.ErrorMessage)
                .FirstOrDefault();
        }

        public ModelErrorResult(ValidationFailure validationFailure)
        {
            Success = false;
            Code = -1;
            if (int.TryParse(validationFailure.ErrorCode, out var errorCode))
            {
                Code = errorCode;
            }
            Message = validationFailure.ErrorMessage;
        }
    }
}
