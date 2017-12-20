using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Common
{
    public class TEBApiResponse
    {
        public TEBApiResponse()
        {

        }

        public TEBApiResponse(bool issuccess, string message)
        {
            IsSuccess = issuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
