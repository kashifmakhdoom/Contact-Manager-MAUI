using Contman.Application.Interfaces;
using Contman.Application.Interfaces.Datastore;
using Contact = Contman.Core.Models.Contact;
namespace Contman.Application.Usecases
{

    public class ViewContactListUsecase : IViewContactListUsecase
    {
        private readonly IContactRepository _contactRepository;

        public ViewContactListUsecase(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<List<Contact>> ExecuteAsync(string filter="")
        {
            return await _contactRepository.GetAllAsync(filter);
        }
    }
}
