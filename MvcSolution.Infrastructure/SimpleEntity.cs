using System;

namespace MvcSolution.Infrastructure
{
    public interface ISimpleEntity
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }

    public interface ISimpleEntity<T> : ISimpleEntity
    {
        T Value { get; set; }
    }

    public class SimpleEntity : ISimpleEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class SimpleEntity<T> : SimpleEntity, ISimpleEntity<T>
    {
        public T Value { get; set; }
    }
}