using AOWebApp.Data;
using AOWebApp.Helpers;
using AOWebApp.Models;
using AOWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AOWebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AmazonOrdersContext _context;

        public ItemsController(AmazonOrdersContext context)
        {
            _context = context;
        }

        // GET: Items
        // POST: Items (for search)
        [HttpGet, HttpPost]
        public async Task<IActionResult> Index(string searchText, int? CategoryId, string? sortOrder, int? PageNumber)
        {
            if (!PageNumber.HasValue) { PageNumber = 1; }
            ViewBag.SortOrder = sortOrder;
            ViewBag.PageNumber = PageNumber;

            #region CategoriesQuery

            var Categories = await _context.ItemCategories.Where(i => i.ParentCategoryId == null).Select(c => new { c.CategoryId, c.CategoryName }).OrderBy(c => c.CategoryName).ToListAsync();
           
            var categorySelectList = new SelectList(Categories, nameof(ItemCategory.CategoryId), nameof(ItemCategory.CategoryName), null);

            #endregion


            #region ItemQuery
            // start with a base query and keep it IQueryable for chaining
            var itemsQuery = _context.Items
                .Include(i => i.Category)
                .Include(i => i.Reviews)
                .AsQueryable();

            // check if search text provided
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                itemsQuery = itemsQuery.Where(i => i.ItemName.Contains(searchText));
                ViewBag.searchText = searchText;
            }

            if (CategoryId != null)
            {
                itemsQuery = itemsQuery.Where(i => i.Category.ParentCategoryId == CategoryId);
            }

            // order records

            switch (sortOrder)
            {
                case "name_desc":
                    itemsQuery = itemsQuery.OrderByDescending(i => i.ItemName);
                    break;

                case "price_asc":
                    itemsQuery = itemsQuery.OrderBy(i => i.ItemCost);
                    break;

                case "price_desc":
                    itemsQuery = itemsQuery.OrderByDescending(i => i.ItemName);
                    break;

                default:
                    itemsQuery = itemsQuery.OrderBy(i => i.ItemName);
                    break;


            }

            // execute query
            var itemsWithReviewsQuery = itemsQuery.Select(i => new ItemWithReviews
            {
                Item = i,
                ReviewCount = i.Reviews != null ? i.Reviews.Count() : 0,
                AverageRating = (i.Reviews != null && i.Reviews.Count() > 0) ? i.Reviews.Average(r => r.Rating) : 0
            });

            int pageSize = 5;

            var pagedItems = await PaginatedList<ItemWithReviews>.CreateAsync(itemsWithReviewsQuery, PageNumber ?? 1, pageSize);

            #endregion

            var model = new ItemSearchViewModel
            {
                searchText = searchText,
                CategoryId = CategoryId,
                CategoryList = categorySelectList,
                ItemList = pagedItems
            };

            // pass to views
            return View(model);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'AmazonOrdersContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }
    }
}
