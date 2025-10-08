using AOWebApp.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AOWebApp.ViewModels
{
    public class ItemSearchViewModel
    {
        public string searchText { get; set; }
        public int? CategoryId { get; set; }
        public SelectList CategoryList { get; set; }
        public PaginatedList<ItemWithReviews> ItemList { get; set; }
    }

    public class ItemWithReviews
    {
        public Models.Item Item { get; set; }
        public int ReviewCount { get; set; }
        public double AverageRating { get; set; }
    }
}
