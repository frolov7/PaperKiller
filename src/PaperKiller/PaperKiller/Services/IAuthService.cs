using PaperKiller.DTO;
using PaperKiller.Models;
using static PaperKiller.Utils.Constants;

namespace PaperKiller.Services
{
    public interface IAuthService
    {
        //public string GenerateJwtToken(string login, string password);
        public UserStatus GetUserStatus(string login, string password);
        public Student AuthenticateUser(Student authData);
    }
}
