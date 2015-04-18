/* Commands to seed the values for grade tables */

INSERT INTO DiplomaReqs VALUES( 1, 'CS Classes + MQP'	);
INSERT INTO DiplomaReqs VALUES( 2, 'Math'				);
INSERT INTO DiplomaReqs VALUES( 3, 'Science'			);
INSERT INTO DiplomaReqs VALUES( 4, 'Breadth'			);
INSERT INTO DiplomaReqs VALUES( 5, 'Depth'				);
INSERT INTO DiplomaReqs VALUES( 6, 'Seminar'			);
INSERT INTO DiplomaReqs VALUES( 7, 'Social Sciences'	);
INSERT INTO DiplomaReqs VALUES( 8, 'Physical Education'	);
INSERT INTO DiplomaReqs VALUES( 9, 'IQP'				);
INSERT INTO DiplomaReqs VALUES( 10, 'Free Elective'		);

INSERT INTO SimpleGrades VALUES( 'A', 2016, 'Financial Accounting',									'ACC1100',	90.00,	'I', 'Registered', 10	);

INSERT INTO SimpleGrades VALUES( 'D', 2016, 'Programming Languages',								'CS4536',	90.00,	'I', 'Registered', 1	);
INSERT INTO SimpleGrades VALUES( 'D', 2016, 'Practicum in Humanities: Visual Persuasion',			'HU3910',	90.00,	'I', 'Registered', 6	);
INSERT INTO SimpleGrades VALUES( 'D', 2016, 'Introduction to Electrical and Computer Engineering',	'ECE2010',	90.00,	'I', 'Registered', 3	);

INSERT INTO SimpleGrades VALUES( 'C', 2016, 'Distributed Computing Systems',						'CS4513',	90.00,	'I', 'Registered', 1	);
INSERT INTO SimpleGrades VALUES( 'C', 2016, '3D Modeling I',										'AR2102',	90.00,	'I', 'Registered', 5	);
INSERT INTO SimpleGrades VALUES( 'C', 2016, 'MATTERS. Data Mining',									'EAR1501',	90.00,	'I', 'Registered', 1	);
INSERT INTO SimpleGrades VALUES( 'C', 2016, 'Soccer',												'PE1019',	90.00,	'I', 'Registered', 8	);

INSERT INTO SimpleGrades VALUES( 'B', 2015, 'Webware: Computational Technology for Network Inforamtion Systems',	'CS4241',	90.00,	'I', 'Registered', 1	);
INSERT INTO SimpleGrades VALUES( 'B', 2015, 'Data Mining and Knowledge Discovery in Databases',		'CS4445',	90.00,	'I', 'Registered', 1	);
INSERT INTO SimpleGrades VALUES( 'B', 2015, 'MATTERS. Data Mining',									'EAR1501',	90.00,	'I', 'Registered', 1	);
INSERT INTO SimpleGrades VALUES( 'B', 2015, 'Soccer',												'PE1019',	90.00,	'I', 'Registered', 8	);

INSERT INTO SimpleGrades VALUES( 'A', 2015, 'Operating Systems',									'CS3013',	90.00,	'I', 'Registered', 1	);
INSERT INTO SimpleGrades VALUES( 'A', 2015, 'Introduction to History of Technology',				'HI1332',	90.00,	'I', 'Registered', 4	);
INSERT INTO SimpleGrades VALUES( 'A', 2015, 'MATTERS. Data Mining',									'EAR1501',	90.00,	'I', 'Registered', 1	);

INSERT INTO SimpleGrades VALUES( 'D', 2015, 'IQP Term D',											'HH1423',	100,	'A', 'in progress...', 9	);
INSERT INTO SimpleGrades VALUES( 'D', 2015, 'Algorithms',											'CS2223',	97.00,	'A', 'in progress...', 1	);
INSERT INTO SimpleGrades VALUES( 'D', 2015, 'Elements of Writing',									'WR1010',	93.00,	'A', 'in progress...', 4	);
INSERT INTO SimpleGrades VALUES( 'D', 2015, 'Soccer',												'PE1019',	100,	'A', 'in progress...', 8	);

