using DBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Repository.Interface
{
    public interface ICustomerService
    {
        public Task<List<Customer>> fetchAllCustomers();
        public Task<Customer> fetchCustomerById(int customerId);
        public Task<Customer> fetchCustomerByNo(string customerNo);
        public Task<Customer> AddOrEditCustomer(Customer customer);
    }
}
