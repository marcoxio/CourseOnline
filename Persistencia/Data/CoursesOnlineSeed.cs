using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.Extensions.Logging;

namespace Persistencia.Data.SeedData
{
    public class CoursesOnlineSeed
    {
        public static async Task SeedAsync(CoursesOnlineContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                  if (!context.Course.Any())
                {
                    var courseData = File.ReadAllText("../Persistencia/Data/SeedData/courses.json");

                    var courses = JsonSerializer.Deserialize<List<Course>>(courseData);

                    foreach (var course in courses)
                    {
                        context.Course.Add(course);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                
                    var logger = loggerFactory.CreateLogger<CoursesOnlineSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}