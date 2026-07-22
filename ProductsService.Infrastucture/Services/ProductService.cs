using ProductsService.Application.Interfaces;
using ProductsService.Domain.Entities;

namespace ProductsService.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existing = await _repository.GetByIdAsync(product.ProductId);

            if (existing == null)
                return false;

            existing.ProductName = product.ProductName;
            existing.Quantity = product.Quantity;
            existing.Price = product.Price;
            existing.Unit = product.Unit;
            existing.Description = product.Description;
            existing.IsActive = product.IsActive;
            existing.ModifiedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            await _repository.DeleteAsync(product);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
