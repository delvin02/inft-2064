using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AOWebApp.ViewModels
{
    public class CustomerIndexViewModel
    {
        [Required(ErrorMessage ="You must provide a Customer Name")]
        public string? SearchText { get; set; }
        public string? Suburb { get; set; }
        public SelectList SuburbList { get; set; }
        public List<Customer> Customers { get; set; }

        public List<string> NameSuggestions { get; set; } = new();
    }
}
