using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using DAL.Security;
using Domain.Security;
using System.Net.Mail;
using System.Configuration;

namespace Service.Security
{
    /// <summary>
    /// Service para la clase UniReconocimientoMaterias
    /// </summary>
    public class SMTPService
    {
        private UniCorreoEnviadoDal uniCorreoEnviadoDal;

        /// <summary>
        /// Constructor de UniProfesoresHsService
        /// </summary>
        public SMTPService(UniCorreoEnviadoDal uniCorreoEnviadoDal)
        {
            this.uniCorreoEnviadoDal = uniCorreoEnviadoDal;
        }

        public List<UniProfesorInstitutoDto> EnviarAvisoDeDesvios(List<UniProfesorInstitutoDto> desviosLista)
        {
            List<UniProfesorInstitutoDto> rtn = new List<UniProfesorInstitutoDto>();

            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTP_Servidor"]);
            SmtpServer.Port = Int32.Parse(ConfigurationManager.AppSettings["SMTP_Puerto"]);
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["CorreoEmisorDeAvisos"], "741852");
            SmtpServer.EnableSsl = true;

            foreach (UniProfesorInstitutoDto desvio in desviosLista)
            {
                try
                {
                    string horaEntrada = desvio.HoraInicio.HasValue ? desvio.HoraInicio.Value.TimeOfDay + " - " : "";
                    string horaSalida = desvio.HoraFin.HasValue ? desvio.HoraFin.Value.TimeOfDay + "" : "";

                    // Receptor
                    MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["CorreoEmisorDeAvisos"], desvio.CorreoInstituto);
                    
                    // CC
                    if(!string.IsNullOrEmpty(desvio.CorreoCopia))
                    {
                        string[] correoDeCopiaList = desvio.CorreoCopia.Split(';');
                        foreach (string correoDeCopia in correoDeCopiaList)
                        {
                            mail.CC.Add(new MailAddress(correoDeCopia));
                        }
                    }

                    mail.Subject = "[Desvío horario] " + desvio.LegajoProfesor + ": " + desvio.apellido;
                    mail.Body = "El profesor de legajo " + desvio.LegajoProfesor + ", " + desvio.apellido + " se encuentra desvíado del horario planificado \n\n" +
                        horaEntrada + horaSalida + "\n";

                    SmtpServer.Send(mail);
                    rtn.Add(desvio);

                    uniCorreoEnviado uniCorreoEnviadoModel = new uniCorreoEnviado();

                    uniCorreoEnviadoModel.codins = desvio.codins.HasValue ? desvio.codins.Value : 0;
                    uniCorreoEnviadoModel.cursoid = desvio.CursoId;
                    uniCorreoEnviadoModel.Emisor = ConfigurationManager.AppSettings["CorreoEmisorDeAvisos"];
                    uniCorreoEnviadoModel.EstadoAviso = desvio.EstadoAviso;
                    uniCorreoEnviadoModel.FechaDeEnvio = DateTime.Now;
                    uniCorreoEnviadoModel.legajoProfesor = desvio.LegajoProfesor;
                    uniCorreoEnviadoModel.Receptor = desvio.CorreoInstituto + (desvio.CorreoInstituto == null ? "" : ";" + desvio.CorreoInstituto);

                    uniCorreoEnviadoDal.GuardarCorreoEnviado(uniCorreoEnviadoModel);
                }
                catch (Exception e)
                {
                    //TODO: registrar error?
                }
            }

            return rtn;
        }
    }
}