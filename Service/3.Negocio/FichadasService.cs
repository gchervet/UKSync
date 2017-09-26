using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using Domain.Negocio;
using DAL.Negocio;

namespace Service.Negocio
{
    /// <summary>
    /// Servicio FichadasService
    /// </summary>
    public class FichadasService
    {
        private FichadasDal fichadasDal;

        /// <summary>
        /// Constructor de FichadasService
        /// </summary>
        public FichadasService(FichadasDal fichadasDal)
        {
            this.fichadasDal = fichadasDal;
        }

        public void VerifyFichadaAdicional()
        {
            fichadasDal.VerifyFichadaAdicional();
        }

        public void EstadoSincroAEnProceso(IList<Fichadas> fichadasIList)
        {
            fichadasDal.ChangeEstadoSincro(fichadasIList, EstadoSincroEnum.EnProceso.GetHashCode());
        }

        public void EstadoSincroAEnviado(IList<Fichadas> fichadasIList)
        {
            fichadasDal.ChangeEstadoSincro(fichadasIList, EstadoSincroEnum.Enviado.GetHashCode());
        }

        public IList<Fichadas> GetFichadasParaEnviar()
        {
            return fichadasDal.GetFichadaByEstadoSincro(EstadoSincroEnum.ParaEnviar.GetHashCode());
        }

        public IList<Fichadas> GetFichadasEnProceso()
        {
            return fichadasDal.GetFichadaByEstadoSincro(EstadoSincroEnum.EnProceso.GetHashCode());
        }
    }
}
