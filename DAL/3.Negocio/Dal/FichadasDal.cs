using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

using Domain;
using AutoMapper;
using Domain.Security;
using Domain.Negocio;

namespace DAL.Negocio
{
    /// <summary>
    /// DAL para Fichadas
    /// </summary>
    public class FichadasDal : DalBase<ModelEntities>
    {
        public FichadasDal(ModelEntities context)
            : base(context)
        {
        }

        public void VerifyFichadaAdicional()
        {
            List<Fichadas> fichadas = context.Fichadas.ToList();
            foreach (Fichadas fichada in fichadas)
            {
                if (!context.FichadasAdicional.Where(fa => fa.Id == fichada.Id).Any())
                {
                    FichadasAdicional fichadasAdicional = new FichadasAdicional() { Id = fichada.Id, EstadoSincro = EstadoSincroEnum.ParaEnviar.GetHashCode() };
                    context.FichadasAdicional.Add(fichadasAdicional);
                }
            }
            context.SaveChanges();

        }

        public IList<Fichadas> GetFichadaByEstadoSincro(int idEstadoSincro)
        {
            var fichadasToSent = from fichada in context.Fichadas
                                 join fichadaAdicional in context.FichadasAdicional on fichada.Id equals fichadaAdicional.Id
                                 where fichadaAdicional.EstadoSincro == idEstadoSincro
                                 select fichada;

            return fichadasToSent.ToList();
        }

        public void ChangeEstadoSincro(IList<Fichadas> fichadasIList, int idEstadoSincro)
        {
            foreach (Fichadas fichada in fichadasIList)
            {
                FichadasAdicional fichadasAdicional = context.FichadasAdicional.Where(f => f.Id == fichada.Id).FirstOrDefault();
                fichadasAdicional.EstadoSincro = idEstadoSincro;
            }
            context.SaveChanges();

        }

        public int GetLegajoByPersona(int perso)
        {
            Personas persona = context.Personas.Where(p => p.Id == perso).FirstOrDefault();
            if (persona != null)
                return int.Parse(persona.Legajo);
            else
                return 0;
        }
    }
}