﻿CREATE PROC FI_SP_PesqBeneficiario
	@IDCLIENTE int
AS
BEGIN
	SELECT ID, NOME, CPF, IDCLIENTE
	FROM BENEFICIARIOS
	WHERE IDCLIENTE = @IDCLIENTE 
	ORDER BY NOME
END