using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Models
{
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
