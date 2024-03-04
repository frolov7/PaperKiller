namespace PaperKiller.Models.linen
{
    // Базовый класс для всех предметов

    public abstract class LinenBase : ILinen
    {
        public int? BedsheetID { get; set; }
        public int? PillowcaseID { get; set; }
        public int? DuvetID { get; set; }
        public int? BedspreadID { get; set; }
        public int? TowelID { get; set; }


        public string? SerialNumber { get; set; }
        public string? IsGiven { get; set; }
        public bool Change { get; set; }

        public LinenBase()
        {
            BedsheetID = null;
            PillowcaseID = null;
            DuvetID = null;
            BedspreadID = null;
            TowelID = null;
            SerialNumber = null;
            Change = false;
            IsGiven = "На складе";
        }

        protected LinenBase(int? bedsheetID, int? pillowcaseID, int? duvetID, int? bedspreadID, int? towelID, string serialNumber, bool change, string status)
        {
            BedsheetID = bedsheetID;
            PillowcaseID = pillowcaseID;
            DuvetID = duvetID;
            BedspreadID = bedspreadID;
            TowelID = towelID;
            SerialNumber = serialNumber;
            Change = change;
            if (string.IsNullOrWhiteSpace(status))
                IsGiven = "На складе";
            else
                IsGiven = "У студента";
        }
    }
}
