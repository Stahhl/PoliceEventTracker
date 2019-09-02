using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PoliceEventTracker.App.Models;
using PoliceEventTracker.Data;
using PoliceEventTracker.Domain.Models;

namespace PoliceEventTracker.App.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(DataAccess data)
        {
            dataAccess = data;
        }

        private DataAccess dataAccess;

        public async Task<IActionResult> UpdateDatabase()
        {
            var update = await dataAccess.UpdateDatabase();

            return RedirectToAction(nameof(UpdateDatabaseResult), update);
        }
        public IActionResult UpdateDatabaseResult(Update update)
        {
            var eventsInUpdate = update.Events;

            return View(eventsInUpdate);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DisplayLatestEvents()
        {
            var events = await dataAccess.GetLatestEvents(50);

            return View(events);
        }
        public async Task<IActionResult> Details(int id)
        {
            var e = await dataAccess.GetEventById(id);

            return View(e);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
