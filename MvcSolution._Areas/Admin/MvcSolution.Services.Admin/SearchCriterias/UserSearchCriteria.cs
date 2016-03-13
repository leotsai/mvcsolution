using System;

namespace MvcSolution.Services.Admin
{
    public class UserSearchCriteria
    {
        public string Keyword { get; set; }
        public Guid? TagId { get; set; }
    }
}
