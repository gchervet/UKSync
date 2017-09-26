using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using DAL.Security;
using Domain.Security;

namespace Service.Security
{
    /// <summary>
    /// Service para la clase UniAniosLectivosFacturados
    /// </summary>
    public class UniAniosLectivosFacturadosService
    {
        UniAniosLectivosFacturadosDal uniAniosLectivosFacturadosDal;

        public UniAniosLectivosFacturadosService(UniAniosLectivosFacturadosDal uniAniosLectivosFacturadosDal)
        {
            this.uniAniosLectivosFacturadosDal = uniAniosLectivosFacturadosDal;
        }

        /// <summary>
        /// Obtiene todos los UniAniosLectivosFacturados. No tracking.
        /// </summary>
        public IList<UniAniosLectivosFacturados> GetAll()
        {
            return uniAniosLectivosFacturadosDal.GetAll();
        }

        /// <summary>
        /// Obtiene todos los UniAniosLectivosFacturados por legajo provisional. No tracking.
        /// </summary>
        public IList<UniAniosLectivosFacturados> GetAllByLegajo(int legProvi)
        {
            return uniAniosLectivosFacturadosDal.GetAllByLegajo(legProvi);
        }

        /// <summary>
        /// Crea un UniAniosLectivosFacturados
        /// </summary>
        public UniAniosLectivosFacturados Create(UniAniosLectivosFacturados uniAniosLectivosFacturados)
        {
            return uniAniosLectivosFacturadosDal.Create(uniAniosLectivosFacturados);
        }
    }
}