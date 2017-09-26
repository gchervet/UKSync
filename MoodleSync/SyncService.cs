using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace UKSync
{
    public partial class SyncService : ServiceBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private DateTime? lastAliveSignal = null;
        public bool runTasks;

        public SyncService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                EventLog.WriteEntry("OnStart", EventLogEntryType.Information);
                base.OnStart(args);
                this.runTasks = true;

                System.Threading.ThreadPool.QueueUserWorkItem(delegate { RunMainThread(); }, null);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(String.Concat("OnStart - ", ex.Message), EventLogEntryType.Error);
                logger.Error(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                EventLog.WriteEntry("OnStop", EventLogEntryType.Information);
                logger.Info("Deteniendo el servicio");
                this.runTasks = false;
                AcademicoApiClient.GetInstance().Dispose();
                base.OnStop();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(String.Concat("OnStop - ", ex.Message), EventLogEntryType.Error);

                logger.Error(ex);
            }
        }

        /// <summary>
        /// El hilo principal del servicio
        /// </summary>
        public void RunMainThread()
        {
            EventLog.WriteEntry(String.Concat("RunMainThread"), EventLogEntryType.Information);
            logger.Info("Ejecutando thread principal del servicio");
            while (runTasks)
            {
                try
                {
                    SendAliveSignal();
                }
                catch (Exception e)
                {
                    logger.Error(e);
                }

                try
                {
                    RunTasks();
                }
                catch (Exception e)
                {
                    logger.Error(e);
                }

                int timeout = 5;
                string timeoutString = ConfigurationManager.AppSettings["Timeout"];
                if (!string.IsNullOrEmpty(timeoutString))
                {
                    timeout = Convert.ToInt32(timeout);
                    timeout = timeout * 1000;
                }

                System.Threading.Thread.Sleep(timeout);
            }
            logger.Info("Saliendo del thread principal del servicio");
        }

        /// <summary>
        /// Envia una señal que indica que el servicio sigue en ejecución
        /// </summary>
        protected void SendAliveSignal()
        {
            if (!lastAliveSignal.HasValue || DateTime.Now > lastAliveSignal.Value.AddSeconds(20))
            {
                AcademicoApiClient.GetInstance().ApiGet<object>("api/Sync/Alive", null);
                lastAliveSignal = DateTime.Now;
            }
        }

        /// <summary>
        /// Consulta por las tareas a WebApi y las hace correr asincronicamente
        /// </summary>
        protected void RunTasks()
        {
            EventLog.WriteEntry("RunTasks", EventLogEntryType.Information);


            string cicloParam = ConfigurationManager.AppSettings["Ciclo"];
            string cuatrimestreParam = ConfigurationManager.AppSettings["Cuatrimestre"];

            if (!string.IsNullOrEmpty(cicloParam))
                cicloParam = DateTime.Today.Year.ToString();

            if (!string.IsNullOrEmpty(cuatrimestreParam))
                cuatrimestreParam = DateTime.Today.Month.ToString();

            Dictionary<string, string> parametrosDic = new Dictionary<string, string>();
            parametrosDic.Add("ciclo", cicloParam);
            parametrosDic.Add("cuatrimestre", cuatrimestreParam);


            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/MoodleSync/UpdateEnrolamiento", parametrosDic);
            EventLog.WriteEntry("UpdateEnrolamiento", EventLogEntryType.Information);

            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/MoodleSync/AltaCursos", null);
            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/MoodleSync/AltaGrupos", null);
            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/MoodleSync/AltaUsuarios", null);
            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/MoodleSync/AltaMatriculaciones", null);
            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/MoodleSync/AltaAgrupamientos", null);
            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/MoodleSync/BajaAgrupamientos", null);
            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/MoodleSync/BajaMatriculaciones", null);

            //string moodleDirectoryString = ConfigurationManager.AppSettings["MoodleDirectory"];
            //Process.Start(moodleDirectoryString);
        }

        /// <summary>
        /// Método para pruebas
        /// </summary>
        public void Start()
        {
            this.InitializeComponent();
            this.OnStart(null);
        }
    }
}
