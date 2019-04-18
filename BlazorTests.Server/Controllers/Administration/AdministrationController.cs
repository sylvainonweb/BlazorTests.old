using BlazorTests.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorTests.Server.Entities.EntityClasses;
using BlazorTests.Server.Entities.FactoryClasses;
using BlazorTests.Server.Entities.HelperClasses;
using SD.LLBLGen.Pro.QuerySpec;
using BlazorTests.Common.Technical.Database;

namespace BlazorTests.Server.Controllers
{
    [Route("api/[controller]")]
    public class AdministrationController : ControllerEx
    {
        public AdministrationController(IRepository repository)
        {
            this.Repository = repository;
        }

        #region Gestion des types de client

        [HttpGet("CustomerType")]
        public IList<CustomerType> GetCustomerTypes()
        {
            var query = GetCustomerTypesQuery();
            return Repository.FetchQuery(query);
        }

        private DynamicQuery<CustomerType> GetCustomerTypesQuery()
        {
            QueryFactory qf = new QueryFactory();
            var query = qf.CustomerType
                .Select(() => new CustomerType
                {
                    Id = CustomerTypeFields.Id.ToValue<int>(),
                    Text = CustomerTypeFields.Text.ToValue<string>(),
                });
            return query;
        }

        [HttpGet("CustomerType/{customerTypeId:int}")]
        public CustomerType GetCustomerType(int customerTypeId)
        {
            var query = GetCustomerTypesQuery();
            query = query.Where(CustomerTypeFields.Id == customerTypeId);
            return Repository.FetchFirst(query);
        }

        [HttpDelete("CustomerType/{customerTypeId:int}")]
        public void DeleteCustomerType(int customerTypeId)
        {
            CustomerTypeEntity customerType = Repository.Get<CustomerTypeEntity, int>(customerTypeId);
            Repository.Delete(customerType);
        }

        [HttpPost("CustomerType/Save")]
        public void SaveCustomerType([FromBody] CustomerType customerType)
        {
            CustomerTypeEntity customerTypeEntity = Repository.Get<CustomerTypeEntity, int>(customerType.Id, null, false);
            if (customerTypeEntity == null)
            {
                customerTypeEntity = new CustomerTypeEntity();
            }

            customerTypeEntity.Text = customerType.Text;

            Repository.Save(customerTypeEntity);
        }

        #endregion
    }
}
