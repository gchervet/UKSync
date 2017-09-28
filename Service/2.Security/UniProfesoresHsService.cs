using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using DAL.Security;
using Domain.Security;
using System.Configuration;

namespace Service.Security
{
    /// <summary>
    /// Servicio UniProfesoresHs
    /// </summary>
    public class UniProfesoresHsService
    {
        private UniProfesoresHsDal uniProfesoresHsDal;
        private UniCorreoxEdificioDal uniCorreoxEdificioDal;
        private UniCorreoEnviadoDal uniCorreoEnviadoDal;
        private SNFichadasDal snFichadasDal;

        /// <summary>
        /// Constructor de UniProfesoresHsService
        /// </summary>
        public UniProfesoresHsService(UniProfesoresHsDal uniProfesoresHsDal, UniCorreoxEdificioDal uniCorreoxEdificioDal, SNFichadasDal snFichadasDal, UniCorreoEnviadoDal uniCorreoEnviadoDal)
        {
            this.uniProfesoresHsDal = uniProfesoresHsDal;
            this.uniCorreoxEdificioDal = uniCorreoxEdificioDal;
            this.snFichadasDal = snFichadasDal;
            this.uniCorreoEnviadoDal = uniCorreoEnviadoDal;
        }

        /// <summary>
        /// Retorna una lista con todos los sistemas
        /// </summary>
        /// <returns>Una lista con todos los sistemas</returns>
        public IList<UniProfesoresHs> GetAll()
        {
            return uniProfesoresHsDal.GetAll();
        }

        public UniProfesoresHs GetByCursoClaseLegajo(int cursoId, int claseNro, int legajoProfesor)
        {
            return uniProfesoresHsDal.GetByCursoClaseLegajo(cursoId, claseNro, legajoProfesor);
        }

        public void Create(UniProfesoresHs uniProfesoresHs)
        {
            uniProfesoresHsDal.Create(uniProfesoresHs);
        }

        public void Update(UniProfesoresHs uniProfesoresHs)
        {
            uniProfesoresHsDal.Update(uniProfesoresHs);
        }

        public List<UniProfesorInstitutoDto> ComprobarDesvios()
        {
            IList<UniCorreoxEdificio> correoxEdificioList = uniCorreoxEdificioDal.ObtenerEdificioCorreo();
            List<UniProfesorInstitutoDto> desvioList = new List<UniProfesorInstitutoDto>();

            foreach (UniCorreoxEdificio correoxEdificio in correoxEdificioList)
            {
                DateTime fechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime fechaHasta = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                IList<sp_get_ProfesoresHs_Result> fichadasPlanificadasPorDiaYEdificio = snFichadasDal.InfoHorasPlanificadasPorDia(correoxEdificio.codins, DateTime.Now.Date, fechaDesde, fechaHasta);

                foreach (sp_get_ProfesoresHs_Result fichadaPlanificada in fichadasPlanificadasPorDiaYEdificio)
                {
                    if (fichadaPlanificada.Ausente != 1)
                    {
                        int minutosDeTolerancia = Int32.Parse(ConfigurationManager.AppSettings["MinutosDeTolerancia"]);
                        DateTime? horaInicio = null;
                        DateTime? horaFin = null;

                        if (fichadaPlanificada.HoraInicio.HasValue)
                            horaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                fichadaPlanificada.HoraInicio.Value.Hour,
                                fichadaPlanificada.HoraInicio.Value.Minute,
                                fichadaPlanificada.HoraInicio.Value.Second);

                        if (fichadaPlanificada.HoraFin.HasValue)
                            horaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                fichadaPlanificada.HoraFin.Value.Hour,
                                fichadaPlanificada.HoraFin.Value.Minute,
                                fichadaPlanificada.HoraFin.Value.Second);

                        if (fichadaPlanificada.Persistido == 1)
                        {
                            if (!fichadaPlanificada.Entrada.HasValue)
                            {
                                if (horaInicio.HasValue)
                                {
                                    TimeSpan diferencia = DateTime.Now.Subtract(horaInicio.Value);
                                    if (diferencia.Minutes >= minutosDeTolerancia &&
                                        (fichadaPlanificada.NovedadesId == null || fichadaPlanificada.NovedadesId == 0 ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_CATEDRA.GetHashCode() ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_EVALUACION.GetHashCode()))
                                    {
                                        desvioList.Add(CargarDesvio(correoxEdificio, fichadaPlanificada));
                                    }
                                }
                            }
                            else
                            {
                                if (horaFin.HasValue)
                                {
                                    TimeSpan diferencia = DateTime.Now.Subtract(horaFin.Value);
                                    if (diferencia.Minutes >= minutosDeTolerancia &&
                                        (fichadaPlanificada.NovedadesId == null || fichadaPlanificada.NovedadesId == 0 ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_CATEDRA.GetHashCode() ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_EVALUACION.GetHashCode()))
                                    {
                                        desvioList.Add(CargarDesvio(correoxEdificio, fichadaPlanificada));
                                    }
                                }

                            }

                        }
                        else
                        {
                            if (horaInicio.HasValue)
                            {
                                TimeSpan diferencia = DateTime.Now.Subtract(horaInicio.Value);
                                if (diferencia.TotalMinutes >= minutosDeTolerancia &&
                                        (fichadaPlanificada.NovedadesId == null || fichadaPlanificada.NovedadesId == 0 ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_CATEDRA.GetHashCode() ||
                                         fichadaPlanificada.NovedadesId == TipoNovedadesEnum.HS_EVALUACION.GetHashCode()))
                                {
                                    desvioList.Add(CargarDesvio(correoxEdificio, fichadaPlanificada));
                                }
                            }
                        }
                    }
                }
            }

