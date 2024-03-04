using PaperKiller.DTO;

namespace PaperKiller.Models
{
    public class Student
    {
        public int? UserId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? UserType { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CheckInDate { get; set; }
        public string? Gender { get; set; }
        public string? StudentId { get; set; }
        public string RoomNumber { get; set; }
        public int? LinenId { get; set; }
        public int? ItemsId { get; set; }

        public Student() { }
        public Student(int userId, string login, string password, string userType, string name, string surname, string phoneNumber, string checkInDate, string gender, string studentId, string roomNumber, int linenId, int itemsId)
        {
            UserId = userId;
            Login = login;
            Password = password;
            UserType = userType;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            CheckInDate = checkInDate;
            Gender = gender;
            StudentId = studentId;
            RoomNumber = roomNumber;
            LinenId = linenId;
            ItemsId = itemsId;
        }
        /*
        public Student(object[] data)
        {
            UserId = data[0].ToString();
            Login = data[1] as string;
            Password = data[2] as string;
            UserType = data[3] as string;
            Name = data[4] as string;
            Surname = data[5] as string;
            PhoneNumber = data[6] as string;
            CheckInDate = data[7] as string;
            StudentId = data[8] as string;
            Gender = data[9] as string;
            RoomNumber = data[10]?.ToString() ?? "Не назначена";
            LinenId = data[11] as int? ?? 0;
            ItemsId = data[12] as int? ?? 0;
        }
        */
        public Student(StudentDTO data)
        {
            UserId = data.Id as int? ?? 0;
            var studentDTOWith = (StudentDTOAdd)data;
            Login = studentDTOWith.Login;
            Password = studentDTOWith.Password;
            UserType = studentDTOWith.UserType;
            Name = data.Name as string;
            Surname = data.Surname as string;
            PhoneNumber = data.PhoneNumber as string;
            CheckInDate = data.CheckInDate as string;
            StudentId = data.Studak as string;
            Gender = data.Gender as string;
            RoomNumber = data.RoomNumber?.ToString() ?? "Не назначена";
            LinenId = data.LinenId as int? ?? 0;
            ItemsId = data.ItemsId as int? ?? 0;
        }
    }
}