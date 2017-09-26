using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using DAL.Security;
using DAL.Negocio;
using Domain.Security;
using Domain.Negocio;

namespace Service.Security
{
    /// <summary>
    /// Servicio SNFichadasService
    /// </summary>
    public class SNFichadasService
    {
        private SNFichadasDal sNFichadasDal;
        private FichadasDal fichadasDal;
        private SNControladorEdificioDal sNControladorEdificioDal;

        /// <summary>
        /// Constructor de UniProfesoresService
        /// </summary>
        public SNFichadasService(SNFichadasDal sNFichadasDal, FichadasDal fichadasDal, SNControladorEdificioDal sNControladorEdificioDal)
        {
            this.sNFichadasDal = sNFichadasDal;
            this.fichadasDal = fichadasDal;
            this.sNControladorEdificioDal = sNControladorEdificioDal;
        }

        /// <summary>
        /// Retorna una lista con todos los sistemas
        /// </summary>
        /// <returns>Una lista con todos los sistemas</returns>
        public void SincroFichadas(IList<Fichadas> fichadasIList)
        {
            IList<SN_Fichadas> snfichadasIList = new List<SN_Fichadas>();
            foreach (Fichadas fichada in fichadasIList)
            {
                SN_Fichadas snFichada = new SN_Fichadas();
                snFichada.Id = fichada.Id;
                snFichada.Accion = fichada.Accion;
                snFichada.Capturada = fichada.Capturada;
                snFichada.Codigo = fichada.Codigo;
                snFichada.Consola = fichada.Consola;
                snFichada.Controlador = fichada.Controlador;
                snFichada.Fecha = fichada.Fecha;
                snFichada.IdPerso = fichada.IdPerso;
                snFichada.Observaciones = fichada.Observaciones;
                snFichada.Secuencia = fichada.Secuencia;
                snFichada.Tarjeta = fichada.Tarjeta;
                snFichada.Transferida = fichada.Transferida;
                snFichada.TReal = fichada.TReal;
                snFichada.EstadoSincro = EstadoSincroEnum.ParaEnviar.GetHashCode();

                snFichada.legajo = fichadasDal.GetLegajoByPersona(fichada.IdPerso.Value);

                snFichada.Planta = sNControladorEdificioDal.GetByControlador(fichada.Controlador);

                snfichadasIList.Add(snFichada);
            }

            sNFichadasDal.SincroFichadas(snfichadasIList);
        }

        public void EstadoSincroAEnProceso(IList<SN_Fichadas> snfichadas)
        {
            sNFichadasDal.ChangeEstadoSincro(snfichadas, EstadoSincroEnum.EnProceso.GetHashCode());
        }

        public void EstadoSincroAEnviado(SN_Fichadas snfichada)
        {
            sNFichadasDal.ChangeEstadoSincro(snfichada.Id, EstadoSincroEnum.Enviado.GetHashCode());
        }

        public IList<SN_Fichadas> GetFichadasParaEnviar()
        {
            return sNFichadasDal.GetSNFichadaByEstadoSincro(EstadoSincroEnum.ParaEnviar.GetHashCode());
        }

        public IList<SN_Fichadas> GetFichadasEnProceso()
        {
            return sNFichadasDal.GetSNFichadaByEstadoSincro(EstadoSincroEnum.EnProceso.GetHashCode());
        }

        public IList<sp_get_ProfesoresHs_Result> InfoSNFichadaParaInsertar(SN_Fichadas sNFichada)
        {
            return sNFichadasDal.InfoSNFichadaParaInsertar(sNFichada);
        }

        public IList<SN_Fichadas> EliminarRegistrosSucesivos(IList<SN_Fichadas> snfichadasIList, int toleranceSeconds)
        {
            IList<SN_Fichadas> snfichadasIListFinal = snfichadasIList.ToList();

            int legajo = 0;
            DateTime horaFichada = new DateTime();

            foreach (SN_Fichadas snfichada in snfichadasIList)
            {
                snfichada.legajo = snfichada.legajo == 0 ? snfichada.Tarjeta.Value : snfichada.legajo;

                if (legajo != snfichada.legajo)
                {
                    horaFichada = snfichada.Fecha.Value;
                    legajo = snfichada.legajo;
                }
                else
                {
                    TimeSpan diff = snfichada.Fecha.Value - horaFichada;

                    if (diff.TotalSeconds < toleranceSeconds)
                    {
                        snfichadasIListFinal.Remove(snfichada);
                        sNFichadasDal.ChangeEstadoSincro(snfichada.Id, EstadoSincroEnum.Ignorado.GetHashCode());
                    }
                    else
                        horaFichada = snfichada.Fecha.Value;
                }

            }

            return snfichadasIListFinal;
        }

    }
}
