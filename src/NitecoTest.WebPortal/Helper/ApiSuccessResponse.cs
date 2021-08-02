using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NitecoTest.WebPortal.Helper
{
    public class ApiSuccessResponse<T> : ApiResponse
    {
        public T _objectResponse { get; set; }

        public List<T> _lstObjectResponse { get; set; }

        //public ApiSuccessResponse(T objectResponse)
        //    : base(200)
        //{
        //    _objectResponse = objectResponse;
        //}
        public ApiSuccessResponse(T objectResponse, List<T> lstObjectResponse)
            : base(200)
        {
            _objectResponse = objectResponse;
            _lstObjectResponse = lstObjectResponse;
        }
    }
}
