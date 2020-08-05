using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistencia.Data
{
    public class TestData
    {
        public static async Task InsertData(CoursesOnlineContext context, UserManager<User> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new User{FullName = "Marco Jimenez", UserName="marcoxio", Email="mj475676@gmail.com"};
                await userManager.CreateAsync(user, "Password123#");
            }
        }
    }
}