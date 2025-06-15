using DBLayer.Models;
using DBLayer.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Repository.Interface
{
    public interface ICustomerService
    {
        public Task<List<VMCustomer>> fetchAllCustomers();
        public Task<VMCustomer> fetchCustomerById(int customerId);
        public Task<VMCustomer> fetchCustomerByNo(string customerNo);
        public Task<VMCustomer> AddOrEditCustomer(VMCustomer customer);
    }
}
