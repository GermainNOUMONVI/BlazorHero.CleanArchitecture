using Blazored.FluentValidation;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.AddCommandCommand;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetCommandsById;
using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllProduct;
using BlazorHero.CleanArchitecture.Application.Requests.Identity;
using BlazorHero.CleanArchitecture.Client.Extensions;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Catalog.Command;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Catalog.Product;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Identity.Users;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Pages.Catalog
{
    public partial class AddEditCommandModal
    {
        [Inject] private ICommandManager CommandManager { get; set; }
        [Inject] private IProductManager ProductManager { get; set; }
        [Inject] private IUserManager UserManager { get; set; }

        [Parameter] public AddEditCommandCommand AddEditCommandModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllProductResponse> _productsList = new();
        private string Email { get; set; } = string.Empty;
        private bool IsAlreadyClient { get; set; } = false;
        private RegisterRequest NewClient { get; set; } = new();

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager, _localStorage);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            if (!IsAlreadyClient)
            {
                //await AddNewClient();
            }
            AddEditCommandModel.Command.ClientEmail = Email;
            var response = await CommandManager.SaveAsync(AddEditCommandModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
            }
        }

        public async Task AddNewClient()
        {
            NewClient.Email = Email;
            NewClient.AutoConfirmEmail = true;
            NewClient.UserName = Email.Split('@')[0] + NewClient.PhoneNumber;
            var response = await UserManager.RegisterUserAsync(NewClient);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task GetProductAsync()
        {
            var response = await ProductManager.GetProductsAsync();
            if (response.Succeeded)
            {
                _productsList = response.Data.ToList();
            }
            else
            {
                foreach(var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        public void AddQuantity(CommandDetailsResponse commandDetailsResponse)
        {
            commandDetailsResponse.Quantity += 1;
        }
        
        public void DecreaseQuantity(CommandDetailsResponse commandDetailsResponse)
        {
            if(commandDetailsResponse.Quantity > 1) 
                commandDetailsResponse.Quantity -= 1;
        }

        private async Task LoadDataAsync()
        {
            await Task.CompletedTask;
        }
    }
}
