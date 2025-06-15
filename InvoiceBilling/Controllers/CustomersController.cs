using DBLayer.Models;
using DBLayer.Repository.Interface;
using DBLayer.VModels;
using Microsoft.AspNetCore.Mvc;
using System.Windows.Input;

namespace InvoiceBilling.Controllers
{
    [ApiController]
    [Route("api/Customer/")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService iCustomerService;
        public CustomersController(ICustomerService _customerService)
        {
            iCustomerService = _customerService;
        }
        //////////// Get Http Calls //////////////////
        [HttpGet("fetchAllCustomers")]
        public async Task<IActionResult> fetchAllCustomers()
        {
            var customers = await iCustomerService.fetchAllCustomers();
            return Ok(customers);
        }
        [HttpGet("fetchCustomerById/{customerId}")]
        public async Task<IActionResult> fetchCustomerById(int customerId)
        {
            var customers = await iCustomerService.fetchCustomerById(customerId);
            return Ok(customers);
        }
        [HttpGet("fetchCustomerByNo/{customerNo}")]
        public async Task<IActionResult> fetchCustomerByNo(string customerNo)
        {
            var customers = await iCustomerService.fetchCustomerByNo(customerNo);
            return Ok(customers);
        }


        //////////// Post Http Calls//////////////////
        [HttpPost("AddOrEditCustomer")]
        public async Task<IActionResult> AddOrEditCustomer(VMCustomer customer)
        {
            var savedCustomer = await iCustomerService.AddOrEditCustomer(customer);
            return Ok(savedCustomer);
        }
    }
}
