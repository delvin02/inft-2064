using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AOWebApp.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemMarkupHistories = new HashSet<ItemMarkupHistory>();
            ItemsInOrders = new HashSet<ItemsInOrder>();
            Reviews = new HashSet<Review>();
        }

        [Display(Name ="Item #")]
        [Key]
        public int ItemId { get; set; }

        [Display(Name ="Product Name")]
        [Required(ErrorMessage = "The Product Name is mandatory")]
        [MaxLength(150, ErrorMessage ="The Product Name must be < 150 characters")]
        public string ItemName { get; set; } = null!;

        [Display(Name ="Product Description")]
        [Required(ErrorMessage ="A Product Description is a must!")]
        [DataType(DataType.MultilineText)]
        [MaxLength(5000, ErrorMessage = "The Product Description must be < 5000 characters")]
        public string ItemDescription { get; set; } = null!;

        [Display(Name ="Product Cost")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage ="You must provide the cost of this product")]
        [Range(0, 99999999, ErrorMessage ="The product cost must be between 0-999,999")]
        public decimal ItemCost { get; set; }

        [Display(Name ="Product Image")]
        [Required(ErrorMessage ="A Product Image is required!")]
        [MaxLength(5000, ErrorMessage ="The Image URL must be < 5000 characters")]
        [DataType(DataType.ImageUrl)]
        public string ItemImage { get; set; } = null!;

        [Display(Name ="Product Category")]
        public int? CategoryId { get; set; }

        public virtual ItemCategory? Category { get; set; }
        public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; }
        public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
