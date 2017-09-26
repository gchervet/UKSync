using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Negocio;

namespace DAL.Negocio
{
    public static class ScanNETMappingSettings
    {
        public static void RegisterMappings(IConfiguration configuration)
        {
            //configuration.CreateMap<ParameterAllowedValue, ParameterAllowedValueDto>()
            //            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            //            .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Value));

            //configuration.CreateMap<Auditing, AuditingDto>()
            //           .ForMember(dest => dest.IdAuditing, opts => opts.MapFrom(src => src.IdAuditing))
            //           .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date))
            //           .AfterMap((src, dest) =>
            //           {
            //               dest.User = new UserDto();
            //               dest.User.UserName = src.Username;
            //               if (!string.IsNullOrWhiteSpace(src.Roles))
            //               {
            //                   dest.User.Roles = src.Roles.Split('|').Select(y => new RoleDto { Name = y }).Where(x => x.Name != string.Empty).ToList();
            //               }
            //           });
        }
    }
}
