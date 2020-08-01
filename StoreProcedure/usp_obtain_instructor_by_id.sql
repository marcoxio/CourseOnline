CREATE PROCEDURE usp_obtain_instructor_by_id(
	@Id uniqueidentifier
)
AS
	BEGIN
		SELECT
			InstructorId,
			Name AS FirstName,
			LastName,
			Grade
		FROM	Instructor WHERE InstructorId = @Id

	END