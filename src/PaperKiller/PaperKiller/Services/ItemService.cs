using PaperKiller.Repository;
using PaperKiller.Models;
using static PaperKiller.Utils.Constants;
using PaperKiller.DTO;
using PaperKiller.Models.items;
using PaperKiller.Models.linen;

namespace PaperKiller.Services
{
    public class ItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILinenRepository _linenRepository;

        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;

        public ItemService(IItemRepository itemRepository, ILinenRepository linenRepository, IStudentRepository studentRepository, IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _linenRepository = linenRepository;

            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }

        public T? GetLinenByID<T>(string linenID) where T : LinenBase, new()
        {
            return _linenRepository.GetLinenByID<T>(linenID);
        }

    }
}