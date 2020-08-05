CREATE PROCEDURE usp_instructor_editor(
	@InstructorId uniqueidentifier,
	@Name nvarchar(500),
	@LastName nvarchar(500),
	@Grade nvarchar(100)
)
AS 
	BEGIN
		UPDATE Instructor
		SET
			Name = @Name,
			LastName = @LastName,
			Grade = @Grade,
			CreationDate = GETUTCDATE()
		WHERE InstructorId = @InstructorId
	END