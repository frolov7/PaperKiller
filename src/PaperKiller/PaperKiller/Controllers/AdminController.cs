using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using PaperKiller.Services;
using static PaperKiller.Utils.Constants;
using PaperKiller.DTO;
using PaperKiller.Models.items;
using System.Net;
using PaperKiller.Models;
//using Microsoft.AspNetCore.Authorization;

namespace PaperKiller.Controllers
{
    [Route("/api/v1")]
    [ApiController]
    /// <summary>
    /// Администратор
    /// </summary>
    public class AdminController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;
        private readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;

        public AdminController(IExchangeService exchangeService, IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _exchangeService = exchangeService;

            _logger = logger;
        }

        /// <summary>
        /// Получить журнал обмена вещей 
        /// </summary>
        [HttpGet("report")]
        [SwaggerResponse(200, "Данные получены")]
        [SwaggerResponse(404, "Данные не найдены")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> ShowReport()
        {
            try
            {
                List<ReportDTO> reportData = new List<ReportDTO>();

                var report = _userService.ShowReport();

                foreach (var row in report)
                {
                    ReportDTO item = new ReportDTO
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Surname = row.Surname,
                        DateChange = row.DateChange,
                        RoomNumber = row.RoomNumber,
                        ItemType = row.ItemType
                    };

                    reportData.Add(item);
                }

                if (reportData.Count != 0)
                {
                    return Ok(reportData);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "Данные не найдены"
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
        /// Получить список студентов
        /// </summary>
        [HttpGet("students")]
        [SwaggerResponse(200, "Данные получены")]
        [SwaggerResponse(404, "Данные не найдены")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> ShowStudents()
        {
            try
            {
                List<StudentDTO> studentData = new List<StudentDTO>();

                var queryResult = _userService.ShowStudent();

                foreach (var row in queryResult)
                {
#pragma warning disable CS8629 // Тип значения, допускающего NULL, может быть NULL.
                    StudentDTO student = new StudentDTO
                    {
                        Id = (int)row.UserId,
                        Name = row.Name,
                        Surname = row.Surname,
                        PhoneNumber = row.PhoneNumber,
                        CheckInDate = row.CheckInDate,
                        Studak = row.StudentId,
                        Gender = row.Gender,
                        RoomNumber = row.RoomNumber,
                        LinenId = row.LinenId,
                        ItemsId = row.ItemsId
                    };
#pragma warning restore CS8629 // Тип значения, допускающего NULL, может быть NULL.

                    studentData.Add(student);
                }

                if (studentData.Count != 0)
                {
                    return Ok(studentData);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "Данные не найдены"
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
        /// Получить список вещей на складе
        /// </summary>
        [HttpGet("items")]
        [SwaggerResponse(200, "Данные получены")]
        [SwaggerResponse(404, "Данные не найдены")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> ShowItems()
        {
            try
            {
                List<ItemCredentialsDTO> itemsList = new List<ItemCredentialsDTO>();

                var queryResult = _userService.ShowItems();

                foreach (var row in queryResult)
                {
                    ItemCredentialsDTO items = new ItemCredentialsDTO
                    {
                        ItemName = row.ItemName,
                        SerialNumber = row.SerialNumber,
                    };

                    itemsList.Add(items);
                }

                if (itemsList.Count != 0)
                {
                    return Ok(itemsList);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "Данные не найдены"
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
        /// Выселить студента
        /// </summary>
        [HttpDelete("students/evictStudent/{userID}")]
        [SwaggerResponse(200, "Пользователь успешно выселен")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(401, "Пользователь не авторизован")]
        [SwaggerResponse(404, "Пользователь не найден")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> EvictStudent(string userID)
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
                        ErrorMessage = "Пользователь успешно выселен"
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
        /// Добавить предмет на склад
        /// </summary>
        [HttpPost("storage")]
        [SwaggerResponse(200, "Предмет успешно добавлен")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(404, "Пользователь уже имеет этот предмет")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> AddItemsToStorage([FromBody] ItemCredentialsDTO item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.ItemName))
                    return BadRequest(new { Error = "Ошибка в запросе" });

                string itemClassName = "PaperKiller.Models.items." + item.ItemName;
                string linenClassName = "PaperKiller.Models.linen." + item.ItemName;

                if (Type.GetType(linenClassName) != null || Type.GetType(itemClassName) != null)
                {
                    InputError rc = _exchangeService.AddItems(item.ItemName, item.SerialNumber);

                    if (rc == InputError.FieldERROR)
                    {
                        var responseDTO = new ErrorResponseDTO
                        {
                            ErrorCode = 400,
                            ErrorMessage = $"Ошибка в запросе"
                        };
                        return NotFound(responseDTO);
                    }
                    else
                    {
                        var responseDTO = new ErrorResponseDTO
                        {
                            ErrorCode = 200,
                            ErrorMessage = $"{item.ItemName} успешно добавлен"
                        };
                        return Ok(responseDTO);
                    }
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = "Ошибка в запросе"
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
    }
}
