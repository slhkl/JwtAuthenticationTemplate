using Business.Concrete.Model;
using Business.Discrete;
using Data.Dto;
using Data.Entity;

namespace Business.Concrete.Services
{
    public class ClientService : IService<Client>
    {
        private static List<Client> _clients = new List<Client>
        {
            new Client { Id = 1, Name = "Salih Kol", Email = "salih.kol2@gmail.com", Password = "salihkol" }
        };

        private UserDto userInformationFromJwt;

        public ClientService(RequestMiddlewareModel requestMiddlewareModel)
        {
            userInformationFromJwt = requestMiddlewareModel.User;
        }

        public async Task Add(Client Entity)
        {
            await Task.Run(() => _clients.Add(Entity));
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => _clients.Remove(Get(id)));
        }

        public IEnumerable<Client> Get()
        {
            return _clients;
        }

        public Client Get(int id)
        {
            return _clients.FirstOrDefault(c => c.Id.Equals(id));
        }

        public Client Get(Func<Client, bool> filter)
        {
            return _clients.FirstOrDefault(filter);
        }

        public Task Update(Client entity)
        {
            throw new NotImplementedException();
        }
    }
}
