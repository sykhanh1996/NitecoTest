using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.BackendApi.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<CategoriesVm> GetCategoriesPaging(string filter, int pageIndex, int pageSize);
    }
}
