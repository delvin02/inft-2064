using AOWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AOWebApp
{
    public class ReportsController : Controller
    {
        private readonly AmazonOrdersContext _context;

        public ReportsController(AmazonOrdersContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var yearList = _context.CustomerOrders.Select(co => co.OrderDate.Year).Distinct().OrderByDescending(co => co).ToList();

            return View("AnnualSalesReport", new SelectList(yearList));
        }

        [Produces("application/json")]
        public IActionResult AnnualSalesReportData(int Year)
        {
            if (Year  < 1)
            {
                return BadRequest();
            }

            var orderSummary = _context.ItemsInOrders.Where(iio => iio.OrderNumberNavigation.OrderDate.Year == Year).GroupBy(item => new { item.OrderNumberNavigation.OrderDate.Year, item.OrderNumberNavigation.OrderDate.Month }).Select(group => new
            {
                year = group.Key.Year,
                monthNo = group.Key.Month,
                monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month),
                totalItems = group.Sum(item => item.NumberOf),
                totalSales = group.Sum(item => item.TotalItemCost)
            }).OrderBy(data => data.monthNo);

            return Json(orderSummary);
        }
    }
}
