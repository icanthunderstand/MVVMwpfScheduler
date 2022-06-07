CREATE TABLE [dbo].[EVENT_TB] (
    [Id]    INT      NOT NULL,
    [Title] NVARCHAR(100) NULL, 
    [Location] NVARCHAR(100) NULL, 
    [Description] NVARCHAR(300) NULL, 
    [StartTime] DATETIME NOT NULL,
    [EndTime] DATETIME NOT NULL, 
    [Foreground] NVARCHAR(50) NULL, 
    [Background] NVARCHAR(50) NULL, 
    [LoginId] NVARCHAR(50) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_EVENT_LOGINID] FOREIGN KEY ([LoginId]) REFERENCES [LOGIN_TB]([LoginId]) 
);

