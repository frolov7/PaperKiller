namespace PaperKiller.Models.items
{
    public class Chair : ItemBase
    {
        public Chair() { }

        public Chair(int? chairID, string serialNumber, string status)
            : base(chairID, null, null, null, serialNumber, status)
        {
        }

    }
}
