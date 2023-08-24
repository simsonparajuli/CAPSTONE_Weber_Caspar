using Omu.Awem.Utils;
using CASPAR.Utility.Utils.Awe;
using CASPAR.Utility.Utils;
using Omu.AwesomeMvc;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CASPAR.Utility.Utils.Awesome
{
    public static class AweDataExtensions
    {
        /// <summary>
        /// will call Where(o => o.Prop.Contains(term) || o.Prop2.Contains(term) ...)
        /// depending on Settings.StringFilterIgnoreCase will either use Contains (works with EF),
        /// or IndexOf(term, StringComparison.InvariantCultureIgnoreCase)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="term"></param>
        /// <param name="props"></param>
        /// <returns></returns>
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, string term, params Expression<Func<T, string>>[] props)
        {
            if (term == null) return query;


            if (Settings.StringFilterIgnoreCase)
            {
                return query.Where(AweExprUtil.IndexOf(term, props));
            }

            return query.Where(AweExprUtil.ContainsExpr(term, props));
        }
    }
}