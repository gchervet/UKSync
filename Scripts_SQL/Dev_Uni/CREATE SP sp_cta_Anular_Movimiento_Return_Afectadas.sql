CREATE PROCEDURE [dbo].[sp_cta_Anular_Movimiento_Return_Afectadas]
@MovimientoNro AS INT
AS
BEGIN
	declare @aplicadoExterno as int
	declare @result as int
	set @result = 0

	set @aplicadoExterno = (select COUNT(*) from ctaMovimientosCuotas c
	inner join CtaMovimientosAplicacion a on a.MovimientoCuotaClaveAplicado=c.Clave or a.MovimientoCuotaClaveAplicando=c.Clave
	where c.MovimientoNro=@MovimientoNro and a.MovimientoNro<>@MovimientoNro)

	IF (@aplicadoExterno=0) 
	BEGIN
		delete CtaMovimientosAplicacion	where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;
		delete CtaMovimientosCuotas	where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;
		delete CtaMovimientosAsientos where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;
		delete CtaMovimientoCuotaFacturacion where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;		
		delete CtaMovimientosMediosEstados where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;
		delete CtaMovimientosMedios	where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;
		delete CtaMovimientosFinancieras where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;
		delete CtaMovimientosVariaciones where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;
		delete CtaAplicacionDeConsumos where MovimientoItemClave in (select clave from CtaMovimientosItems where MovimientoNro=@MovimientoNro)
		select @result = @result + @@ROWCOUNT;
		delete CtaLiquidacionesIntereses where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;
		delete CtaMovimientosItems where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;

		update CtaMovimientos set anulado=1	where MovimientoNro=@MovimientoNro
		select @result = @result + @@ROWCOUNT;		
	END

	return @result
END
