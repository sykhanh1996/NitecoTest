﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NitecoTest.BackendApi.Helpers;
using NitecoTest.ViewModels;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.BackendApi.Services.Interfaces
{
    public interface IOrderServices
    {
        Task<Pagination<OrdersVm>> GetOrdersPaging(string filter, int pageIndex, int pageSize);
    }
}
