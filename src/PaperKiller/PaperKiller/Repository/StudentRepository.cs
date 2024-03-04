using NLog.Fluent;
using PaperKiller.DTO;
using PaperKiller.Models;

namespace PaperKiller.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly Connector _connection;

        public StudentRepository(Connector connection)
        {
            _connection = connection;
        }

        public bool IsStudentIDExists(string studak)
        {
            string cmdTxt = "SELECT * FROM users, students WHERE studak = @studak";

            var parameters = new Dictionary<string, object>
            {
                { "studak", studak }
            };

            var tmp = _connection.ExecuteSelect(cmdTxt, parameters);

            return tmp.Count == 0 ? false : true;
        }

        public Student GetStudentByID(string studentID)
        {
            string cmdTxt = "SELECT students.id, login, password, userType, name, surname, phoneNumber, checkInDate, studak, gender, roomNumber, linen_id, items_id " +
                            "FROM students INNER JOIN users ON users.id = students.id " +
                            "WHERE students.id = @studentID";

            var parameters = new Dictionary<string, object>
            {
                { "studentID", studentID }
            };

            var tmp = _connection.ExecuteSelect(cmdTxt, parameters);

            if (tmp.Count != 0)
            {
                var studentDTO = new StudentDTOAdd
                {
                    Id = tmp[0][0] != null ? int.Parse(tmp[0][0].ToString()) : 0,
                    Login = tmp[0][1] as string,
                    Password = tmp[0][2] as string,
                    UserType = tmp[0][3] as string,
                    Name = tmp[0][4] as string,
                    Surname = tmp[0][5] as string,
                    PhoneNumber = tmp[0][6] as string,
                    CheckInDate = tmp[0][7] as string,
                    Studak = tmp[0][8] as string,
                    Gender = tmp[0][9] as string,
                    RoomNumber = tmp[0][10] != null ? tmp[0][10].ToString() : "Не назначена",
                    LinenId = tmp[0][11] != null ? int.Parse(tmp[0][11].ToString()) : 0,
                    ItemsId = tmp[0][12] != null ? int.Parse(tmp[0][12].ToString()) : 0
                };

                return new Student(studentDTO);
            }
            else
            {
                return null;
            }
        }

        public Student GetStudentByLogin(string login, string password)
        {
            string cmdTxt = "SELECT students.id, login, password, userType, name, surname, phoneNumber, checkInDate, studak, gender, roomNumber, linen_id, items_id " +
                            "FROM students INNER JOIN users ON users.id = students.id " +
                            "WHERE login = @login AND password = @password";

            var parameters = new Dictionary<string, object>
            {
                { "login", login },
                { "password", password }
            };

            var tmp = _connection.ExecuteSelect(cmdTxt, parameters);

            if (tmp.Count != 0)
            {
                var studentDTO = new StudentDTOAdd
                {
                    Id = tmp[0][0] != null ? int.Parse(tmp[0][0].ToString()) : 0,
                    Login = tmp[0][1] as string,
                    Password = tmp[0][2] as string,
                    UserType = tmp[0][3] as string,
                    Name = tmp[0][4] as string,
                    Surname = tmp[0][5] as string,
                    PhoneNumber = tmp[0][6] as string,
                    CheckInDate = tmp[0][7] as string,
                    Studak = tmp[0][8] as string,
                    Gender = tmp[0][9] as string,
                    RoomNumber = tmp[0][10] != null ? tmp[0][10].ToString() : "Не назначена",
                    LinenId = tmp[0][11] != null ? int.Parse(tmp[0][11].ToString()) : 0,
                    ItemsId = tmp[0][12] != null ? int.Parse(tmp[0][12].ToString()) : 0
                };

                return new Student(studentDTO);
            }
            else
            {
                return null;
            }
        }

        public Student GetStudentByRoom(string name, string surname, string room)
        {
            string cmdTxt = "SELECT students.id, login, password, userType, name, surname, phoneNumber, checkInDate, studak, gender, roomNumber, linen_id, items_id " +
                            "FROM students INNER JOIN users ON users.id = students.id " +
                            "WHERE name = @name AND surname = @surname AND roomNumber = @room";

            var parameters = new Dictionary<string, object>
            {
                { "name", name },
                { "surname", surname },
                { "room", room }
            };

            var tmp = _connection.ExecuteSelect(cmdTxt, parameters);

            if (tmp.Count != 0)
            {
                var studentDTO = new StudentDTOAdd
                {
                    Id = tmp[0][0] != null ? int.Parse(tmp[0][0].ToString()) : 0,
                    Login = tmp[0][1] as string,
                    Password = tmp[0][2] as string,
                    UserType = tmp[0][3] as string,
                    Name = tmp[0][4] as string,
                    Surname = tmp[0][5] as string,
                    PhoneNumber = tmp[0][6] as string,
                    CheckInDate = tmp[0][7] as string,
                    Studak = tmp[0][8] as string,
                    Gender = tmp[0][9] as string,
                    RoomNumber = tmp[0][10] != null ? tmp[0][10].ToString() : "Не назначена",
                    LinenId = tmp[0][11] != null ? int.Parse(tmp[0][11].ToString()) : 0,
                    ItemsId = tmp[0][12] != null ? int.Parse(tmp[0][12].ToString()) : 0
                };

                return new Student(studentDTO);
            }
            else
            {
                return null;
            }
        }

        public int InsertStudentData(string name, string surname, string phoneNumber, string studak, string gender)
        {
            string cmdTxt = "INSERT INTO students (name, surname, phoneNumber, checkInDate, studak, gender) " +
                            "VALUES (@name, @surname, @phoneNumber, GETDATE(), @studak, @gender)";

            var parameters = new Dictionary<string, object>
            {
                { "name", name },
                { "surname", surname },
                { "phoneNumber", phoneNumber },
                { "studak", studak },
                { "gender", gender }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int UpdateStudentData(Student updatedStudent, string id)
        {
            string cmdTxt = "UPDATE students " +
                            "SET name = @name, " +
                            "surname = @surname, " +
                            "phoneNumber = @phoneNumber " +
                            "WHERE id = @id";

            var parameters = new Dictionary<string, object>
            {
                { "name", updatedStudent.Name },
                { "surname", updatedStudent.Surname },
                { "phoneNumber", updatedStudent.PhoneNumber },
                { "id", id }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int UpdateRoomNumber(string userID, string newRoom)
        {
            string cmdTxt = "UPDATE students " +
                            "SET roomNumber = @newRoom " +
                            "WHERE id = @userID";

            var parameters = new Dictionary<string, object>
            {
                { "newRoom", newRoom },
                { "userID", userID }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }
    }
}