using Microsoft.AspNetCore.Mvc;
using PaperKiller.Repository;
using PaperKiller.Services;
using static PaperKiller.Utils.Constants;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using PaperKiller.Models.linen;
using PaperKiller.DTO;

namespace PaperKiller.Controllers
{
    [Route("/api/v1")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IExchangeService _exchangeService;
        private readonly IStudentRepository _studentRepository;

        private readonly ILogger<UserController> _logger;

        public ItemController(IItemRepository itemRepository, IExchangeService exchangeService, IStudentRepository studentRepository, ILogger<UserController> logger)
        {
            _itemRepository = itemRepository;
            _exchangeService = exchangeService;
            _studentRepository = studentRepository;

            _logger = logger;
        }

        /// <summary>
        /// Обменять предмет студенту
        /// </summary>
        [SwaggerResponse(200, "Предмет успешно обменян(а)")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(404, "Пользователь не имеет предмета для обмена")]
        [SwaggerResponse(422, "На складе нет свободного предмета для обмена")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        [HttpPost("exchange/{userID}/items/{itemname}")]
        public ActionResult<ErrorResponseDTO> ExchangeItem(string userID, string itemname)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(itemname))
            {
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 400,
                    ErrorMessage = "Ошибка в запросе"
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

                var items = _itemRepository.GetItemsByID(userID);

                if (items == null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Пользователь не имеет предмета для обмена"
                    };
                    return NotFound(responseDTO);
                }

                var exchangeResult = _exchangeService.ExchangeItem(items, itemname, userID);

                if (exchangeResult == ExchangeResult.SUCCESS)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 200,
                        ErrorMessage = $"{itemname} успешно обменян(а)"
                    };
                    return Ok(responseDTO);
                }
                else if (exchangeResult == ExchangeResult.ItemNotFound)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Не найден {itemname} для обмена"
                    };
                    return NotFound(responseDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 422,
                        ErrorMessage = "На складе нет свободного предмета для обмена"
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
        /// Сдать предмет студенту
        /// </summary>
        [HttpPut("pass/{userID}/items/{itemname}")]
        [SwaggerResponse(200, "Предмет успешно сдан")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(404, "Пользователь не имеет предмета для сдачи")]
        [SwaggerResponse(404, "Пользователь не найден")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> PassItem(string userID, string itemname)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(itemname))
            {
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 404,
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

                var items = _itemRepository.GetItemsByID(userID);

                if (items == null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Пользователь не имеет {itemname} для сдачи"
                    };
                    return NotFound(responseDTO);
                }

                var passResult = _exchangeService.PassItem(items, itemname, userID);

                if (passResult == ExchangeResult.SUCCESS)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 200,
                        ErrorMessage = $"{itemname} успешно сдан(а)"
                    };
                    return Ok(responseDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Пользователь не имеет {itemname} для сдачи"
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
        [HttpPut("give/{userID}/items/{itemname}")]
        [SwaggerResponse(200, "Предмет успешно выдан")]
        [SwaggerResponse(400, "Ошибка в запросе")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> GiveItems(string userID, string itemname)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(itemname))
            {
                var responseDTO = new ErrorResponseDTO
                {
                    ErrorCode = 400,
                    ErrorMessage = "Ошибка в запросе"
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

                var items = _itemRepository.GetItemsByID(userID);

                if (items != null)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = "Пользователь уже имеет этот предмет"
                    };
                    return NotFound(responseDTO);
                }

                var giveResult = _exchangeService.GiveItem(items, itemname, userID);

                if (giveResult == ExchangeResult.SUCCESS)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 200,
                        ErrorMessage = $"{itemname} успешно выдан(а)"
                    };
                    return Ok(responseDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 404,
                        ErrorMessage = $"Не найден {itemname} для выдачи"
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
