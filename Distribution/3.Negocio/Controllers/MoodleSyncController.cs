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
using MoodleWS;
using System.Web.Script.Serialization;

namespace Distribution.Controllers
{
    /// <summary>
    /// Controlador WebApi para la clase Order
    /// </summary>
    [RoutePrefix("api/MoodleSync")]
    public class MoodleSyncController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MoodleSyncService MoodleSyncService;
        private MoodleOperations MoodleOperationsInstance;

        public MoodleSyncController(MoodleSyncService MoodleSyncService)
        {
            this.MoodleSyncService = MoodleSyncService;
            this.MoodleOperationsInstance = new MoodleOperations();
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

        /// <summary>
        /// Obtiene todos los pedidos de la base de datos
        /// </summary>
        /// <returns></returns>
        [Route("SincroAll")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public HttpResponseMessage SincroAll(int ciclo, int cuatri)
        {
            Dictionary<string, string> respuestas = new Dictionary<string, string>();
            string myJsonString = "";
            try
            {

                bool rta = MoodleSyncService.UpdateEnrolamiento(ciclo, cuatri);
                respuestas.Add("UpdateEnrolamiento", rta.ToString());

                rta = MoodleOperationsInstance.AltaCursos();
                respuestas.Add("AltaCursos", rta.ToString());

                rta = MoodleOperationsInstance.AltaGrupos();
                respuestas.Add("AltaGrupos", rta.ToString());

                rta = MoodleOperationsInstance.AltaUsuarios();
                respuestas.Add("AltaUsuarios", rta.ToString());

                rta = MoodleOperationsInstance.AltaMatriculaciones();
                respuestas.Add("AltaMatriculaciones", rta.ToString());

                rta = MoodleOperationsInstance.AltaAgrupamientos();
                respuestas.Add("AltaAgrupamientos", rta.ToString());

                rta = MoodleOperationsInstance.BajaAgrupamientos();
                respuestas.Add("BajaAgrupamientos", rta.ToString());

                rta = MoodleOperationsInstance.BajaMatriculaciones();
                respuestas.Add("BajaMatriculaciones", rta.ToString());

                myJsonString = (new JavaScriptSerializer()).Serialize(respuestas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
            return Request.CreateResponse(HttpStatusCode.OK, myJsonString);
        }


        [Route("UpdateEnrolamiento")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void UpdateEnrolamiento(int ciclo = 0, int cuatri = 0)
        {
            MoodleSyncService.UpdateEnrolamiento(ciclo, cuatri);
        }


        [Route("AltaCursos")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void AltaCursos()
        {
            MoodleOperationsInstance.AltaCursos();
        }

        [Route("AltaGrupos")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void AltaGrupos()
        {
            MoodleOperationsInstance.AltaGrupos();
        }

        [Route("AltaUsuarios")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void AltaUsuarios()
        {
            MoodleOperationsInstance.AltaUsuarios();
        }

        [Route("AltaMatriculaciones")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void AltaMatriculaciones()
        {
            MoodleOperationsInstance.AltaMatriculaciones();
        }

        [Route("AltaAgrupamientos")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void AltaAgrupamientos()
        {
            MoodleOperationsInstance.AltaAgrupamientos();
        }

        [Route("BajaAgrupamientos")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void BajaAgrupamientos()
        {
            MoodleOperationsInstance.BajaAgrupamientos();
        }

        [Route("BajaMatriculaciones")]
        [HttpGet, RequireHttps]
        [AllowAnonymous]
        public void BajaMatriculaciones()
        {
            MoodleOperationsInstance.BajaMatriculaciones();
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