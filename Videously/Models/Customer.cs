using System;
using System.ComponentModel.DataAnnotations;
using Videously.Attributes;

namespace Videously.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Required]
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Date of Birth")]
        [MinimumAgeMembership]
        public DateTime? BirthDate { get; set; }
    }
}