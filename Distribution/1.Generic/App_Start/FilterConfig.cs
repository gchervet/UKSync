using DAL;
using Distribution.Attributes;
using Ninject;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Distribution
{
    /// <summary>
    /// FilterConfig
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// RegisterGlobalFilters
        /// </summary>
        /// <param name="filters">filters</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// RegisterHttpFilters
        /// </summary>
        /// <param name="filters">filters</param>
        public static void RegisterHttpFilters(HttpFilterCollection filters)
        {
            //El orden de estos atributos es importante!
            filters.Add(new AuthorizeOnlyAttribute());
            filters.Add(new ValidationActionFilter());
            filters.Add(new ConcurrencyExceptionHandler());
            filters.Add(new ChecksumExceptionHandler());
            filters.Add(new UnauthorizedOperationExceptionHandler());
            filters.Add(new InvalidArgumentExceptionHandler());
        }
    }
}
