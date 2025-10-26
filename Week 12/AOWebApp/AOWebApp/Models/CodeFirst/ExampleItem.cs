using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models.CodeFirst
{
    public class ExampleItem
    {

        [Key]
        public int itemId { get; set; }

        [Required(ErrorMessage = "You must supply an Item Name")]
        [Display(Name ="Item Name")]
        [StringLength(100, ErrorMessage = "The item name must be less than 100 characters")]
        public string ItemName { get; set; } = String.Empty;

        [Display(Name = "Price")]
        [Required(ErrorMessage = "You must provide an item price")]
        [Range(1, 100000, ErrorMessage = "The {0} must be between {1} and {2}")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Decimal(8,2)")]
        public decimal itemPrice { get; set; } 
    }
}
