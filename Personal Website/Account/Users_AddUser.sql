ALTER PROCEDURE [dbo].[AddUser]
	@name		NVARCHAR(255),
	@email		VARCHAR(127),
	@comment	NVARCHAR(2083),
	@language	VARCHAR(63),
	@hash		VARCHAR(255)
AS

	Declare @SQL as varchar(1000);

	SET @SQL = 'INSERT INTO Users
			(userName, userEmail, userComment, userLanguage, regTime, hash)
		VALUES
			(N'''+ @name +''', @email, @comment, @language, SYSDATETIME(), @hash);';

	EXEC (@SQL);
	/*
	INSERT
		INTO Users
			(userName, userEmail, userComment, userLanguage, regTime, hash)
		VALUES
			(@name, @email, @comment, @language, SYSDATETIME(), @hash);
	*/
RETURN 0