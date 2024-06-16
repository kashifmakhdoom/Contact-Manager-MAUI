using Contman.Application.Interfaces;
using Contman.Application.Interfaces.Datastore;
using Contact = Contman.Core.Models.Contact;
namespace Contman.Application.Usecases
{

    public class AddContactUsecase : IAddContactUsecase
    {
        private readonly IContactRepository _contactRepository;

        public AddContactUsecase(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task ExecuteAsync(Contact contact)
        {
            await _contactRepository.AddAsync(contact);
        }
    }
}
