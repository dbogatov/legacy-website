/* TABLE to hold a summary of projects */

ALTER TABLE ProjectTag DROP CONSTRAINT FK_projectID	;
ALTER TABLE ProjectTag DROP CONSTRAINT FK_tagID		;

DROP TABLE Projects		;
DROP TABLE Tags			;
DROP TABLE ProjectTag	;


CREATE TABLE Projects (

	projectID		INT				PRIMARY KEY			, 
	title			VARCHAR(200)	NOT NULL			,
	descriptionText	VARCHAR(1024)	NOT NULL			,
	dateCompleted	DATE			NOT NULL			,
	weblink			VARCHAR(2083)	NOT NULL			,
	imgeFilePath	VARCHAR(255)	NOT NULL			,

);

CREATE TABLE Tags (
	
	tagID			INT				PRIMARY KEY	,
	tagName			VARCHAR(127)	NOT NULL	

);

CREATE TABLE ProjectTag (

	relID			INT				PRIMARY KEY IDENTITY,
	projectID		INT									,
	tagID			INT									,

	CONSTRAINT FK_projectID	FOREIGN KEY (projectID)
		REFERENCES Projects (projectID)					,
	CONSTRAINT FK_tagID		FOREIGN KEY (tagID)
		REFERENCES Tags (tagID)

);




INSERT INTO Projects VALUES (1, 'Minesweeper',	'The description for Minesweeper',	'20140930', 'http://minesweeper.dbogatov.org',	'../Images/DmytroBogatov.jpg');

INSERT INTO Tags VALUES (1, 'Large');
INSERT INTO Tags VALUES (2, 'University');

INSERT INTO ProjectTag (projectID, tagID) VALUES (1, 1);
INSERT INTO ProjectTag (projectID, tagID) VALUES (1, 2);