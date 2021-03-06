﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsumosCapataz.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ConsumosCapataz.Domain;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ModelEntities : DbContext
    {
        public ModelEntities()
            : base("name=ModelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FinishedProduct> FinishedProduct { get; set; }
        public virtual DbSet<RawMaterials> RawMaterials { get; set; }
    
        public virtual int SPE_CIERRE_OT(string tipoOt, string numeroOt, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var tipoOtParameter = tipoOt != null ?
                new ObjectParameter("tipoOt", tipoOt) :
                new ObjectParameter("tipoOt", typeof(string));
    
            var numeroOtParameter = numeroOt != null ?
                new ObjectParameter("numeroOt", numeroOt) :
                new ObjectParameter("numeroOt", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPE_CIERRE_OT", tipoOtParameter, numeroOtParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual ObjectResult<SPE_CREA_OT_Result> SPE_CREA_OT(string codigoArticulo, Nullable<decimal> cantidad, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("codigoArticulo", codigoArticulo) :
                new ObjectParameter("codigoArticulo", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(decimal));
    
            var codigoDepositoParameter = codigoDeposito != null ?
                new ObjectParameter("codigoDeposito", codigoDeposito) :
                new ObjectParameter("codigoDeposito", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPE_CREA_OT_Result>("SPE_CREA_OT", codigoArticuloParameter, cantidadParameter, codigoDepositoParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual ObjectResult<SPE_CREA_ICA_Result> SPE_CREA_ICA(string tipoOt, string numeroOt, string codigoArticulo, string descripcionArticulo, Nullable<decimal> cantidad, string lote, Nullable<int> idOrden, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var tipoOtParameter = tipoOt != null ?
                new ObjectParameter("tipoOt", tipoOt) :
                new ObjectParameter("tipoOt", typeof(string));
    
            var numeroOtParameter = numeroOt != null ?
                new ObjectParameter("numeroOt", numeroOt) :
                new ObjectParameter("numeroOt", typeof(string));
    
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("codigoArticulo", codigoArticulo) :
                new ObjectParameter("codigoArticulo", typeof(string));
    
            var descripcionArticuloParameter = descripcionArticulo != null ?
                new ObjectParameter("descripcionArticulo", descripcionArticulo) :
                new ObjectParameter("descripcionArticulo", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(decimal));
    
            var loteParameter = lote != null ?
                new ObjectParameter("lote", lote) :
                new ObjectParameter("lote", typeof(string));
    
            var idOrdenParameter = idOrden.HasValue ?
                new ObjectParameter("idOrden", idOrden) :
                new ObjectParameter("idOrden", typeof(int));
    
            var codigoDepositoParameter = codigoDeposito != null ?
                new ObjectParameter("codigoDeposito", codigoDeposito) :
                new ObjectParameter("codigoDeposito", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPE_CREA_ICA_Result>("SPE_CREA_ICA", tipoOtParameter, numeroOtParameter, codigoArticuloParameter, descripcionArticuloParameter, cantidadParameter, loteParameter, idOrdenParameter, codigoDepositoParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual int ENSOL_CIERRE_OT(string tipoOt, string numeroOt, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var tipoOtParameter = tipoOt != null ?
                new ObjectParameter("tipoOt", tipoOt) :
                new ObjectParameter("tipoOt", typeof(string));
    
            var numeroOtParameter = numeroOt != null ?
                new ObjectParameter("numeroOt", numeroOt) :
                new ObjectParameter("numeroOt", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ENSOL_CIERRE_OT", tipoOtParameter, numeroOtParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual ObjectResult<SPE_CREA_ICA_Result> ENSOL_CREA_ICA(string tipoOt, string numeroOt, string codigoArticulo, string descripcionArticulo, Nullable<decimal> cantidad, string lote, Nullable<int> idOrden, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var tipoOtParameter = tipoOt != null ?
                new ObjectParameter("tipoOt", tipoOt) :
                new ObjectParameter("tipoOt", typeof(string));
    
            var numeroOtParameter = numeroOt != null ?
                new ObjectParameter("numeroOt", numeroOt) :
                new ObjectParameter("numeroOt", typeof(string));
    
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("codigoArticulo", codigoArticulo) :
                new ObjectParameter("codigoArticulo", typeof(string));
    
            var descripcionArticuloParameter = descripcionArticulo != null ?
                new ObjectParameter("descripcionArticulo", descripcionArticulo) :
                new ObjectParameter("descripcionArticulo", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(decimal));
    
            var loteParameter = lote != null ?
                new ObjectParameter("lote", lote) :
                new ObjectParameter("lote", typeof(string));
    
            var idOrdenParameter = idOrden.HasValue ?
                new ObjectParameter("idOrden", idOrden) :
                new ObjectParameter("idOrden", typeof(int));
    
            var codigoDepositoParameter = codigoDeposito != null ?
                new ObjectParameter("codigoDeposito", codigoDeposito) :
                new ObjectParameter("codigoDeposito", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPE_CREA_ICA_Result>("ENSOL_CREA_ICA", tipoOtParameter, numeroOtParameter, codigoArticuloParameter, descripcionArticuloParameter, cantidadParameter, loteParameter, idOrdenParameter, codigoDepositoParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual ObjectResult<SPE_CREA_OT_Result> ENSOL_CREA_OT(string codigoArticulo, Nullable<decimal> cantidad, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("codigoArticulo", codigoArticulo) :
                new ObjectParameter("codigoArticulo", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(decimal));
    
            var codigoDepositoParameter = codigoDeposito != null ?
                new ObjectParameter("codigoDeposito", codigoDeposito) :
                new ObjectParameter("codigoDeposito", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPE_CREA_OT_Result>("ENSOL_CREA_OT", codigoArticuloParameter, cantidadParameter, codigoDepositoParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual int ENSOLFOOD_CIERRE_OT(string tipoOt, string numeroOt, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var tipoOtParameter = tipoOt != null ?
                new ObjectParameter("tipoOt", tipoOt) :
                new ObjectParameter("tipoOt", typeof(string));
    
            var numeroOtParameter = numeroOt != null ?
                new ObjectParameter("numeroOt", numeroOt) :
                new ObjectParameter("numeroOt", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ENSOLFOOD_CIERRE_OT", tipoOtParameter, numeroOtParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual ObjectResult<SPE_CREA_ICA_Result> ENSOLFOOD_CREA_ICA(string tipoOt, string numeroOt, string codigoArticulo, string descripcionArticulo, Nullable<decimal> cantidad, string lote, Nullable<int> idOrden, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var tipoOtParameter = tipoOt != null ?
                new ObjectParameter("tipoOt", tipoOt) :
                new ObjectParameter("tipoOt", typeof(string));
    
            var numeroOtParameter = numeroOt != null ?
                new ObjectParameter("numeroOt", numeroOt) :
                new ObjectParameter("numeroOt", typeof(string));
    
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("codigoArticulo", codigoArticulo) :
                new ObjectParameter("codigoArticulo", typeof(string));
    
            var descripcionArticuloParameter = descripcionArticulo != null ?
                new ObjectParameter("descripcionArticulo", descripcionArticulo) :
                new ObjectParameter("descripcionArticulo", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(decimal));
    
            var loteParameter = lote != null ?
                new ObjectParameter("lote", lote) :
                new ObjectParameter("lote", typeof(string));
    
            var idOrdenParameter = idOrden.HasValue ?
                new ObjectParameter("idOrden", idOrden) :
                new ObjectParameter("idOrden", typeof(int));
    
            var codigoDepositoParameter = codigoDeposito != null ?
                new ObjectParameter("codigoDeposito", codigoDeposito) :
                new ObjectParameter("codigoDeposito", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPE_CREA_ICA_Result>("ENSOLFOOD_CREA_ICA", tipoOtParameter, numeroOtParameter, codigoArticuloParameter, descripcionArticuloParameter, cantidadParameter, loteParameter, idOrdenParameter, codigoDepositoParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual ObjectResult<SPE_CREA_OT_Result> ENSOLFOOD_CREA_OT(string codigoArticulo, Nullable<decimal> cantidad, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("codigoArticulo", codigoArticulo) :
                new ObjectParameter("codigoArticulo", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(decimal));
    
            var codigoDepositoParameter = codigoDeposito != null ?
                new ObjectParameter("codigoDeposito", codigoDeposito) :
                new ObjectParameter("codigoDeposito", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPE_CREA_OT_Result>("ENSOLFOOD_CREA_OT", codigoArticuloParameter, cantidadParameter, codigoDepositoParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual int ENSOLPIGS_CIERRE_OT(string tipoOt, string numeroOt, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var tipoOtParameter = tipoOt != null ?
                new ObjectParameter("tipoOt", tipoOt) :
                new ObjectParameter("tipoOt", typeof(string));
    
            var numeroOtParameter = numeroOt != null ?
                new ObjectParameter("numeroOt", numeroOt) :
                new ObjectParameter("numeroOt", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ENSOLPIGS_CIERRE_OT", tipoOtParameter, numeroOtParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual ObjectResult<SPE_CREA_ICA_Result> ENSOLPIGS_CREA_ICA(string tipoOt, string numeroOt, string codigoArticulo, string descripcionArticulo, Nullable<decimal> cantidad, string lote, Nullable<int> idOrden, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var tipoOtParameter = tipoOt != null ?
                new ObjectParameter("tipoOt", tipoOt) :
                new ObjectParameter("tipoOt", typeof(string));
    
            var numeroOtParameter = numeroOt != null ?
                new ObjectParameter("numeroOt", numeroOt) :
                new ObjectParameter("numeroOt", typeof(string));
    
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("codigoArticulo", codigoArticulo) :
                new ObjectParameter("codigoArticulo", typeof(string));
    
            var descripcionArticuloParameter = descripcionArticulo != null ?
                new ObjectParameter("descripcionArticulo", descripcionArticulo) :
                new ObjectParameter("descripcionArticulo", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(decimal));
    
            var loteParameter = lote != null ?
                new ObjectParameter("lote", lote) :
                new ObjectParameter("lote", typeof(string));
    
            var idOrdenParameter = idOrden.HasValue ?
                new ObjectParameter("idOrden", idOrden) :
                new ObjectParameter("idOrden", typeof(int));
    
            var codigoDepositoParameter = codigoDeposito != null ?
                new ObjectParameter("codigoDeposito", codigoDeposito) :
                new ObjectParameter("codigoDeposito", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPE_CREA_ICA_Result>("ENSOLPIGS_CREA_ICA", tipoOtParameter, numeroOtParameter, codigoArticuloParameter, descripcionArticuloParameter, cantidadParameter, loteParameter, idOrdenParameter, codigoDepositoParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    
        public virtual ObjectResult<SPE_CREA_OT_Result> ENSOLPIGS_CREA_OT(string codigoArticulo, Nullable<decimal> cantidad, string codigoDeposito, string usuario, Nullable<int> codigoUsuario, string terminal)
        {
            var codigoArticuloParameter = codigoArticulo != null ?
                new ObjectParameter("codigoArticulo", codigoArticulo) :
                new ObjectParameter("codigoArticulo", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(decimal));
    
            var codigoDepositoParameter = codigoDeposito != null ?
                new ObjectParameter("codigoDeposito", codigoDeposito) :
                new ObjectParameter("codigoDeposito", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var codigoUsuarioParameter = codigoUsuario.HasValue ?
                new ObjectParameter("codigoUsuario", codigoUsuario) :
                new ObjectParameter("codigoUsuario", typeof(int));
    
            var terminalParameter = terminal != null ?
                new ObjectParameter("terminal", terminal) :
                new ObjectParameter("terminal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPE_CREA_OT_Result>("ENSOLPIGS_CREA_OT", codigoArticuloParameter, cantidadParameter, codigoDepositoParameter, usuarioParameter, codigoUsuarioParameter, terminalParameter);
        }
    }
}
