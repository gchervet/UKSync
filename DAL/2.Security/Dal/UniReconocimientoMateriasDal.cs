using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;
using Domain;
using Domain.Security;

namespace DAL.Security
{
    /// <summary>
    /// DAL para la clase UniReconocimientoMaterias
    /// </summary>
    public class UniReconocimientoMateriasDal : DalBase<SecurityEntities>
    {
        public UniReconocimientoMateriasDal(SecurityEntities context)
            : base(context)
        {
        }

        /// <summary>
        /// Obtiene todos los reconocimientoMaterias. No tracking.
        /// </summary>
        public IList<uniReconocimientoMaterias> GetAll()
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<uniReconocimientoMaterias> reconocimientoMaterias = context.uniReconocimientoMaterias.ToList();

            return reconocimientoMaterias;
        }

        /// <summary>
        /// Abre un trámite.
        /// </summary>
        public void AbrirTramite(int nroTramite)
        {
            uniReconocimientoMaterias reconocimientoMateria = context.uniReconocimientoMaterias.Where(rm => rm.Clave == nroTramite).FirstOrDefault();

            if (reconocimientoMateria == null)
            {
                throw new ArgumentNullException("reconocimientoMateria");
            }

            reconocimientoMateria.estado = 1;
            reconocimientoMateria.numeroResolucion = null;
            reconocimientoMateria.fechaResolucion = null;
            reconocimientoMateria.CodigoComprobacion = null;

            uniReconocimientoMateriasFirmantes reconocimientoMateriasFirmantes = context.uniReconocimientoMateriasFirmantes.Where(rmf => rmf.idReconocimientoMaterias == nroTramite).FirstOrDefault();

            if (reconocimientoMateriasFirmantes == null)
            {
                throw new ArgumentNullException("reconocimientoMateriasFirmantes");
            }
            
            context.uniReconocimientoMateriasFirmantes.Remove(reconocimientoMateriasFirmantes);

            context.SaveChanges();
        }

        /// <summary>
        /// Cerrar un trámite.
        /// </summary>
        public void CerrarTramite(int nroTramite, int legProv)
        {
            uniReconocimientoMaterias recoMat = context.uniReconocimientoMaterias.Where(rm => rm.legajo == legProv && rm.Clave == nroTramite).FirstOrDefault();

            if (recoMat != null)
            {
                if (!context.uniReconocimientoMateriasDetalle.Where(rmd => rmd.estado != "F" && rmd.idReconocimientoMaterias == nroTramite).Any())
                {
                    if (recoMat.estado > 1)
                        recoMat.estado = 1;

                    context.SaveChanges();

                    context.sp_CierraReconocimientos(nroTramite, legProv);
                }
                else
                {
                    throw new ArgumentException("El reconocimiento tiene una o mas materias que no está en estado F");
                }
            }
            else
            {
                throw new ArgumentNullException("No se encontro reconocimiento con ese número de legajo y número de reconocimiento");
            }
        }

        /// <summary>
        /// Cambiar el estado a una materia (F Favorable, D Desfavorable)
        /// </summary>
        /// <returns></returns>
        public void CambiarEstadoMateria(int nroTramite, string codMat, string estado)
        {
            uniReconocimientoMateriasDetalle recoMatDetalle = context.uniReconocimientoMateriasDetalle.Where(rm => rm.idReconocimientoMaterias == nroTramite && rm.codmat == codMat).FirstOrDefault();

            if (recoMatDetalle != null)
            {
                recoMatDetalle.estado = estado;
            }
            else
            {
                throw new ArgumentNullException("uniReconocimientoMateriasDetalle");
            }
        }

        /// <summary>
        /// Cambiar la fecha de resolución
        /// </summary>
        /// <returns></returns>
        public void CambiarFechaResolucion(int nroTramite, DateTime fechaResolucion)
        {
            uniReconocimientoMaterias recoMat = context.uniReconocimientoMaterias.Where(rm => rm.Clave == nroTramite && rm.fechaResolucion.HasValue).FirstOrDefault();

            if (recoMat != null)
            {
                recoMat.fechaResolucion = fechaResolucion;

                context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("uniReconocimientoMateriasDetalle");
            }
        }

        /// <summary>
        /// Cambiar el estado a una materia (F Favorable, D Desfavorable)
        /// </summary>
        /// <returns></returns>
        public void CambiarCodMatTramite(int nroTramite, string codMat)
        {
            uniReconocimientoMateriasDetalle recoMat = context.uniReconocimientoMateriasDetalle.Where(rm => rm.idReconocimientoMaterias == nroTramite && rm.codmat == codMat).FirstOrDefault();

            if (recoMat != null)
            {
                recoMat.codmat = codMat;

                context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("uniReconocimientoMaterias");
            }
        }
    }
}