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

namespace Distribution.Controllers
{
    /// <summary>
    /// Controlador WebApi para la clase Operations
    /// </summary>
    [RoutePrefix("api/Operations")]
    public class OperationsController : ApiController
    {
        private UniCtaCteEstadoService uniCtaCteEstadoService;
        private UniAlumnosService uniAlumnosService;
        private UniAniosLectivosFacturadosService uniAniosLectivosFacturadosService;
        private UniExamenesService uniExamenesService;
        private UniActasAlumnosService uniActasAlumnosService;
        private CtaMovimientosService ctaMovimientosService;
        private CtaCierresDePeriodosService ctaCierresDePeriodosService;
        private UniArancelesService uniArancelesService;
        private UniReconocimientoMateriasService uniReconocimientoMateriasService;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public OperationsController(UniCtaCteEstadoService uniCtaCteEstadoService, UniAlumnosService uniAlumnosService, UniAniosLectivosFacturadosService uniAniosLectivosFacturadosService, UniExamenesService uniExamenesService, UniActasAlumnosService uniActasAlumnosService, CtaMovimientosService ctaMovimientosService, CtaCierresDePeriodosService ctaCierresDePeriodosService, UniArancelesService uniArancelesService, UniReconocimientoMateriasService uniReconocimientoMateriasService)
        {
            this.uniCtaCteEstadoService = uniCtaCteEstadoService;
            this.uniAlumnosService = uniAlumnosService;
            this.uniAniosLectivosFacturadosService = uniAniosLectivosFacturadosService;
            this.uniExamenesService = uniExamenesService;
            this.uniActasAlumnosService = uniActasAlumnosService;
            this.ctaMovimientosService = ctaMovimientosService;
            this.ctaCierresDePeriodosService = ctaCierresDePeriodosService;
            this.uniArancelesService = uniArancelesService;
            this.uniReconocimientoMateriasService = uniReconocimientoMateriasService;
        }

