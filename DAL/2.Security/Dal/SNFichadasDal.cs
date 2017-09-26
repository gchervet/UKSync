using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;
using Domain;
using Domain.Security;
using Domain.Negocio;

namespace DAL.Security
{
    /// <summary>
    /// DAL para la clase SN_Fichadas
    /// </summary>
    public class SNFichadasDal : DalBase<SecurityEntities>
    {
        public SNFichadasDal(SecurityEntities context, ContextFactory contextFactory)
            : base(context, contextFactory)
        {
        }

        public void SincroFichadas(IList<SN_Fichadas> snfichadasIList)
        {
            foreach (SN_Fichadas SN_Fichada in snfichadasIList)
            {
                context.SN_Fichadas.Add(SN_Fichada);
            }
            context.SaveChanges();
        }

        public IList<sp_get_ProfesoresHs_Result> InfoSNFichadaParaInsertar(SN_Fichadas sNFichada)
        {
            List<sp_get_ProfesoresHs_Result> ProfesoresHsList = context.sp_get_ProfesoresHs(sNFichada.Planta, sNFichada.Fecha.Value.Date, "", null, null).ToList();
            List<sp_get_ProfesoresHs_Result> ProfesoresHsListFinal = new List<sp_get_ProfesoresHs_Result>();
            ProfesoresHsList = ProfesoresHsList.Where(phl => phl.LegajoProfesor == sNFichada.Tarjeta).ToList();

            ProfesoresHsList = ProfesoresHsList.Where(ph => (ph.NovedadesId.HasValue ? !NovedadesHsTotal(ph.NovedadesId.Value) : true) && ph.Ausente == 0).ToList();

            if (ProfesoresHsList.Count > 1)
            {
                var query =
                from c in ProfesoresHsList
                group c by new
                {
                    c.codins,
                    c.FechaPlanificada,
                    c.HoraInicio,
                } into g
                select g;


                foreach (var item in query)
                {
                    if (item.Count() > 1)
                    {
                        int alm = 0;
                        sp_get_ProfesoresHs_Result definitivo = new sp_get_ProfesoresHs_Result();
                        foreach (sp_get_ProfesoresHs_Result item2 in item)
                        {
                            int alm2 = (from uIMD in context.uniInscripcionesMateriasDetalle
                                        join uIM in context.uniInscripcionesMaterias on uIMD.Clave equals uIM.Clave
                                        where uIMD.cursoId == item2.CursoId && uIM.Eliminada.HasValue ? !uIM.Eliminada.Value : true
                                        select uIMD).Count();

                            if (alm2 > alm)
                                definitivo = item2;
                        }
                        ProfesoresHsListFinal.Add(definitivo);
                    }
                    else
                        ProfesoresHsListFinal.Add(item.First());
                }
            }
            else
            {
                ProfesoresHsListFinal = ProfesoresHsList;
            }

            return ProfesoresHsListFinal.OrderBy(ph => ph.HoraInicio.Value).ToList();
        }

        public IList<sp_get_ProfesoresHs_Result> InfoHorasPlanificadasPorDia(int codins, DateTime fecha, DateTime desde, DateTime hasta)
        {
            return context.sp_get_ProfesoresHs(codins, fecha, null, desde, hasta).ToList();
        }

        public IList<SN_Fichadas> GetSNFichadaByEstadoSincro(int idEstadoSincro)
        {
            return context.SN_Fichadas.Where(snf => snf.EstadoSincro == idEstadoSincro).ToList();
        }

        public void ChangeEstadoSincro(int idSNFichada, int idEstadoSincro)
        {
            SN_Fichadas sNFichada = context.SN_Fichadas.Where(f => f.Id == idSNFichada).FirstOrDefault();
            sNFichada.EstadoSincro = idEstadoSincro;
            context.SaveChanges();
        }

        public void ChangeEstadoSincro(IList<SN_Fichadas> sNFichadasIList, int idEstadoSincro)
        {
            foreach (SN_Fichadas sNFichada in sNFichadasIList)
            {
                SN_Fichadas sNFichadadb = context.SN_Fichadas.Where(f => f.Id == sNFichada.Id).FirstOrDefault();
                sNFichadadb.EstadoSincro = idEstadoSincro;
            }

            context.SaveChanges();
        }

        private bool NovedadesHsTotal(int novedadId)
        {
            return novedadId == TipoNovedadesEnum.HS_FERIADOS.GetHashCode()
                || novedadId == TipoNovedadesEnum.HS_LIC_ENFERMEDAD.GetHashCode()
                || novedadId == TipoNovedadesEnum.HS_LIC_ACCIDENTE.GetHashCode()
                || novedadId == TipoNovedadesEnum.HS_EXAMEN.GetHashCode()
                || novedadId == TipoNovedadesEnum.ASUETO_ADMINISTRATIVO.GetHashCode()
                || novedadId == TipoNovedadesEnum.HS_LIC_GREMIAL.GetHashCode()
                || novedadId == TipoNovedadesEnum.HS_LIC_NACIMIENTO.GetHashCode()
                || novedadId == TipoNovedadesEnum.HS_MATERNIDAD.GetHashCode()
                || novedadId == TipoNovedadesEnum.HS_JUSTIFICADAS.GetHashCode()
                || novedadId == TipoNovedadesEnum.FALLECIMIENTO.GetHashCode();
        }
    }
}