using SSIApi.Interfaces;
using SSIApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSIApi.Helpers;

namespace SSIApi.Services
{
  public class UserService : IUserService
  {
    private DataContext context;

    public UserService(DataContext _context)
    {
      context = _context;
    }

    public User AuthenticateUser(string username, string password)
    {
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
      {
        return null;
      }

      var user = context.Users.SingleOrDefault(x => x.Username == username);
      if (user == null)
      {
        return null;
      }

      if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
      {
        return null;
      }
      return user;
    }

    public User Create(User user, string password)
    {
      if (string.IsNullOrWhiteSpace(password))
      {
        throw new ApplicationException("Password required for user creation.");
      }
      if (context.Users.Any(x => x.Username == user.Username))
      {
        throw new ApplicationException("Username \"" + user.Username + "\" is already taken");
      }

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      context.Users.Add(user);
      context.SaveChanges();

      return user;
    }

    public IEnumerable<User> GetAll()
    {
      return context.Users;
    }

    public User GetById(int id)
    {
      return context.Users.Find(id);
    }

    public void Update(User userParam, string password = null)
    {
      var user = context.Users.Find(userParam.UserId);
      if (user == null)
      {
        throw new ApplicationException("User not found");
      }

      //Username is set to be updated, check if its already taken
      if (userParam.Username != user.Username)
      {
        if (context.Users.Any(x => x.Username == userParam.Username))
        {
          throw new ApplicationException("Username " + userParam.Username + "is already taken.");
        }
      }

      user.FirstName = userParam.FirstName;
      user.LastName = userParam.LastName;
      user.Username = userParam.Username;

      if (!string.IsNullOrWhiteSpace(password))
      {
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(password, out passwordHash, out passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
      }
      context.Users.Update(user);
      context.SaveChanges();
    }

    public void Delete(int id)
    {
      var user = context.Users.Find(id);
      if (user != null)
      {
        context.Users.Remove(user);
        context.SaveChanges();
      }
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Argument cannot be empty or whitespace.", "password");

      using (var hashAlgorithm = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hashAlgorithm.Key;
        passwordHash = hashAlgorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Argument cannot be empty or whitespace.", "password");
      if (storedHash.Length != 64) throw new ArgumentException("Invalid password hash length (Length is not 64 bytes).", "passwordHash");
      if (storedSalt.Length != 128) throw new ArgumentException("Invalid password salt length(Length is not 128 bytes).", "passwordSalt");

      using (var hashAlgorithm = new System.Security.Cryptography.HMACSHA512(storedSalt))
      {
        var computedHash = hashAlgorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != storedHash[i])
          {
            return false;
          }
        }
      }
      return true;
    }
  }
}