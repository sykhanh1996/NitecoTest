using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NitecoTest.BackendApi.Data.Entities;

namespace NitecoTest.BackendApi.Automapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Order, PostVm>().MaxDepth(2);
        }
    }
}
