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
    /// Service para MoodleSync
    /// </summary>
    public class MoodleSyncService
    {
        MoodleSyncDal MoodleSyncDal;

        public MoodleSyncService(MoodleSyncDal MoodleSyncDal)
        {
            this.MoodleSyncDal = MoodleSyncDal;
        }

        public bool UpdateEnrolamiento(int ciclo = 0, int cuatri = 0)
        {
            return MoodleSyncDal.UpdateEnrolamiento(ciclo, cuatri);
        }
        
    }
}