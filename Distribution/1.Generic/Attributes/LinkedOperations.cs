using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Distribution.Attributes
{
    /// <summary>
    /// Atributo de validacion de operaciones requeridas
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class LinkedOperationsAttribute : Attribute
    {
        private List<Operations> operations;

        public LinkedOperationsAttribute(params Operations[] allowedOperations)
        {
            if (allowedOperations == null)
            {
                this.operations = new List<Operations>();
            }
            else
            {
                this.operations = allowedOperations.ToList();
            }
        }

        public List<Operations> AllowedOperations
        {
            get { return operations; }
        }
    }
}