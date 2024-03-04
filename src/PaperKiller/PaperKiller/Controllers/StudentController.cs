using Microsoft.AspNetCore.Mvc;
using PaperKiller.Repository;
using Swashbuckle.AspNetCore.Annotations;
using PaperKiller.Services;
using static PaperKiller.Utils.Constants;
using PaperKiller.DTO;
using PaperKiller.Models;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace PaperKiller.Controllers
{
    [Route("/api/v1")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository, IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _userService = userService;

            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Изменить данные пользователя
        /// </summary>
        [HttpPut("{userID}")]
        [SwaggerResponse(200, "Данные пользователя успешно обновлены")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(401, "Пользователь не авторизован")]
        [SwaggerResponse(404, "Пользователь не найден")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> UpdateStudentData(string userID, [FromBody] StudentChangeDataDTO student)
        {
            try
            {
                Student studentEntity = _mapper.Map<Student>(student);
                int result = _studentRepository.UpdateStudentData(studentEntity, userID);

                if (result > 0)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 200,
                        ErrorMessage = "Данные студента успешно обновлены"
                    };
                    return Ok(responseDTO);
                }
                else if (result == 0)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "Пользователь не найден"
                    };
                    return BadRequest(responseDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = $"Ошибка в запросе"
                    };
                    return NotFound(responseDTO);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server error: {Message}", ex.Message);
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = "Ошибка на стороне сервера"
                };
                return StatusCode(500, responseDTO);
            }
        }

        /// <summary>
        /// Съехать из общежития
        /// </summary>
        [HttpDelete("moveout/{userID}")]
        [SwaggerResponse(200, "Пользователь успешно выселен")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(404, "Пользователь не найден")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> MoveOut(string userID)
        {
            try
            {
                if (string.IsNullOrEmpty(userID))
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "Пользователь не найден"
                    };
                    return BadRequest(responseDTO);
                }

                RoomStatus status = _userService.MoveOutService(userID);

                if (status == RoomStatus.EVICTED)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 200,
                        ErrorMessage = "Пользователь успешно съехал"
                    };
                    return Ok(responseDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = $"Ошибка в запросе"
                    };
                    return NotFound(responseDTO);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server error: {Message}", ex.Message);
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = "Ошибка на стороне сервера"
                };
                return StatusCode(500, responseDTO);
            }
        }

        /// <summary>
        /// Получить список вещей студента
        /// </summary>
        [HttpGet("myitems/{userID}")]
        [SwaggerResponse(200, "Данные получены")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(404, "Пользователь не найден")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> ShowMyItems(string userID)
        {
            try
            {
                if (string.IsNullOrEmpty(userID))
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "Пользователь не найден"
                    };
                    return BadRequest(responseDTO);
                }

                List<MyItemsDTO> itemsList = new List<MyItemsDTO>();

                var queryResult = _userService.ShowMyItems(userID);

                foreach (var row in queryResult)
                {
                    MyItemsDTO items = new MyItemsDTO
                    {
                        ChairSerialNumber = row.ChairSerialNumber,
                        TablesSerialNumber = row.TablesSerialNumber,
                        ShelfSerialNumber = row.ShelfSerialNumber,
                        WardrobeSerialNumber = row.WardrobeSerialNumber,
                        BedsheetSerialNumber = row.BedsheetSerialNumber,
                        PillowcaseSerialNumber = row.PillowcaseSerialNumber,
                        DuvetSerialNumber = row.DuvetSerialNumber,
                        BedspreadSerialNumber = row.BedspreadSerialNumber,
                        TowelSerialNumber = row.TowelSerialNumber
                    };

                    itemsList.Add(items);
                }

                if (itemsList.Count == 0)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "Пользователь не найден"
                    };
                    return BadRequest(responseDTO);
                }

                return Ok(itemsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server error: {Message}", ex.Message);
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = "Ошибка на стороне сервера"
                };
                return StatusCode(500, responseDTO);
            }
        }

        [HttpGet("showmydata/{userID}")]
        [SwaggerResponse(200, "Данные получены")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(404, "Пользователь не найден")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<StudentDTO> ShowMyData(string userID)
        {
            try
            {
                if (string.IsNullOrEmpty(userID))
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "1 не найден"
                    };
                    return BadRequest(responseDTO);
                }

                Student student = _userService.ShowMyData(userID);

                if (student == null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "2 не найден"
                    };
                    return BadRequest(responseDTO);
                }

                StudentDTO studentDTO = new StudentDTO
                {
                    Id = student.UserId,
                    Name = student.Name,
                    Surname = student.Surname,
                    PhoneNumber = student.PhoneNumber,
                    CheckInDate = student.CheckInDate, // Предполагается, что CheckInDate является DateTime
                    Studak = student.StudentId,
                    Gender = student.Gender,
                    RoomNumber = student.RoomNumber,
                    LinenId = student.LinenId,
                    ItemsId = student.ItemsId
                };

                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server error: {Message}", ex.Message);
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = "Ошибка на стороне сервера"
                };
                return StatusCode(500, responseDTO);
            }
        }
    }
}
