using PaperKiller.Repository;
using PaperKiller.Models;
using static PaperKiller.Utils.Constants;
using PaperKiller.DTO;
using PaperKiller.Models.items;
using PaperKiller.Models.linen;

namespace PaperKiller.Services
{
    public class UserService : IUserService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILinenRepository _linenRepository;

        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IItemRepository itemRepository, ILinenRepository linenRepository, IStudentRepository studentRepository, IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _linenRepository = linenRepository;

            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }

        private void ReturnItemsToStock(string StudentID)
        {
            string[] itemTypes = { "chair", "tables", "shelf", "wardrobe" };
            string[] linenTypes = { "bedsheet", "pillowcase", "duvet", "bedspread", "towel" };

            foreach (var itemType in itemTypes)
            {
                _itemRepository.ReturnItemBack("items", $"{itemType}_id", StudentID.ToString());
                _itemRepository.ChangeItemStatus(itemType, "На складе", StudentID.ToString());
            }

            foreach (var linenType in linenTypes)
            {
                _itemRepository.ReturnItemBack("linen", $"{linenType}_id", StudentID.ToString());
                _itemRepository.ChangeItemStatus(linenType, "На складе", StudentID.ToString());
            }
        }

        public RoomStatus MoveOutService(string studentID)
        {
            Student student = _studentRepository.GetStudentByID(studentID);

            if (student == null)
            {
                return RoomStatus.NOT_ASSIGNED;
            }
            else
            {
                if (student.RoomNumber == "Не назначена")
                {
                    return RoomStatus.NOT_ASSIGNED;
                }
                else
                {
                    _userRepository.UpdateUserType(studentID, "O");
                    _studentRepository.UpdateRoomNumber(studentID, "Выселен");

                    ReturnItemsToStock(studentID);

                    return RoomStatus.EVICTED;
                }
            }
        }

        public List<Report> ShowReport()
        {
            return _userRepository.GetReport();
        }

        public List<Student> ShowStudent()
        {
            return _userRepository.GetStudents();
        }

        public List<MyItems> ShowMyItems(string userID)
        {
            return _userRepository.GetMyItems(userID); 
        }

        public Student ShowMyData(string userID)
        {
            return _userRepository.GetMyData(userID);
        }

        public List<ItemCredentials> ShowItems()
        {
            return _userRepository.GetItems();
        }
    }
}