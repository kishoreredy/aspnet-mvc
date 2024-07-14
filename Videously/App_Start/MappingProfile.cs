using AutoMapper;
using Videously.Dtos;
using Videously.Models;

namespace Videously.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<CustomerDto, Customer>();

            });
        }
    }
}