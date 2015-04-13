CREATE PROCEDURE [dbo].[addNickNameID]
	@userID int = -1,
	@nickName ntext = "error"
AS
	INSERT INTO NickNameID(UserID, UserNickName) Values (@userID, @nickName)