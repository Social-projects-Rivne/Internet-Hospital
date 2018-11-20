using System.Linq;

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
