using System;
using System.Collections.Generic;
using MarketLearning.Repositories;
using MarketLearning.Models.Entities;
using System.Threading.Tasks;
namespace MarketLearning.Services
{
    public class ErrorService
    {
        private IRepository<Error> repository = new ErrorRepository();

        public async Task AddErrorAsync(Error error)
        {
            await repository.AddAsync(error);
        }

        public async Task<List<Error>> GetErrorLogAsync()
        {
            return await repository.GetAllAsync();
        }
        //Server side
        /// <summary>
        /// Server side error logging
        /// </summary>
        /// <param name="err"> The error thrown</param>
        /// <param name="location">The location of the error</param>
        /// <param name="username">The employee using the system at the time of the error</param>
        public async Task ErrorHandler(Exception err, string username)
        {
            Error error = new Error(username, err.Message, err.TargetSite.Name, false);
            await repository.AddAsync(error);
        }

        /// <summary>
        /// Client side error logging
        /// </summary>
        /// <param name="message"> The error message</param>
        /// <param name="location">The location of the error</param>
        /// <param name="username">The employee using the system at the time of the error</param>
        public async Task ErrorHandler(string message, string location, string username)
        {
            Error error = new Error(username, message, location, true);
            await repository.AddAsync(error);
        }
    }
}
