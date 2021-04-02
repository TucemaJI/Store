using Store.BusinessLogic.Models.Base;
using System;
using System.Collections.Generic;
using System.Net;

namespace Store.BusinessLogic.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public List<string> Errors { get; set; }
        public HttpStatusCode Code { get; set; }
        public BusinessLogicException(List<string> errors, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            Code = code;
            Errors = errors;
        }
    }
}
