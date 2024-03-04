using PaperKiller.Models.items;
using PaperKiller.Models.linen;
using static PaperKiller.Utils.Constants;

namespace PaperKiller.Services
{
    public interface IExchangeService
    {
        public ExchangeResult GiveItem<T>(T item, string itemName, string UserID) where T : class;
        public ExchangeResult PassItem<T>(T item, string itemName, string UserID) where T : class;
        public ExchangeResult ExchangeItem<T>(T item, string itemName, string UserID) where T : class;
        public InputError AddItems(string itemName, string serialNumber);
    }
}
