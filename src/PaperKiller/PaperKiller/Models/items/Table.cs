namespace PaperKiller.Models.items
{
    public class Tables : ItemBase
    {
        public Tables() { }
        public Tables(int? tablesID, string serialNumber, string status)
            : base(null, tablesID, null, null, serialNumber, status)
        {
        }
    }
}
