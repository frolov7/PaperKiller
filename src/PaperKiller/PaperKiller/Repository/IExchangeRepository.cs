namespace PaperKiller.Repository
{
    public interface IExchangeRepository
    {
        public int? GetFreeItem(string tableName);
        int InsertItemStorage(string tablesName, string sNumber);
        int UpLinenID();
        int UpItemID();
    }
}
