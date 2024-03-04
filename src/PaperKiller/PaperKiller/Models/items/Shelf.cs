namespace PaperKiller.Models.items
{
    public class Shelf : ItemBase
    {
        public Shelf() { }

        public Shelf(int? shelfID, string serialNumber, string status)
            : base(null, null, shelfID, null, serialNumber, status)
        {
        }
    }

}
