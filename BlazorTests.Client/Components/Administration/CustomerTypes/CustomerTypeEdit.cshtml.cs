using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorTests.Client.Controls;
using BlazorTests.Client.Services;
using BlazorTests.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;

namespace BlazorTests.Client
{
    public class CustomerTypeEditComponent : EditComponentBase
    {
        #region Services

        [Inject]
        protected AdministrationService AdministrationService { get; set; }

        #endregion

        #region Propriétés bindées

        [RequiredEx("Libellé")]
        public string Text { get; set; }

        #endregion

        #region Initialisation

        protected override async Task OnParametersSetAsync()
        {
            if (IsNew())
            {
                this.Title = "Ajouter un type de client";             
            }
            else
            {
                this.Title = "Modifier un type de client";

                CustomerType customerType = await AdministrationService.GetCustomerType(Id.Value);
                this.Text = customerType.Text;
            }
        }

        #endregion

        #region Sauvegarde

        protected override async Task Save()
        {
            IList<string> errors = CheckRequiredFields();
            if (errors.Count > 0)
            {
                await JavascriptFunctions.ShowAlert(errors[0]);
            }

            CustomerType customerType = null;
            if (IsNew())
            {
                customerType = new CustomerType();         
            }
            else
            {
                customerType = await AdministrationService.GetCustomerType(Id.Value);
            }

            customerType.Text = this.Text;

            await AdministrationService.SaveCustomerType(customerType);
            UriHelper.NavigateTo("/Administration/CustomerType");
        }

        protected override void Close()
        {
            UriHelper.NavigateTo("/Administration/CustomerType");
        }

        #endregion
    }
}


