namespace PaperKiller.Models.items
{
    public class Wardrobe : ItemBase
    {
        public Wardrobe() { }
        public Wardrobe(int? wardrobeID, string serialNumber, string status)
            : base(null, null, null, wardrobeID, serialNumber, status)
        {
        }
    }
}
