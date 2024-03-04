using PaperKiller.DTO;
using PaperKiller.Models;
using PaperKiller.Models.items;
using PaperKiller.Models.linen;
using PaperKiller.Repository;
using static PaperKiller.Utils.Constants;

namespace PaperKiller.Services
{
    public interface IUserService
    {
        public RoomStatus MoveOutService(string studentID);
        public List<Report> ShowReport();
        public List<Student> ShowStudent();
        public List<MyItems> ShowMyItems(string userID);
        public List<ItemCredentials> ShowItems();
        public Student ShowMyData(string userID);
    }
}
