using System;

namespace MvcSolution.Infrastructure.Mvc
{
    public interface IStandardResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        void Succeed();
        void Fail();
        void Succeed(string message);
        void Fail(string message);
        void Try(Action action);
    }

    public interface IStandardResult<T> : IStandardResult
    {
        T Value { get; set; }
    }
}
