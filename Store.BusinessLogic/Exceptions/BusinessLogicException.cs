using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Store.BusinessLogic.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public List<string> Errors { get; set; }
        public HttpStatusCode Code { get; set; }
        public BusinessLogicException(string message) : base(message) { }
    }
}
