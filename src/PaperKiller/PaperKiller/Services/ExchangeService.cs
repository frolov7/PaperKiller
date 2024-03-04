using PaperKiller.Models.items;
using PaperKiller.Models.linen;
using PaperKiller.Repository;
using static PaperKiller.Utils.Constants;

namespace PaperKiller.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILinenRepository _linenRepository;
        private readonly IExchangeRepository _exchangeRepository;

        public ExchangeService(IItemRepository itemRepository, ILinenRepository linenRepository, IExchangeRepository exchangeRepository)
        {
            _itemRepository = itemRepository;
            _linenRepository = linenRepository;
            _exchangeRepository = exchangeRepository;
        }

        public ExchangeResult GiveItem<T>(T item, string itemName, string UserID) where T : class
        {
            var itemType = item.GetType();
            var itemIdProperty = itemType.GetProperty($"{itemName}ID");
            
            if (itemIdProperty != null)
            {
                int? freeItem = _exchangeRepository.GetFreeItem(itemName);

                if (freeItem == null) 
                { 
                    return ExchangeResult.ItemNotFound;
                }

                _itemRepository.ChangeItemStatus(itemName, "У студента", freeItem.ToString());

                if (typeof(T) == typeof(ILinen))
                    _linenRepository.NewLinen(itemName + "_id", freeItem.ToString(), UserID);
                else if (typeof(T) == typeof(IItems))
                    _itemRepository.NewItem(itemName + "_id", freeItem.ToString(), UserID);

                return ExchangeResult.SUCCESS;
            }

            return ExchangeResult.ItemNotFound;
        }

        public ExchangeResult PassItem<T>(T item, string itemName, string UserID) where T : class
        {
            var itemType = item.GetType();
            var itemIdProperty = itemType.GetProperty($"{itemName}ID");

            if (itemIdProperty != null)
            {
                var itemId = itemIdProperty.GetValue(item);

                if (itemId != null)
                {
                    if (typeof(T) == typeof(ILinen))
                        _itemRepository.ReturnItemBack("linen", itemName + "_id", UserID);
                    else if (typeof(T) == typeof(IItems))
                        _itemRepository.ReturnItemBack("items", itemName + "_id", UserID);

                    _itemRepository.ChangeItemStatus(itemName, "На складе", itemId.ToString());

                    return ExchangeResult.SUCCESS;
                }
            }
            return ExchangeResult.ItemNotFound;
        }

        public ExchangeResult ExchangeItem<T>(T item, string itemName, string UserID) where T : class
        {
            var itemType = item.GetType();
            var itemIdProperty = itemType.GetProperty($"{itemName}ID");

            if (itemIdProperty != null)
            {
                var itemId = itemIdProperty.GetValue(item);

                if (itemId != null)
                {
                    _itemRepository.ChangeItemStatus(itemName, "На складе", itemId.ToString());
                    int? newItemID = _exchangeRepository.GetFreeItem(itemName);

                    if (newItemID == null)
                    {
                        return ExchangeResult.ItemNotFound;
                    }

                    if (newItemID == -1)
                    {
                        return ExchangeResult.NoAvailableItem;
                    }

                    if (typeof(T) == typeof(ILinen))
                        _linenRepository.NewLinen(itemName + "_id", newItemID.ToString(), UserID);
                    else if (typeof(T) == typeof(IItems))
                        _itemRepository.NewItem(itemName + "_id", newItemID.ToString(), UserID);
                    
                    _itemRepository.ChangeItemStatus(itemName, "У студента", newItemID.ToString());

                    return ExchangeResult.SUCCESS;
                }
            }
            return ExchangeResult.ItemNotFound;
        }

        public InputError AddItems(string itemName, string serialNumber)
        {
            if (serialNumber.Length != 6)
                return InputError.FieldERROR;
            else
            {
                _exchangeRepository.InsertItemStorage(itemName, serialNumber);
                return InputError.SUCCESS;
            }
        }
    }
}