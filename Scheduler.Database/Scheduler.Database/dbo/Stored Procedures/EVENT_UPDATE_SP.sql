CREATE PROCEDURE [dbo].[EVENT_UPDATE_SP]
	@Id INT,
	@Title NVARCHAR(100),
	@Location NVARCHAR(100),
	@Description NVARCHAR(300),
	@StartTime DATETIME,
	@EndTime DATETIME,
	@Foreground NVARCHAR(50),
	@Background NVARCHAR(50),
	@LoginId NVARCHAR(50)
AS
BEGIN
	BEGIN TRAN

	UPDATE EVENT_TB 
	SET Title=@Title,Location=@Location,Description=@Description,StartTime=@StartTime,EndTime=@EndTime,Foreground=@Foreground,Background=@Background,LoginId=@LoginId
	WHERE Id=@Id

	IF(@@ERROR != 0)
	BEGIN
		ROLLBACK TRAN
		SELECT 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
		SELECT 1
	END
END
RETURN 0