INSERT INTO SimpleGrades VALUES( 'C', 2015, 'Molecularity',											'CH1010',	93.05,	'A', 'Completed', 3		);
INSERT INTO SimpleGrades VALUES( 'C', 2015, 'Database Systems I',									'CS3431',	91.70,	'A', 'Completed', 1		);
INSERT INTO SimpleGrades VALUES( 'C', 2015, 'Probability for Applications',							'MA2621',	88.60,	'A', 'Completed', 2		);
INSERT INTO SimpleGrades VALUES( 'C', 2015, 'IQP Term C',											'HH1423',	100,	'A', 'Completed', 9		);

INSERT INTO SimpleGrades VALUES( 'B', 2014, 'IQP Term B',											'HH1423',	100,	'A', 'Completed', 9		);
INSERT INTO SimpleGrades VALUES( 'B', 2014, 'Software Engineering',									'CS3733',	89.85,	'B', 'Completed', 1		);
INSERT INTO SimpleGrades VALUES( 'B', 2014, 'Digital Image',										'AR1101',	97.65,	'A', 'Completed', 5		);
INSERT INTO SimpleGrades VALUES( 'B', 2014, 'Volleyball & Squash',									'PE1002',	100,	'A', 'Completed', 8		);

INSERT INTO SimpleGrades VALUES( 'A', 2014, 'ISP Trading Systems, Investment & Risk Management',	'HH2000',	100,	'A', 'Completed', 7		);
INSERT INTO SimpleGrades VALUES( 'A', 2014, 'Physics: Intermediate Mechanics I',					'PH2201',	90.55,	'A', 'Completed', 3		);
INSERT INTO SimpleGrades VALUES( 'A', 2014, 'Applied Statistics I',									'MA2611',	95.72,	'A', 'Completed', 2		);
INSERT INTO SimpleGrades VALUES( 'A', 2014, 'Discrete Mathematics',									'CS2202',	93.5,	'A', 'Completed', 1		);

INSERT INTO SimpleGrades VALUES( 'D', 2014, 'Macroeconomics',										'ECON1120',	93.33,	'A', 'Completed', 7		);
INSERT INTO SimpleGrades VALUES( 'D', 2014, 'Machine Organization and Assembly Language',			'CS2011',	95.6,	'A', 'Completed', 1		);
INSERT INTO SimpleGrades VALUES( 'D', 2014, 'Ordinary Differential Equations',						'MA2051',	98.5,	'A', 'Completed', 2		);

INSERT INTO SimpleGrades VALUES( 'C', 2014, 'Calculus IV',											'MA1024',	96.02,	'A', 'Completed', 2		);
INSERT INTO SimpleGrades VALUES( 'C', 2014, 'Essentials of Art',									'AR1100',	96.7,	'A', 'Completed', 5		);
INSERT INTO SimpleGrades VALUES( 'C', 2014, 'Systems Programming',									'CS2303',	94.4,	'A', 'Completed', 1		);

INSERT INTO SimpleGrades VALUES( 'B', 2013, 'Calculus III',											'MA1023',	95.7,	'A', 'Completed', 2		);
INSERT INTO SimpleGrades VALUES( 'B', 2013, 'Physics: General Electricity',							'PH1120',	96,		'A', 'Completed', 3		);
INSERT INTO SimpleGrades VALUES( 'B', 2013, 'Introduction to Object Oriented Programming',			'CS2102',	91.4,	'A', 'Completed', 1		);
										 
INSERT INTO SimpleGrades VALUES( 'A', 2013, 'Calculus II',											'MA1022',	97.4,	'A', 'Completed', 2		);
INSERT INTO SimpleGrades VALUES( 'A', 2013, 'Physics: General Mechanics',							'PH1110',	95.3,	'A', 'Completed', 3		);
INSERT INTO SimpleGrades VALUES( 'A', 2013, 'Introduction to Program Design',						'CS1101',	97.6,	'A', 'Completed', 1		);