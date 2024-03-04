namespace PaperKiller.Models.items
{
    public interface IItems
    {
        public int? ChairID { get; set; }
        public int? TablesID { get; set; }
        public int? ShelfID { get; set; }
        public int? WardrobeID { get; set; }
        public int? LinenID { get; set; }


        public string? SerialNumber { get; set; }
        public string? IsGiven { get; set; }
    }
}
