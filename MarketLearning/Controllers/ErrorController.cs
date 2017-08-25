using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MarketLearning.Models;
using MarketLearning.Models.Entities;
using MarketLearning.Services;

namespace MarketLearning.Controllers
{
    public class ErrorController : Controller
    {
        private ErrorService errorService = new ErrorService();
        private Searching_SortingService<Error> ssService = new Searching_SortingService<Error>();

        // GET: Error
        public async Task<ActionResult> ErrorIndex(List<Error> log)
        {
            ViewBag.HighlightTab = "error";

            if (log == null || !log.Any())
            {
                log = await errorService.GetErrorLogAsync();
            }
            try
            {
                log = ssService.SortListByPropertyDesc(log, "Occurance");
            }
            catch (Exception err)
            {
                string username = User.Identity.Name;
                await errorService.ErrorHandler(err, username);
            }
            return View(log);
        }
    }
}
