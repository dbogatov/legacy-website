CREATE PROCEDURE [dbo].[addTryID]
   @userID int = -1
AS
   IF EXISTS(SELECT * FROM GameStats WHERE UserID=@userID) UPDATE GameStats SET GamesPlayed=GamesPlayed+1, DateStart=getDate() WHERE UserID=@UserID ELSE INSERT INTO GameStats(UserID, GamesPlayed, GamesWon, DateStart) VALUES(@UserID, 1, 0, getDate() )