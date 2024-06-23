using Contman.Application.Interfaces.Datastore;
using SQLite;
using Contact = Contman.Core.Models.Contact;

namespace Contman.Infrastructure.Implementations.Datastore
{
    public class ContactSqliteRepository : IContactRepository
    {
        private SQLiteAsyncConnection _database;
        public ContactSqliteRepository()
        {
            _database = new SQLiteAsyncConnection(Config.DatabasePath);
            _database.CreateTableAsync<Contact>();
        }
        public async Task<List<Contact>> GetAllAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return await _database.Table<Contact>().ToListAsync();

            return await _database.QueryAsync<Contact>(@"SELECT * FROM Contact WHERE Name Like ? OR Email LIKE ? OR Phone LIKE ?", $"{filter}%", $"{filter}%", $"{filter}%");
        }
        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _database.GetAsync<Contact>(id);
        }
        public async Task AddAsync(Contact contact)
        {
            await _database.InsertAsync(contact);
        }
        public async Task UpdateAsync(int id, Contact contact)
        {
            if(id == contact.Id)
                await _database.UpdateAsync(contact);
        }
        public async Task DeleteAsync(int id)
        {
            var contact = await GetByIdAsync(id);
            if (contact is not null && contact.Id == id)
            {
                await _database.DeleteAsync(contact);
            }
        }
    }
}
