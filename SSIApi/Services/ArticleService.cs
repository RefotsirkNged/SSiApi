﻿using SSIApi.Entities;
using SSIApi.Helpers;
using SSIApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Services
{
  public class ArticleService : IArticleService
  {
    private DataContext context;

    public ArticleService(DataContext _context)
    {
      context = _context;
    }

    public Article Create(Article article)
    {
      article.ArticleId = 0; //set to 0, so the auto incrementing column is used in postgres
      if (!context.StockItems.Any(x => x.StockItemId == article.StockItemId))
      {
        throw new ApplicationException("StockItem not found");
      }

      if (context.Articles.Any(x => x.StockItemId == article.StockItemId))
      {
        throw new ApplicationException("Stockitem already has article");
      }
      context.Articles.Add(article);
      context.SaveChanges();

      //Second savechanges(), to get the correct id, as it is generated by the database.
      var stockItem = context.StockItems.Find(article.StockItemId);
      stockItem.ArticleId = article.ArticleId;
      context.StockItems.Update(stockItem);
      context.SaveChanges();

      return article;
    }

    public IEnumerable<Article> GetAll()
    {
      return context.Articles;
    }

    public Article GetById(int id)
    {
      return context.Articles.Find(id);
    }

    public void Update(Article articleParam)
    {
      var article = context.Articles.Find(articleParam.ArticleId);
      if (article == null)
      {
        throw new ApplicationException("Article not found");
      }

      if (article.Name != articleParam.Name)
      {
        //Name has changed
        if (context.Articles.Any(x => x.Name == articleParam.Name))
        {
          throw new ApplicationException("Article name " + articleParam.Name + "is already taken.");
        }
      }
      if (article.StockItemId != articleParam.StockItemId)
      {
        if (context.Articles.Any(x => x.StockItemId == articleParam.StockItemId))
        {
          throw new ApplicationException("StockItemId " + articleParam.StockItemId + " already contains articles.");
        }
      }

      article.Name = articleParam.Name;
      article.StockItemId = articleParam.StockItemId;

      context.Articles.Update(article);
      context.SaveChanges();
    }

    public void Delete(int id)
    {
      var article = context.Articles.Find(id);
      if (article != null)
      {
        context.Articles.Remove(article);
        context.SaveChanges();
      }
    }
  }
}