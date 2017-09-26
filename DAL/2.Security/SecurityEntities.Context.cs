﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using System.Data.Entity.Core.Objects;
using System.Linq;


public partial class SecurityEntities : DbContext
{
    public SecurityEntities()
        : base("name=SecurityEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<UniCorreoxEdificio> UniCorreoxEdificio { get; set; }

    public virtual DbSet<CtaAplicacionDeConsumos> CtaAplicacionDeConsumos { get; set; }

    public virtual DbSet<CtaCierresDePeriodos> CtaCierresDePeriodos { get; set; }

    public virtual DbSet<CtaMovimientos> CtaMovimientos { get; set; }

    public virtual DbSet<CtaMovimientosAplicacion> CtaMovimientosAplicacion { get; set; }

    public virtual DbSet<CtaMovimientosCuotas> CtaMovimientosCuotas { get; set; }

    public virtual DbSet<CtaMovimientosFinancieras> CtaMovimientosFinancieras { get; set; }

    public virtual DbSet<SN_Controlador_Edificio> SN_Controlador_Edificio { get; set; }

    public virtual DbSet<SN_Fichadas> SN_Fichadas { get; set; }

    public virtual DbSet<uniActas> uniActas { get; set; }

    public virtual DbSet<uniActasAlumnos> uniActasAlumnos { get; set; }

    public virtual DbSet<UniAniosLectivosFacturados> UniAniosLectivosFacturados { get; set; }

    public virtual DbSet<UniAranceles> UniAranceles { get; set; }

    public virtual DbSet<UniCtaCteEstado> UniCtaCteEstado { get; set; }

    public virtual DbSet<uniCuatrimestres> uniCuatrimestres { get; set; }

    public virtual DbSet<uniCursos> uniCursos { get; set; }

    public virtual DbSet<uniCursosDias> uniCursosDias { get; set; }

    public virtual DbSet<uniInscripcionesMaterias> uniInscripcionesMaterias { get; set; }

    public virtual DbSet<uniInscripcionesMateriasDetalle> uniInscripcionesMateriasDetalle { get; set; }

    public virtual DbSet<uniMatriculaciones> uniMatriculaciones { get; set; }

    public virtual DbSet<UniProfesoresHs> UniProfesoresHs { get; set; }

    public virtual DbSet<uniReconocimientoMaterias> uniReconocimientoMaterias { get; set; }

    public virtual DbSet<uniReconocimientoMateriasDetalle> uniReconocimientoMateriasDetalle { get; set; }

    public virtual DbSet<uniReconocimientoMateriasFirmantes> uniReconocimientoMateriasFirmantes { get; set; }

    public virtual DbSet<uniAlumnos> uniAlumnos { get; set; }

    public virtual DbSet<uniCorreoEnviado> uniCorreoEnviado { get; set; }


    public virtual ObjectResult<UKSync_GetByFechaCodinsLegajo_Result> UKSync_GetByFechaCodinsLegajo(Nullable<System.DateTime> fecha, Nullable<int> codins, Nullable<int> legajo)
    {

        var fechaParameter = fecha.HasValue ?
            new ObjectParameter("fecha", fecha) :
            new ObjectParameter("fecha", typeof(System.DateTime));


        var codinsParameter = codins.HasValue ?
            new ObjectParameter("codins", codins) :
            new ObjectParameter("codins", typeof(int));


        var legajoParameter = legajo.HasValue ?
            new ObjectParameter("legajo", legajo) :
            new ObjectParameter("legajo", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UKSync_GetByFechaCodinsLegajo_Result>("UKSync_GetByFechaCodinsLegajo", fechaParameter, codinsParameter, legajoParameter);
    }


    public virtual int sp_sav_examen_a_verificar(Nullable<int> legajo, string codmat, Nullable<int> acta, Nullable<System.DateTime> fecha, Nullable<int> folio, string libro, Nullable<int> notadef, Nullable<int> ausente, Nullable<int> notaesc, Nullable<int> notaora)
    {

        var legajoParameter = legajo.HasValue ?
            new ObjectParameter("legajo", legajo) :
            new ObjectParameter("legajo", typeof(int));


        var codmatParameter = codmat != null ?
            new ObjectParameter("codmat", codmat) :
            new ObjectParameter("codmat", typeof(string));


        var actaParameter = acta.HasValue ?
            new ObjectParameter("acta", acta) :
            new ObjectParameter("acta", typeof(int));


        var fechaParameter = fecha.HasValue ?
            new ObjectParameter("fecha", fecha) :
            new ObjectParameter("fecha", typeof(System.DateTime));


        var folioParameter = folio.HasValue ?
            new ObjectParameter("folio", folio) :
            new ObjectParameter("folio", typeof(int));


        var libroParameter = libro != null ?
            new ObjectParameter("libro", libro) :
            new ObjectParameter("libro", typeof(string));


        var notadefParameter = notadef.HasValue ?
            new ObjectParameter("notadef", notadef) :
            new ObjectParameter("notadef", typeof(int));


        var ausenteParameter = ausente.HasValue ?
            new ObjectParameter("ausente", ausente) :
            new ObjectParameter("ausente", typeof(int));


        var notaescParameter = notaesc.HasValue ?
            new ObjectParameter("notaesc", notaesc) :
            new ObjectParameter("notaesc", typeof(int));


        var notaoraParameter = notaora.HasValue ?
            new ObjectParameter("notaora", notaora) :
            new ObjectParameter("notaora", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_sav_examen_a_verificar", legajoParameter, codmatParameter, actaParameter, fechaParameter, folioParameter, libroParameter, notadefParameter, ausenteParameter, notaescParameter, notaoraParameter);
    }


    public virtual int sp_cta_Anular_Movimiento_Return_Afectadas(Nullable<int> movimientoNro)
    {

        var movimientoNroParameter = movimientoNro.HasValue ?
            new ObjectParameter("MovimientoNro", movimientoNro) :
            new ObjectParameter("MovimientoNro", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_cta_Anular_Movimiento_Return_Afectadas", movimientoNroParameter);
    }


    public virtual int sp_cta_Anular_MovimientoAPL(Nullable<int> movimientoNro, string usuario)
    {

        var movimientoNroParameter = movimientoNro.HasValue ?
            new ObjectParameter("MovimientoNro", movimientoNro) :
            new ObjectParameter("MovimientoNro", typeof(int));


        var usuarioParameter = usuario != null ?
            new ObjectParameter("Usuario", usuario) :
            new ObjectParameter("Usuario", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_cta_Anular_MovimientoAPL", movimientoNroParameter, usuarioParameter);
    }


    public virtual int sp_CierraReconocimientos(Nullable<int> idReconocimiento, Nullable<int> legajoprov)
    {

        var idReconocimientoParameter = idReconocimiento.HasValue ?
            new ObjectParameter("idReconocimiento", idReconocimiento) :
            new ObjectParameter("idReconocimiento", typeof(int));


        var legajoprovParameter = legajoprov.HasValue ?
            new ObjectParameter("legajoprov", legajoprov) :
            new ObjectParameter("legajoprov", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_CierraReconocimientos", idReconocimientoParameter, legajoprovParameter);
    }


    public virtual int fn_anotados_cursoId(Nullable<long> cursoId)
    {

        var cursoIdParameter = cursoId.HasValue ?
            new ObjectParameter("cursoId", cursoId) :
            new ObjectParameter("cursoId", typeof(long));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("fn_anotados_cursoId", cursoIdParameter);
    }


    public virtual int sp_ins_uniMoodle_matric_agrup_usuarios(Nullable<int> ciclo, Nullable<int> cuatri, Nullable<bool> matricProfesores)
    {

        var cicloParameter = ciclo.HasValue ?
            new ObjectParameter("ciclo", ciclo) :
            new ObjectParameter("ciclo", typeof(int));


        var cuatriParameter = cuatri.HasValue ?
            new ObjectParameter("cuatri", cuatri) :
            new ObjectParameter("cuatri", typeof(int));


        var matricProfesoresParameter = matricProfesores.HasValue ?
            new ObjectParameter("matricProfesores", matricProfesores) :
            new ObjectParameter("matricProfesores", typeof(bool));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ins_uniMoodle_matric_agrup_usuarios", cicloParameter, cuatriParameter, matricProfesoresParameter);
    }


    public virtual ObjectResult<sp_get_ProfesoresHs_Result> sp_get_ProfesoresHs(Nullable<int> codins, Nullable<System.DateTime> fecha, string turno, Nullable<System.DateTime> desde, Nullable<System.DateTime> hasta)
    {

        var codinsParameter = codins.HasValue ?
            new ObjectParameter("codins", codins) :
            new ObjectParameter("codins", typeof(int));


        var fechaParameter = fecha.HasValue ?
            new ObjectParameter("fecha", fecha) :
            new ObjectParameter("fecha", typeof(System.DateTime));


        var turnoParameter = turno != null ?
            new ObjectParameter("turno", turno) :
            new ObjectParameter("turno", typeof(string));


        var desdeParameter = desde.HasValue ?
            new ObjectParameter("desde", desde) :
            new ObjectParameter("desde", typeof(System.DateTime));


        var hastaParameter = hasta.HasValue ?
            new ObjectParameter("hasta", hasta) :
            new ObjectParameter("hasta", typeof(System.DateTime));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_ProfesoresHs_Result>("sp_get_ProfesoresHs", codinsParameter, fechaParameter, turnoParameter, desdeParameter, hastaParameter);
    }

}

}

