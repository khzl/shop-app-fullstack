using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Wpf.UI.ClientServices.Interfaces;
using Wpf.UI.Shared;
using Wpf.UI.ViewModels.Base;

namespace Wpf.UI.ViewModels.ViewModel.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        // private property
        private readonly IProductClientService _productClientService;

        // public Property ReadOnly 
        public ObservableCollection<Product_Dto> Products { get; } = new(); // ReadOnly 

        // public properties
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }

        // Read Only Property
        public ICommand SearchCommand { get; } // read only
        // public Constructor 
        public ProductsViewModel(IProductClientService productClientService)
        {
            _productClientService = productClientService;
            SearchCommand = new RelayCommand(async _ => await Search());
        }

        // Method Search Products 
        private async Task Search()
        {
            // ensure non-null search argument
            var search = SearchTerm ?? string.Empty;

            var result = await _productClientService.SearchProductsAsync(search, PageNumber, PageSize);

            if (result == null)
                return;

            Products.Clear();

            // guard against null Items
            foreach (var item in result.Items ?? Array.Empty<Product_Dto>())
                Products.Add(item);

            TotalCount = result.TotalCount;
        }
    }
}
