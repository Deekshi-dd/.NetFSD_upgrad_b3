using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ContactInfo
    {
        [Required]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(15, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(15, MinimumLength = 3)]
        public string LastName { get; set; }

        public string CompanyName { get; set; }

        [EmailAddress]
        public string EmailId { get; set; }
        public long MobileNo { get; set; }
        public string Designation { get; set; }
    }
}
