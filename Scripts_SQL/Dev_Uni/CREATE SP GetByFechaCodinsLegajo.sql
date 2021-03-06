CREATE PROCEDURE [dbo].[UKSync_GetByFechaCodinsLegajo]
	@fecha datetime,
	@codins int,
	@legajo int
AS
BEGIN
	declare @dia varchar(2)
	select @dia = dbo.fn_dia_de_fecha(@fecha)

	select
	 codmat
	,codcur
	,ano
	,cuatri
	,sum(cantidad) cantidad
	into #alumnos_curso
	from
	(
		SELECT
		 uc.codmat
		,uc.codcur
		,uc.ano
		,uc.cuatri
		,COUNT(*) cantidad
		--into #alumnos
		from uniCursos uC
		inner join uniInscripcionesMaterias uIM on uIM.ciclo = uC.ano 
		inner join uniInscripcionesMateriasDetalle uIMD  on uIM.clave = uIMD.clave 
		where uC.codmat = uIMD.codmat and uC.codcur = uIMD.codcur and uC.cuatri = uIM.cuatri
		group by uc.codmat
		,uc.codcur
		,uc.ano
		,uc.cuatri
		union all
		SELECT
		 ucf.matbase
		,ucf.curbase
		,ucf.ano
		,ucf.cuatri
		,COUNT(*) cantidad
		from uniInscripcionesMaterias uIM 
			inner join uniInscripcionesMateriasDetalle uIMD  on uIM.clave = uIMD.clave 
			inner join uniCursosFusiones uCF on uIM.ciclo = uCF.ano and uCF.matfus = uIMD.codmat and uCF.curfus = uIMD.codcur and uIM.cuatri= uCF.cuatri
		group by ucf.matbase
		,ucf.curbase
		,ucf.ano
		,ucf.cuatri
		union all
		select 
		 matfus
		,ucf.curfus
		,ucf.ano
		,ucf.cuatri
		,COUNT(*) cantidad
		from uniInscripcionesMaterias uIM 
			inner join uniInscripcionesMateriasDetalle uIMD  on uIM.clave = uIMD.clave 
			inner join uniCursosFusiones uCF on uIM.ciclo = uCF.ano and uCF.matbase = uIMD.codmat and uCF.curbase = uIMD.codcur and uIM.cuatri= uCF.cuatri
		group by matfus
		,ucf.curfus
		,ucf.ano
		,ucf.cuatri
	) t
	group by  codmat
	,codcur
	,ano
	,cuatri
	
	SELECT a.cursoId,
	a.dia,
	a.desde,
	a.hasta,
	a.codins,
	c.ClaseNro,
	c.fecha,
	b.legajo,
	d.LegajoProfesor,
	e.ano,
	e.codcur,
	e.codcar,
	isnull(ac.cantidad,0) cantidad
	into #datos
	FROM [UniCursosDias] as a
	inner join [dbo].[UniCursosProfesores] as b
	on a.cursoId = b.cursoId and a.dia = b.dia
	inner join [UniCursosDiasClases] as c
	on a.CursoId = c.CursoId and a.desde = c.HoraDesde and a.hasta = c.HoraHasta and a.codins = c.CodigoColegio
	inner join [UniCursosDiasClasesProfesores] as d
	on a.cursoId = d.CursoId and c.ClaseNro = d.ClaseNro and b.legajo = d.LegajoProfesor
	inner join [UniCursos] as e
	on a.cursoId = e.id
	left join #alumnos_curso ac on ac.codmat=e.codmat and ac.codcur=e.codcur and ac.ano=e.ano and ac.cuatri=e.cuatri
	where c.ClaseNro is not null 
	and legajo is not null 
	and LegajoProfesor is not null 
	and fecha = DATEADD(dd, 0, DATEDIFF(dd, 0, @fecha)) 
	and legajo = @legajo
	and a.dia = @dia
	and a.codins = @codins
	order by c.Fecha, legajo desc
	
	select *
	from 
	(
		select *,RANK() OVER   
			(PARTITION BY legajo,legajoprofesor,clasenro,fecha ORDER BY cantidad DESC) AS ranking 
		from #datos
	) t
	where ranking=1
	order by fecha
END