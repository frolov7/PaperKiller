using PaperKiller.Models.items;
using PaperKiller.Models.linen;

namespace PaperKiller.Repository
{
    public class LinenRepository : ILinenRepository
    {
        private readonly Connector _connection;

        public LinenRepository(Connector connection)
        {
            _connection = connection;
        }

        public ILinen? GetLinensByID(string userID)
        {
            string cmdTxt = "SELECT linen.id, bedsheet_id, pillowcase_id, duvet_id, bedspread_id, towel_id " +

                            "FROM students INNER JOIN linen ON linen.id = students.linen_id " +
                            "WHERE students.id = @userID";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "userID", userID }
            };

            var tmp = _connection.ExecuteSelect(cmdTxt, parameters);

            if (tmp.Count != 0)
            {
                var itemData = tmp[0];
                var bedsheetID = itemData[1] as int?;
                var pillowcaseID = itemData[2] as int?;
                var duvetID = itemData[3] as int?;
                var bedspreadID = itemData[4] as int?;
                var towelID = itemData[5] as int?;

                return new Linen(bedsheetID, pillowcaseID, duvetID, bedspreadID, towelID);
            }
            else
            {
                return null;
            }
        }

        public T? GetLinenByID<T>(string linenID) where T : LinenBase, new()
        {
            string tableName = typeof(T).Name; // Получить имя таблицы из имени класса
            string cmdTxt = $"SELECT {tableName}.id, {tableName}.serialNumber, {tableName}.isGiven " +
                            $"FROM items INNER JOIN {tableName} ON {tableName}.id = items.{tableName}_id " +
                            "WHERE linen.id = @linenID";

            var parameters = new Dictionary<string, object>
            {
                { "linenID", linenID }
            };

            var tmp = _connection.ExecuteSelect(cmdTxt, parameters);

            if (tmp.Count != 0)
            {
                return new T
                {
                    SerialNumber = tmp[0][1] as string,
                    IsGiven = tmp[0][2] as string
                };
            }
            else
            {
                return null;
            }
        }

        public int InsertLinenReport(string name, string surname, string room, string item)
        {
            string cmdTxt = "INSERT INTO report (name, surname, DateChange, roomNumber, itemType) " +
                            "VALUES (@name, @surname, GETDATE(), @room, @item)";

            var parameters = new Dictionary<string, object>
            {
                { "name", name },
                { "surname", surname },
                { "room", room },
                { "item", item }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int NewLinen(string item_id, string newItemID, string linenID)
        {
            string cmdTxt = "UPDATE linen " +
                            $"SET {item_id} = @newItemID " +
                            "WHERE id = @linenID";

            var parameters = new Dictionary<string, object>
            {
                { "newItemID", newItemID },
                { "linenID", linenID }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }
    }
}
