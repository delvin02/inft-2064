using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerOrders = new HashSet<CustomerOrder>();
            Reviews = new HashSet<Review>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MainPhoneNumber { get; set; } = null!;
        public string? SecondaryPhoneNumber { get; set; }
        public int? AddressId { get; set; }

        [NotMapped]
        [Display(Name = "Customer Name")]
        public string FullName => FirstName + " " + LastName;

        [NotMapped]
        [Display(Name = "Contact Number")]
        public string ContactNumber
        {
            get
            {
                var contact = "";
                if (!string.IsNullOrWhiteSpace(MainPhoneNumber)) { contact = MainPhoneNumber;  }
                if (!string.IsNullOrWhiteSpace(SecondaryPhoneNumber)) { contact += (contact.Length > 0 ? "<br />" : "") + SecondaryPhoneNumber; }
                return contact;
            }
        }

        public virtual Address? Address { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
