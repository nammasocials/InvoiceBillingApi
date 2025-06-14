using DBLayer;
using DBLayer.Models;
using DBLayer.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceBilling.Controllers
{
    [ApiController]
    [Route("api/Product/")]
    public class ProductController : Controller
    {
        private readonly IProductService iProductService;
        public ProductController(IProductService _productService)
        {
            iProductService = _productService;
        }
        //////////// Get Http Calls //////////////////
        [HttpGet("fetchAllProducts")]
        public async Task<IActionResult> fetchAllProducts()
        {
            var products = await iProductService.fetchAllProducts();
            return Ok(products);
        }
        [HttpGet("fetchProductById/{productId}")]
        public async Task<IActionResult> fetchProductById(int productId)
        {
            var products = await iProductService.fetchProductById(productId);
            return Ok(products);
        }
        [HttpGet("fetchProductByNo/{customerNo}")]
        public async Task<IActionResult> fetchProductByNo(string productNo)
        {
            var products = await iProductService.fetchProductByNo(productNo);
            return Ok(products);
        }


        //////////// Post Http Calls//////////////////
        [HttpPost("AddOrEditProduct")]
        public async Task<IActionResult> AddOrEditProduct([FromForm] ProductDto productDto)
        {
            byte[] fileData = null;

            if (productDto.ImageFile != null && productDto.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await productDto.ImageFile.CopyToAsync(memoryStream);
                    fileData = memoryStream.ToArray();
                }
            }
            var savedProduct = await iProductService.AddOrEditProduct(productDto.product, fileData);
            return Ok(savedProduct);
        }
    }
}
