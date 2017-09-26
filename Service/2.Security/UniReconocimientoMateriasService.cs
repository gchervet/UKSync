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
    /// Service para la clase UniReconocimientoMaterias
    /// </summary>
    public class UniReconocimientoMateriasService
    {
        UniReconocimientoMateriasDal UniReconocimientoMateriasDal;

        public UniReconocimientoMateriasService(UniReconocimientoMateriasDal UniReconocimientoMateriasDal)
        {
            this.UniReconocimientoMateriasDal = UniReconocimientoMateriasDal;
        }

        /// <summary>
        /// Obtiene todos los uniAlumnos. No tracking.
        /// </summary>
        public IList<uniReconocimientoMaterias> GetAll()
        {
            return UniReconocimientoMateriasDal.GetAll();
        }

        /// <summary>
        /// Abre un trámite.
        /// </summary>
        public void AbrirTramite(int nroTramite)
        {
            UniReconocimientoMateriasDal.AbrirTramite(nroTramite);
        }

        /// <summary>
        /// Cerrar un trámite.
        /// </summary>
        public void CerrarTramite(int nroTramite, int legProv)
        {
            UniReconocimientoMateriasDal.CerrarTramite(nroTramite, legProv);
        }

        /// <summary>
        /// Cambiar el estado a una materia (F Favorable, D Desfavorable)
        /// </summary>
        /// <returns></returns>
        public void CambiarEstadoMateria(int nroTramite, string codMat, string estado)
        {
            UniReconocimientoMateriasDal.CambiarEstadoMateria(nroTramite, codMat, estado);
        }
        
        /// <summary>
        /// Cambiar la fecha de resolución
        /// </summary>
        /// <returns></returns>
        public void CambiarFechaResolucion(int nroTramite, DateTime fechaResolucion)
        {
            UniReconocimientoMateriasDal.CambiarFechaResolucion(nroTramite,fechaResolucion);
        }
        
        /// <summary>
        /// Cambiar código de materia en un trámite
        /// </summary>
        /// <returns></returns>
        public void CambiarCodMatTramite(int nroTramite, string codMat)
        {
            UniReconocimientoMateriasDal.CambiarCodMatTramite(nroTramite, codMat);
        }
        
    }
}