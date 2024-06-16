using Contman.Application.Interfaces;
using Contman.Application.Interfaces.Datastore;
using Contact = Contman.Core.Models.Contact;
namespace Contman.Application.Usecases
{

    public class EditContactUsecase : IEditContactUsecase
    {
        private readonly IContactRepository _contactRepository;

        public EditContactUsecase(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task ExecuteAsync(string id, Contact contact)
        {
            await _contactRepository.UpdateAsync(id, contact);
        }
    }
}
