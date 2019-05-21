using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSIApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSIApi.Helpers;
using Microsoft.Extensions.Options;
using SSIApi.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using SSIApi.Entities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SSIApi.Controllers
{
  [ResponseCache(NoStore = true, Duration = 0)]
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    private IUserService userService;
    private IMapper mapper;
    private readonly AppSettings appSettings;

    public UsersController(IUserService _userService, IMapper _mapper, IOptions<AppSettings> _appSettings)
    {
      userService = _userService;
      mapper = _mapper;
      appSettings = _appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody]UserDto userDto)
    {
      var user = userService.AuthenticateUser(userDto.Username, userDto.Password);
      if (user == null)
      {
        return BadRequest(new { message = "Username or password is incorrect" });
      }

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[] {
          new Claim(ClaimTypes.Name, user.UserId.ToString())
        }),
        Expires = DateTime.UtcNow.AddHours(8),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);
      return Ok(new UserDto
      {
        UserId = user.UserId,
        Username = user.Username,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Token = tokenString
      });
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Register([FromBody]UserDto userDto)
    {
      var user = mapper.Map<User>(userDto);

      try
      {
        userService.Create(user, userDto.Password);
        return Ok();
      }
      catch (ApplicationException ex)
      {
        return BadRequest(new
        {
          message = ex.Message
        });
      }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      var users = userService.GetAll();
      var userDtos = mapper.Map<IList<UserDto>>(users);

      return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var user = userService.GetById(id);
      var userDto = mapper.Map<UserDto>(user);
      return Ok(userDto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody]UserDto userDto)
    {
      var user = mapper.Map<User>(userDto);
      user.UserId = id;

      try
      {
        userService.Update(user, userDto.Password);
        return Ok();
      }
      catch (ApplicationException ex)
      {
        return BadRequest(new
        {
          message = ex.Message
        });
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      userService.Delete(id);
      return Ok();
    }
  }
}