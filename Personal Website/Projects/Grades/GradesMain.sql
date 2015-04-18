/* TABLE to hold a summary of grades */

/*
DROP TABLE GradeReq;
*/

DROP TABLE SimpleGrades;
DROP TABLE DiplomaReqs;

CREATE TABLE DiplomaReqs (
	[reqID]			INT				PRIMARY KEY			,
	[reqName]		VARCHAR(128)	NOT NULL
);

CREATE TABLE SimpleGrades (
	
	[term]			CHAR(1)			NOT NULL					,
	[year]			INT				NOT NULL					,
	[title]			VARCHAR(200)	NOT NULL PRIMARY KEY		,
	[courseID]		VARCHAR(10)		NOT NULL					,
	[gradePercent]	FLOAT			NOT NULL					,
	[gradeLetter]	VARCHAR(2)		NOT NULL					,
	[status]		VARCHAR(20)		NOT NULL					,
	[reqID]			INT											,

	CHECK(	term IN ('A', 'B', 'C', 'D')						),
	CHECK(	Year > 2012 AND Year < 2018							),
	CHECK(	gradePercent <= 100 AND gradePercent > 0			),
	CHECK(	gradeLetter IN ('A', 'B', 'C', 'NR')				),
	CHECK(	status IN ('Completed', 'in progress...', 'In plan')),

	CONSTRAINT FK_GRADES_reqID		FOREIGN KEY (reqID)
		REFERENCES DiplomaReqs (reqID)
);

/*
CREATE TABLE GradeReq (

	[relID]			INT				PRIMARY KEY IDENTITY,
	[gradeTitle]	VARCHAR(200)						,
	[reqID]			INT									,

	CONSTRAINT FK_GRADES_gradeTitle	FOREIGN KEY (gradeTitle)
		REFERENCES SimpleGrades (title)					,
	CONSTRAINT FK_GRADES_reqID		FOREIGN KEY (reqID)
		REFERENCES DiplomaReqs (reqID)

);
*/