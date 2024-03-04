namespace PaperKiller.Models.linen
{
    public class Bedsheet : LinenBase
    {
        public Bedsheet() { }

        public Bedsheet(int? bedsheetID, string serialNumber, bool change, string status)
            : base(bedsheetID, null, null, null, null, serialNumber, change, status)
        {
        }
    }
}
