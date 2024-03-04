using PaperKiller.Models.items;

namespace PaperKiller.Repository
{
    public interface IItemRepository
    {
        public IItems? GetItemsByID(string userID);
        public T? GetItemByID<T>(string itemID) where T : ItemBase, new();
        public int ReturnItemBack(string tableName, string item, string itemID);
        int NewItem(string item_id, string newItemID, string userID);
        int ChangeItemStatus(string tablesName, string itemStatus, string itemID);
    }
}
