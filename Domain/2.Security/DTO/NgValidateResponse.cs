using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    /// <summary>
    /// Clase para realizar retornos obedientes a la estructura definida por la librería de angularjs ng-remote-validation.
    /// </summary>
    public class NgValidateResponse
    {
        public bool isValid
        {
            get; set;
        }

        public string value
        {
            get; set;
        }
    }
}