using Reviews.Domain.Models;
using System.Threading.Tasks;

namespace Reviews.Domain.Interfaces
{
    public interface ILoginService
    {
        /// <summary>
        /// Добавить логин и пароль
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Логическое (true или false) значение</returns>
        Task<bool> AddLoginAsync(Login login);

        /// <summary>
        /// Проверить логин и пароль
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Логическое (true или false) значение</returns>
        Task<bool> CheckLoginAsync(string userName);
    }
}
