using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MediatR;

namespace Order.Domain.Application.Commands
{
    public class CreateOrderCommand
            : IRequest<bool>
    {
        public List<OrderItemDTO> OrderItems { get; }
        public string UserId { get; }
        public string UserName { get; }

        public string City { get; }
        public string Street { get; }
        
        public string State { get; }
        
        public string Country { get; }
        
        public string ZipCode { get; }

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
    }
}
