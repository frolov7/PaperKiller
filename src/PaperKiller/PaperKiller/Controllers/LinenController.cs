using Microsoft.AspNetCore.Mvc;
using PaperKiller.DTO;
using PaperKiller.Models.items;
using PaperKiller.Repository;
using PaperKiller.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using static PaperKiller.Utils.Constants;

namespace PaperKiller.Controllers
{
    [Route("/api/v1")]
    [ApiController]
    public class LinenController : ControllerBase
    {
        private readonly ILinenRepository _linenRepository;
        private readonly IExchangeService _exchangeService;
        private readonly IStudentRepository _studentRepository;

        private readonly ILogger<UserController> _logger;

        public LinenController(ILinenRepository linenRepository, IExchangeService exchangeService, IStudentRepository studentRepository, ILogger<UserController> logger)
        {
            _linenRepository = linenRepository;
            _exchangeService = exchangeService;
            _studentRepository = studentRepository;

            _logger = logger;
        }

        /// <summary>
        /// Обменять постель студенту
        /// </summary>
        [HttpPost("exchange/{userID}/linen/{linenname}")]
        [SwaggerResponse(200, "Предмет успешно обменян")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(404, "Пользователь не имеет белья для обмена")]
        [SwaggerResponse(422, "На складе нет свободного белья для обмена")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> ExchangeLinen(string userID, string linenname)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(linenname))
            {
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 400,
                    ErrorMessage = "Пользователь/предмет не найден"
                };
                return BadRequest(responseDTO);
            }
            try
            {
                var student = _studentRepository.GetStudentByID(userID);
                if (student == null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = $"Ошибка в запросе"
                    };
                    return NotFound(responseDTO);
                }

                var linen = _linenRepository.GetLinensByID(userID);

                if (linen == null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Пользователь не имеет белья для обмена"
                    };
                    return NotFound(responseDTO);
                }

                var exchangeResult = _exchangeService.ExchangeItem(linen, linenname, userID);

                if (exchangeResult == ExchangeResult.SUCCESS)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 200,
                        ErrorMessage = $"{linenname} успешно обменян(а)"
                    };
                    return Ok(responseDTO);
                }
                else if (exchangeResult == ExchangeResult.ItemNotFound)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Не найден {linenname} для обмена"
                    };
                    return NotFound(responseDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 422,
                        ErrorMessage = $"На складе нет свободного предмета для обмена"
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
        /// Сдать постель студенту
        /// </summary>
        [HttpPut("pass/{userID}/linen/{linenname}")]
        [SwaggerResponse(200, "Предмет успешно сдан")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(404, "Пользователь не имеет предмета для сдачи")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> PassLinen(string userID, string linenname)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(linenname))
            {
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 400,
                    ErrorMessage = "Пользователь/предмет не найден"
                };
                return BadRequest(responseDTO);
            }
            try
            {
                var student = _studentRepository.GetStudentByID(userID);
                if (student == null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = $"Ошибка в запросе"
                    };
                    return NotFound(responseDTO);
                }

                var linen = _linenRepository.GetLinensByID(userID);

                if (linen == null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Пользователь не имеет {linenname} для сдачи"
                    };
                    return NotFound(responseDTO);
                }

                var passResult = _exchangeService.PassItem(linen, linenname, userID);

                if (passResult == ExchangeResult.SUCCESS)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 200,
                        ErrorMessage = $"{linenname} успешно сдан(а)"
                    };
                    return Ok(responseDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Пользователь не имеет {linenname} для сдачи"
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
        /// Выдать предмет студенту
        /// </summary>
        [HttpPut("give/{userID}/linen/{linenname}")]
        [SwaggerResponse(200, "Предмет успешно выдан")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(404, "Пользователь уже имеет этот предмет")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> GiveLinen(string userID, string linenname)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(linenname))
            {
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 400,
                    ErrorMessage = "Пользователь/предмет не найден"
                };
                return NotFound(responseDTO);
            }

            try
            {
                var student = _studentRepository.GetStudentByID(userID);
                if (student == null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = $"Ошибка в запросе"
                    };
                    return NotFound(responseDTO);
                }

                var linen = _linenRepository.GetLinensByID(userID);

                if (linen != null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "Пользователь уже имеет этот предмет"
                    };
                    return NotFound(responseDTO);
                }

                var giveResult = _exchangeService.GiveItem(linen, linenname, userID);

                if (giveResult == ExchangeResult.SUCCESS)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 200,
                        ErrorMessage = $"{linenname} успешно выдан(а)"
                    };
                    return Ok(responseDTO);
                }
                else if (giveResult == ExchangeResult.ItemNotFound)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Не найден {linenname} для выдачи"
                    };
                    return NotFound(responseDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Пользователь уже имеет {linenname}"
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
