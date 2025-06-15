using DBLayer.Models;
using DBLayer.VModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer
{
    public class ProductDto
    {
        public VMProduct product { get; set; }
        public IFormFile ImageFile { get; set; }  // For uploaded file
    }
    public class InvoiceDto
    {
        public int? CustomerId { get; set; }

        public string? VehicelNo { get; set; }

        public string? Ewaybill { get; set; }

        [FromForm(Name = "products")]
        public string ProductsJson { get; set; }
        //public List<VMAddInvoiceProduct> products { get; set; }
        public IFormFile ImageFile { get; set; }  // For uploaded file
    }
}
