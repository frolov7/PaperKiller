namespace PaperKiller.Models.linen
{
    public class Duvet : LinenBase
    {
        public Duvet() { }

        public Duvet(int? duvetID, string serialNumber, bool change, string status)
            : base(null, null, duvetID, null, null, serialNumber, change, status)
        {
        }
    }
}
