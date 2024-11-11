using Microsoft.EntityFrameworkCore;
using Reviews.Domain.Interfaces;
using Reviews.Domain.Models;

namespace Reviews.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly DataBaseContext databaseContext;

        public LoginService(DataBaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<bool> CheckLoginAsync(string userName)
        {
            return await databaseContext.Logins.AnyAsync(item => item.UserName == userName);
        }
        public async Task<bool> AddLoginAsync(Login login)
        {
            if(!await CheckLoginAsync(login.UserName)) 
            {
                await databaseContext.Logins.AddAsync(login);
                await databaseContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
