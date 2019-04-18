using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorTests.Client.Controls;
using BlazorTests.Client.Services;
using BlazorTests.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;

namespace BlazorTests.Client
{
    public class CustomerEditComponent : EditComponentBase
    {
        #region Services

        [Inject]
        protected AdministrationService AdministrationService { get; set; }

        [Inject]
        protected CustomerService CustomerService { get; set; }

        #endregion

        #region Propriétés bindées

        [RequiredEx("Nom")]
        public string Name { get; set; }

        [RequiredEx("Prénom")]
        public string FirstName { get; set; }

        [RequiredEx("Type")]
        public int CustomerTypeId { get; set; }

        protected IList<SelectListItem> CustomerTypes { get; set; } = new List<SelectListItem>();

        #endregion

        #region Initialisation

        protected override async Task OnParametersSetAsync()
        {
            this.CustomerTypes = SelectListItems.Convert(await AdministrationService.GetCustomerTypes(), (src, dest) =>
            {
                dest.Id = src.Id;
                dest.Text = src.Text;
            });


            if (IsNew())
            {
                this.Title = "Ajouter un client";             
            }
            else
            {
                this.Title = "Modifier un client";

                Customer customer = await CustomerService.GetCustomer(Id.Value);
                this.Name = customer.Name;
                this.FirstName = customer.FirstName;
                this.CustomerTypeId = customer.CustomerTypeId;
            }
        }

        #endregion

        #region Sauvegarde

        protected override async Task Save()
        {
            Customer customer = null;
            if (IsNew())
            {
                customer = new Customer();         
            }
            else
            {
                customer = await CustomerService.GetCustomer(Id.Value);
            }

            customer.Name = this.Name;
            customer.FirstName = this.FirstName;
            customer.CustomerTypeId = this.CustomerTypeId;

            await CustomerService.SaveCustomer(customer);
            UriHelper.NavigateTo("/Customer");
        }

        protected override void Close()
        {
            UriHelper.NavigateTo("/Customer");
        }

        #endregion
    }
}


