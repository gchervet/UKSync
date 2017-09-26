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
    /// Service para la clase uniAlumnos
    /// </summary>
    public class UniAlumnosService
    {
        UniAlumnosDal uniAlumnosDal;

        public UniAlumnosService(UniAlumnosDal uniAlumnosDal)
        {
            this.uniAlumnosDal = uniAlumnosDal;
        }

        /// <summary>
        /// Obtiene todos los uniAlumnos. No tracking.
        /// </summary>
        public IList<uniAlumnos> GetAll()
        {
            return uniAlumnosDal.GetAll();
        }

        /// <summary>
        /// Obtiene uniAlumno por medio de legajo definitivo.
        /// </summary>
        public uniAlumnos GetByLegDef(int legDef)
        {
            return uniAlumnosDal.GetByLegDef(legDef);
        }

        /// <summary>
        /// Obtiene uniAlumno por medio de legajo provisional.
        /// </summary>
        public uniAlumnos GetByLegProvi(int legProvi)
        {
            return uniAlumnosDal.GetByLegProvi(legProvi);
        }
    }
}