using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using DAL.Security;
using Domain.Security;

namespace Service.Security
{
    /// <summary>
    /// Service para la clase uniAlumnos
    /// </summary>
    public class UniArancelesService
    {
        UniArancelesDal uniArancelesDal;

        public UniArancelesService(UniArancelesDal uniArancelesDal)
        {
            this.uniArancelesDal = uniArancelesDal;
        }

        /// <summary>
        /// Obtiene todos los uniAranceles. No tracking.
        /// </summary>
        public IList<UniAranceles> GetAll()
        {
            return uniArancelesDal.GetAll();
        }

        /// <summary>
        /// Baja de arancel facturado
        /// </summary>
        public void BajaArancelFacturado(int legProv, int aniolectivo)
        {
            uniArancelesDal.BajaArancelFacturado(legProv, aniolectivo);
        }

        /// <summary>
        /// Baja de arancel no facturado
        /// </summary>
        public void BajaArancelNoFacturado(int legProv, int aniolectivo)
        {
            uniArancelesDal.BajaArancelNoFacturado(legProv, aniolectivo);
        }
    }
}