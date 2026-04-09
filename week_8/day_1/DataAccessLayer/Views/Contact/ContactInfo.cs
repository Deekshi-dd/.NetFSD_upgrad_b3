using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DataAccessLayer.Models
{
    public class ContactInfo
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public long MobileNo { get; set; }
        public string Designation { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }

        [ValidateNever]
        public string CompanyName { get; set; }

        [ValidateNever]
        public string DepartmentName { get; set; }
    }
}
