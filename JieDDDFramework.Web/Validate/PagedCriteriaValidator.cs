using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JieDDDFramework.Web.Models;

namespace JieDDDFramework.Web.Validate
{
    public class PagedCriteriaValidator : AbstractValidator<PagedCriteria>
    {
        public PagedCriteriaValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0).WithMessage("PagedIndex必须大于等于0");
            RuleFor(x=>x.PageSize).GreaterThan(0).WithMessage("PageSize必须大于0");
            RuleForEach(x => x.OrderSorts).Must(x => x.OrderType >= 0 && (uint) x.OrderType <= 1)
                .WithMessage("OrderType无效");

        }
    }
}
