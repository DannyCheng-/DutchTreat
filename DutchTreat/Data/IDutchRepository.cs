using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
	public interface IDutchRepository
	{
		IEnumerable<Product> GetAllProducts();

		IEnumerable<Product> GetProductByCategory(string category);

		bool SaveAll();
	}
}