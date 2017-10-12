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
            IList<int> idsIgnorados = new List<int>();
            var query =
                from c in snfichadasIList
                group c by new
                {
                    c.Planta,
                    c.legajo,
                } into g
                select g;

            foreach (var fichadaList in query)
            {
                int legajo = fichadaList.First().legajo == 0 ? fichadaList.First().Tarjeta.Value : fichadaList.First().legajo;
                IList<SN_Fichadas> pivotList = sNFichadasDal.GetSNFichadaByEstadoSincro(EstadoSincroEnum.Enviado.GetHashCode()).Where(f => f.Planta == fichadaList.First().Planta && (f.legajo == legajo || f.Tarjeta.Value == legajo)).OrderBy(f => f.Fecha).ToList();
                SN_Fichadas pivot = pivotList.LastOrDefault();
                foreach (SN_Fichadas fichada in fichadaList)
                {
                    fichada.legajo = fichada.legajo == 0 ? fichada.Tarjeta.Value : fichada.legajo;
                    if (pivot != null)
                    {
                        TimeSpan diff = fichada.Fecha.Value - pivot.Fecha.Value;
                        if (diff.TotalSeconds < toleranceSeconds)
                        {
                            idsIgnorados.Add(fichada.Id);
                            sNFichadasDal.ChangeEstadoSincro(fichada.Id, EstadoSincroEnum.Ignorado.GetHashCode());
                        }
                        else
                            pivot = fichada;
                    }
                    else
                        pivot = fichada;

                }
            }

            foreach (int id in idsIgnorados)
            {
                snfichadasIList.Remove(snfichadasIList.Where(f => f.Id == id).First());
            }

            return snfichadasIList;
        }


        //public IList<SN_Fichadas> EliminarRegistrosSucesivos(IList<SN_Fichadas> snfichadasIList, int toleranceSeconds)
        //{
        //    IList<SN_Fichadas> snfichadasIListFinal = snfichadasIList.ToList();

        //    SN_Fichadas snfichadaMenor = snfichadasIListFinal.OrderBy(x => x.Fecha).FirstOrDefault();
        //    IList<SN_Fichadas> snFichadasExistentes = sNFichadasDal.GetSNFichadaByEstadoSincro(EstadoSincroEnum.Enviado.GetHashCode()).Where(x => x.Fecha.Value.Date >= snfichadaMenor.Fecha.Value.Date).ToList();
            
        //    IList<SN_Fichadas> snfichadasIListTotal = snfichadasIListFinal.Union(snFichadasExistentes).ToList();
        //    IList<SN_Fichadas> snfichadasIListTotalIgnore = new List<SN_Fichadas>();

        //    foreach (SN_Fichadas snfichada in snfichadasIList)
        //    {
        //        snfichada.legajo = snfichada.legajo == 0 ? snfichada.Tarjeta.Value : snfichada.legajo;

        //        foreach (SN_Fichadas snFichadaTotal in snfichadasIListTotal)
        //        {
        //            if (snfichada.Id != snFichadaTotal.Id)
        //            {
        //                TimeSpan diff = snfichada.Fecha.Value - snFichadaTotal.Fecha.Value;

        //                if (diff.TotalSeconds < toleranceSeconds)
        //                {
        //                    snfichadasIListFinal.Remove(snfichada);
        //                    snfichadasIListTotal.Remove(snfichada);

        //                    sNFichadasDal.ChangeEstadoSincro(snfichada.Id, EstadoSincroEnum.Ignorado.GetHashCode());
        //                }
        //                else
        //                    horaFichada = snfichada.Fecha.Value;
        //            }
        //        }
        //    }

        //    return snfichadasIListFinal;
        //}

    }
}
