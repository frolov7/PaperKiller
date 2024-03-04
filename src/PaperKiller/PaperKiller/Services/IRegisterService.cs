using PaperKiller.DTO;
using PaperKiller.Models;
using static PaperKiller.Utils.Constants;

namespace PaperKiller.Services
{
    public interface IRegisterService
    {
        public bool AreAllFieldsFilled(string name, string surname, string studentID, string phone, string login, string password, string passwordRepeat);
        public bool IsGenderSelected(string gender);
        public bool IsLoginUnique(string login);
        public bool IsStudentIDUnique(string studentID);
        public bool ArePasswordsEqual(string password, string passwordRepeat);
        public bool IsPhoneNumberValid(string phone);
        //public InputError RegisterUser(string name, string surname, string studentID, string gender, string phone, string login, string password, string passwordRepeat);
        public InputError RegisterUser(RegistrationDTO student);
    }
}