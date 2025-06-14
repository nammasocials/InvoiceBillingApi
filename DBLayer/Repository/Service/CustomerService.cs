using DBLayer.Models;
using DBLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Repository.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly InvoiceBillingContext _context;
        public CustomerService(InvoiceBillingContext context) 
        {
            _context = context;
        }
        /// <summary>
        /// // Fetch Services
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> fetchAllCustomers()
        {
            return await _context.Customers.Where(C => C.IsActive == true).ToListAsync();
        }
        public async Task<Customer> fetchCustomerById(int customerId)
        {
            return await _context.Customers.Where(C => C.IsActive == true && C.CustomerId == customerId).FirstOrDefaultAsync();
        }
        public async Task<Customer> fetchCustomerByNo(string customerNo)
        {
            return await _context.Customers.Where(C => C.IsActive == true && C.CustomerNo == customerNo).FirstOrDefaultAsync();
        }
        /// <summary>
        /// // Add Or Edit Services
        /// </summary>
        /// <returns></returns>
        public async Task<Customer> AddOrEditCustomer(Customer customer)
        {
            var existingCustomer = await _context.Customers
                    .FirstOrDefaultAsync(a => a.CustomerId == customer.CustomerId);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.ContactNo = customer.ContactNo;
                existingCustomer.EmailId = customer.EmailId;
                existingCustomer.Gstno = customer.Gstno;
                existingCustomer.Address = customer.Address;
                existingCustomer.City = customer.City;
                existingCustomer.State = customer.State;
                existingCustomer.PinCode = customer.PinCode;
                existingCustomer.MId = 0;
                existingCustomer.MTime = DateTime.Now;
                _context.Customers.Update(existingCustomer);
                await _context.SaveChangesAsync();

                return existingCustomer;
            }
            else
            {
                var newCustomer = new Customer
                {
                    CustomerName = customer.CustomerName,
                    ContactNo = customer.ContactNo,
                    EmailId = customer.EmailId,
                    Gstno = customer.Gstno,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    PinCode = customer.PinCode,
                    CId = 0,
                    CTime = DateTime.Now,
                };
                await _context.Customers.AddAsync(newCustomer);
                await _context.SaveChangesAsync();

                return newCustomer;
            }
        }
    }
}
