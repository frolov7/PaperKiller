using Microsoft.IdentityModel.Tokens;
using PaperKiller.Models;
using PaperKiller.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using static PaperKiller.Utils.Constants;

namespace PaperKiller.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;

        public AuthService(IUserRepository userRepository, IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }
        /*
        public string GenerateJwtToken(string login, string password)
        {
            var key = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            var securityKey = new SymmetricSecurityKey(key);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login),
                new Claim("Password", password)
            };

            var token = new JwtSecurityToken(
                issuer: "https://server.com",
                audience: "https://clientapp.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Время жизни токена
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        */
        public UserStatus GetUserStatus(string login, string password)
        {
            if (login == "Admin" && password == "Commandant")
                return UserStatus.COMMANDANT;
            else if (login == "Admin" && password == "Manager")
                return UserStatus.MANAGER;
            else
                return UserStatus.STUDENT;
        }

        public Student AuthenticateUser(Student authData)
        {
            UserStatus userStatus = GetUserStatus(authData.Login, authData.Password);

            if (userStatus == UserStatus.STUDENT)
            {
                bool isLoginTaken = _userRepository.IsLoginExists(authData.Login);

                if (isLoginTaken)
                {
                    var student = _studentRepository.GetStudentByLogin(authData.Login, authData.Password);
                    return student; // Возвращаем объект Student
                }
            }
            return null; // Возвращаем null, если аутентификация не удалась
        }

    }
}
