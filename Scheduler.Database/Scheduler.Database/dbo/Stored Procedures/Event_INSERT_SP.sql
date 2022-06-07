CREATE PROCEDURE [dbo].[EVENT_INSERT_SP]
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
	Insert EVENT_TB
	(
		Id,Title,Location,Description,StartTime,EndTime,Foreground,Background,LoginId
	)
	values
	(
		@Id,@Title,@Location,@Description,@StartTime,@EndTime,@Foreground,@Background,@LoginId
	)
RETURN 0
