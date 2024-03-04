namespace PaperKiller.Models.linen
{
    public class Linen : ILinen
    {
        public int? BedsheetID { get; set; }
        public int? PillowcaseID { get; set; }
        public int? DuvetID { get; set; }
        public int? BedspreadID { get; set; }
        public int? TowelID { get; set; }


        public string? SerialNumber { get; set; }
        public string? IsGiven { get; set; }

        public Linen(int? bedsheetID, int? pillowcaseID, int? duvetID, int? bedspreadID, int? towelID)
        {
            BedsheetID = bedsheetID;
            PillowcaseID = pillowcaseID;
            DuvetID = duvetID;
            BedspreadID = bedspreadID;
            TowelID = towelID;
        }

        public Linen(string serialNumber, string status)
        {
            SerialNumber = serialNumber;
            if (string.IsNullOrWhiteSpace(status))
                IsGiven = "На складе";
            else
                IsGiven = "У студента";
        }
    }
}
