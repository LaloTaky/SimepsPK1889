USE [mirDB]
GO
/****** Object:  StoredProcedure [dbo].[spMSIME_ConsultarFondosRamo33]    Script Date: 29/11/2019 12:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spMSIME_ConsultarFondosRamo33]
	-- Add the parameters for the stored procedure here
	@iCiclo int	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select 
	m.[CICLO]
	,m.[RAMO]
	,m.[MODALIDAD]
	,m.[CLAVE]
	,m.[SIGLAS_FONDO]
	,m.[NOMBRE_FONDO]
	,m.[COMPONENTE]
	,m.SIGLAS_COMPONENTE
	,m.[RAMO_RESP]
	,m.[UR]
FROM
(SELECT 
		f.[CICLO]
      ,f.[RAMO]
      ,f.[MODALIDAD]
      ,f.[CLAVE]
      ,f.[SIGLAS_FONDO]
      ,f.[NOMBRE_FONDO]
      ,f.[COMPONENTE]
      ,f.[SIGLAS_COMPONENTE_2] as SIGLAS_COMPONENTE
	  ,f.[RAMO_RESP]
      ,f.[UR]
      --,f.[SIGLAS_COMPONENTE_2]
	FROM
		[comunDB].[dbo].[TC_FONDO_R33] f 
	WHERE f.SIGLAS_FONDO IN ('FAETA', 'FAIS', 'FAM', 'FASSA')
	
union
(SELECT 
		f.[CICLO]
      ,f.[RAMO]
      ,f.[MODALIDAD]
      ,f.[CLAVE]
      ,f.[SIGLAS_FONDO]
      ,f.[NOMBRE_FONDO]
      ,f.[COMPONENTE]
      ,f.[SIGLAS_COMPONENTE_2] as SIGLAS_COMPONENTE
	  ,f.[RAMO_RESP]
      ,f.[UR]
      --,f.[SIGLAS_COMPONENTE_2]
	FROM
		[comunDB].[dbo].[TC_FONDO_R33] f 
	WHERE f.SIGLAS_FONDO IN ( 'FONE') and MODALIDAD = 'I' and f.CLAVE = 13
) 
) m	WHERE m.CICLO = @iCiclo
	ORDER BY m.SIGLAS_FONDO, m.SIGLAS_COMPONENTE

END
