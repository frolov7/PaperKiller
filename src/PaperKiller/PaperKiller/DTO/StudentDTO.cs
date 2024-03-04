namespace PaperKiller.DTO
{
    public class StudentDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string CheckInDate { get; set; }
        public string Studak { get; set; }
        public string Gender { get; set; }
        public string RoomNumber { get; set; }
        public int? LinenId { get; set; }
        public int? ItemsId { get; set; }
    }

    public class StudentDTOAdd : StudentDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
