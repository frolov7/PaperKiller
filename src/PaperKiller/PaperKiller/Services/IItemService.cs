using PaperKiller.DTO;
using PaperKiller.Models;
using PaperKiller.Models.linen;
using static PaperKiller.Utils.Constants;

namespace PaperKiller.Services
{
    public interface IItemService
    {
        public T? GetLinenByID<T>(string linenID) where T : LinenBase, new();
    }
}
