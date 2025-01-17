using Chilaqueria_API.Datos;
using Microsoft.EntityFrameworkCore;
using static Chilaqueria_API.Models.Globals;
using System.Diagnostics;
using Chilaqueria_API.Handlers;
using static Chilaqueria_API.Models.Chi_Prod_db_Models;

namespace Chilaqueria_API.Repositories
{
   public interface IProductService
    {
        Task<IEnumerable<Prod_Products>> GetAllProductsAsync();
        Task<Prod_Products> GetProductByIdAsync(int id);
        Task AddProductAsync(Prod_Products prod);
        Task UpdateProductAsync(Prod_Products prod);
        Task DeleteProductAsync(int id);
    }

    public class ProductService : IProductService
    {
        private readonly IRepository<Prod_Products> _repository;

        public ProductService(IRepository<Prod_Products> repository)
        {
            _repository = repository;
        }

        public async Task AddProductAsync(Prod_Products prod)
        {
            await _repository.AddAsync(prod);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Prod_Products>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Prod_Products> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateProductAsync(Prod_Products prod)
        {
            await _repository.UpdateAsync(prod);
        }

    }
}
