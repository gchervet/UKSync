﻿using NLog;
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
                CanvasApiClient.GetInstance().Dispose();
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
                    RunTasks();
                }
                catch (Exception e)
                {
                    logger.Error(e);
                }

                int timeout = 3600;
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
        /// Consulta por las tareas a WebApi y las hace correr asincronicamente
        /// </summary>
        protected void RunTasks()
        {
            EventLog.WriteEntry("RunTasks", EventLogEntryType.Information);

            CanvasApiClient.GetInstance().SetSessionToken(UKSync.Properties.Settings.Default.Token);

            CanvasApiClient.GetInstance().ApiPost<HttpResponseMessage>("api/USER/SYNC", null);
            EventLog.WriteEntry("Sync user", EventLogEntryType.Information);

            CanvasApiClient.GetInstance().ApiPost<HttpResponseMessage>("api/INSCRIPTION/SYNC", null);
            EventLog.WriteEntry("Sync inscription", EventLogEntryType.Information);
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
