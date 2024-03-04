using PaperKiller.Repository;
using PaperKiller.Controllers;
using System.Text.RegularExpressions;
using static PaperKiller.Utils.Constants;
using PaperKiller.Models;
using PaperKiller.DTO;

namespace PaperKiller.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;

        public RegisterService(IUserRepository userRepository, IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }

        public bool AreAllFieldsFilled(string name, string surname, string studentID, string phone, string login, string password, string passwordRepeat)
        {
            return !string.IsNullOrWhiteSpace(name) &&
                   !string.IsNullOrWhiteSpace(surname) &&
                   !string.IsNullOrWhiteSpace(studentID) &&
                   !string.IsNullOrWhiteSpace(phone) &&
                   !string.IsNullOrWhiteSpace(login) &&
                   !string.IsNullOrWhiteSpace(password) &&
                   !string.IsNullOrWhiteSpace(passwordRepeat);
        }

        public bool IsGenderSelected(string gender)
        {
            return gender != "Пол не выбран";
        }

        public bool IsLoginUnique(string login)
        {
            return !_userRepository.IsLoginExists(login);
        }

        public bool IsStudentIDUnique(string studentID)
        {
            return !_userRepository.IsUserExists(studentID);
        }

        public bool ArePasswordsEqual(string password, string passwordRepeat)
        {
            return password == passwordRepeat;
        }

        public bool IsPhoneNumberValid(string phone)
        {
            return Regex.IsMatch(phone, @"\+7\d{3}\d{3}\d{4}$");
        }

        public InputError RegisterUser(RegistrationDTO student)
        {
            if (!AreAllFieldsFilled(student.Name, student.Surname, student.StudentID, student.Phone, student.Login, student.Password, student.PasswordRepeat))
                return InputError.FieldERROR;
            else if (!IsGenderSelected(student.Gender))
                return InputError.GenderERROR;
            else if (!IsLoginUnique(student.Login))
                return InputError.LoginERROR;
            else if (!IsStudentIDUnique(student.StudentID))
                return InputError.StudentIDERROR;
            else if (!ArePasswordsEqual(student.Password, student.Password))
                return InputError.PasswordERROR;
            else if (!IsPhoneNumberValid(student.Phone))
                return InputError.PhoneERROR;
            else
            {
                _userRepository.InsertUser(student.Login, student.Password);
                _studentRepository.InsertStudentData(student.Name, student.Surname, student.Phone, student.StudentID, student.Gender);
                return InputError.SUCCESS;
            }
        }

    }
}
