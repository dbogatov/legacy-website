CREATE PROCEDURE [dbo].[getNickname]
   @userID int = -1
AS
   SELECT UserNickName FROM NickNameID WHERE UserID=@userID