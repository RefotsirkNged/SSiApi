﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationSSI.Models
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Username is required.")]
    public string Username
    {
      get; set;
    }

    [Required(ErrorMessage = "Password is required.")]
    public string password
    {
      get; set;
    }

    public string message
    {
      get; set;
    }
  }
}