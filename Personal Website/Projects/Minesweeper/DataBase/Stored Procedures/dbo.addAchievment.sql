CREATE PROCEDURE [dbo].[addAchievment]
   @userID int = -1,
   @mode int = -1
AS
   DECLARE @dateStart datetime
   SELECT @dateStart = DateStart FROM GameStats WHERE UserID=@userID

   DECLARE @dateEnd datetime
   SET @dateEnd = getDate()

   DECLARE @duration int
   SET @duration= datediff(millisecond, @dateStart, @dateEnd)

   IF EXISTS(SELECT * FROM LeaderBoards WHERE UserID=@userID AND Mode=@mode)
 BEGIN

         DECLARE @prevDuration int
         SELECT @prevDuration=Duration FROM LeaderBoards WHERE UserID=@userID AND Mode=@mode

         IF @prevDuration > @duration
            UPDATE LeaderBoards SET DateStart=@dateStart, DateEnd=@dateEnd, Duration=@duration WHERE UserID=@userID AND Mode=@mode
     
      END
 
      ELSE
      INSERT INTO LeaderBoards(UserID, Mode, DateStart, DateEnd, Duration) VALUES(@userID, @mode, @dateStart, @dateEnd, @duration)