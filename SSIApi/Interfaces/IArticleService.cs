using SSIApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Interfaces
{
  public interface IArticleService
  {
    IEnumerable<Article> GetAll();

    Article GetById(int id);

    Article Create(Article article);

    void Update(Article article);

    void Delete(int id);
  }
}