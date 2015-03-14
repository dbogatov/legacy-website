﻿INSERT INTO Projects VALUES (1, 'Minesweeper',	'The Minsweeper is a Web Application imitating a well-known Microsoft Minesweeper.',	'20140930', 'http://minesweeper.dbogatov.org', 'https://bitbucket.org/Dima4ka/minsweeper', '../Images/Minesweeper.png');
INSERT INTO Projects VALUES (2, 'Personal Website',	'Personal Website is a private project containing the resume, projects and blog of its author.',	SYSDATETIME(), 'http://dbogatov.org', 'https://bitbucket.org/Dima4ka/personal-website', '../Images/PersonalWebsite.png');
INSERT INTO Projects VALUES (3, 'Banker Game Assistant',	'This is an iOS application serving as a banker for board games. It keeps track of all your money accounts.',	'20150215', 'https://itunes.apple.com/us/app/banker-game-assistant/id961296297', 'https://bitbucket.org/Dima4ka/monopoly-banker/', '../Images/Banker.png');


INSERT INTO Tags VALUES (1, 'Large');
INSERT INTO Tags VALUES (2, 'University');

INSERT INTO ProjectTag (projectID, tagID) VALUES (1, 1);
INSERT INTO ProjectTag (projectID, tagID) VALUES (2, 1);
INSERT INTO ProjectTag (projectID, tagID) VALUES (3, 1);
