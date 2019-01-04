using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Models
{
    public interface IPagerQueryParameter : IPagerBase
    {
        List<PageOrders> OrderSorts { get; set; }
    }
}
