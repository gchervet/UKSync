using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

/// <summary> 
/// Contiene Tipos para realizar llamadas a clientes REST
/// </summary>
namespace Domain
{
    /// <summary>
    /// Representa excepción que se lanza al verificar el checksum de una entidad obtenida desde la base de datos
    /// </summary>
    [Serializable]
    public class ChecksumException : Exception
    {
        /// <summary>
        /// Inicializa la clase
        /// </summary>
        public ChecksumException()
            : base("Ocurrió un error de verificación de checksum")
        {
        }

        /// <summary>
        /// Inicializa la clase
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        public ChecksumException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Inicializa la clase
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        /// <param name="innerException">Excepción anterior</param>
        public ChecksumException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}