DROP FUNCTION GetTagsForProject;
GO

CREATE FUNCTION GetTagsForProject (@projectID INT )
RETURNS @returntable TABLE
(
	tagName VARCHAR(127)
)
AS
BEGIN
	INSERT	@returntable
	SELECT	tagName
	FROM	(Projects	INNER JOIN ProjectTag	ON Projects.projectID = ProjectTag.projectID)
						INNER JOIN Tags			ON ProjectTag.tagID = Tags.tagID
	WHERE	Projects.projectID = @projectID;
	RETURN
END