            desvioList = ComprobarDesviosEnviados(desvioList);
            return desvioList;
        }

        public void CalcularHoras(UniProfesoresHs mProfesoresHs, sp_get_ProfesoresHs_Result profesoresHs, int toleranciaEntrada, int toleranciaSalida, bool toleranciaAcumuladaDisponible)
        {
            bool toleranciaEntradaDisponible = toleranciaEntrada > 0;
            bool toleranciaSalidaDisponible = toleranciaSalida > 0;
            int toleranciaAcumulable = toleranciaEntrada + toleranciaSalida;

            if (mProfesoresHs.Salida.Value.Date != DateTime.MinValue.Date && mProfesoresHs.Entrada.Value.Date != DateTime.MinValue.Date)
            {
                TimeSpan _tiempoDictado = (mProfesoresHs.Salida.Value - mProfesoresHs.Entrada.Value);
                DateTime entradaReal = mProfesoresHs.Entrada.Value;
                DateTime entradaPlanificada = profesoresHs.HoraInicio.Value;
                DateTime salidaReal = mProfesoresHs.Salida.Value;
                DateTime salidaPlanificada = profesoresHs.HoraFin.Value;

                // --E--|-------|--S--
                if (entradaReal.TimeOfDay <= entradaPlanificada.TimeOfDay && salidaReal.TimeOfDay >= salidaPlanificada.TimeOfDay)
                {
                    _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                }
                // --E--|-----S--|----
                else if (entradaReal.TimeOfDay <= entradaPlanificada.TimeOfDay && salidaReal.TimeOfDay <= salidaPlanificada.TimeOfDay)
                {
                    if (toleranciaEntradaDisponible || toleranciaAcumuladaDisponible)
                    {
                        // Se toma en cuenta la tolerancia de entrada
                        if (toleranciaEntradaDisponible && salidaReal.TimeOfDay >= entradaPlanificada.TimeOfDay)
                        {
                            TimeSpan _tiempoFaltante = (profesoresHs.HoraFin.Value.TimeOfDay - mProfesoresHs.Salida.Value.TimeOfDay);
                            if ((_tiempoFaltante.Hours * 60) + _tiempoFaltante.Minutes <= toleranciaSalida)
                            {
                                _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                            }
                            else
                            {
                                _tiempoDictado = (mProfesoresHs.Salida.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                            }
                        }
                        else if (toleranciaAcumuladaDisponible && salidaReal.TimeOfDay >= entradaPlanificada.TimeOfDay)
                        {
                            TimeSpan _tiempoFaltante = (profesoresHs.HoraFin.Value.TimeOfDay - mProfesoresHs.Salida.Value.TimeOfDay);
                            if ((_tiempoFaltante.Hours * 60) + _tiempoFaltante.Minutes <= toleranciaAcumulable)
                            {
                                toleranciaAcumulable = toleranciaAcumulable - ((_tiempoFaltante.Hours * 60) + _tiempoFaltante.Minutes);
                                _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                            }
                            else
                            {
                                _tiempoDictado = (mProfesoresHs.Salida.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                            }
                        }
                        else
                        {
                            _tiempoDictado = (DateTime.MinValue.TimeOfDay - DateTime.MinValue.TimeOfDay);
                        }
                    }
                    else
                    {
                        // No se toma en cuenta la tolerancia de entrada
                        _tiempoDictado = (mProfesoresHs.Salida.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                    }
                }
                // ----|--E-----|--S--
                else if (entradaReal.TimeOfDay >= entradaPlanificada.TimeOfDay && salidaReal.TimeOfDay >= salidaPlanificada.TimeOfDay)
                {
                    if (toleranciaSalidaDisponible || toleranciaAcumuladaDisponible)
                    {
                        // Se toma en cuenta la tolerancia de salida
                        if (toleranciaSalidaDisponible && entradaReal.TimeOfDay <= salidaPlanificada.TimeOfDay)
                        {
                            TimeSpan _tiempoFaltante = (mProfesoresHs.Entrada.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                            if ((_tiempoFaltante.Hours * 60) + _tiempoFaltante.Minutes <= toleranciaEntrada)
                            {
                                _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                            }
                            else
                            {
                                _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - mProfesoresHs.Entrada.Value.TimeOfDay);
                            }
                        }
                        else if (toleranciaAcumuladaDisponible && entradaReal.TimeOfDay <= salidaPlanificada.TimeOfDay)
                        {
                            TimeSpan _tiempoFaltante = (mProfesoresHs.Entrada.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                            if ((_tiempoFaltante.Hours * 60) + _tiempoFaltante.Minutes <= toleranciaAcumulable)
                            {
                                _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                            }
                            else
                            {
                                _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - mProfesoresHs.Entrada.Value.TimeOfDay);
                            }
                        }
                        else
                        {
                            _tiempoDictado = (DateTime.MinValue.TimeOfDay - DateTime.MinValue.TimeOfDay);
                        }
                    }
                    else
                    {
                        // No se toma en cuenta la tolerancia de salida
                        _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - mProfesoresHs.Entrada.Value.TimeOfDay);
                    }
                }
                // ----|--E---S--|----
                else if (entradaReal.TimeOfDay >= entradaPlanificada.TimeOfDay && salidaReal.TimeOfDay <= salidaPlanificada.TimeOfDay)
                {
                    TimeSpan _tiempoFaltanteEntrada = (mProfesoresHs.Entrada.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                    TimeSpan _tiempoFaltanteSalida = (profesoresHs.HoraFin.Value.TimeOfDay - mProfesoresHs.Salida.Value.TimeOfDay);

                    if ((_tiempoFaltanteEntrada.Hours * 60) + _tiempoFaltanteEntrada.Minutes <= toleranciaEntrada || (_tiempoFaltanteEntrada.Hours * 60) + _tiempoFaltanteEntrada.Minutes <= toleranciaAcumulable)
                    {
                        // El profesor tiene tolerancia en la entrada
                        if ((_tiempoFaltanteSalida.Hours * 60) + _tiempoFaltanteSalida.Minutes <= toleranciaSalida || (_tiempoFaltanteSalida.Hours * 60) + _tiempoFaltanteSalida.Minutes <= toleranciaAcumulable)
                        {
                            // El profesor tiene tolerancia en la salida
                            _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                        }
                        else
                        {
                            // El profesor NO tiene tolerancia en la salida
                            _tiempoDictado = (mProfesoresHs.Salida.Value.TimeOfDay - profesoresHs.HoraInicio.Value.TimeOfDay);
                        }
                    }
                    else
                    {
                        // El profesor NO tiene tolerancia en la entrada
                        if (toleranciaAcumuladaDisponible)
                        {
                            toleranciaEntrada = toleranciaAcumulable - (_tiempoFaltanteEntrada.Hours * 60) + _tiempoFaltanteEntrada.Minutes;
                        }
                        if ((_tiempoFaltanteSalida.Hours * 60) + _tiempoFaltanteSalida.Minutes <= toleranciaSalida || (_tiempoFaltanteSalida.Hours * 60) + _tiempoFaltanteSalida.Minutes <= toleranciaAcumulable)
                        {
                            // El profesor tiene tolerancia en la salida
                            _tiempoDictado = (profesoresHs.HoraFin.Value.TimeOfDay - mProfesoresHs.Entrada.Value.TimeOfDay);
                        }
                        else
                        {
                            // El profesor NO tiene tolerancia en la salida 
                            _tiempoDictado = (mProfesoresHs.Salida.Value.TimeOfDay - mProfesoresHs.Entrada.Value.TimeOfDay);
                        }
                    }
                }
                else
                {
                    _tiempoDictado = (DateTime.MinValue.TimeOfDay - DateTime.MinValue.TimeOfDay);
                }

                double hsCalculadas = _tiempoDictado.Hours + _tiempoDictado.Minutes * 0.01;
                mProfesoresHs.HsEfectivas = (decimal)hsCalculadas;

                if (mProfesoresHs.HsEfectivas > mProfesoresHs.HsPlanificadas)
                    mProfesoresHs.HsEfectivas = mProfesoresHs.HsPlanificadas;
            }
            //return mProfesoresHs;
        }


        private List<UniProfesorInstitutoDto> ComprobarDesviosEnviados(List<UniProfesorInstitutoDto> desvioList)
        {
            List<UniProfesorInstitutoDto> rtn = new List<UniProfesorInstitutoDto>();
            foreach (UniProfesorInstitutoDto desvio in desvioList)
            {
                uniCorreoEnviado uniCorreoEnviadoModel = uniCorreoEnviadoDal.ObtenerPorId(desvio.codins, desvio.LegajoProfesor, desvio.CursoId);
                if (uniCorreoEnviadoModel == null)
                {
                    desvio.EstadoAviso = EstadoAvisoEnum.AVISO_ENTRADA.GetHashCode();
                    rtn.Add(desvio);
                }
                else
                {
                    if(uniCorreoEnviadoModel.EstadoAviso == EstadoAvisoEnum.DEFAULT.GetHashCode())
                    {
                        desvio.EstadoAviso = EstadoAvisoEnum.AVISO_ENTRADA.GetHashCode();
                        rtn.Add(desvio);
                    }
                    if (uniCorreoEnviadoModel.EstadoAviso == EstadoAvisoEnum.AVISO_ENTRADA.GetHashCode())
                    {
                        desvio.EstadoAviso = EstadoAvisoEnum.AVISO_SALIDA.GetHashCode();
                        rtn.Add(desvio);
                    }
                }
            }
            return rtn;
        }

        private UniProfesorInstitutoDto CargarDesvio(UniCorreoxEdificio correoxEdificio, sp_get_ProfesoresHs_Result fichadaPlanificada)
        {
            UniProfesorInstitutoDto rtn = new UniProfesorInstitutoDto();

            if (correoxEdificio != null && fichadaPlanificada != null)
            {
                rtn.LegajoProfesor = fichadaPlanificada.LegajoProfesor;
                rtn.CorreoInstituto = correoxEdificio.Correo;
                rtn.apellido = fichadaPlanificada.apellido;
                rtn.ClaseNro = fichadaPlanificada.ClaseNro;
                rtn.codcur = fichadaPlanificada.codcur;
                rtn.codins = fichadaPlanificada.codins;
                rtn.codmat = fichadaPlanificada.codmat;
                rtn.comision = fichadaPlanificada.comision;
                rtn.CursoId = fichadaPlanificada.CursoId.ToString();
                rtn.FechaPlanificada = fichadaPlanificada.FechaPlanificada;
                rtn.HoraFin = fichadaPlanificada.HoraFin;
                rtn.HoraInicio = fichadaPlanificada.HoraInicio;
                rtn.LegajoProfesor = fichadaPlanificada.LegajoProfesor;
                rtn.turno = fichadaPlanificada.turno;
                rtn.Usuario = fichadaPlanificada.Usuario;
                rtn.CorreoCopia = correoxEdificio.CorreoCopia;

                return rtn;
            }
            return null;
        }

    }
}
