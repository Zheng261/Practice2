using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using MarketLearning.Models.Entities;
using MarketLearning.Repositories;

namespace MarketLearning.Services
{
    public class Searching_SortingService<T>
    {
        private ErrorService _errorService = new ErrorService();
        /// <summary>
        /// Sort method that works for all models, ascending order
        /// </summary>
        /// <param name="origList"> The Data list to sort</param>
        /// <param name="param"> The field name within the model to sort by </param>
        /// <returns>A Data List of records sorted by the specified field in ascending order</returns>
        public List<T> SortListByPropertyAsc(List<T> origList, string param)
        {
            List<T> newList = origList.OrderBy(x => typeof(T).GetProperty(param).GetValue(x, null)).ToList();
            return newList;
        }

        /// <summary>
        /// Sort method that works for all models, descending order
        /// </summary>
        /// <param name="origList"> The Data list to sort</param>
        /// <param name="param"> The field name within the model to sort by </param>
        /// <returns>A Data List of records sorted by the specified field in descending order</returns>
        public List<T> SortListByPropertyDesc(List<T> origList, string param)
        {
            List<T> newList = origList.OrderByDescending(x => typeof(T).GetProperty(param).GetValue(x, null)).ToList();
            return newList;
        }
        //This may be useful in determining the type of a generic 
        //   Type typeParameterType = typeof(T);

        /// <summary>
        /// Search method that works for all repositories, comparing for partial/fullequality
        /// </summary>
        /// <param name="repository">The repository to search</param>
        /// <param name="expression">The expression to search for</param>
        /// <returns>A Data List of records containing the search expression</returns>
        public async Task<List<T>> SearchByProperty(IRepository<T> repository, Expression<Func<T, bool>> expression)
        {
            List<T> list = await repository.GetByPropertyAsync(expression);
            return list;
        }

        /// <summary>
        /// Search method that works for all models, comparing for equality
        /// </summary>
        /// <param name="origList"> The Data list to search </param>
        /// <param name="param"> The field name within the model to search by </param>
        /// <param name="value"> The value to compare for equality </param>
        /// <returns>A Data List of records with a specific value for a given field</returns>
        public async Task<List<T>> SearchListByProperty<S>(List<T> origList, string param, S value)
        {
            try
            {
                List<T> newList = origList.Where(x => typeof(T).GetProperty(param).GetValue(x, null).Equals(value)).ToList();
                return newList;
            }
            catch (Exception e)
            {
                await _errorService.ErrorHandler(e, "");
                return origList;
            }

        }

        /// <summary>
        /// Search method that works for all models, comparing for date range
        /// </summary>
        /// <param name="origList"> The Data list to search </param>
        /// <param name="param"> The field name within the model to search by. Must be a datetime </param>  
        /// <param name="date1"> One end of the date range </param>
        /// <param name="date2"> The other end of the date range </param>
        /// Note:The dates may be in any order.
        /// <returns>A Data List of records within the specified date range</returns>
        public async Task<List<T>> SearchListByDateTimeRange(List<T> origList, string param, DateTime date1, DateTime date2)
        {
            List<T> newList = new List<T>();
            if (date1 > date2)
            {
                newList = SearchByDateTimeRange(origList, param, date2, date1);
            }
            else if (date1 == date2)
            {
                newList = await SearchListByProperty(origList, param, date1);
            }
            else if (date1 < date2)
            {
                newList = SearchByDateTimeRange(origList, param, date1, date2);
            }
            return newList;
        }
        /// <summary>
        /// This is a private method that does the work of generating the List of records within a date range.
        /// The order of the dates has been fixed by the helper methos SearchListByDateTimeRange
        /// </summary>
        /// <returns>A Data List of records within the specified date range</returns>

        private List<T> SearchByDateTimeRange(List<T> origList, string param, DateTime date1, DateTime date2)
        {
            List<T> newList = origList.Where(x => (DateTime)typeof(T).GetProperty(param).GetValue(x, null) >= date1 && (DateTime)typeof(T).GetProperty(param).GetValue(x, null) <= date2).ToList();
            return newList;
        }
    }
}