using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class Customers
    {
        public List<Customer> CustomerList;
        
        public Customers()
        {
            CustomerList = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            CustomerList.Add(customer);
        }
         
        public List<Customer> GetCustomers()
        {
            return CustomerList;
        }
    }
}