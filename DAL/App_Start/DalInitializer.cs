using AutoMapper;
using ConsumosCapataz.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.DAL.App_Start
{
    /// <summary>
    /// Clase de inicialización de la capa de datos
    /// </summary>
    public class DalInitializer
    {
        /// <summary>
        /// Inicializa la capa de datos, debería llamarse en Global.asax de WebApi
        /// </summary>
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize(x => GetAutoMapperConfiguration(x));
        }

        /// <summary>
        /// Configuración de los mapeos de automapper
        /// </summary>
        /// <param name="configuration">Configuración de automapper</param>
        private static void GetAutoMapperConfiguration(IConfiguration configuration)
        {
            //configuration.CreateMap<OperationGroupsOperations, OperationDto>()
            //    .ForMember(dest => dest.IsRequired, opts => opts.MapFrom(src => src.IsRequired))
            //    .ForMember(dest => dest.IdOperation, opts => opts.MapFrom(src => src.IdOperation))
            //    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Operation.Name))
            //    .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Operation.Description))
            //    .ForMember(dest => dest.SortOrder, opts => opts.MapFrom(src => src.Operation.SortOrder))
            //    .ForMember(dest => dest.ShortDescription, opts => opts.MapFrom(src => src.Operation.ShortDescription));

        }

    }
}
