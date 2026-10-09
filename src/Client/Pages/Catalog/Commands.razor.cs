using BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.AddCommandCommand;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetAllCommands;
using BlazorHero.CleanArchitecture.Client.Extensions;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Catalog.Command;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Pages.Catalog
{
    public partial class Commands
    {
        [Inject] private ICommandManager CommandManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }
        private List<GetAllCommandsResponse> _commandList = new();
        //private GetCommandByIdResponse _commandSelected = new();
        private GetAllCommandsResponse _command = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateCommands;
        private bool _canEditCommands;
        private bool _canDeleteCommands;
        private bool _canExportCommands;
        private bool _canSearchCommands;
        private bool _canImportCommands;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateCommands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Commands.Create)).Succeeded;
            _canEditCommands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Commands.Edit)).Succeeded;
            _canDeleteCommands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Commands.Delete)).Succeeded;
            _canExportCommands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Commands.Export)).Succeeded;
            _canSearchCommands = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Commands.Search)).Succeeded;

            await GetCommandsAsync();
            _loaded = true;

            HubConnection = HubConnection.TryInitialize(_navigationManager, _localStorage);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetCommandsAsync()
        {
            var response = await CommandManager.GetCommandsAsync();
            if (response.Succeeded)
            {
                _commandList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, MudBlazor.Severity.Error);
                }
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = _localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                { nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id) }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await CommandManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        private bool Search(GetAllCommandsResponse command)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (command.ClientName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return command.Date.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true;
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var _commandSelected = CommandManager.GetCommandByIdAsync(id).Result.Data;
                if (_commandSelected != null)
                {
                    parameters.Add(nameof(AddEditCommandModal.AddEditCommandModel), new AddEditCommandCommand
                    {
                        Command = new Command
                        {
                            Id = id,
                            //ClientName = _commandSelected.ClientName,
                            Description = _commandSelected.Description,
                            Date = _commandSelected.Date,
                            DeliveryAddress = _commandSelected.DeliveryAddress,
                        },
                        CommandDetails = _commandSelected.ProductsList
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditCommandModal>(id == 0 ? _localizer["Add"] : _localizer["Edit"], parameters, options);
            //var dialog = _dialogService.Show<AddEditCommandModal>(id == 0 ? "Add" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _command = new GetAllCommandsResponse();
            await GetCommandsAsync();
        }
    }
}
