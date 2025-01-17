using static Chilaqueria_API.Models.Chi_Prod_db_Models;

namespace Chilaqueria_API.Repositories
{
    public interface IAccountService
    {
        Task<IEnumerable<Prod_Users>> GetAllUsersAsync();
        Task<Prod_Users> GetUserByIdAsync(int id);
        Task AddUserAsync(Prod_Users user);
        Task UpdateUserAsync(Prod_Users user);
        Task DeleteUserAsync(int id);

        Task LoginAsync();
    }
    public class AccountService : IAccountService
    {
        private readonly IRepository<Prod_Users> _repository;

        public AccountService(IRepository<Prod_Users> repository)
        {
            _repository = repository;
        }

        public async Task AddUserAsync(Prod_Users user)
        {
            await _repository.AddAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Prod_Users>> GetAllUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Prod_Users> GetUserByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task LoginAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(Prod_Users user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}
