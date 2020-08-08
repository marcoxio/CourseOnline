CREATE PROCEDURE usp_obtain_pagination(
	@NameCourse nvarchar(500),
	@Sorted nvarchar(500),
	@NumberPage int,
	@QuantityElements int,
	@TotalRecords int OUTPUT,
	@TotalPages int OUTPUT
)AS
BEGIN

	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	DECLARE @Start int
	DECLARE @End int

	IF @NumberPage = 1
		BEGIN
			SET @Start = (@NumberPage*@QuantityElements) - @QuantityElements
			SET @End = @NumberPage * @QuantityElements
		END
	ELSE
		BEGIN
			SET @Start = ((@NumberPage*@QuantityElements) - @QuantityElements) + 1
			SET @End = @NumberPage * @QuantityElements
		END

	CREATE TABLE #TMP(
		rowNumber int IDENTITY(1,1),
		ID uniqueidentifier
	)

	DECLARE @SQL nvarchar(max)
	SET @SQL = ' SELECT CourseId FROM Course '
	
	IF @NameCourse IS NOT NULL
		BEGIN
			SET @SQL = @SQL + ' WHERE Title LIKE ''%' + @NameCourse +'%''  '
		END

	IF @Sorted IS NOT NULL
		BEGIN
			SET @SQL = @SQL + ' ORDER BY  ' + @Sorted
		END

		--SELECT CursoId FROM Curso WHERE Titulo LIKE '% ASP %' ORDER BY Titulo
		INSERT INTO #TMP(ID)
		EXEC sp_executesql @SQL

		SELECT @TotalRecords =Count(*) FROM #TMP

		IF @TotalRecords > @QuantityElements 
			BEGIN
				SET @TotalPages = @TotalRecords / @QuantityElements
				IF (@TotalRecords % @QuantityElements) > 0
					BEGIN
						SET @TotalPages = @TotalPages + 1 
					END

			END
		ELSE
		BEGIN
			SET @TotalPages = 1
		END

		SELECT 
			c.CourseId,
			c.Title,
			c.Description,
			c.DateOfPublication,
			c.CoverPhoto,
			c.CreationDate,
			p.ActualPrice,
			p.Promotion
		FROM #TMP t INNER JOIN dbo.Course c 
						ON t.ID = c.CourseId
					LEFT JOIN Price p 
						ON c.CourseId = p.CourseId
		 WHERE t.rowNumber >= @Start AND t.rowNumber <= @End

END