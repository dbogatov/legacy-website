CREATE PROCEDURE [dbo].[AddUser]
	@name		VARCHAR(255),
	@email		VARCHAR(127),
	@comment	VARCHAR(2083),
	@language	VARCHAR(63),
	@hash		VARCHAR(255)
AS
	INSERT
		INTO Users
			(userName, userEmail, userComment, userLanguage, regTime, hash)
		VALUES
			(@name, @email, @comment, @language, SYSDATETIME(), @hash);
RETURN 0