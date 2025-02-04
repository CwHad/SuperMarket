using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
	public interface IProductRepository
	{
		void AddProduct(Product product);
		void DeleteProduct(int productId);
		Product? GetProductById(int productId, bool loadCategory = false);
		IEnumerable<Product> GetProducts(bool loadCategory);
		IEnumerable<Product> GetProductsByCategoryId(int categoryId);
		void UpdateProduct(int productId, Product product);
	}
}