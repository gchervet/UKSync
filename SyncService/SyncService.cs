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
                    timeout = Convert.ToInt32(timeoutString);
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


            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/Sync/ParaEnviarAEnProceso", null);
            EventLog.WriteEntry("ParaEnviarAEnProceso", EventLogEntryType.Information);

            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/Sync/SincroFichadas", null);
            EventLog.WriteEntry("SincroFichadas", EventLogEntryType.Information);

            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/Sync/EnProcesoAEnviado", null);
            EventLog.WriteEntry("EnProcesoAEnviado", EventLogEntryType.Information);

            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/Sync/ParaEnviarAEnProcesoSNFichadas", null);
            EventLog.WriteEntry("ParaEnviarAEnProcesoSNFichadas", EventLogEntryType.Information);

            string toleranceString = ConfigurationManager.AppSettings["ToleranceInSeconds"];
            string toleranciaEntradaString = ConfigurationManager.AppSettings["ToleranciaEntrada"];
            string toleranciaSalidaString = ConfigurationManager.AppSettings["ToleranciaSalida"];
            string acumulativasString = ConfigurationManager.AppSettings["Acumulativas"];

            int toleranceSeconds = 0;
            if(!int.TryParse(toleranceString, out toleranceSeconds))
                toleranceSeconds = 120;

            int toleranciaEntrada = 0;
            int.TryParse(toleranciaEntradaString, out toleranciaEntrada);

            int toleranciaSalida = 0;
            int.TryParse(toleranciaSalidaString, out toleranciaSalida);

            bool acumulativas = false;
            bool.TryParse(acumulativasString, out acumulativas);

            Dictionary<string, string> toleranceSecondsDic = new Dictionary<string, string>();
            toleranceSecondsDic.Add("toleranceSeconds", toleranceSeconds.ToString());
            toleranceSecondsDic.Add("toleranciaEntrada", toleranciaEntrada.ToString());
            toleranceSecondsDic.Add("toleranciaSalida", toleranciaSalida.ToString());
            toleranceSecondsDic.Add("toleranciaAcumuladaDisponible", acumulativas.ToString());

            AcademicoApiClient.GetInstance().ApiGet<HttpResponseMessage>("api/Sync/TransformarSNFichadasEnProceso", toleranceSecondsDic);
            EventLog.WriteEntry("TransformarSNFichadasEnProceso", EventLogEntryType.Information);
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
