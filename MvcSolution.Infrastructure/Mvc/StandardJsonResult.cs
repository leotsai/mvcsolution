using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSolution.Infrastructure;
using MvcSolution.Infrastructure.Extensions;
using MvcSolution.Infrastructure.Utilities;

namespace MvcSolution.Infrastructure.Mvc
{
    public class StandardJsonResult : JsonResult, IStandardResult
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
                this.Fail(ex.Message);
            }
        }

        #endregion

        public StandardJsonResult()
        {
            this.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var isHttpGet = context.HttpContext.Request.HttpMethod.Eq("get");
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && isHttpGet)
            {
                const string error = @"This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.";
                throw new InvalidOperationException(error);
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            response.Write(Serializer.ToJson(this.ToCustomResult()));
        }

        protected virtual IStandardResult ToCustomResult()
        {
            var result = new StandardResult();
            result.Success = this.Success;
            result.Message = this.Message;
            return result;
        }

        public void ValidateModelState(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var message = "<h5>Please clear the errors first:</h5><ul>";
                foreach (var error in modelState.Values.SelectMany(x => x.Errors))
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        message += "<li>" + error.ErrorMessage + "</li>";
                    }
                }
                message += "</ul>";
                this.Fail(message);
            }
            else
            {
                this.Succeed();
            }
        }
    }

    public class StandardJsonResult<T> : StandardJsonResult, IStandardResult<T>
    {
        public T Value { get; set; }

        protected override IStandardResult ToCustomResult()
        {
            var result =  new StandardResult<T>();
            result.Success = this.Success;
            result.Message = this.Message;
            result.Value = this.Value;
            return result;
        }
    }

}