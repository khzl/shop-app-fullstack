using Shop.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wpf.UI.ClientServices.Interfaces
{
    public interface IProductClientService
    {
        // Contracts

        // Get All Products
        public Task<IEnumerable<Product_Dto>> GetProductsAsync();

        // Search And Pagination Products 
        public Task<PagedResult_Dto<Product_Dto>> SearchProductsAsync(
            string search, int page, int size);
    }
}
