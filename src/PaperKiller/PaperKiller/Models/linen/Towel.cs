namespace PaperKiller.Models.linen
{
    public class Towel : LinenBase
    {
        public Towel() { }

        public Towel(int? towelID, string serialNumber, bool change, string status)
            : base(null, null, null, null, towelID, serialNumber, change, status)
        {
        }
    }
}
