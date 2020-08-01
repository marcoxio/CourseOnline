CREATE PROCEDURE usp_Obtain_Instructors
AS
	BEGIN
		SET NOCOUNT ON
		SELECT
			X.InstructorId,
			X.Name AS FirstName,
			X.LastName,
			X.Grade
		FROM Instructor X
	END