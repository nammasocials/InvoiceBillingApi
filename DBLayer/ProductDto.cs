using DBLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer
{
    public class ProductDto
    {
        public Product product { get; set; }
        public IFormFile ImageFile { get; set; }  // For uploaded file
    }
}
