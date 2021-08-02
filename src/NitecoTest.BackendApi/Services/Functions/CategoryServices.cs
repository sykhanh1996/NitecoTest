using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NitecoTest.BackendApi.Data;
using NitecoTest.BackendApi.Services.Interfaces;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.BackendApi.Services.Functions
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CategoryServices> _logger;

        public CategoryServices(ILogger<CategoryServices> logger,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<CategoriesVm> GetCategoriesPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.Categorys.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }
            return  new CategoriesVm();
        }
    }
}
