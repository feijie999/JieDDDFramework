using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Order.API.Models
{
    public class OrderAddedViewModel
    {
        public string ProductId { get; set; }
        public int Count { get; set; }
    }

    public class OrderAddedViewModelValidator : AbstractValidator<OrderAddedViewModel>
    {
        public OrderAddedViewModelValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId无效");
            RuleFor(x => x.Count).LessThan(1).WithMessage("Count无效");
        }
    }
}
