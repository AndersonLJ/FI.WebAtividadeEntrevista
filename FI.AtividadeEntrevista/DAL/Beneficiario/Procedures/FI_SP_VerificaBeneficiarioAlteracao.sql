﻿CREATE PROC FI_SP_VerificaBeneficiarioAlteracao
	@CPF VARCHAR(14),
	@IDCLIENTE BIGINT,
	@ID BIGINT
AS
BEGIN
	SELECT 1 FROM BENEFICIARIOS WHERE CPF = @CPF AND IDCLIENTE = @IDCLIENTE AND ID !=@ID
END