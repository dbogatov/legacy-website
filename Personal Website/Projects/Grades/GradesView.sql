CREATE VIEW GradesView
	AS
	SELECT term, year, title, courseID, gradePercent, gradeLetter, status, reqName
	FROM (SimpleGrades INNER JOIN DiplomaReqs ON SimpleGrades.reqID = DiplomaReqs.reqID);
