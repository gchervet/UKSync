
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Domain.Security
{

using System;
    using System.Collections.Generic;
    
public partial class UniCtaCteEstado
{

    public int legajo { get; set; }

    public System.DateTime fecha { get; set; }

    public int Permisos { get; set; }

    public Nullable<decimal> Deuda { get; set; }

    public Nullable<decimal> InscrcripcionAFavor { get; set; }

    public Nullable<int> InsmatAnio { get; set; }

    public Nullable<decimal> DeudaSuspension { get; set; }

    public Nullable<decimal> DeudaBaja { get; set; }

    public Nullable<int> Origen { get; set; }

}

}
