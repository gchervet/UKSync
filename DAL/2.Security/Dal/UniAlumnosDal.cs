using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;
using Domain;
using Domain.Security;

namespace DAL.Security
{
    /// <summary>
    /// DAL para la clase uniAlumnos
    /// </summary>
    public class UniAlumnosDal : DalBase<SecurityEntities>
    {
        public UniAlumnosDal(SecurityEntities context)
            : base(context)
        {
        }

        /// <summary>
        /// Obtiene todos los uniAlumnos. No tracking.
        /// </summary>
        public IList<uniAlumnos> GetAll()
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<uniAlumnos> alumnos = context.uniAlumnos.ToList();

            return alumnos;
        }

        /// <summary>
        /// Obtiene uniAlumno por medio de legajo definitivo.
        /// </summary>
        public uniAlumnos GetByLegDef(int legDef)
        {
            context.Configuration.LazyLoadingEnabled = false;

            uniAlumnos alumno = context.uniAlumnos.Where(a => a.legdef == legDef).FirstOrDefault();

            return alumno;
        }

        /// <summary>
        /// Obtiene uniAlumno por medio de legajo provisional.
        /// </summary>
        public uniAlumnos GetByLegProvi(int legProvi)
        {
            context.Configuration.LazyLoadingEnabled = false;

            uniAlumnos alumno = context.uniAlumnos.Where(a => a.legprovi == legProvi).FirstOrDefault();

            return alumno;
        }
    }
}