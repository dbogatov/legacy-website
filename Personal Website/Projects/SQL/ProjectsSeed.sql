INSERT INTO Projects VALUES (1, 'Minesweeper',	'The Minsweeper is a Web Application imitating a well-known Microsoft Minesweeper.',	'20140930', 'http://minesweeper.dbogatov.org', 'https://bitbucket.org/Dima4ka/minsweeper', '../Images/Minesweeper.png');
INSERT INTO Projects VALUES (2, 'Personal Website',	'Personal Website is a private project containing the resume, projects and blog of its author.',	SYSDATETIME(), 'http://dbogatov.org', 'https://bitbucket.org/Dima4ka/personal-website', '../Images/PersonalWebsite.png');
INSERT INTO Projects VALUES (3, 'Banker Game Assistant',	'This is an iOS application serving as a banker for board games. It keeps track of all your money accounts.',	'20150215', 'Banker/Default.aspx', 'https://bitbucket.org/Dima4ka/monopoly-banker/', '../Images/Banker.png');
INSERT INTO Projects VALUES (4, 'WPI Event Creator',	'This is an iOS application allowing WPI students to create appointment events with faculty easily and quickly.',	'20140410', 'Calendar-Event-Creator/Default.aspx', 'https://bitbucket.org/Dima4ka/wpi-calendar-event-creator/', '../Images/WPICalendarEventCreator.png');
INSERT INTO Projects VALUES (5, 'Finance Parser',	'Simple parser uses Google API to display current bid and ask prices for selected symbols and strikes.',	'20150405', 'Google-Finance-Parser/Default.aspx', 'https://bitbucket.org/Dima4ka/personal-website', '../Images/FinanceParser.png');
INSERT INTO Projects VALUES (6, 'IQP - Trading&Investment',	'Interactive Qualifying Project - developing a system of trading systems using scientific approches.',	'20150506', 'IQP/Default.aspx', '#', '../Images/IQP.jpg');


INSERT INTO Tags VALUES (1, 'Large');
INSERT INTO Tags VALUES (2, 'University');

INSERT INTO ProjectTag (projectID, tagID) VALUES (1, 1);
INSERT INTO ProjectTag (projectID, tagID) VALUES (2, 1);
INSERT INTO ProjectTag (projectID, tagID) VALUES (3, 1);
INSERT INTO ProjectTag (projectID, tagID) VALUES (4, 1);
INSERT INTO ProjectTag (projectID, tagID) VALUES (4, 2);
INSERT INTO ProjectTag (projectID, tagID) VALUES (6, 1);
INSERT INTO ProjectTag (projectID, tagID) VALUES (6, 2);