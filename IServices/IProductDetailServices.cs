using API_WEB_FINAL.Constant;
using API_WEB_FINAL.Entities;

namespace API_WEB_FINAL.IServices
{
    public interface IProductDetailServices
    {
        ErrorMessage addProductToCart(int spdetailID, int? soluongMinus);
        ErrorMessage updateProductQuantity(int spdetailID, int soluong);
        PageInfo<ProductDetails> getAllProductDetail(Pagination page);
    }
}
