using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MarketLearning.Models.Entities;
using MarketLearning.Repositories;

namespace MarketLearning.Services
{
    public class ProductService
    {

        private ProductRepository repository = new ProductRepository();
        public async Task AddProduct(Product toAdd)
        {
            await repository.AddAsync(toAdd);
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await repository.GetAllAsync();
        }

        public async Task DeleteProduct(int id)
        {
            await repository.RemoveAsync(id);
        }

        public async Task<List<Product>> GetProductByName(string name)
        {
            return await repository.GetByPropertyAsync(em => em.Name.Equals(name));
        }

        public async Task<Product> GetProductByID(int? id)
        {
            return await repository.GetByIDAsync(id);
        }

        public Product GetProductByIDSync(int? id)
        {
            return repository.GetByID(id);
        }

        public async Task SaveUpdatedState(Product toUpdate)
        {
            await repository.UpdateStateAsync(toUpdate);
        }
    }
}