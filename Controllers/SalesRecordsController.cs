using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SalesRecordsController(SalesRecordService salesRecordService) : Controller
    {
        private readonly SalesRecordService _salesRecordService = salesRecordService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minimumDate, DateTime? maximumDate)
        {
            if (minimumDate.HasValue == false)
                minimumDate = new(DateTime.Now.Year, 1, 1);

            if (maximumDate.HasValue == false)
                maximumDate = DateTime.Now;

            ViewData["minDate"] = $"{minimumDate:yyyy-MM-dd}";
            ViewData["maxDate"] = $"{maximumDate:yyyy-MM-dd}";

            var result = await _salesRecordService.FindByDateAsync(minimumDate, maximumDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minimumDate, DateTime? maximumDate)
        {
            if (minimumDate.HasValue == false)
                minimumDate = new(DateTime.Now.Year, 1, 1);

            if (maximumDate.HasValue == false)
                maximumDate = DateTime.Now;

            ViewData["minDate"] = $"{minimumDate.Value:yyyy-MM-dd}";
            ViewData["maxDate"] = $"{maximumDate.Value:yyyy-MM-dd}";

            var result = await _salesRecordService.FindByDataGroupingAsync(minimumDate, maximumDate);
            return View(result);
        }
    }
}
