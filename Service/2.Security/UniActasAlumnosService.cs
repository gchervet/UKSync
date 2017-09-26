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
    /// Service para la clase UniActasAlumnos
    /// </summary>
    public class UniActasAlumnosService
    {
        UniActasAlumnosDal uniActasAlumnosDal;

        public UniActasAlumnosService(UniActasAlumnosDal uniActasAlumnosDal)
        {
            this.uniActasAlumnosDal = uniActasAlumnosDal;
        }

        /// <summary>
        /// Obtiene todos los uniActasAlumnos. No tracking.
        /// </summary>
        public IList<uniActasAlumnos> GetAll()
        {
            return uniActasAlumnosDal.GetAll();
        }

        /// <summary>
        /// Obtiene uniActasAlumnos por codigo materia y legajo definitivo. No tracking.
        /// </summary>
        public uniActasAlumnos GetByCodMatLegDef(string codMat, int legDef)
        {
            return uniActasAlumnosDal.GetByCodMatLegDef(codMat, legDef);
        }

        /// <summary>
        /// Actualiza el uniActasAlumnos
        /// </summary>
        public uniActasAlumnos Update(uniActasAlumnos uniActasAlumnos)
        {
            return uniActasAlumnosDal.Update(uniActasAlumnos);
        }
    }
}