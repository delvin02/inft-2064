using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebAppActivity.Models;

public partial class Item
{
    public int ItemId { get; set; }

    [Required(ErrorMessage = "Item name is required.")]
    [StringLength(150, ErrorMessage = "Item name cannot exceed 150 characters.")]
    [Display(Name = "Item Name")]
    public string ItemName { get; set; } = null!;

    [Required(ErrorMessage = "Item description is required.")]
    [Display(Name = "Item Description")]
    public string ItemDescription { get; set; } = null!;

    [Required(ErrorMessage = "Item cost is required.")]
    [Range(0.01, 9999999999.99, ErrorMessage = "Item cost must be greater than 0.")]
    [Display(Name = "Item Cost ($)")]
    [DataType(DataType.Currency)]
    public decimal ItemCost { get; set; }

    [Required(ErrorMessage = "Item image is required.")]
    [Display(Name = "Item Image")]
    public string ItemImage { get; set; } = null!;

    [Display(Name = "Category ID")]
    public int CategoryId { get; set; }

    public virtual ItemCategory? Category { get; set; } = null!;

    public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; } = new List<ItemMarkupHistory>();

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
