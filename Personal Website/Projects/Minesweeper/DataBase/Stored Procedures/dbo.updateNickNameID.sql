CREATE PROCEDURE [dbo].[updateNickNameID]
   @userID int = -1,
   @nickName ntext = "error"
AS
  IF EXISTS( SELECT UserNickName FROM NickNameID WHERE UserID=@userID )
    UPDATE NickNameID SET UserNickName=@nickName WHERE UserID=@userID
  ELSE
    EXEC [dbo].[addNickNameID] @userID, @nickName