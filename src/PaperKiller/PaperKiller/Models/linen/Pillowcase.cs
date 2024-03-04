namespace PaperKiller.Models.linen
{
    public class Pillowcase : LinenBase
    {
        public Pillowcase() { }

        public Pillowcase(int? pillowcaseID, string serialNumber, bool change, string status)
            : base(null, pillowcaseID, null, null, null, serialNumber, change, status)
        {
        }
    }
}
