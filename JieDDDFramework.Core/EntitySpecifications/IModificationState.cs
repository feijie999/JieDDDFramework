using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.EntitySpecifications
{
    public interface IModificationState
    {
        DateTime? ModifiedTime { get; set; }
    }
}
