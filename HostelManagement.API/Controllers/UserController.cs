using AutoMapper;
using HostelManagement.DAL.Data;
using HostelManagement.DAL.Models;
using HostelManagement.DAL.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ApplicationDbContext _db;
        private IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IConfiguration config, ApplicationDbContext db, IMapper mapper, ILogger<UserController> logger)
        {
            _config = config;
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        // [Authorize(Policy = "admin")]
        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users;
        }


        [HttpPost("[action]")]
        public IActionResult Register([FromBody] UserVM user)
        {
            _logger.LogInformation("Register method is called at " + DateTime.Now);

            //checks whether the user email in register method is already present in database or not
            var userExists = _db.Users.FirstOrDefault(u => u.Email == user.Email);
            var c = _mapper.Map<User>(user);
            if (userExists == null)
            {
                _db.Users.Add(c);
                _db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest("User with same Email Id already exists!");
            }
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] UserVM user)
        {
            _logger.LogInformation("Login method is called at " + DateTime.Now);

            var currentUser = _db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (currentUser == null)
            {
                return NotFound();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
              new Claim(ClaimTypes.Email, user.Email),
          };
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
           );
            //return token in string format
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwt);
        }
    }
}
