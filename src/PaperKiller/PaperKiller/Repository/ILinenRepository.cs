using PaperKiller.Models.linen;

namespace PaperKiller.Repository
{
    public interface ILinenRepository
    {
        public ILinen? GetLinensByID(string userID);
        public T? GetLinenByID<T>(string linenID) where T : LinenBase, new();
        int InsertLinenReport(string name, string surname, string room, string item);
        int NewLinen(string item_id, string newItemID, string linenID);
    }
}
