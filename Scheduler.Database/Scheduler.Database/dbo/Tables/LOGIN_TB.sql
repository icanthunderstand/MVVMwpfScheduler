﻿CREATE TABLE [dbo].[LOGIN_TB]
(
	[LoginId] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Password] VARBINARY(500) NOT NULL, 
    [NickName] NVARCHAR(50) NOT NULL
)
