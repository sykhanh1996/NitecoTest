using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NitecoTest.BackendApi.Helpers
{
    public class ApiSuccessResponse<T> : ApiResponse
    {
        public T _objectResponse { get; }
        public List<T> _lstObjectResponse { get; }

        public ApiSuccessResponse(T objectResponse, List<T> lstObjectResponse = null)
            : base(200)
        {
            _objectResponse = objectResponse;
            _lstObjectResponse = lstObjectResponse;
        }
    }
}
