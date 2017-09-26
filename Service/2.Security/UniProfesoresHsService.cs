using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using DAL.Security;
using Domain.Security;
using System.Configuration;

namespace Service.Security
{
    /// <summary>
    /// Servicio UniProfesoresHs
    /// </summary>
    public class UniProfesoresHsService
    {
        private UniProfesoresHsDal uniProfesoresHsDal;
        private UniCorreoxEdificioDal uniCorreoxEdificioDal;
        private UniCorreoEnviadoDal uniCorreoEnviadoDal;
        private SNFichadasDal snFichadasDal;

        /// <summary>
        /// Constructor de UniProfesoresHsService
        /// </summary>
        public UniProfesoresHsService(UniProfesoresHsDal uniProfesoresHsDal, UniCorreoxEdificioDal uniCorreoxEdificioDal, SNFichadasDal snFichadasDal, UniCorreoEnviadoDal uniCorreoEnviadoDal)
        {
            this.uniProfesoresHsDal = uniProfesoresHsDal;
            this.uniCorreoxEdificioDal = uniCorreoxEdificioDal;
            this.snFichadasDal = snFichadasDal;
            this.uniCorreoEnviadoDal = uniCorreoEnviadoDal;
        }

        /// <summary>
        /// Retorna una lista con todos los sistemas
        /// </summary>
        /// <returns>Una lista con todos los sistemas</returns>
        public IList<UniProfesoresHs> GetAll()
        {
            return uniProfesoresHsDal.GetAll();
        }

        public UniProfesoresHs GetByCursoClaseLegajo(int cursoId, int claseNro, int legajoProfesor)
        {
            return uniProfesoresHsDal.GetByCursoClaseLegajo(cursoId, claseNro, legajoProfesor);
        }

        public void Create(UniProfesoresHs uniProfesoresHs)
        {
            uniProfesoresHsDal.Create(uniProfesoresHs);
        }

        public void Update(UniProfesoresHs uniProfesoresHs)
        {
            uniProfesoresHsDal.Update(uniProfesoresHs);
        }

        public List<UniProfesorInstitutoDto> ComprobarDesvios()
        {
            IList<UniCorreoxEdificio> correoxEdificioList = uniCorreoxEdificioDal.ObtenerEdificioCorreo();
            List<UniProfesorInstitutoDto> desvioList = new List<UniProfesorInstitutoDto>();

            foreach (UniCorreoxEdificio correoxEdificio in correoxEdificioList)
            {
                DateTime fechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime fechaHasta = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                IList<sp_get_ProfesoresHs_Result> fichadasPlanificadasPorDiaYEdificio = snFichadasDal.InfoHorasPlanificadasPorDia(correoxEdificio.codins, DateTime.Now.Date, fechaDesde, fechaHasta);

                foreach (sp_get_ProfesoresHs_Result fichadaPlanificada in fichadasPlanificadasPorDiaYEdificio)
                {
                    if (fichadaPlanificada.Ausente != 1)
                    {
                        int minutosDeTolerancia = Int32.Parse(ConfigurationManager.AppSettings["MinutosDeTolerancia"]);
                        DateTime? horaInicio = null;
                        DateTime? horaFin = null;

                        if (fichadaPlanificada.HoraInicio.HasValue)
                            horaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                fichadaPlanificada.HoraInicio.Value.Hour,
                                fichadaPlanificada.HoraInicio.Value.Minute,
                                fichadaPlanificada.HoraInicio.Value.Second);

                        if (fichadaPlanificada.HoraFin.HasValue)
                            horaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                fichadaPlanificada.HoraFin.Value.Hour,
                                fichadaPlanificada.HoraFin.Value.Minute,
                                fichadaPlanificada.HoraFin.Value.Second);

                        if (fichadaPlanificada.Persistido == 1)
                        {
                            if (!fichadaPlanificada.Entrada.HasValue)
                            {
                                if (horaInicio.HasValue)
                                {
                                    TimeSpan diferencia = DateTime.Now.Subtract(horaInicio.Value);
                                    if (diferencia.Minutes >= minutosDeTolerancia &&
                                        (fichadaPlanificada.NovedadesId == null || fichadaPlanificada.NovedadesId == 0 ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_CATEDRA.GetHashCode() ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_EVALUACION.GetHashCode()))
                                    {
                                        desvioList.Add(CargarDesvio(correoxEdificio, fichadaPlanificada));
                                    }
                                }
                            }
                            else
                            {
                                if (horaFin.HasValue)
                                {
                                    TimeSpan diferencia = DateTime.Now.Subtract(horaFin.Value);
                                    if (diferencia.Minutes >= minutosDeTolerancia &&
                                        (fichadaPlanificada.NovedadesId == null || fichadaPlanificada.NovedadesId == 0 ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_CATEDRA.GetHashCode() ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_EVALUACION.GetHashCode()))
                                    {
                                        desvioList.Add(CargarDesvio(correoxEdificio, fichadaPlanificada));
                                    }
                                }

                            }

                        }
                        else
                        {
                            if (horaInicio.HasValue)
                            {
                                TimeSpan diferencia = DateTime.Now.Subtract(horaInicio.Value);
                                if (diferencia.TotalMinutes >= minutosDeTolerancia &&
                                        (fichadaPlanificada.NovedadesId == null || fichadaPlanificada.NovedadesId == 0 ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_CATEDRA.GetHashCode() ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_EVALUACION.GetHashCode()))
                                {
                                    desvioList.Add(CargarDesvio(correoxEdificio, fichadaPlanificada));
                                }
                            }
                        }
                    }
                }
            }

