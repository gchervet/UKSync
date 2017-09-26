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
    /// DAL para la clase UniProfesoresHs
    /// </summary>
    public class UniProfesoresHsDal : DalBase<SecurityEntities>
    {
        public UniProfesoresHsDal(SecurityEntities context, ContextFactory contextFactory)
            : base(context, contextFactory)
        {
        }

        /// <summary>
        /// Obtiene todos los sistemas. No tracking.
        /// </summary>
        public IList<UniProfesoresHs> GetAll()
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<UniProfesoresHs> profesoresHs = context.UniProfesoresHs.ToList();

            return profesoresHs;
        }

        public UniProfesoresHs GetByCursoClaseLegajo(int cursoId,int claseNro, int legajoProfesor)
        {
            return context.UniProfesoresHs.Where(uph => uph.CursoId == cursoId && uph.ClaseNro == claseNro && uph.LegajoProfesor == legajoProfesor).FirstOrDefault();
        }

        public void Create(UniProfesoresHs uniProfesoresHs)
        {
            context.UniProfesoresHs.Add(uniProfesoresHs);
            context.SaveChanges();
        }

        public void Update(UniProfesoresHs uniProfesoresHs)
        {
            UniProfesoresHs uniProfesoresHsDb = GetByCursoClaseLegajo((int)uniProfesoresHs.CursoId, uniProfesoresHs.ClaseNro, uniProfesoresHs.LegajoProfesor);
            uniProfesoresHsDb.Ausente = uniProfesoresHs.Ausente;
            uniProfesoresHsDb.ClaseNro = uniProfesoresHsDb.ClaseNro;
            uniProfesoresHsDb.Comentarios = uniProfesoresHs.Comentarios;
            uniProfesoresHsDb.CursoId = uniProfesoresHsDb.CursoId;
            uniProfesoresHsDb.Entrada = uniProfesoresHs.Entrada;
            uniProfesoresHsDb.EntradaEditada = uniProfesoresHs.EntradaEditada;
            uniProfesoresHsDb.FechaCreacion = uniProfesoresHsDb.FechaCreacion;
            uniProfesoresHsDb.FechaModificacion = DateTime.Now;
            uniProfesoresHsDb.HsEfectivas = uniProfesoresHs.HsEfectivas;
            uniProfesoresHsDb.HsPlanificadas = uniProfesoresHs.HsPlanificadas;
            uniProfesoresHsDb.LegajoProfesor = uniProfesoresHsDb.LegajoProfesor;
            uniProfesoresHsDb.LegajoProfesorReemplazo = uniProfesoresHs.LegajoProfesorReemplazo;
            uniProfesoresHsDb.NoPlanificado = uniProfesoresHs.NoPlanificado;
            uniProfesoresHsDb.NovedadesId = uniProfesoresHs.NovedadesId;
            uniProfesoresHsDb.Revision = uniProfesoresHs.Revision;
            uniProfesoresHsDb.Salida = uniProfesoresHs.Salida;
            uniProfesoresHsDb.SalidaEditada = uniProfesoresHs.SalidaEditada;
            uniProfesoresHsDb.Usuario = uniProfesoresHs.Usuario;
            uniProfesoresHsDb.Version = uniProfesoresHs.Version;
            context.SaveChanges();
        }
    }
}