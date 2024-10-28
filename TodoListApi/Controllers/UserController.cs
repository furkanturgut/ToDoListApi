using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using TodoListApi.auth.TodoListApi.auth;
using TodoListApi.Dto_s;
using TodoListApi.Interface;
using TodoListApi.Models;

namespace TodoListApi.Controllers
{
    [BasicAuth]
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            this._userRepository = repository;
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(ICollection<UserDto>))]
        public ActionResult<ICollection<UserDto>> GetAllUsers()
        {
            var users = _mapper.Map<ICollection<UserDto>>(_userRepository.GetAllUsers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public ActionResult GetUser(int UserId)
        {
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(UserId));
            if (user == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser(CreateUserDto user)
        {
            if (user == null)
            {
                return BadRequest(ModelState);
            }
            var Mappinguser = _mapper.Map<User>(user);
            if (!_userRepository.CreateUser(Mappinguser))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly Created");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(UpdateUserDto UpdatedUser )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_userRepository.UserExist(UpdatedUser.Id))
                {
                    return NotFound(ModelState);
                }
            var MappingUser = _mapper.Map<User>(UpdatedUser);
            if (!_userRepository.UpdateUser(MappingUser))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
                
        }

        [HttpDelete("{UserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int UserId)
        {
            if (!_userRepository.UserExist(UserId))
            {
                return NotFound();
            }
            var user = _userRepository.GetUser(UserId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_userRepository.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
