CREATE PROCEDURE [dbo].[LOGIN_LOGIN_SP]
	@LoginId nvarchar(50),
	@Password NVARCHAR(50)
AS
	SELECT PWDCOMPARE(@Password,Password) FROM LOGIN_TB WHERE LoginId = @LoginId
RETURN 0
