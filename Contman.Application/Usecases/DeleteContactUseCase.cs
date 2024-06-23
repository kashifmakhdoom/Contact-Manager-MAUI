using Contman.Application.Interfaces;
using Contman.Application.Interfaces.Datastore;
using Contact = Contman.Core.Models.Contact;
namespace Contman.Application.Usecases
{

    public class DeleteContactUsecase : IDeleteContactUsecase
    {
        private readonly IContactRepository _contactRepository;

        public DeleteContactUsecase(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task ExecuteAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
        }
    }
}
