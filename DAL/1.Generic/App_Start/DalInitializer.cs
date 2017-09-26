using AutoMapper;
using DAL.Negocio;
using DAL.Security;
using Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DAL.App_Start
{
    /// <summary>
    /// Clase de inicialización del a capa de datos
    /// </summary>
    public class DalInitializer
    {
        public static List<Delegate> loadedConfigurations = new List<Delegate>();

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
        /// <param name="configuration">COnfiguracion de automapper</param>
        private static void GetAutoMapperConfiguration(IConfiguration configuration)
        {
            AcademicoMappingSettings.RegisterMappings(configuration);
            ScanNETMappingSettings.RegisterMappings(configuration);
        }

    }
}
