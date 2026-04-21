using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication17.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public ICollection<ContactInfo> Contacts { get; set; }
    }
}
