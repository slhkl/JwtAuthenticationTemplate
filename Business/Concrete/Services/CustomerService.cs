using Business.Concrete.Model;
using Business.Discrete;
using Data.Dto;
using Data.Entity;

namespace Business.Concrete.Services
{
    public class CustomerService : IService<Customer>
    {
        private static List<Customer> _customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Salih", Email = "salih.kol2@gmail.com", Password = "salih.kol"}
        };

        private UserDto userInformationFromJwt;

        public CustomerService(RequestMiddlewareModel requestMiddlewareModel)
        {
            userInformationFromJwt = requestMiddlewareModel.User;
        }

        public async Task Add(Customer Entity)
        {
            await Task.Run(() => _customers.Add(Entity));
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => _customers.Remove(Get(id)));
        }

        public IEnumerable<Customer> Get()
        {
            return _customers;
        }

        public Customer Get(int id)
        {
            return _customers.FirstOrDefault(c => c.Id.Equals(id));
        }

        public Customer Get(Func<Customer, bool> filter)
        {
            return _customers.FirstOrDefault(filter);
        }

        public Task Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
