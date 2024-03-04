namespace PaperKiller.Models.linen
{
    public interface ILinen
    {
        public int? BedsheetID { get; set; }
        public int? PillowcaseID { get; set; }
        public int? DuvetID { get; set; }
        public int? BedspreadID { get; set; }
        public int? TowelID { get; set; }


        public string? SerialNumber { get; set; }
        public string? IsGiven { get; set; }
    }
}
