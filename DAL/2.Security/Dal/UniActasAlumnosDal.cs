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
    /// DAL para la clase UniActasAlumnos
    /// </summary>
    public class UniActasAlumnosDal : DalBase<SecurityEntities>
    {
        public UniActasAlumnosDal(SecurityEntities context)
            : base(context)
        {
        }

        /// <summary>
        /// Obtiene todos los uniActasAlumnos. No tracking.
        /// </summary>
        public IList<uniActasAlumnos> GetAll()
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<uniActasAlumnos> actasAlumnos = context.uniActasAlumnos.ToList();

            return actasAlumnos;
        }

        /// <summary>
        /// Obtiene uniActasAlumnos por codigo materia y legajo definitivo. No tracking.
        /// </summary>
        public uniActasAlumnos GetByCodMatLegDef(string codMat, int legDef)
        {
            context.Configuration.LazyLoadingEnabled = false;

            var query =
                from a in context.uniActas
                join aa in context.uniActasAlumnos on a.Clave equals aa.Clave
                where a.codmat == codMat && aa.legajo == legDef
                select aa;

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Actualiza el uniActasAlumnos
        /// </summary>
        public uniActasAlumnos Update(uniActasAlumnos uniActasAlumnos)
        {
            if (uniActasAlumnos == null)
            {
                throw new ArgumentNullException("uniActasAlumnos");
            }

            uniActasAlumnos uniActasAlumnosDb = context.uniActasAlumnos.Where(aa => aa.legajo == uniActasAlumnos.legajo && aa.Clave == uniActasAlumnos.Clave).FirstOrDefault();

            uniActasAlumnosDb.apellido = uniActasAlumnos.apellido;
            uniActasAlumnosDb.cond = uniActasAlumnos.cond;
            uniActasAlumnosDb.ausente = uniActasAlumnos.ausente;
            uniActasAlumnosDb.notaesc = uniActasAlumnos.notaesc;
            uniActasAlumnosDb.notaora = uniActasAlumnos.notaora;
            uniActasAlumnosDb.notadef = uniActasAlumnos.notadef;
            uniActasAlumnosDb.Fila = uniActasAlumnos.Fila;
            uniActasAlumnosDb.orden = uniActasAlumnos.orden;
            uniActasAlumnosDb.codigoComprobacion = uniActasAlumnos.codigoComprobacion;

            context.SaveChanges();

            return uniActasAlumnosDb;
        }
    }
}