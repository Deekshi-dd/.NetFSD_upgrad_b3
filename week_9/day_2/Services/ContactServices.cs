using ContactCachingAPI.Models;
using ContactCachingAPI.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace ContactCachingAPI.Services
{
    public class ContactService
    {
        private readonly ContactRepository _repo;
        private readonly IMemoryCache _cache;

        // Cache keys - just strings to identify what we cached
        private const string AllContactsCacheKey = "all_contacts";

        public ContactService(ContactRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public List<Contact> GetAll()
        {
            // Try to get from cache first
            if (_cache.TryGetValue(AllContactsCacheKey, out List<Contact> cachedContacts))
            {
                Console.WriteLine("Returning from CACHE");
                return cachedContacts;
            }

            // Not in cache -> get from repository
            var contacts = _repo.GetAll();

            // Save in cache for 60 seconds
            _cache.Set(AllContactsCacheKey, contacts, TimeSpan.FromSeconds(60));

            return contacts;
        }

        public Contact GetById(int id)
        {
            string cacheKey = $"contact_{id}";

            if (_cache.TryGetValue(cacheKey, out Contact cachedContact))
            {
                Console.WriteLine($"Returning contact {id} from CACHE");
                return cachedContact;
            }

            var contact = _repo.GetById(id);

            if (contact != null)
            {
                _cache.Set(cacheKey, contact, TimeSpan.FromSeconds(60));
            }

            return contact;
        }
    }
}