        /// <summary>
        /// Carga el Año Administrativo de Corte
        /// </summary>
        /// <returns></returns>
        [Route("CargaAAC")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage CargaAAC()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int legajo = Int32.Parse(httpRequest.Params["legajo"]);
                bool esDef = bool.Parse(httpRequest.Params["esDef"]);
                int AAC = Int32.Parse(httpRequest.Params["AAC"]);
                int legajoProvisional = 0;

                if (esDef)
                {
                    uniAlumnos uniAlumnos = uniAlumnosService.GetByLegProvi(legajo);
                    if (uniAlumnos != null)
                    {
                        legajoProvisional = uniAlumnos.legprovi;
                    }
                }
                else
                {
                    legajoProvisional = legajo;
                }

                if (legajoProvisional > 0)
                {
                    bool ok = false;

                    UniCtaCteEstado uniCtaCteEstado = uniCtaCteEstadoService.GetByLegProvi(legajoProvisional);
                    if (uniCtaCteEstado == null)
                    {
                        uniCtaCteEstado = new UniCtaCteEstado();
                        uniCtaCteEstado.legajo = legajoProvisional;
                        uniCtaCteEstado.fecha = DateTime.Today;
                        uniCtaCteEstado.Permisos = 0;
                        uniCtaCteEstado.Deuda = 0;
                        uniCtaCteEstado.InscrcripcionAFavor = 0;
                        uniCtaCteEstado.InsmatAnio = AAC;
                        uniCtaCteEstado.DeudaSuspension = null;
                        uniCtaCteEstado.DeudaBaja = null;
                        uniCtaCteEstado.Origen = null;

                        uniCtaCteEstadoService.Create(uniCtaCteEstado);
                        ok = true;
                    }
                    else if (uniCtaCteEstado.InsmatAnio < AAC)
                    {
                        uniCtaCteEstado.InsmatAnio = AAC;
                        uniCtaCteEstadoService.Update(uniCtaCteEstado);
                        ok = true;
                    }

                    if (ok)
                    {
                        IList<UniAniosLectivosFacturados> uniAniosLectivosFacturadosList = uniAniosLectivosFacturadosService.GetAllByLegajo(legajoProvisional);

                        for (int i = 0; i < uniCtaCteEstado.InsmatAnio.Value; i++)
                        {
                            if (!uniAniosLectivosFacturadosList.Where(alf => alf.añoLectivoFacturado == i + 1).Any())
                            {
                                UniAniosLectivosFacturados uniAniosLectivosFacturados = new UniAniosLectivosFacturados();
                                uniAniosLectivosFacturados.añoLectivoFacturado = i + 1;
                                uniAniosLectivosFacturados.legajo = legajoProvisional;
                                uniAniosLectivosFacturados.derechosExamenIncluidos = false;

                                uniAniosLectivosFacturadosService.Create(uniAniosLectivosFacturados);
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException("uniCtaCteEstado");
                    }
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Corrección de notas en actas de examen
        /// </summary>
        /// <returns></returns>
        [Route("CorreccionNotasActasExamen")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage CorreccionNotasActasExamen()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int legajoDef = Int32.Parse(httpRequest.Params["legajoDef"]);
                string codMat = httpRequest.Params["codMat"];
                int? nroActa = Int32.Parse(httpRequest.Params["nroActa"]);
                DateTime fechaActa = DateTime.Parse(httpRequest.Params["fechaActa"]);
                int folio = Int32.Parse(httpRequest.Params["folio"]);
                string libro = httpRequest.Params["libro"];
                int nota = Int32.Parse(httpRequest.Params["nota"]);
                bool ausente = bool.Parse(httpRequest.Params["ausente"]);

                uniExamenesService.sp_sav_examen_a_verificar(legajoDef, codMat, nroActa, fechaActa, folio, libro, nota, ausente);

                uniActasAlumnos uniActasAlumnos = uniActasAlumnosService.GetByCodMatLegDef(codMat, legajoDef);

                if (uniActasAlumnos != null)
                {
                    uniActasAlumnos.ausente = ausente ? 1 : 0;
                    uniActasAlumnos.notadef = nota;

                    uniActasAlumnosService.Update(uniActasAlumnos);
                }
                else
                {
                    throw new ArgumentNullException("uniActasAlumnos");
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Anular un movimiento
        /// </summary>
        /// <returns></returns>
        [Route("AnularMovimiento")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage AnularMovimiento()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int nroMovimiento = Int32.Parse(httpRequest.Params["nroMovimiento"]);

                CtaMovimientos ctaMovimiento = ctaMovimientosService.GetByMovimientoNro(nroMovimiento);

                if (ctaCierresDePeriodosService.MovimientoEnPeriodoContableAbierto(ctaMovimiento))
                {
                    ctaMovimientosService.sp_cta_Anular_Movimiento(nroMovimiento);
                }
                else
                {
                    throw new ArgumentNullException("ctaMovimientos");
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Cambio de condición de pago de arancelamiento
        /// </summary>
        /// <returns></returns>
        [Route("CambioCondicionPagoArancelamiento")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage CambioCondicionPagoArancelamiento()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int legProv = Int32.Parse(httpRequest.Params["legProv"]);
                int anioLectivo = string.IsNullOrEmpty(httpRequest.Params["anioLectivo"]) ? 0 : Int32.Parse(httpRequest.Params["anioLectivo"]);

                UniAniosLectivosFacturados anioFacturado = uniAniosLectivosFacturadosService.GetAllByLegajo(legProv).AsEnumerable().LastOrDefault();

                if (anioLectivo > 0)
                {
                    anioFacturado = uniAniosLectivosFacturadosService.GetAllByLegajo(legProv).Where(alf => alf.añoLectivoFacturado == anioLectivo).FirstOrDefault();
                }


                if (anioFacturado != null)
                    uniArancelesService.BajaArancelFacturado(legProv, anioFacturado.añoLectivoFacturado);
                else
                    uniArancelesService.BajaArancelNoFacturado(legProv, anioLectivo);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Abrir un trámite
        /// </summary>
        /// <returns></returns>
        [Route("AbrirTramite")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage AbrirTramite()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int nroTramite = Int32.Parse(httpRequest.Params["nroTramite"]);

                uniReconocimientoMateriasService.AbrirTramite(nroTramite);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Cerrar un trámite
        /// </summary>
        /// <returns></returns>
        [Route("CerrarTramite")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage CerrarTramite()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int nroTramite = Int32.Parse(httpRequest.Params["nroTramite"]);
                int legProv = Int32.Parse(httpRequest.Params["legProv"]);

                uniReconocimientoMateriasService.CerrarTramite(nroTramite, legProv);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Cambiar el estado a una materia (F Favorable, D Desfavorable)
        /// </summary>
        /// <returns></returns>
        [Route("CambiarEstadoMateria")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage CambiarEstadoMateria()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int nroTramite = Int32.Parse(httpRequest.Params["nroTramite"]);
                string codMat = httpRequest.Params["codMat"].ToString();
                string estado = httpRequest.Params["estado"].ToString();

                uniReconocimientoMateriasService.CambiarEstadoMateria(nroTramite, codMat, estado);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Correcciones de cobros con tarjetas (con POSNET) - Corregir tarjeta
        /// </summary>
        /// <returns></returns>
        [Route("CorregirTarjeta")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage CorregirTarjeta()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int nroMovimiento = Int32.Parse(httpRequest.Params["nroMovimiento"]);
                int tipoTarjeta = Int32.Parse(httpRequest.Params["tipoTarjeta"]);

                ctaMovimientosService.CorregirTarjeta(nroMovimiento, tipoTarjeta);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Cambiar la fecha de resolución
        /// </summary>
        /// <returns></returns>
        [Route("CambiarFechaResolucion")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage CambiarFechaResolucion()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int nroTramite = Int32.Parse(httpRequest.Params["nroTramite"]);
                DateTime fechaResolucion = DateTime.Parse(httpRequest.Params["fechaResolucion"]);

                uniReconocimientoMateriasService.CambiarFechaResolucion(nroTramite, fechaResolucion);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Cambiar código de materia en un trámite
        /// </summary>
        /// <returns></returns>
        [Route("CambiarCodMatTramite")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage CambiarCodMatTramite()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                int nroTramite = Int32.Parse(httpRequest.Params["nroTramite"]);
                string codMat = httpRequest.Params["codMat"].ToString();

                uniReconocimientoMateriasService.CambiarCodMatTramite(nroTramite, codMat);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted);
                return response;
            }
            catch (ArgumentNullException ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                return response;
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        [Route("Test")]
        [HttpPost, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage Test()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, "Error message");
            return response;
        }

    }
}