using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;
using Domain;
using Domain.Security;
using Domain.Negocio;

namespace DAL.Security
{
    /// <summary>
    /// DAL para la clase Examen
    /// </summary>
    public class UniCorreoEnviadoDal : DalBase<SecurityEntities>
    {
        public UniCorreoEnviadoDal(SecurityEntities context)
            : base(context)
        {
        }

        public void GuardarCorreoEnviado(uniCorreoEnviado uniCorreoEnviado)
        {
            if (uniCorreoEnviado != null)
            {
                try
                {
                    if (uniCorreoEnviado.EstadoAviso != EstadoAvisoEnum.AVISO_ENTRADA.GetHashCode() || uniCorreoEnviado.EstadoAviso == null)
                    {
                        uniCorreoEnviado uniCorreoEnviadoModel = context.uniCorreoEnviado.Where(x => x.codins == uniCorreoEnviado.codins
                                                                                                    && x.legajoProfesor == uniCorreoEnviado.legajoProfesor
                                                                                                    && x.cursoid == uniCorreoEnviado.cursoid).FirstOrDefault();

                        if (uniCorreoEnviado != null)
                        {
                            uniCorreoEnviadoModel.EstadoAviso = uniCorreoEnviado.EstadoAviso;
                            uniCorreoEnviadoModel.FechaDeEnvio = uniCorreoEnviado.FechaDeEnvio;
                            uniCorreoEnviadoModel.Emisor = uniCorreoEnviado.Emisor;
                            uniCorreoEnviadoModel.Receptor = uniCorreoEnviado.Receptor;

                            context.Entry(uniCorreoEnviadoModel).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        context.uniCorreoEnviado.Add(uniCorreoEnviado);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public uniCorreoEnviado ObtenerPorId(int? codins, int? legajoProfesor, string cursoId)
        {
            try
            {
                return context.uniCorreoEnviado.Where(x => x.codins == codins && x.legajoProfesor == legajoProfesor && x.cursoid == cursoId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}