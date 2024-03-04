namespace PaperKiller.Models.linen
{
    public class Bedspread : LinenBase
    {
        public Bedspread() { }

        public Bedspread(int? bedspreadID, string serialNumber, bool change, string status)
            : base(null, null, null, bedspreadID, null, serialNumber, change, status)
        {
        }
    }
}
