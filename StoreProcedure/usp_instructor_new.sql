CREATE PROCEDURE usp_instructor_new(
@InstructorId uniqueidentifier,
@Name nvarchar(500),
@LastName nvarchar(500),
@Grade nvarchar(100)
)
AS
	BEGIN
		
		INSERT INTO Instructor(InstructorId, Name, LastName, Grade)
		VALUES(@InstructorId,@Name,@LastName,@Grade)

	END
