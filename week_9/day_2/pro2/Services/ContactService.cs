using ContactPagingAPI.Models;
using ContactPagingAPI.Repositories;

namespace ContactPagingAPI.Services
{
    public class ContactService
    {
        private readonly ContactRepository _repo;

        public ContactService(ContactRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResponse<Contact>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var (contacts, totalCount) = await _repo.GetPagedAsync(pageNumber, pageSize);

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return new PagedResponse<Contact>
            {
                TotalRecords = totalCount,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Data = contacts
            };
        }
    }
}
