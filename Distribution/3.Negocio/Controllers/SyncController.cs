using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

using DAL;
using Distribution.Attributes;
using Domain.Security;
using Domain.Negocio;
using System.Web;
using Service.Security;
using Service.Negocio;
using NLog;
using Domain;

namespace Distribution.Controllers
{
    /// <summary>
    /// Controlador WebApi para la clase Order
    /// </summary>
    [RoutePrefix("api/Sync")]
    public class SyncController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private FichadasService fichadasService;
        private SNFichadasService sNFichadasService;
        private UniProfesoresHsService uniProfesoresHsService;
        private SMTPService smtpService;

        public SyncController(FichadasService fichadasService, SNFichadasService sNFichadasService, UniProfesoresHsService uniProfesoresHsService, SMTPService smtpService)
        {
            this.fichadasService = fichadasService;
            this.sNFichadasService = sNFichadasService;
            this.uniProfesoresHsService = uniProfesoresHsService;
            this.smtpService = smtpService;
        }

        /// <summary>
        /// Obtiene todos los pedidos de la base de datos
        /// </summary>
        /// <returns></returns>
        [Route("Alive")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage Alive()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("Sincronizar")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage Sincronizar(int tolerancia)
        {
            ParaEnviarAEnProceso();
            SincroFichadas();
            EnProcesoAEnviado();
            ParaEnviarAEnProcesoSNFichadas();
            TransformarSNFichadasEnProceso(tolerancia);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("ParaEnviarAEnProceso")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void ParaEnviarAEnProceso()
        {
            fichadasService.VerifyFichadaAdicional();
            IList<Fichadas> fichadasIList = fichadasService.GetFichadasParaEnviar();
            fichadasService.EstadoSincroAEnProceso(fichadasIList);
        }

        [Route("SincroFichadas")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void SincroFichadas()
        {
            IList<Fichadas> fichadasIList = fichadasService.GetFichadasEnProceso();
            sNFichadasService.SincroFichadas(fichadasIList);
        }

        [Route("EnProcesoAEnviado")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void EnProcesoAEnviado()
        {
            IList<Fichadas> fichadasIList = fichadasService.GetFichadasEnProceso();
            fichadasService.EstadoSincroAEnviado(fichadasIList);
        }

        [Route("ParaEnviarAEnProcesoSNFichadas")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void ParaEnviarAEnProcesoSNFichadas()
        {
            IList<SN_Fichadas> snfichadasIList = sNFichadasService.GetFichadasParaEnviar();
            sNFichadasService.EstadoSincroAEnProceso(snfichadasIList);
        }

        [Route("TransformarSNFichadasEnProceso")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void TransformarSNFichadasEnProceso(int toleranceSeconds)
        {
            IList<SN_Fichadas> snfichadasIList = sNFichadasService.GetFichadasEnProceso();
            IList<SN_Fichadas> snfichadasIListFinal = sNFichadasService.EliminarRegistrosSucesivos(snfichadasIList, toleranceSeconds);

            foreach (SN_Fichadas snfichada in snfichadasIListFinal)
            {
                IList<sp_get_ProfesoresHs_Result> infoFichadas = sNFichadasService.InfoSNFichadaParaInsertar(snfichada);
                if (infoFichadas.Count == 1)
                {
                    sp_get_ProfesoresHs_Result infoF = infoFichadas.FirstOrDefault();
                    UniProfesoresHs uniProfesoresHs = uniProfesoresHsService.GetByCursoClaseLegajo((int)infoF.CursoId, infoF.ClaseNro, infoF.LegajoProfesor);

                    if (uniProfesoresHs != null)
                    {
                        if (uniProfesoresHs.Entrada.HasValue)
                        {
                            TimeSpan diff = snfichada.Fecha.Value - uniProfesoresHs.Entrada.Value;

                            //if (diff.TotalSeconds < toleranceSeconds)
                            //    break;

                            if (!uniProfesoresHs.Salida.HasValue)
                            {
                                uniProfesoresHs.Salida = snfichada.Fecha.Value;
                                uniProfesoresHs.SalidaEditada = snfichada.Fecha.Value;
                                uniProfesoresHs.FechaModificacion = DateTime.Today;
                                diff = uniProfesoresHs.Salida.Value - uniProfesoresHs.Entrada.Value;
                                decimal hours = (decimal)diff.TotalHours;
                                uniProfesoresHs.HsEfectivas = hours;
                            }
                            else
                            {
                                diff = snfichada.Fecha.Value - uniProfesoresHs.Salida.Value;

                                if (diff.TotalSeconds < toleranceSeconds)
                                    break;
                            }
                        }
                        else
                        {
                            uniProfesoresHs.Entrada = snfichada.Fecha.Value;
                            uniProfesoresHs.EntradaEditada = snfichada.Fecha.Value;
                            uniProfesoresHs.FechaModificacion = DateTime.Today;
                            if (!uniProfesoresHs.NovedadesId.HasValue || uniProfesoresHs.NovedadesId.Value == 0)
                                uniProfesoresHs.NovedadesId = TipoNovedadesEnum.HS_CATEDRA.GetHashCode();
                        }
                        uniProfesoresHsService.Update(uniProfesoresHs);
                    }
                    else
                    {
                        uniProfesoresHs = new UniProfesoresHs();
                        uniProfesoresHs.Ausente = false;
                        uniProfesoresHs.ClaseNro = infoF.ClaseNro;
                        uniProfesoresHs.Comentarios = "";
                        uniProfesoresHs.CursoId = infoF.CursoId;
                        uniProfesoresHs.Entrada = snfichada.Fecha;
                        uniProfesoresHs.EntradaEditada = snfichada.Fecha;
                        uniProfesoresHs.FechaCreacion = DateTime.Now;
                        uniProfesoresHs.FechaModificacion = DateTime.Now;
                        uniProfesoresHs.HsEfectivas = null;

                        TimeSpan diff = infoF.HoraFin.Value - infoF.HoraInicio.Value;
                        decimal hours = (decimal)diff.TotalHours;

                        uniProfesoresHs.HsPlanificadas = hours;
                        uniProfesoresHs.LegajoProfesor = snfichada.legajo == 0 ? snfichada.Tarjeta.Value : snfichada.legajo;
                        uniProfesoresHs.LegajoProfesorReemplazo = 0;
                        uniProfesoresHs.NoPlanificado = false;
                        uniProfesoresHs.NovedadesId = TipoNovedadesEnum.HS_CATEDRA.GetHashCode();
                        uniProfesoresHs.Revision = false;
                        uniProfesoresHs.Salida = null;
                        uniProfesoresHs.SalidaEditada = null;
                        uniProfesoresHs.Usuario = "UKSync";
                        uniProfesoresHs.Version = 1;

                        uniProfesoresHsService.Create(uniProfesoresHs);
                    }


                }
                else
                {
                    bool notFound = true;
                    foreach (sp_get_ProfesoresHs_Result infoFichada in infoFichadas)
                    {
                        if (notFound)
                        {
                            UniProfesoresHs uniProfesoresHs = uniProfesoresHsService.GetByCursoClaseLegajo((int)infoFichada.CursoId, infoFichada.ClaseNro, infoFichada.LegajoProfesor);
                            if (uniProfesoresHs != null)
                            {

                                TimeSpan diff = snfichada.Fecha.Value - uniProfesoresHs.Entrada.Value;

                                if (diff.TotalSeconds < toleranceSeconds)
                                {
                                    notFound = true;
                                }
                                else if (!uniProfesoresHs.Salida.HasValue)
                                {
                                    uniProfesoresHs.Salida = snfichada.Fecha;
                                    uniProfesoresHs.SalidaEditada = snfichada.Fecha;
                                    diff = uniProfesoresHs.Salida.Value - uniProfesoresHs.Entrada.Value;
                                    decimal hours = (decimal)diff.TotalHours;
                                    uniProfesoresHs.HsEfectivas = hours;
                                    uniProfesoresHs.FechaModificacion = DateTime.Today;
                                    notFound = false;
                                    uniProfesoresHsService.Update(uniProfesoresHs);
                                }
                                else
                                {
                                    diff = snfichada.Fecha.Value - uniProfesoresHs.Salida.Value;

                                    if (diff.TotalSeconds < toleranceSeconds)
                                        notFound = true;
                                }
                            }
                            else
                            {
                                uniProfesoresHs = new UniProfesoresHs();
                                uniProfesoresHs.Ausente = false;
                                uniProfesoresHs.ClaseNro = infoFichada.ClaseNro;
                                uniProfesoresHs.Comentarios = "";
                                uniProfesoresHs.CursoId = infoFichada.CursoId;
                                uniProfesoresHs.Entrada = snfichada.Fecha;
                                uniProfesoresHs.EntradaEditada = snfichada.Fecha;
                                uniProfesoresHs.FechaCreacion = DateTime.Now;
                                uniProfesoresHs.FechaModificacion = DateTime.Now;
                                uniProfesoresHs.HsEfectivas = null;

                                TimeSpan diff = infoFichada.HoraFin.Value - infoFichada.HoraInicio.Value;
                                decimal hours = (decimal)diff.TotalHours;

                                uniProfesoresHs.HsPlanificadas = hours;
                                uniProfesoresHs.LegajoProfesor = snfichada.legajo == 0 ? snfichada.Tarjeta.Value : snfichada.legajo; ;
                                uniProfesoresHs.LegajoProfesorReemplazo = 0;
                                uniProfesoresHs.NoPlanificado = false;
                                uniProfesoresHs.NovedadesId = TipoNovedadesEnum.HS_CATEDRA.GetHashCode();
                                uniProfesoresHs.Revision = false;
                                uniProfesoresHs.Salida = null;
                                uniProfesoresHs.SalidaEditada = null;
                                uniProfesoresHs.Usuario = "UKSync";
                                uniProfesoresHs.Version = 1;

                                uniProfesoresHsService.Create(uniProfesoresHs);
                                notFound = false;
                            }
                        }
                    }

                }
                sNFichadasService.EstadoSincroAEnviado(snfichada);
            }

        }

        [Route("ComprobarDesvios")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void ComprobarDesvios()
        {
            /// TODO:
            /// Cargar los envíos realizados en una tabla para no reenviar
            /// Borrar hardcodes de prueba

            List<UniProfesorInstitutoDto> desviosLista = uniProfesoresHsService.ComprobarDesvios();
            smtpService.EnviarAvisoDeDesvios(desviosLista);
        }

        [Route("Test")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void Test()
        {
            logger.Info("Test");
        }
    }
}