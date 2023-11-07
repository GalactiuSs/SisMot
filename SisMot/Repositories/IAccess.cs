using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SisMot.Models;
using SisMot.Models.CustomModels;

namespace SisMot.Repositories
{
    public interface IAccess
    {
        Task<bool> CreatePerson(Person person, User user, string confirmPassword);
        Task<User> Login(LoginDTO login);
        Task<int> RecoveryPassword(string email);
        Task<bool> ChangePassword(string newPassword, string confirmPassword, string code, int id);
    }
}
