using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcSolution
{
    public class StandardJsonResult : ActionResult, IStandardResult
    {
        public string ContentType { get; set; }

        #region Implementation of ICustomResult

        public bool Success { get; set; }

        public string Message { get; set; }

        public StandardJsonResult()
        {
            this.ContentType = "application/json";
        }

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
            catch (KnownException ex)
            {
                this.Fail(ex.Message);
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.Where(x => !x.IsValid)
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.PropertyName + ": " + x.ErrorMessage);
                this.Fail("DB Validation Error：<br/>" + string.Join("<br/>", errors));
            }
            catch (Exception ex)
            {
                this.Fail(ex.GetAllMessages());
            }
        }

        #endregion

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = this.ContentType;
            response.ContentEncoding = Encoding.UTF8;
            response.Write(Serializer.ToJson(this.ToJsonObject()));
        }

        protected virtual IStandardResult ToJsonObject()
        {
            var result = new StandardResult();
            result.Success = this.Success;
            result.Message = this.Message;
            return result;
        }
    }

    public class StandardJsonResult<T> : StandardJsonResult, IStandardResult<T>
    {
        public T Value { get; set; }

        protected override IStandardResult ToJsonObject()
        {
            var result = new StandardResult<T>();
            result.Success = this.Success;
            result.Message = this.Message;
            result.Value = this.Value;
            return result;
        }
    }

}