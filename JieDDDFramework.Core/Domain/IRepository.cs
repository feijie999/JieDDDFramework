using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
