using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Dto
{
  public class UserDto
  {
    public int UserId
    {
      get; set;
    }

    public string FirstName
    {
      get; set;
    }

    public string LastName
    {
      get; set;
    }

    public string Username
    {
      get; set;
    }

    public string Password
    {
      get; set;
    }

    public string Token
    {
      get; set;
    }
  }
}