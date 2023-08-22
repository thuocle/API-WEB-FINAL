using API_WEB_FINAL.Entities;
using API_WEB_FINAL.IServices;
using API_WEB_FINAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WEB_FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductDetailServices _productServices;
        public ProductController()
        {
            _productServices = new ProductDetailServices();
        }
        [HttpPut("checkoutProduct")]
        public IActionResult checkoutProduct([FromQuery] int spID, [FromQuery] int sl)
        {
            var ret = _productServices.addProductToCart(spID, sl);
            if(ret == Constant.ErrorMessage.ThanhCong)
            {
                return Ok("Mua thanh cong");
            }
            return BadRequest("Mua that bai");
        }
        [HttpPut("updateProductQuantity")]
        public IActionResult updateProductQuantity([FromQuery] int spID, [FromQuery] int sl)
        {
            var ret = _productServices.updateProductQuantity(spID, sl);
            if(ret == Constant.ErrorMessage.ThanhCong)
            {
                return Ok("Cap nhat thanh cong");
            }
            return BadRequest("Cap nhat that bai");
        } 
        [HttpGet("getAllProductDetail")]
        public IActionResult getAllProductDetail([FromQuery] Pagination page)
        {
                return Ok(_productServices.getAllProductDetail(page));
        }

    }
}
