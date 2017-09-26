using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Clase de ayuda para enumerar los tipos de parametros. Debe reflejar la tabla ParameterType de la base de datos.
    /// </summary>
    public class ParameterTypes
    {
        private static Dictionary<int, Type> tipos = new Dictionary<int, Type> 
        { 
            //{ 1, typeof(int) },
            //{ 2, typeof(bool) },
            //{ 3, typeof(string) },
            //{ 4, typeof(List<string>) },
            //{ 5, typeof(char) },

            { 1, typeof(string) },
            { 2, typeof(char) },
            { 3, typeof(decimal) },
            { 4, typeof(bool) },
            { 5, typeof(DateTime) },
            { 6, typeof(List<string>) },
        };

        /// <summary>
        /// Obtiene el tipo de un parametro
        /// </summary>
        /// <param name="parameterTypeId">Id del parametro</param>
        /// <returns>El tipo del parametro</returns>
        public static Type GetParameterType(int parameterTypeId)
        {
            Type val;
            tipos.TryGetValue(parameterTypeId, out val);
            return val;
        }

        /// <summary>
        /// Obtiene el Id de un tipo de parametro
        /// </summary>
        /// <param name="type">Typo que representa el ParameterType</param>
        /// <returns>Id del tipo de parametro</returns>
        public static Type GetParameterTypeId(Type type)
        {
            KeyValuePair<int, Type> pair = tipos.FirstOrDefault(x => x.Value == type);

            //if (pair.Key == default(KeyValuePair<int, Type>).Key && pair.Value == default(KeyValuePair<int, Type>).Value)
            //{
            //    return null;
            //}
            return pair.Value;
        }
    }
}
