using ConsumosCapataz.Domain;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace ConsumosCapataz.DAL
{
    /// <summary>
    /// Modulo de manejor de errores de validación de entidadd en la base de datos.
    /// </summary>
    public class ModelEntitiesEntityErrorModule : IModelEntitiesErrorModule
    {
        private ModelEntitiesModuleRunner modelEntities;

        /// <summary>
        /// Agrega información sobre los errores de validación que ocurran en la base de datos a la excepción que se lanzó.
        /// </summary>
        /// <param name="error">Error lanzado al guardar la entidad</param>
        /// <returns>Excepción con la información de los errores de validación de entidades agregada</returns>
        public Exception OnError(Exception error)
        {
            if (error.GetType() == typeof(DbEntityValidationException))
            {
                DbEntityValidationException ex = (DbEntityValidationException)error;

                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(error.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                error = new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return error;
        }

        /// <summary>
        /// Setea el contexto sobre el que se ejecutará el módulo.
        /// </summary>
        /// <param name="context">Contexto sobre el que se ejecutará el módulo.</param>
        public void SetContext(ModelEntitiesModuleRunner context)
        {
            this.modelEntities = context;
        }
    }
}