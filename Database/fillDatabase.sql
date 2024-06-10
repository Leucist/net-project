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

INSERT INTO Courses (Id, Name, Description) VALUES (1, 'Computer Science', 'Description for Course 1');
INSERT INTO Courses (Id, Name, Description) VALUES (2, 'Environmental Science', 'Description for Course 2');

INSERT INTO Pages (IdCourse, IdPage, Path, PageName)
VALUES (1, 1, 'Pages/course/contents/page_1_1.json', 'Introduction to Computer Science');

INSERT INTO Pages (IdCourse, IdPage, Path, PageName)
VALUES (1, 2, 'Pages/course/contents/page_1_2.json', 'Networking and the Internet');

INSERT INTO Pages (IdCourse, IdPage, Path, PageName)
VALUES (2, 1, 'Pages/course/contents/page_2_1.json', 'Introduction to Environmental Science');

-- DELETE FROM Pages
-- WHERE IdPage = 1 AND IdCourse = 1;

-- SELECT * FROM Pages;
