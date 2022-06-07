CREATE PROCEDURE [dbo].[LOGIN_INSERT_SP]
	@LoginId NVARCHAR(50),
	@Password NVARCHAR(50),
	@NickName NVARCHAR(50)
AS
	DECLARE @IsExist int
	SELECT @IsExist=Count(*) From LOGIN_TB where LoginId = @LoginId

	IF @IsExist > 0
	BEGIN
		SELECT 0
	END
	ELSE
	BEGIN
		INSERT LOGIN_TB
		(
			LoginId,Password,NickName
		)
		VALUES
		(
			@LoginId,PWDENCRYPT(@Password),@NickName
		)
		SELECT 1
	END
RETURN 1
