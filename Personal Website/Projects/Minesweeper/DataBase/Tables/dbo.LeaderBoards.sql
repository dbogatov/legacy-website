CREATE TABLE [dbo].[LeaderBoards] (
    [UserID]    INT      PRIMARY KEY CLUSTERED (UserID),
    [Mode]      INT      NULL,
    [DateStart] DATETIME NULL,
    [DateEnd]   DATETIME NULL,
    [Duration]  INT      NULL
);