using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Domain
{
    public abstract class Entity<TKey> : Entity where TKey :class
    {
        public virtual TKey Id { get; protected set; }

        public bool IsTransient()
        {
            return this.Id == default(TKey);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TKey>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity<TKey>)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!RequestedHashCode.HasValue)
                    RequestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return RequestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            if (Equals(left, null))
                return Equals(right, null);
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
