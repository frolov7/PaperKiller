using System.Data.SqlClient;

namespace PaperKiller.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly Connector _connection;

        public ExchangeRepository(Connector connection)
        {
            _connection = connection;
        }
        // Получить предмет со склада. Вернет ID предмета со склада
        public int? GetFreeItem(string tableName)
        {
            List<int> indexes = new List<int>();

            string cmdTxt = $"SELECT id FROM {tableName} WHERE isGiven = @isGiven";

            var parameters = new Dictionary<string, object>
            {
                { "isGiven", "На складе" }
            };

            var result = _connection.ExecuteSelect(cmdTxt, parameters);

            foreach (var row in result)
            {
                indexes.Add((int)row[0]);
            }

            if (indexes.Count == 0)
            {
                return null;
            }

            return indexes[new Random().Next(indexes.Count)];
        }


        // Добавить предмет на склад
        public int InsertItemStorage(string tablesName, string sNumber)
        {
            string cmdTxt = $"insert into {tablesName} (serialNumber, isGiven) values(@sNumber, 'На складе')";

            var parameters = new Dictionary<string, object>
            {
                { "sNumber", sNumber }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int UpLinenID()
        {
            string cmdTxt = "update students " +
                            "set linen_id = (select count(linen_id) from students) + 1 " +
                            "where id = (select count(id) from students)";

            return _connection.ExecuteNonQuery(cmdTxt);
        }

        public int UpItemID()
        {
            string cmdTxt = "update students " +
                            "set items_id = (select count(items_id) from students) + 1 " +
                            "where id = (select count(id) from students)";

            return _connection.ExecuteNonQuery(cmdTxt);
        }
    }
}
