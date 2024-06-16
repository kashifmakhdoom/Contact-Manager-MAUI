using Contact = Contman.Core.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contman.Application.Interfaces.Datastore
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync(string filter);
        Task<Contact> GetByIdAsync(string id);
        Task AddAsync(Contact contact);
        Task UpdateAsync(string id, Contact contact);
        Task DeleteAsync(string id);
    }
}
