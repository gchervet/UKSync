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
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowedForPasswordChangeAttribute : Attribute
    {
        public AllowedForPasswordChangeAttribute()
        {

        }
    }
}