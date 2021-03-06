USE [dev_Uni]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_ProfesoresHs]    Script Date: 8/8/2017 19:45:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_get_ProfesoresHs]
@codins as int,
@fecha datetime,
@turno as char(1)=null,
@desde as datetime=null,
@hasta as datetime=null
AS
BEGIN

declare @dia as int
declare @diachr as char(2)
select @dia = datepart(dw,@fecha)

if(@dia=1) set @diachr = 'LU';
if(@dia=2) set @diachr = 'MA';
if(@dia=3) set @diachr = 'MI';
if(@dia=4) set @diachr = 'JU';
if(@dia=5) set @diachr = 'VI';
if(@dia=6) set @diachr = 'SA';
if(@dia=7) set @diachr = 'DO';

if(@turno='') set @turno=null;
if(year(@desde)=9999) set @desde=null;
if(year(@hasta)=9999) set @hasta=null;


select uc.id as CursoId,ucdc.ClaseNro,ucp.legajo as LegajoProfesor,UPPER(apellido) as apellido,
DATEDIFF(minute,isnull(ucd.desde,ucdr.desde),isnull(ucd.hasta,ucdr.hasta))/60.00 as HsPlanificadas,
null as Entrada,null as Salida,0 as Enfermedad,0 as Evaluacion,0 as Ausente,0 as Revision,uc.cantprof,
0 as NoPlanificado,null as LegajoProfesorReemplazo,null as Usuario,0 as Version,
null as FechaCreacion,null as FechaModificacion,null as FechaCursada,0.00 as HsEfectivas,0 as Persistido,
ued.nomins as NombreColegio,umat.nombre as NombreMateria,ucdc.Fecha as FechaPlanificada,
uc.turno,isnull(ucd.codins,ucdr.codins) as codins,
cast(0 as bit) as Justificada,
cast(0 as bit) as Norealizada,
'' as Comentarios,
uc.codcur,
uc.codmat,
uc.comision,
ucd.desde as HoraInicio,
ucd.hasta as HoraFin
  from uniCursos uc
  left join uniCursosDias ucd on ucd.cursoId = uc.id and ucd.dia =@diachr
  inner join UniCursosDiasClases ucdc on ucdc.CursoId = uc.id 
  left join uniCursosDiasRecupera ucdr on ucdr.cursoId = uc.id and ucdr.FechaClase = @fecha and ucdr.ClaseNro = ucdc.ClaseNro 
  inner join UniCursosProfesores ucp on ucp.cursoId = uc.id and ucp.dia = ucd.dia
  inner join uniEdificios ued on ued.codins = ucdc.CodigoColegio 
  inner join uniMaterias umat on umat.codmat = uc.codmat 
  left join uniProfesores up on up.legajo = ucp.legajo 
  left join UniProfesoresHs uphs on uphs.CursoId = uc.id and uphs.LegajoProfesor = ucp.legajo and uphs.ClaseNro = ucdc.ClaseNro 
  where ucdc.Fecha =@fecha and dbo.fn_curso_cabecera_global(uc.codcur,uc.ano,uc.cuatri) in (0,uc.codcur) and uc.regional in (0,1,2) 
  and (uc.turno = @turno or isnull(@turno,'')='') 
  and (DATEADD(day, -DATEDIFF(day, 0, @desde), @desde) <= DATEADD(day, -DATEDIFF(day, 0, ucdc.HoraDesde), ucdc.HoraDesde) or (isnull(@desde,0)=0)) 
  and (DATEADD(day, -DATEDIFF(day, 0, @hasta), @hasta) >= DATEADD(day, -DATEDIFF(day, 0, ucdc.HoraDesde), ucdc.HoraDesde) or (isnull(@hasta,0)=0)) 
  and (isnull(ucd.codins,ucdr.codins) = @codins) and uphs.CursoId is null and (ISNULL(ucd.codins,0) >0 or ISNULL(ucdr.codins,0)>0)
union 
(select  uphs.CursoId ,uphs.ClaseNro,uphs.LegajoProfesor,UPPER(up.apellido) as apellido,uphs.HsPlanificadas,uphs.Entrada,uphs.Salida,uphs.Enfermedad,uphs.Evaluacion,uphs.Ausente,uphs.Revision,uc.cantprof,uphs.NoPlanificado,uphs.LegajoProfesorReemplazo,uphs.Usuario,uphs.Version,uphs.FechaCreacion,uphs.FechaModificacion,uphs.FechaCursada,uphs.HsEfectivas,1 as Persistido,ued.nomins as NombreColegio,umat.nombre as NombreMateria,ucdc.Fecha as FechaPlanificada,uc.turno ,isnull(ucd.codins,ucdr.codins) as codins,
uphs.Justificada,
uphs.Norealizada,
uphs.Comentarios,
uc.codcur,
uc.codmat,
uc.comision,
ucd.desde as HoraInicio,
ucd.hasta as HoraFin
from UniProfesoresHs uphs
inner join uniCursos uc on uc.id = uphs.CursoId 
left join uniCursosDias ucd on ucd.cursoId = uphs.CursoId and ucd.dia = @diachr
inner join UniCursosDiasClases ucdc on ucdc.CursoId = uc.id and ucdc.ClaseNro = uphs.ClaseNro
left join uniCursosDiasRecupera ucdr on ucdr.cursoId = uc.id and ucdr.FechaClase = @fecha and ucdr.ClaseNro = ucdc.ClaseNro 
inner join uniEdificios ued on ued.codins = cast(isnull(ucd.codins,ucdr.codins) as varchar(2))
inner join uniMaterias umat on umat.codmat = uc.codmat 
left join uniProfesores up on up.legajo = uphs.LegajoProfesor 
where ucdc.Fecha =@fecha 
and (uc.turno = @turno or isnull(@turno,'')='') 
  and (DATEADD(day, -DATEDIFF(day, 0, @desde), @desde) <= DATEADD(day, -DATEDIFF(day, 0, ucdc.HoraDesde), ucdc.HoraDesde) or (isnull(@desde,0)=0)) 
  and (DATEADD(day, -DATEDIFF(day, 0, @hasta), @hasta) >= DATEADD(day, -DATEDIFF(day, 0, ucdc.HoraDesde), ucdc.HoraDesde) or (isnull(@hasta,0)=0)) 
and isnull(ucd.codins,ucdr.codins) = @codins and (ISNULL(ucd.codins,0) >0 or ISNULL(ucdr.codins,0)>0)
) order by cursoId  

END
