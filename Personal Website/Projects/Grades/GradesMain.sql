/* TABLE to hold a summary of grades */

DROP TABLE SimpleGrades;

CREATE TABLE SimpleGrades (
	[term]			CHAR(1)			NOT NULL,
	[year]			INT				NOT NULL,
	[title]			VARCHAR(200)	NOT NULL,
	[courseID]		VARCHAR(10)		NOT NULL PRIMARY KEY,
	[gradePercent]	FLOAT			NOT NULL,
	[gradeLetter]	VARCHAR(2)		NOT NULL,
	[status]		VARCHAR(20)		NOT NULL,

	CHECK(	term IN ('A', 'B', 'C', 'D')				),
	CHECK(	Year > 2012 AND Year < 2018					),
	CHECK(	gradePercent <= 100 AND gradePercent > 0	),
	CHECK(	gradeLetter IN ('A', 'B', 'C', 'NR')		),
	CHECK(	status IN ('Completed', 'in progress...')	)
);