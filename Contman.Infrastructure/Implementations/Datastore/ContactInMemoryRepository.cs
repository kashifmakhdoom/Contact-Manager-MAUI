using Contact = Contman.Core.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contman.Application.Interfaces.Datastore;

namespace Contman.Infrastructure.Implementations.Datastore
{
    public class ContactInMemoryRepository : IContactRepository
    {
        public List<Contact> _contactList;

        public ContactInMemoryRepository()
        {
            _contactList = new List<Contact>()
            {
                new() {Id="0001", Name="Joh Doe", Email="john@email.com", Phone="555-75757", Address=""},
                new() {Id="0002", Name="Jane Doe", Email="jane@email.com", Phone="666-76767", Address=""},
                new() {Id="0003",Name="Tom Hanks", Email="tom@email.com", Phone="777-35353", Address = ""},
            };

        }

        public Task<List<Contact>> GetAllAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return Task.FromResult(_contactList);

            var contactList = _contactList.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (contactList is null || contactList.Count <= 0)
                contactList = _contactList.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return Task.FromResult(contactList);

            if (contactList is null || contactList.Count <= 0)
                contactList = _contactList.Where(x => !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.StartsWith(filter, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return Task.FromResult(contactList);

            return Task.FromResult(contactList)!;
        }

        public Task<Contact> GetByIdAsync(string id)
        {
            var contact = _contactList.FirstOrDefault(x => x.Id == id);
            
            if(contact is not null)
                return Task.FromResult(contact);

            return null!;
        }

        public Task AddAsync(Contact contact)
        {
            var maxId = int.Parse(_contactList.Max(x => x.Id)) + 1;

            contact.Id = maxId.ToString().PadLeft(4, '0');
            _contactList.Add(contact);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(string id, Contact contact)
        {
            if (id != contact.Id) return Task.CompletedTask;

            var contactToUpdate = _contactList.FirstOrDefault(x => x.Id == id);
            if (contactToUpdate is not null)
            {
                // TODO: Automapper
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Address = contact.Address;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(string id)
        {
            var contact = _contactList.FirstOrDefault(x => x.Id == id);
            if (contact is not null)
            {
                _contactList.Remove(contact);
            }
            return Task.CompletedTask;
        }
    }
}
