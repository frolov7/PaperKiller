using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using PaperKiller.Services;
using static PaperKiller.Utils.Constants;
using AutoMapper;
using PaperKiller.DTO;
using PaperKiller.Models;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using PaperKiller.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace PaperKiller.Controllers
{
    [Route("/api/v1")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRegisterService _registerService;

        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IAuthService authService, IRegisterService registerService, ILogger<UserController> logger, IMapper mapper)
        {
            _authService = authService;
            _registerService = registerService;

            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        [HttpPost("register")]
        [SwaggerResponse(200, "Успешно")]
        [SwaggerResponse(400, "Некорректные данные")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<ErrorResponseDTO> Registration([FromBody] RegistrationDTO registerData)
        {
            try
            {
                InputError inputError = _registerService.RegisterUser(registerData);

                if (inputError == InputError.SUCCESS)
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 200,
                        ErrorMessage = "Успешная регистрация"
                    };

                    return Ok(responseDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 401,
                        ErrorMessage = "Некорректные данные"
                    };

                    return BadRequest(responseDTO);
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
        /// Авторизация
        /// </summary>
        [HttpPost("authorization")]
        [SwaggerResponse(200, "Успешная авторизация и токен")]
        [SwaggerResponse(401, "Ошибка авторизации")]
        [SwaggerResponse(500, "Ошибка на стороне сервера")]
        public ActionResult<StudentDTO> AuthenticateUser([FromBody] AuthorizationDTO authorizationData)
        {
            try
            {
                if (authorizationData.Login == "Admin" && (authorizationData.Password == "Manager" || authorizationData.Password == "Commandant"))
                    return Ok(authorizationData);

                var user = new Student
                {
                    Login = authorizationData.Login,
                    Password = authorizationData.Password
                };

                // Вызываем сервис для аутентификации пользователя
                var student = _authService.AuthenticateUser(user);

                if (student != null)
                {
                    // Создаем экземпляр DTO, заполняя его данными из student
                    var studentDTO = new StudentDTO
                    {
                        Id = student.UserId ?? 0,
                        Name = student.Name ?? string.Empty,
                        Surname = student.Surname ?? string.Empty,
                        PhoneNumber = student.PhoneNumber ?? string.Empty,
                        CheckInDate = student.CheckInDate ?? string.Empty,
                        Studak = student.StudentId ?? string.Empty,
                        Gender = student.Gender ?? string.Empty,
                        RoomNumber = student.RoomNumber ?? string.Empty,
                        LinenId = student.LinenId,
                        ItemsId = student.ItemsId
                    };
                    return Ok(studentDTO);
                }
                else
                {
                    var responseDTO = new ErrorResponseDTO
                    {
                        ErrorCode = 401,
                        ErrorMessage = "Ошибка авторизации"
                    };
                    return Unauthorized(responseDTO);
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
