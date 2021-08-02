using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NitecoTest.BackendApi.Data.Entities;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.BackendApi.Automapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerVm>().MaxDepth(2);
        }
    }
}
