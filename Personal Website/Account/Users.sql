
CREATE TABLE Users (
	[userID]		INT				PRIMARY KEY IDENTITY,
	[userName]		NVARCHAR(255)	NOT NULL,
	[userEmail]		VARCHAR(127)	NOT NULL,
	[userComment]	NVARCHAR(2083)	NOT NULL,
	[userLanguage]	VARCHAR(63)		NOT NULL,
	[regTime]		DATE			NOT NULL,
	[hash]			VARCHAR(255)	NOT NULL
);