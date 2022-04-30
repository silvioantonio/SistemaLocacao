using SistemaLocacao.Repositories.Client;

namespace SistemaLocacao.Services.Client
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Models.Client> Create(Models.Client client)
        {
            if(DateTime.MinValue == client.BirthDate)
            {
                throw new ArgumentNullException("BirthDate", "Data não pode ser vazia!");
            }

            return await _clientRepository.Create(client);
        }

        public async Task<bool> Delete(int id)
        {
            return await _clientRepository.Delete(id);
        }

        public async Task<IEnumerable<Models.Client>> FindAll()
        {
            return await _clientRepository.FindAll();
        }

        public async Task<Models.Client?> FindById(int id)
        {
            return await _clientRepository.FindById(id);
        }

        public async Task<Models.Client> Update(Models.Client client)
        {
            if (DateTime.MinValue == client.BirthDate)
            {
                throw new ArgumentNullException("BirthDate", "Data não pode ser vazia!");
            }

            return await _clientRepository.Update(client);
        }
    }
}
