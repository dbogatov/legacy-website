CREATE PROCEDURE [dbo].[getLeaderBoard]
   @mode int = -1
AS
   SELECT TOP 100 NickNameID.UserNickName, LeaderBoards.Duration FROM LeaderBoards INNER JOIN NickNameID ON NickNameID.UserID=LeaderBoards.UserID WHERE Mode=@mode ORDER BY Duration ASC