using AutoMapper;
using DBLayer.Models;
using DBLayer.Repository.Interface;
using DBLayer.VModels;
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
        private readonly IMapper iMapper;
        public CustomerService(InvoiceBillingContext context, IMapper _mapper) 
        {
            _context = context;
            iMapper = _mapper;
        }
        /// <summary>
        /// // Fetch Services
        /// </summary>
        /// <returns></returns>
        public async Task<List<VMCustomer>> fetchAllCustomers()
        {
            var contextCustomer = await _context.Customers.Where(C => C.IsActive == true).ToListAsync();
            return iMapper.Map<List<VMCustomer>>(contextCustomer);
        }
        public async Task<VMCustomer> fetchCustomerById(int customerId)
        {
            var contextCustomer = await _context.Customers.Where(C => C.IsActive == true && C.CustomerId == customerId).FirstOrDefaultAsync();
            return iMapper.Map<VMCustomer>(contextCustomer);
        }
        public async Task<VMCustomer> fetchCustomerByNo(string customerNo)
        {
            var contextCustomer = await _context.Customers.Where(C => C.IsActive == true && C.CustomerNo == customerNo).FirstOrDefaultAsync();
            return iMapper.Map<VMCustomer>(contextCustomer);
        }
        /// <summary>
        /// // Add Or Edit Services
        /// </summary>
        /// <returns></returns>
        public async Task<VMCustomer> AddOrEditCustomer(VMCustomer customer)
        {
            var mappedCustomer = iMapper.Map<Customer>(customer);
            var existingCustomer = await _context.Customers
                    .FirstOrDefaultAsync(a => a.CustomerId == mappedCustomer.CustomerId);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerName = mappedCustomer.CustomerName;
                existingCustomer.ContactNo = mappedCustomer.ContactNo;
                existingCustomer.EmailId = mappedCustomer.EmailId;
                existingCustomer.Gstno = mappedCustomer.Gstno;
                existingCustomer.Address = mappedCustomer.Address;
                existingCustomer.City = mappedCustomer.City;
                existingCustomer.State = mappedCustomer.State;
                existingCustomer.PinCode = mappedCustomer.PinCode;
                existingCustomer.MId = 0;
                existingCustomer.MTime = DateTime.Now;
                _context.Customers.Update(existingCustomer);
                await _context.SaveChangesAsync();
                var updatedCustomer = iMapper.Map<VMCustomer>(existingCustomer);
                return updatedCustomer;
            }
            else
            {
                var newCustomer = new Customer
                {
                    CustomerName = mappedCustomer.CustomerName,
                    ContactNo = mappedCustomer.ContactNo,
                    EmailId = mappedCustomer.EmailId,
                    Gstno = mappedCustomer.Gstno,
                    Address = mappedCustomer.Address,
                    City = mappedCustomer.City,
                    State = mappedCustomer.State,
                    PinCode = mappedCustomer.PinCode,
                    CId = 0,
                    CTime = DateTime.Now,
                };
                await _context.Customers.AddAsync(newCustomer);
                await _context.SaveChangesAsync();
                var insertedCustomer = iMapper.Map<VMCustomer>(newCustomer);
                return insertedCustomer;
            }
        }
    }
}
