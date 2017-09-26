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
    /// DAL para la clase MoodleSync
    /// </summary>
    public class MoodleSyncDal : DalBase<SecurityEntities>
    {
        public MoodleSyncDal(SecurityEntities context)
            : base(context)
        {
        }

        public bool UpdateEnrolamiento(int ciclo, int cuatri)
        {
            bool error = false;
            int actualMonth = ciclo > 0 ? ciclo : DateTime.Today.Month;
            int actualYear = cuatri > 0 ? cuatri : DateTime.Today.Year;
            try
            {
                uniCuatrimestres cuatrimestre = context.uniCuatrimestres.Where(c => c.MesInicio >= actualMonth && c.MesFin <= actualMonth).FirstOrDefault();
                if (cuatrimestre != null)
                {
                    context.sp_ins_uniMoodle_matric_agrup_usuarios(actualYear, cuatrimestre.cuatri, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                error = true;
            }
            return error;
        }
    }
}