using PaperKiller.DTO;
using PaperKiller.Models;
using PaperKiller.Models.items;
using PaperKiller.Models.linen;

namespace PaperKiller.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Connector _connection;

        public UserRepository(Connector connection)
        {
            _connection = connection;
        }

        public bool IsLoginExists(string login)
        {
            string cmdTxt = "SELECT * FROM users WHERE login = @login";

            var parameters = new Dictionary<string, object>
            {
                { "login", login }
            };

            var tmp = _connection.ExecuteSelect(cmdTxt, parameters);

            return tmp.Count == 0 ? false : true;
        }

        public bool IsUserExists(string studak)
        {
            string cmdTxt = "SELECT * FROM users WHERE login = @studak";

            var parameters = new Dictionary<string, object>
            {
                { "studak", studak }
            };

            var tmp = _connection.ExecuteSelect(cmdTxt, parameters);

            return tmp.Count == 0 ? false : true;
        }

        public int DeleteUserByID(string userID)
        {
            string cmdTxt = "DELETE FROM users WHERE id = @userID";

            var parameters = new Dictionary<string, object>
            {
                { "userID", userID }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int InsertUser(string login, string password)
        {
            string cmdTxt = "INSERT INTO users (login, password, userType) VALUES (@login, @password, 'S')";

            var parameters = new Dictionary<string, object>
            {
                { "login", login },
                { "password", password }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int UpdatePassword(string login, string newPassword)
        {
            string cmdTxt = "UPDATE users SET password = @newPassword WHERE login = @login";

            var parameters = new Dictionary<string, object>
            {
                { "newPassword", newPassword },
                { "login", login }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int UpdateUsersData(string oldLogin, string newLogin, string newPassword)
        {
            string cmdTxt = "UPDATE users SET login = @newLogin, password = @newPassword WHERE login = @oldLogin";

            var parameters = new Dictionary<string, object>
            {
                { "newLogin", newLogin },
                { "newPassword", newPassword },
                { "oldLogin", oldLogin }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int UpdateUserType(string userID, string newType)
        {
            string cmdTxt = "UPDATE users SET userType = @newType WHERE id = @userID";

            var parameters = new Dictionary<string, object>
            {
                { "newType", newType },
                { "userID", userID }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public List<Report> GetReport()
        {
            string cmdTxt = "SELECT id, name, surname, datechange, roomnumber, itemtype FROM report";

            var result = _connection.ExecuteSelect(cmdTxt);

            List<Report> report = new List<Report>();

            foreach (var row in result)
            {
                Report item = new Report
                {
                    Id = (int)row[0],
                    Name = (string)row[1],
                    Surname = (string)row[2],
                    DateChange = (string)row[3],
                    RoomNumber = (string)row[4],
                    ItemType = (string)row[5]
                };

                report.Add(item);
            }

            return report;
        }

        public List<Student> GetStudents()
        {
            string cmdTxt = @"
                SELECT
                    students.id,
                    students.name,
                    students.surname,
                    students.phoneNumber,
                    students.checkInDate,
                    students.studak,
                    students.gender,
                    students.roomNumber,
                    students.linen_id,
                    students.items_id,
                    users.login,
                    users.password,
                    users.userType
                FROM students
                INNER JOIN users ON students.id = users.id
            ";

            var result = _connection.ExecuteSelect(cmdTxt);

            List<Student> students = new List<Student>();

            foreach (var row in result)
            {
                Student student = new Student
                { 
                    UserId = (int)row[0],
                    Name = (string)row[1],
                    Surname = (string)row[2],
                    PhoneNumber = (string)row[3],
                    CheckInDate = (string)row[4],
                    StudentId = (string)row[5],
                    Gender = (string)row[6],
                    RoomNumber = (string)row[7],
                    LinenId = (int)row[8],
                    ItemsId = (int)row[9]
                };

                students.Add(student);
            }

            return students;
        }

        public List<ItemCredentials> GetItems()
        {
            string cmdTxt = @"
                SELECT 'bedsheet' AS TableName, serialNumber
                FROM bedsheet
                WHERE isGiven = 'На складе'
                UNION ALL
                SELECT 'pillowcase' AS TableName, serialNumber
                FROM pillowcase
                WHERE isGiven = 'На складе'
                UNION ALL
                SELECT 'duvet' AS TableName, serialNumber
                FROM duvet
                WHERE isGiven = 'На складе'
                UNION ALL
                SELECT 'bedspread' AS TableName, serialNumber
                FROM bedspread
                WHERE isGiven = 'На складе'
                UNION ALL
                SELECT 'towel' AS TableName, serialNumber
                FROM towel
                WHERE isGiven = 'На складе'
                UNION ALL
                SELECT 'wardrobe' AS TableName, serialNumber
                FROM wardrobe
                WHERE isGiven = 'На складе'
                UNION ALL
                SELECT 'tables' AS TableName, serialNumber
                FROM tables
                WHERE isGiven = 'На складе'
                UNION ALL
                SELECT 'chair' AS TableName, serialNumber
                FROM chair
                WHERE isGiven = 'На складе'
                UNION ALL
                SELECT 'shelf' AS TableName, serialNumber
                FROM shelf
                WHERE isGiven = 'На складе'
            ";

            var result = _connection.ExecuteSelect(cmdTxt);

            List<ItemCredentials> items = new List<ItemCredentials>();

            foreach (var row in result)
            {
                ItemCredentials item = new ItemCredentials
                {
                    ItemName = (string)row[0],
                    SerialNumber = (string)row[1]
                };

                items.Add(item);
            }

            return items;
        }

        public List<MyItems> GetMyItems(string userID)
        {
            var result = _connection.ExecuteSelect(
                "SELECT " +
                "   chair.serialNumber AS ChairSerialNumber, " +
                "   tables.serialNumber AS TablesSerialNumber, " +
                "   shelf.serialNumber AS ShelfSerialNumber, " +
                "   wardrobe.serialNumber AS WardrobeSerialNumber, " +
                "   bedsheet.serialNumber AS BedsheetSerialNumber, " +
                "   pillowcase.serialNumber AS PillowcaseSerialNumber, " +
                "   duvet.serialNumber AS DuvetSerialNumber, " +
                "   bedspread.serialNumber AS BedspreadSerialNumber, " +
                "   towel.serialNumber AS TowelSerialNumber " +
                "FROM " +
                "   items " +
                "LEFT JOIN chair ON items.chair_id = chair.id " +
                "LEFT JOIN tables ON items.tables_id = tables.id " +
                "LEFT JOIN shelf ON items.shelf_id = shelf.id " +
                "LEFT JOIN wardrobe ON items.wardrobe_id = wardrobe.id " +
                "LEFT JOIN linen ON items.linen_id = linen.id " +
                "LEFT JOIN bedsheet ON linen.bedsheet_id = bedsheet.id " +
                "LEFT JOIN pillowcase ON linen.pillowcase_id = pillowcase.id " +
                "LEFT JOIN duvet ON linen.duvet_id = duvet.id " +
                "LEFT JOIN bedspread ON linen.bedspread_id = bedspread.id " +
                "LEFT JOIN towel ON linen.towel_id = towel.id " +
                "WHERE " +
                "   items.id = @userID",
                new Dictionary<string, object> { { "userID", userID } }
            );


            List<MyItems> userItemsList = new List<MyItems>();

            foreach (var row in result)
            {
                MyItems userItems = new MyItems
                {
                    ChairSerialNumber = row[0] == DBNull.Value ? null : (string)row[0],
                    TablesSerialNumber = row[1] == DBNull.Value ? null : (string)row[1],
                    ShelfSerialNumber = row[2] == DBNull.Value ? null : (string)row[2],
                    WardrobeSerialNumber = row[3] == DBNull.Value ? null : (string)row[3],
                    BedsheetSerialNumber = row[4] == DBNull.Value ? null : (string)row[4],
                    PillowcaseSerialNumber = row[5] == DBNull.Value ? null : (string)row[5],
                    DuvetSerialNumber = row[6] == DBNull.Value ? null : (string)row[6],
                    BedspreadSerialNumber = row[7] == DBNull.Value ? null : (string)row[7],
                    TowelSerialNumber = row[8] == DBNull.Value ? null : (string)row[8],
                };

                userItemsList.Add(userItems);
            }

            return userItemsList;
        }
        public Student GetMyData(string userID)
        {
            string cmdTxt = @"
                SELECT
                    students.id,
                    students.name,
                    students.surname,
                    students.phoneNumber,
                    students.checkInDate,
                    students.studak,
                    students.gender,
                    students.roomNumber,
                    students.linen_id,
                    students.items_id,
                    users.login,
                    users.password,
                    users.userType
                FROM students
                INNER JOIN users ON students.id = users.id
                WHERE students.id = @userID
            ";

            var result = _connection.ExecuteSelect(cmdTxt, new Dictionary<string, object> { { "userID", userID } });

            if (result.Count == 0)
            {
                return null;
            }

            var row = result[0];
            Student student = new Student
            {
                UserId = (int)row[0],
                Name = (string)row[1],
                Surname = (string)row[2],
                PhoneNumber = (string)row[3],
                CheckInDate = (string)row[4],
                StudentId = (string)row[5],
                Gender = (string)row[6],
                RoomNumber = (string)row[7],
                LinenId = (int)row[8],
                ItemsId = (int)row[9]
            };

            return student;
        }
    }
}
