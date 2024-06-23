using Contman.Application.Interfaces.Datastore;
using System.Text;
using System.Text.Json;
using Contact = Contman.Core.Models.Contact;

namespace Contman.Infrastructure.Implementations.Datastore
{
    public class ContactApiRepository : IContactRepository
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;

        public ContactApiRepository()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Contact>> GetAllAsync(string filter)
        {
            var contactList = new List<Contact>();

            Uri uri = string.IsNullOrWhiteSpace(filter) ?
                new Uri($"{Config.ApiBaseUrl}/contacts")
                : new Uri($"{Config.ApiBaseUrl}/contacts?filter={filter}");
         
            var response = await _httpClient.GetAsync(uri);

            if (response != null && response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                contactList = JsonSerializer.Deserialize<List<Contact>>(content, _serializerOptions);
            }

            return contactList;
        }
        public async Task<Contact> GetByIdAsync(int id)
        {
            Contact contact = null!;
            
            Uri uri = new Uri($"{Config.ApiBaseUrl}/contacts/{id}");
            var response = await _httpClient.GetAsync(uri);

            if (response != null && response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                contact = JsonSerializer.Deserialize<Contact>(content, _serializerOptions);
            }

            return contact;
        }
        public async Task AddAsync(Contact contact)
        {
            string json = JsonSerializer.Serialize<Contact>(contact, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Config.ApiBaseUrl}/contacts");
            await _httpClient.PostAsync(uri, content);
        }
        public async Task UpdateAsync(int id, Contact contact)
        {
            string json = JsonSerializer.Serialize<Contact>(contact, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Config.ApiBaseUrl}/contacts/{id}");
            await _httpClient.PutAsync(uri, content);
        }
        public async Task DeleteAsync(int id)
        {
            Uri uri = new Uri($"{Config.ApiBaseUrl}/contacts/{id}");
            await _httpClient.DeleteAsync(uri);
        }
        
    }
}
