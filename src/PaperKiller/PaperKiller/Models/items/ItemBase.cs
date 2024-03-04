namespace PaperKiller.Models.items
{
    // Базовый класс для всех предметов

    public abstract class ItemBase : IItems
    {
        public int? ChairID { get; set; }
        public int? TablesID { get; set; }
        public int? ShelfID { get; set; }
        public int? WardrobeID { get; set; }
        public int? LinenID { get; set; }
        public string? SerialNumber { get; set; }
        public string? IsGiven { get; set; }

        public ItemBase()
        {
            ChairID = null;
            TablesID = null;
            ShelfID = null;
            WardrobeID = null;
            LinenID = null;
            SerialNumber = null;
            IsGiven = "На складе";
        }

        protected ItemBase(int? chairID, int? tablesID, int? shelfID, int? wardrobeID, string serialNumber, string status)
        {
            ChairID = chairID;
            TablesID = tablesID;
            ShelfID = shelfID;
            WardrobeID = wardrobeID;
            SerialNumber = serialNumber;
            if (string.IsNullOrWhiteSpace(status))
                IsGiven = "На складе";
            else
                IsGiven = "У студента";
        }
    }
}
