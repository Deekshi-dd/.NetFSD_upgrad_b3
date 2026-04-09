using Dapper;
using System.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Data;

namespace DataAccessLayer.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DapperContext _context;

        public ContactRepository(DapperContext context)
        {
            _context = context;
        }

        public List<ContactInfo> GetAllContacts()
        {
            var connection = _context.CreateConnection();

            string query = @"SELECT c.*, comp.CompanyName, d.DepartmentName
                             FROM Contacts c
                             JOIN Companies comp ON c.CompanyId = comp.CompanyId
                             JOIN Departments d ON c.DepartmentId = d.DepartmentId";

            return connection.Query<ContactInfo>(query).ToList();
        }

        // Get By Id
        public ContactInfo GetContactById(int id)
        {
           var connection = _context.CreateConnection();

            string query = "SELECT * FROM Contacts WHERE ContactId = @Id";

            return connection.QueryFirstOrDefault<ContactInfo>(query, new { Id = id });
        }

        // Insert
        public void AddContact(ContactInfo contact)
        {
            var connection = _context.CreateConnection();

            string query = @"INSERT INTO Contacts
        (FirstName, LastName, EmailId, MobileNo, Designation, CompanyId, DepartmentId)
        VALUES (@FirstName, @LastName, @EmailId, @MobileNo, @Designation, @CompanyId, @DepartmentId)";

            connection.Execute(query, contact);
        }

        // Update
        public void UpdateContact(ContactInfo contact)
        {
            var connection = _context.CreateConnection();

            string query = @"UPDATE Contacts SET
                             FirstName = @FirstName,
                             LastName = @LastName,
                             EmailId = @EmailId,
                             MobileNo = @MobileNo,
                             Designation = @Designation,
                             CompanyId = @CompanyId,
                             DepartmentId = @DepartmentId
                             WHERE ContactId = @ContactId";

            connection.Execute(query, contact);
        }

        // Delete
        public void DeleteContact(int id)
        {
            var connection = _context.CreateConnection();

            string query = "DELETE FROM Contacts WHERE ContactId = @Id";

            connection.Execute(query, new { Id = id });
        }

        // Company
        public List<Company> GetCompanies()
        {
            var connection = _context.CreateConnection();

            return connection.Query<Company>("SELECT * FROM Companies").ToList();
        }

        // Department
        public List<Department> GetDepartments()
        {
            var connection = _context.CreateConnection();

            return connection.Query<Department>("SELECT * FROM Departments").ToList();
        }
    }
}