            desvioList = ComprobarDesviosEnviados(desvioList);
            return desvioList;
        }

        private List<UniProfesorInstitutoDto> ComprobarDesviosEnviados(List<UniProfesorInstitutoDto> desvioList)
        {
            List<UniProfesorInstitutoDto> rtn = new List<UniProfesorInstitutoDto>();
            foreach (UniProfesorInstitutoDto desvio in desvioList)
            {
                uniCorreoEnviado uniCorreoEnviadoModel = uniCorreoEnviadoDal.ObtenerPorId(desvio.codins, desvio.LegajoProfesor, desvio.CursoId);
                if (uniCorreoEnviadoModel == null)
                {
                    desvio.EstadoAviso = EstadoAvisoEnum.AVISO_ENTRADA.GetHashCode();
                    rtn.Add(desvio);
                }
                else
                {
                    if(uniCorreoEnviadoModel.EstadoAviso == EstadoAvisoEnum.DEFAULT.GetHashCode())
                    {
                        desvio.EstadoAviso = EstadoAvisoEnum.AVISO_ENTRADA.GetHashCode();
                        rtn.Add(desvio);
                    }
                    if (uniCorreoEnviadoModel.EstadoAviso == EstadoAvisoEnum.AVISO_ENTRADA.GetHashCode())
                    {
                        desvio.EstadoAviso = EstadoAvisoEnum.AVISO_SALIDA.GetHashCode();
                        rtn.Add(desvio);
                    }
                }
            }
            return rtn;
        }

        private UniProfesorInstitutoDto CargarDesvio(UniCorreoxEdificio correoxEdificio, sp_get_ProfesoresHs_Result fichadaPlanificada)
        {
            UniProfesorInstitutoDto rtn = new UniProfesorInstitutoDto();

            if (correoxEdificio != null && fichadaPlanificada != null)
            {
                rtn.LegajoProfesor = fichadaPlanificada.LegajoProfesor;
                rtn.CorreoInstituto = correoxEdificio.Correo;
                rtn.apellido = fichadaPlanificada.apellido;
                rtn.ClaseNro = fichadaPlanificada.ClaseNro;
                rtn.codcur = fichadaPlanificada.codcur;
                rtn.codins = fichadaPlanificada.codins;
                rtn.codmat = fichadaPlanificada.codmat;
                rtn.comision = fichadaPlanificada.comision;
                rtn.CursoId = fichadaPlanificada.CursoId.ToString();
                rtn.FechaPlanificada = fichadaPlanificada.FechaPlanificada;
                rtn.HoraFin = fichadaPlanificada.HoraFin;
                rtn.HoraInicio = fichadaPlanificada.HoraInicio;
                rtn.LegajoProfesor = fichadaPlanificada.LegajoProfesor;
                rtn.turno = fichadaPlanificada.turno;
                rtn.Usuario = fichadaPlanificada.Usuario;
                rtn.CorreoCopia = correoxEdificio.CorreoCopia;

                return rtn;
            }
            return null;
        }
    }
}
