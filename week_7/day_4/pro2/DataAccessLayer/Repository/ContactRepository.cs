using Microsoft.EntityFrameworkCore;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _context;

    public ContactRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<ContactInfo> GetAllContacts()
    {
        return _context.Contacts
            .Include(c => c.Company)
            .Include(c => c.Department)
            .ToList();
    }

    public ContactInfo GetContactById(int id)
    {
        return _context.Contacts
            .Include(c => c.Company)
            .Include(c => c.Department)
            .FirstOrDefault(c => c.ContactId == id);
    }

    public void AddContact(ContactInfo contact)
    {
        _context.Contacts.Add(contact);
        _context.SaveChanges();
    }

    public void UpdateContact(ContactInfo contact)
    {
        _context.Contacts.Update(contact);
        _context.SaveChanges();
    }

    public void DeleteContact(int id)
    {
        var contact = _context.Contacts.Find(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }
    }
}
