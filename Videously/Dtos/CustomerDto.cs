using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Videously.Attributes;
using Videously.Models;

namespace Videously.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Required]
        public MembershipTypeDto MembershipType { get; set; }

        [MinimumAgeMembershipDto]
        public DateTime? BirthDate { get; set; }
    }
}