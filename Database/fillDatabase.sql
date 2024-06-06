-- CREATE TABLE IF NOT EXISTS Courses (
--     Id INTEGER PRIMARY KEY AUTOINCREMENT,
--     Name TEXT NOT NULL,
--     Description TEXT
-- );

-- CREATE TABLE IF NOT EXISTS Pages (
--     IdPage INTEGER PRIMARY KEY AUTOINCREMENT,
--     IdCourse INTEGER NOT NULL,
--     Path TEXT NOT NULL,
--     PageName TEXT NOT NULL,
--     FOREIGN KEY (IdCourse) REFERENCES Courses(Id)
-- );

-- CREATE TABLE IF NOT EXISTS Users (
--     Id INTEGER PRIMARY KEY AUTOINCREMENT,
--     Name TEXT NOT NULL,
--     Surname TEXT NOT NULL,
--     Email TEXT NOT NULL,
--     Username TEXT NOT NULL,
--     Password TEXT NOT NULL
-- );

-- CREATE TABLE IF NOT EXISTS Enrollments (
--     IdCourses INTEGER NOT NULL,
--     IdUsers INTEGER NOT NULL,
--     PRIMARY KEY (IdCourses, IdUsers),
--     FOREIGN KEY (IdCourses) REFERENCES Courses(Id),
--     FOREIGN KEY (IdUsers) REFERENCES Users(Id)
-- );

-- INSERT INTO Courses (Name, Description) VALUES ('Computer Science', 'Description for Course 1');
-- INSERT INTO Courses (Name, Description) VALUES ('Course 2', 'Description for Course 2');

-- INSERT INTO Pages (IdCourse, Path, PageName) VALUES (1, '/path/to/page1', 'Course 1 welcome page');
-- INSERT INTO Pages (IdCourse, Path, PageName) VALUES (1, '/path/to/page2', 'Course 1 Page 2');
-- INSERT INTO Pages (IdCourse, Path, PageName) VALUES (2, '/path/to/page3', 'Course 2 welcome page');

-- DELETE FROM Pages;
-- WHERE IdPage = 1 AND IdCourse = 1;

-- INSERT INTO Pages (IdPage, IdCourse, Path, PageName)
-- VALUES (2, 1, 'Pages/course/contents/page_1_2.json', 'Networking and the Internet');

-- INSERT INTO Pages (IdPage, IdCourse, Path, PageName)
-- VALUES (3, 2, 'Pages/course/contents/page_2_1.json', 'Introduction to Environmental Science');

SELECT * FROM Pages;
