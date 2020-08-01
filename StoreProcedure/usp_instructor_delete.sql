CREATE PROCEDURE usp_instructor_delete(
	@InstructorId uniqueIdentifier
)
AS
	BEGIN
		DELETE FROM CourseInstructor 
		WHERE InstructorId = @InstructorId

		DELETE FROM Instructor
		WHERE InstructorId = @InstructorId
	END