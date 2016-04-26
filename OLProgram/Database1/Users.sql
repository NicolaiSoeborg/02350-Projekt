CREATE TABLE [dbo].[Users]
(
	[studentID] INT NOT NULL PRIMARY KEY, 
    [studentName] NCHAR(40) NOT NULL, 
    [role] NCHAR(10) NOT NULL, 
    [teamName] NCHAR(20) NOT NULL, 
    [major] NCHAR(30) NOT NULL
)
