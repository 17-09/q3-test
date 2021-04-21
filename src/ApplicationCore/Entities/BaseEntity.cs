using System;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntity : BaseEntity<Guid>
    {
    }

    public abstract class BaseEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}