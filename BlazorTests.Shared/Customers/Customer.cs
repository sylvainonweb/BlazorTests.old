using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorTests.Shared
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public int CustomerTypeId { get; set; }

        /// <summary>
        /// Attention, non renseigné lors de la création d'un client
        /// </summary>
        public string CustomerTypeText { get; set; }
    }
}
