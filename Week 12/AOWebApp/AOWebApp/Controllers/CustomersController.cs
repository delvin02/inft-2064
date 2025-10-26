using AOWebApp.Data;
using AOWebApp.Models;
using AOWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AOWebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AmazonOrdersContext _context;

        public CustomersController(AmazonOrdersContext context)
        {
            _context = context;
        }


        [HttpGet, HttpPost]
        public async Task<IActionResult> Index(string? searchText, String? Suburb)
        {

            #region SuburbQuery

            List<String> suburbList = await _context.Addresses.Select(c => c.Suburb).Distinct().OrderBy(a => a).ToListAsync();

            #endregion

            List<Customer> customerList = new List<Customer>();

            #region CustomerQuery
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var nameParts = searchText.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var query = _context.Customers
                    .Include(c => c.Address)
                    .Where(c =>
                        nameParts.Length > 1
                            ? c.FirstName == nameParts[0] && c.LastName == nameParts[1]
                            : c.FirstName.StartsWith(searchText) || c.LastName.StartsWith(searchText)
                    );

                if (!string.IsNullOrEmpty(Suburb))
                {
                    query = query.Where(c => c.Address.Suburb == Suburb);
                }

                if (nameParts.Length > 1)
                {
                    query = query.Where(c =>
                        c.FirstName.StartsWith(nameParts[0]) &&
                        c.LastName.StartsWith(nameParts[1]));

                    query = query
                        .OrderBy(c => c.FirstName.StartsWith(nameParts[0]) ? 0 : 1)
                        .ThenBy(c => c.LastName.StartsWith(nameParts[1]) ? 0 : 1)
                        .ThenBy(c => c.FirstName)
                        .ThenBy(c => c.LastName);
                }
                else
                {
                    // Only one part provided
                    query = query.Where(c =>
                        c.FirstName.StartsWith(nameParts[0]) ||
                        c.LastName.StartsWith(nameParts[0]));

                    query = query
                        .OrderBy(c => c.FirstName.StartsWith(nameParts[0]) ? 0 : 1)
                        .ThenBy(c => c.LastName.StartsWith(nameParts[0]) ? 0 : 1)
                        .ThenBy(c => c.FirstName)
                        .ThenBy(c => c.LastName);
                }


                customerList = await query.ToListAsync();
            }

            #endregion

            var nameSuggestionList = await _context.Customers.AsNoTracking()
                .Select(c => c.FirstName)
                .Union(_context.Customers.Select(c => c.LastName))
                .Union(_context.Customers.Select(c => (c.FirstName + " " + c.LastName)))
                .Where(s => s != null && s != "")
                .Distinct()
                .OrderBy(s => s)
                .Take(200)
                .ToListAsync();


            var model = new CustomerIndexViewModel
            {
                Customers = customerList,
                SuburbList = new SelectList(suburbList, Suburb),
                NameSuggestions = nameSuggestionList
            };

            return View(model);
        }
    }
}
