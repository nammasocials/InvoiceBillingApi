using DBLayer;
using DBLayer.Models;
using DBLayer.Repository.Interface;
using DBLayer.Repository.Service;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceBilling.Controllers
{
    [ApiController]
    [Route("api/Invoice/")]
    public class InvoiceController : Controller
    {
        private readonly IInvoicService iInvoicService;
        public InvoiceController(IInvoicService _invoicService)
        {
            iInvoicService = _invoicService;
        }
        //////////// Get Http Calls //////////////////
        [HttpGet("fetchAllInvoices")]
        public async Task<IActionResult> fetchAllInvoices()
        {
            var invoices = await iInvoicService.fetchAllInvoices();
            return Ok(invoices);
        }
        [HttpGet("fetchInvoiceById/{invoiceId}")]
        public async Task<IActionResult> fetchInvoiceById(int invoiceId)
        {
            var invoice = await iInvoicService.fetchInvoiceById(invoiceId);
            return Ok(invoice);
        }
        //////////// Post Http Calls//////////////////
        [HttpPost("AddOrEditInvoice")]
        public async Task<IActionResult> AddOrEditInvoice(InvoiceDto invoiceDto)
        {
            byte[] fileData = null;

            if (invoiceDto.ImageFile != null && invoiceDto.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await invoiceDto.ImageFile.CopyToAsync(memoryStream);
                    fileData = memoryStream.ToArray();
                }
            }
            var savedInvoice = await iInvoicService.AddOrEditInvoice(invoiceDto.invoice, invoiceDto.products, fileData);
            return Ok(savedInvoice);
        }
    }
}
