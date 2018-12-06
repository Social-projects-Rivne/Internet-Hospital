using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InternetHospital.BusinessLogic.Interfaces;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess;
using InternetHospital.DataAccess.Entities;

namespace InternetHospital.BusinessLogic.Services
{
    public class ArticleTypeService: IArticleTypeService
    {
        private readonly ApplicationContext _context;

        public ArticleTypeService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<ArticleTypeModel> GetArticleType()
        {
            return _context.ArticleTypes.Select(aType => 
                Mapper.Map<ArticleType, ArticleTypeModel>(aType)).ToList();
        }

        public bool CreateArticleType(string articleTypeName)
        {
            if (!string.IsNullOrWhiteSpace(articleTypeName))
            {
                ArticleType newType = _context.ArticleTypes.FirstOrDefault(aType => aType.Name == articleTypeName);
                if (newType == null)
                {
                    newType = new ArticleType {Name = articleTypeName};
                    _context.ArticleTypes.Add(newType);
                    _context.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}
