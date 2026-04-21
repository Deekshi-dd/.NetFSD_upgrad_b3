using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication17.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public ICollection<ContactInfo> Contacts { get; set; }
    }
}
