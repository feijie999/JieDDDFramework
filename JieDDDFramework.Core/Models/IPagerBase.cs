using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Models
{
    public interface IPagerBase
    {
        int PageIndex { get; set; }

        int PageSize { get; set; }

        int PageNumber { get; }
    }
}
