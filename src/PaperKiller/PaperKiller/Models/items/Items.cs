namespace PaperKiller.Models.items
{
    public class Items : IItems
    {
        public int? ChairID { get; set; }
        public int? TablesID { get; set; }
        public int? ShelfID { get; set; }
        public int? WardrobeID { get; set; }
        public int? LinenID { get; set; }


        public string? SerialNumber { get; set; }
        public string? IsGiven { get; set; }

        public Items() { }

        public Items(int? chairID, int? tablesID, int? shelfID, int? wardrobeID, int? linenID)
        {
            ChairID = chairID;
            TablesID = tablesID;
            ShelfID = shelfID;
            WardrobeID = wardrobeID;
            LinenID = linenID;
        }

        public Items(string serialNumber, string status)
        {
            SerialNumber = serialNumber;
            if (string.IsNullOrWhiteSpace(status))
                IsGiven = "На складе";
            else
                IsGiven = "У студента";
        }
    }
}
