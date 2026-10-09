using BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.AddCommandCommand;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetCommandsById;
using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllProduct;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Catalog.Command;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Catalog.Product;
using BlazorHero.CleanArchitecture.Client.Pages.Catalog;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Pages.Customer
{
    public partial class Products
    {
        [Inject] private IProductManager ProductManager { get; set; }
        [Inject] private ICommandManager CommandManager { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        private List<GetAllProductResponse> _productList { get; set; } = new();
        private string _domain = "https://localhost:44390/";
        private List<CommandDetailsResponse> _basket = new();
        private HashSet<string> _basketNames = new();

        protected override async Task OnInitializedAsync()
        {
            await GetProductsAsync();
        }

        private void AddToBasket(GetAllProductResponse product)
        {
            if(_basketNames.Add(product.Name))
                _basket.Add(new CommandDetailsResponse { ProductId = product.Id, ProductName = product.Name, ImageDataURL = product.ImageDataURL, Quantity = 1 });
        }
        
        private void ShowAlert()
        {
            JSRuntime.InvokeVoidAsync("alert", "Basket icon");
            Console.WriteLine("console");
        }
        
        private void InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            parameters.Add(nameof(AddEditCommandModal.AddEditCommandModel), new AddEditCommandCommand { Command = new(), CommandDetails = _basket });
            var options = new DialogOptions { CloseButton = true, MaxWidth= MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCommandModal>("Votre Commande", parameters, options);
        }

        private async Task GetProductsAsync()
        {
            var response = await ProductManager.GetProductsAsync();
            if (response.Succeeded)
            {
                _productList = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}
