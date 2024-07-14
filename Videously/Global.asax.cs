using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Videously.App_Start;
using Videously.Dtos;
using Videously.Models;

namespace Videously
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterMappings();
        }

        // This can be replaced with Mapping Profile
        private static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>(); //.ForMember(m => m.Id, opt => opt.Ignore());
                cfg.CreateMap<CustomerDto, Customer>()
                    .ForMember(m => m.Id, opt => opt.Ignore())
                    .ForMember(c => c.MembershipType, opt => opt.MapFrom(m => Mapper.Map<MembershipTypeDto, MembershipType>(m.MembershipType)));
                cfg.CreateMap<MembershipType, MembershipTypeDto>(); //.ForMember(m => m.Id, opt => opt.Ignore());
                cfg.CreateMap<MembershipTypeDto, MembershipType>(); //.ForMember(m => m.Id, opt => opt.Ignore());
                cfg.CreateMap<Movie, MovieDto>(); //.ForMember(m => m.Id, opt => opt.Ignore());
                cfg.CreateMap<MovieDto, Movie>()
                    .ForMember(m => m.Id, opt => opt.Ignore())
                    .ForMember(m => m.Genre, opt => opt.MapFrom(s => Mapper.Map<GenreDto, Genre>(s.Genre)));

                cfg.CreateMap<Genre, GenreDto>(); //.ForMember(m => m.Id, opt => opt.MapFrom(n => n.Id));
                cfg.CreateMap<GenreDto, Genre>(); //.ForMember(m => m.Id, opt => opt.Ignore()).AfterMap(AddOrUpdateGenres);
            });
        }
    }
}
