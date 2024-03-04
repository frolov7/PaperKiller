using PaperKiller.Models;

namespace PaperKiller.Repository
{
    public interface IStudentRepository
    {
        bool IsStudentIDExists(string studak);
        Student GetStudentByID(string studentID);
        Student GetStudentByLogin(string login, string password);
        Student GetStudentByRoom(string name, string surname, string room);
        int InsertStudentData(string name, string surname, string phoneNumber, string studak, string gender);
        public int UpdateStudentData(Student updatedStudent, string id);
        int UpdateRoomNumber(string userID, string newRoom);
    }
}
