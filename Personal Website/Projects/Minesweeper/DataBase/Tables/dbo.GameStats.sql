CREATE TABLE [dbo].[GameStats] (
    [UserID]      INT      PRIMARY KEY CLUSTERED (UserID),
    [GamesPlayed] INT      DEFAULT ((1)) NULL,
    [GamesWon]    INT      DEFAULT ((0)) NULL,
    [DateStart]   DATETIME NULL
);

