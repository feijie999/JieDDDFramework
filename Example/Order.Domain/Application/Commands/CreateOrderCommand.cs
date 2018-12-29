using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using FluentValidation;
using MediatR;

namespace Order.Domain.Application.Commands
{
    public class CreateOrderCommand
            : IRequest<bool>
    {
        public List<OrderItemDTO> OrderItems { get; private set; }
        public string UserId { get; private set; }
        public string UserName { get; private set; }

        public string City { get; private set; }
        public string Street { get; private set; }
        
        public string State { get; private set; }
        
        public string Country { get; private set; }
        
        public string ZipCode { get; private set; }

        public CreateOrderCommand()
        {
            OrderItems = new List<OrderItemDTO>();
        }

        public CreateOrderCommand(List<OrderItemDTO> orderItem, string userId, string userName, string city, string street, string state, string country, string zipcode)
        {
            OrderItems = orderItem;
            UserId = userId;
            UserName = userName;
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }


        public class OrderItemDTO
        {
            public string ProductId { get; set; }

            public string ProductName { get; set; }

            public decimal UnitPrice { get; set; }

            public decimal Discount { get; set; }

            public int Units { get; set; }

            public string PictureUrl { get; set; }
        }

        public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
        {
            public CreateOrderCommandValidator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(command => command.City).NotEmpty().WithErrorCode("3001");
                RuleFor(command => command.Street).NotEmpty().WithErrorCode("3002");
                RuleFor(command => command.State).NotEmpty().WithErrorCode("3003");
                RuleFor(command => command.Country).NotEmpty().WithErrorCode("3004");
                RuleFor(command => command.ZipCode).NotEmpty().WithErrorCode("3005");
                RuleFor(command => command.OrderItems).Must(x => x.Any())
                    .WithMessage("订单项不能为空").WithErrorCode("3006");
            }
        }

        public class OrderItemValidator : AbstractValidator<OrderItemDTO>
        {
            public OrderItemValidator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(x => x.Discount).GreaterThan(0).WithMessage("数量不能小于1").WithErrorCode("31001");
                RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("数量不能为空价格不能小于0").WithErrorCode("3102");
            }
        }
    }
}
