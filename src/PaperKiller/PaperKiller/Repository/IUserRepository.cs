using PaperKiller.DTO;
using PaperKiller.Models;
using PaperKiller.Models.items;
using PaperKiller.Models.linen;

namespace PaperKiller.Repository
{
    public interface IUserRepository
    {
        bool IsLoginExists(string login);
        bool IsUserExists(string studak);
        int DeleteUserByID(string userID);
        int InsertUser(string login, string password);
        int UpdatePassword(string login, string newPassword);
        int UpdateUsersData(string oldLogin, string newLogin, string newPassword);
        int UpdateUserType(string userID, string newType);
        public List<Report> GetReport();
        public List<Student> GetStudents();
        public List<ItemCredentials> GetItems();
        public List<MyItems> GetMyItems(string userID);
        public Student GetMyData(string userID);
    }
}
