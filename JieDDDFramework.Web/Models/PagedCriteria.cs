using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace JieDDDFramework.Web.Models
{
    public class PagedCriteria
    {
        public PagedCriteria()
        {
        }
        public int PageIndex { get; set; }

        public int PageSize { get; set; } = 50;

        public int PageNumber => PageIndex + 1;

        public List<PageOrders> OrderSorts { get; set; }
    }

    public class PageOrders
    {
        public string OrderName { get; set; }

        public PageOrderType OrderType { get; set; }
    }

    public enum PageOrderType : uint
    {
        Asc = 0,
        Desc = 1
    }
}
