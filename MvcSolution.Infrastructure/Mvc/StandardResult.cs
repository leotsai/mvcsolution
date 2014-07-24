using System;

namespace MvcSolution.Infrastructure.Mvc
{
    public class StandardResult : IStandardResult
    {
        #region Implementation of ICustomResult

        public bool Success { get; set; }

        public string Message { get; set; }

        public void Succeed()
        {
            this.Success = true;
        }

        public void Fail()
        {
            this.Success = false;
        }

        public void Succeed(string message)
        {
            this.Success = true;
            this.Message = message;
        }

        public void Fail(string message)
        {
            this.Success = false;
            this.Message = message;
        }

        public void Try(Action action)
        {
            try
            {
                action();
                this.Succeed();
            }
            catch (Exception ex)
            {
                var message = ex.GetAllMessages();
                if (AppContext.IsTestServer)
                {
                    this.Fail(message);
                }
                else
                {
                    if (ex is KnownException)
                    {
                        this.Fail(message);
                    }
                    else
                    {
                        this.Fail("服务器未知错误，请重试。如果该问题一直存在，请联系管理员。感谢您的支持。");
                    }
                }
            }
        }

        #endregion
    }

    public class StandardResult<T> : StandardResult, IStandardResult<T>
    {
        #region Implementation of ICustomResult<T>

        public T Value { get; set; }

        #endregion
    }
}
