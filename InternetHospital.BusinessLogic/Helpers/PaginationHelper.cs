using System;
using System.Collections.Generic;
using System.Linq;
using InternetHospital.BusinessLogic.Models;
using InternetHospital.DataAccess.Entities;

namespace InternetHospital.BusinessLogic.Helpers
{
    public static class PaginationHelper<T> where T : class
    {
        public static IQueryable<T> GetPageValues(IQueryable<T> entities, int pageCount, int page)
        {
            var result = entities
                .Skip(pageCount * (page - 1))
                .Take(pageCount);

            return result;
        }
    }
}
