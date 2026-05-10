using Shop.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Wpf.UI.ClientServices.Interfaces;

namespace Wpf.UI.ClientServices.ClientService
{
    public class ProductClientService : IProductClientService
    {
        // private properties 
        private readonly HttpClient _http;

        // public Constructor 
        public ProductClientService(HttpClient http)
        {
            _http = http;
        }

        // Feature 4 -> Get all Products (Products List)
        /*
         PSEUDOCODE / PLAN:
         1. Ensure the Authorization header is set on the HttpClient.
         2. Call GetFromJsonAsync<IEnumerable<Product_Dto>>("api/products") which returns a nullable result.
         3. If the result is null, return an empty sequence to avoid returning null (fixes CS8603).
         4. Otherwise return the fetched sequence.
         */
        // Get All Products
        public async Task<IEnumerable<Product_Dto>> GetProductsAsync()
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.AccessToken);

            var products = await _http.GetFromJsonAsync<IEnumerable<Product_Dto>>("api/products"); // Request URL
            return products ?? Enumerable.Empty<Product_Dto>();
        }

        // Search Products And Pagination 
        public async Task<PagedResult_Dto<Product_Dto>> SearchProductsAsync(
            string search, int page, int size)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", TokenStore.AccessToken);

            var encodedSearch = string.IsNullOrEmpty(search) ? string.Empty : Uri.EscapeDataString(search);
            var result = await _http.GetFromJsonAsync<PagedResult_Dto<Product_Dto>>(
                $"api/products/search?searchTerm={encodedSearch}&pageNumber={page}&pageSize={size}"); // Request URL

            // Fix CS0019: the null-coalescing fallback must be the same type as 'result'.
            // Return an empty PagedResult_Dto when the server returns null.
            return result ?? new PagedResult_Dto<Product_Dto>
            {
                Items = Enumerable.Empty<Product_Dto>(),
                TotalCount = 0
            };
        }

    }
}
