//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsumosCapataz.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class RawMaterials
    {
        public int Id { get; set; }
        public Nullable<int> IdCompany { get; set; }
        public Nullable<int> ProcessOrder { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Batch { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string ECAStorageCode { get; set; }
        public string ICAStorageCode { get; set; }
        public string Message { get; set; }
        public string ECA { get; set; }
        public Nullable<System.DateTime> Stamp { get; set; }
        public string OrderType { get; set; }
        public string OrderNumber { get; set; }
    }
}
