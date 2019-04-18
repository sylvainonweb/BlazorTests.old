using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorTests.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using BlazorTests.Client.Services;
using BlazorTests.Client.Controls;

namespace BlazorTests.Client
{
    public class CustomerTypeListComponent : ComponentBase
    {
        #region Services

        [Inject]
        protected AdministrationService AdministrationService { get; set; }

        #endregion

        #region Propriétés bindées

        protected IList<CustomerType> CustomerTypes { get; set; } = new List<CustomerType>();

        #endregion

        #region Initialisation

        protected override async Task OnInitAsync()
        {
            this.Title = "Types de client";
            await LoadCustomerTypes();

            await JavascriptFunctions.InitTable("customerTypeTable");
        }
        
        #endregion

        #region  Gestion des types de clients

        protected void AddCustomerType()
        {
            UriHelper.NavigateTo($"/Administration/CustomerType/Edit");
        }

        protected void EditCustomerType(int id)
        {
            UriHelper.NavigateTo($"/Administration/CustomerType/Edit/{id}");
        }

        protected async Task DeleteCustomerType(int customerTypeId)
        {
            await AdministrationService.DeleteCustomerType(customerTypeId);
            await JavascriptFunctions.ShowAlert("Suppression effectuée");

            await LoadCustomerTypes();
        }

        protected async Task LoadCustomerTypes()
        {
            this.CustomerTypes = await AdministrationService.GetCustomerTypes();
            StateHasChanged();
        }

        #endregion
    }
}
