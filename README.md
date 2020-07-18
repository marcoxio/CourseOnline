# RestFull API CoursesOnline

# How to recognize DbContext
`dotnet add package Microsoft.EntityFrameworkCore --version 3.1.3`

# Install SqlServer
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.3`

# Install Tools for recognize Migrations
`dotnet add package Microsoft.EntityFrameworkCore.Tools --version 3.1.3`

# Install MediatR
`dotnet add package MediatR.Extensions.Microsoft.DependencyInjection --version 3.1.3`


# Install fluentValidation
`dotnet add package FluentValidation.AspNetCore --version 8.5.2`



# Generate Migration
`dotnet ef migrations add IdentityCoreInicial -p .\Persistencia\ -s .\WebAPI\`

# Run migration
`dotnet ef database update -p .\Persistencia\ -s .\WebAPI\`

# Execute File Migration
file Program change
`dotnet watch run`

# How to Consult methods API

## GetAllCourses
`http://localhost:5000/api/cursos`

## DetailCourse
`http://localhost:5000/api/cursos{id}`