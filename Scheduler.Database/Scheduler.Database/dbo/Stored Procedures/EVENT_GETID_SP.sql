CREATE PROCEDURE [dbo].[EVENT_GETID_SP]
AS
	SELECT ISNULL(MAX(Id),0) FROM EVENT_TB
RETURN 0
