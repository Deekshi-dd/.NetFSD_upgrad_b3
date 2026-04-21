using ContactCachingAPI.Models;

namespace ContactCachingAPI.Repositories
{
    public class ContactRepository
    {
        // This acts as our fake database (in-memory list)
        private static List<Contact> _contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "Alice", Email = "alice@example.com" },
            new Contact { Id = 2, Name = "Bob",   Email = "bob@example.com"   },
            new Contact { Id = 3, Name = "Carol",  Email = "carol@example.com" }
        };

        public List<Contact> GetAll()
        {
            Console.WriteLine("Fetching from REPOSITORY (not cache)");
            return _contacts;
        }

        public Contact GetById(int id)
        {
            Console.WriteLine($"Fetching contact {id} from REPOSITORY (not cache)");
            return _contacts.FirstOrDefault(c => c.Id == id);
        }
    }
}
