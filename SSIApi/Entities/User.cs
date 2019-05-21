using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Entities
{
  public class User
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

    public byte[] PasswordHash
    {
      get; set;
    }

    public byte[] PasswordSalt
    {
      get; set;
    }
  }
}