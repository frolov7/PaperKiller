using PaperKiller.Models.items;

namespace PaperKiller.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly Connector _connection;

        public ItemRepository(Connector connection)
        {
            _connection = connection;
        }
        // Получить предметы у студента
        public IItems? GetItemsByID(string userID)
        {
            string cmdTxt = "SELECT items.id, chair_id, tables_id, shelf_id, wardrobe_id, items.linen_id " +
                            "FROM students INNER JOIN items ON items.id = students.items_id " +
                            "where students.id = @userID";

            var parameters = new Dictionary<string, object>
            {
                { "userID", userID }
            };

            var result = _connection.ExecuteSelect(cmdTxt, parameters);

            if (result.Count != 0)
            {
                var itemData = result[0];
                var chairID = itemData[1] as int?;
                var tablesID = itemData[2] as int?;
                var shelfID = itemData[3] as int?;
                var wardrobeID = itemData[4] as int?;
                var linenID = itemData[5] as int?;

                return new Items(chairID, tablesID, shelfID, wardrobeID, linenID);
            }
            else
            {
                return null;
            }
        }

        public T? GetItemByID<T>(string itemID) where T : ItemBase, new()
        {
            string tablesName = typeof(T).Name; // Получить имя таблицы из имени класса
            string cmdTxt = $"SELECT {tablesName}.id, {tablesName}.serialNumber, {tablesName}.isGiven " +
                            $"FROM items INNER JOIN {tablesName} ON {tablesName}.id = items.{tablesName}_id " +
                            $"where items.id = @itemID";

            var parameters = new Dictionary<string, object>
            {
                { "itemID", itemID }
            };

            var result = _connection.ExecuteSelect(cmdTxt, parameters);

            if (result.Count != 0)
            {
                return new T
                {
                    SerialNumber = result[0][1] as string,
                    IsGiven = result[0][2] as string
                };
            }
            else
            {
                return null;
            }
        }


        // Вернуть предмет на склад
        public int ReturnItemBack(string tableName, string item, string itemID)
        {
            string cmdTxt = $"UPDATE {tableName} " +
                            $"SET {item} = NULL " +
                            $"WHERE id = @itemID";

            var parameters = new Dictionary<string, object>
            {
                { "itemID", itemID }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int NewItem(string item_id, string newItemID, string userID)
        {
            string cmdTxt = $"update items " +
                            $"set {item_id} = @newItemID " +
                            $"where id = @userID";

            var parameters = new Dictionary<string, object>
            {
                { "newItemID", newItemID },
                { "userID", userID }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

        public int ChangeItemStatus(string tablesName, string itemStatus, string itemID)
        {
            string cmdTxt = $"update {tablesName} " +
                            $"set isGiven = @itemStatus " +
                            $"where id = @itemID";

            var parameters = new Dictionary<string, object>
            {
                { "itemStatus", itemStatus },
                { "itemID", itemID }
            };

            return _connection.ExecuteNonQuery(cmdTxt, parameters);
        }

    }
}
