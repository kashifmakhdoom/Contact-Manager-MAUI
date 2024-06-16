using Contman.Application.Interfaces;
using Contman.Application.Interfaces.Datastore;
using Contact = Contman.Core.Models.Contact;
namespace Contman.Application.Usecases
{

    public class ViewContactUsecase : IViewContactUsecase
    {
        private readonly IContactRepository _contactRepository;

        public ViewContactUsecase(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<Contact> ExecuteAsync(string id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }
    }
}
