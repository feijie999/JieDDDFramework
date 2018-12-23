using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.EntitySpecifications
{
    public interface IDeletedState
    {
        bool Deleted { get; set; }

        DateTime? DeletedTime { get; set; }
    }
}
