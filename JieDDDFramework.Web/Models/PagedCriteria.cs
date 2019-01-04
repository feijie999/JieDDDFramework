using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JieDDDFramework.Core.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace JieDDDFramework.Web.Models
{
    public class PagedCriteria : IPagerQueryParameter
    {
        public PagedCriteria()
        {
        }
        public int PageIndex { get; set; }

        public int PageSize { get; set; } = 50;

        public int PageNumber => PageIndex + 1;

        public List<PageOrders> OrderSorts { get; set; }
    }

   
}
