CREATE TABLE [dbo].[Dictionary]
(
    [userLanguage] NVARCHAR(63) NOT NULL, 
    [subject] NVARCHAR(128) NOT NULL, 
    [mailBody] NVARCHAR(2048) NOT NULL, 
    PRIMARY KEY ([userLanguage])
)
