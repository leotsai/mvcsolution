using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcSolution.Infrastructure.Mvc.Validation
{
    public static class DataAnnotationsModelValidatorProviderHelper
    {
        public static void RegisterAdapters()
        {
            var dict = new Dictionary<Type, Type>();
            dict.Add(typeof(DataTypeAttribute), typeof(DataTypeAttributeAdapter));
            //dict.Add(typeof(StringLengthAttribute), typeof(StringLengthAttributeAdapter));
            foreach (var pair in dict)
            {
                DataAnnotationsModelValidatorProvider.RegisterAdapter(pair.Key, pair.Value);
            }
        }
    }
}
