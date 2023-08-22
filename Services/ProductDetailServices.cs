using API_WEB_FINAL.Constant;
using API_WEB_FINAL.Context;
using API_WEB_FINAL.Entities;
using API_WEB_FINAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace API_WEB_FINAL.Services
{
    public class ProductDetailServices : IProductDetailServices
    {

        private readonly AppDbContext dbContext;

        public ProductDetailServices()
        {
            this.dbContext = new AppDbContext();
        }
        private  ProductDetails GetProductDetails(int spdetailID)
        {
             var productDetail = dbContext.ProductDetails
        .Include(sp => sp.ParentProductDetail)
        .Include(sp => sp.ChildProductDetails)
        .FirstOrDefault(pdpd => pdpd.ProductDetailId == spdetailID);
            return productDetail;
        }
        private ErrorMessage checkQuantity(int spdetailID,  int? soluong)
        {
            var sp = dbContext.ProductDetails.FirstOrDefault(x => x.ProductDetailId == spdetailID);
            if (sp.Quantity == 0)
                return ErrorMessage.HetHang;
            if (sp.Quantity < soluong)
                return ErrorMessage.KhongDuSanPham;
            return ErrorMessage.MuaThanhCong;
        }
        private void UpdateQuantity(int spdetailID, int? soluongMinus, int? soluongAdd)
        {
            var sp = GetProductDetails(spdetailID);
            if(soluongMinus.HasValue)
            {
                sp.Quantity -= soluongMinus.Value;
                while (sp.ParentId != null)
                {
                    var psp = dbContext.ProductDetails.FirstOrDefault(x => x.ProductDetailId == sp.ParentId);
                    psp.Quantity -= soluongMinus.Value;
                    dbContext.SaveChanges();
                    sp = psp;
                }
            }
            else if(soluongAdd.HasValue)
            {
                while (sp.ParentId != null)
                {
                    var psp = dbContext.ProductDetails.FirstOrDefault(x => x.ProductDetailId == sp.ParentId);
                    sp.Quantity += soluongAdd.Value;
                    psp.Quantity += soluongAdd.Value;
                    dbContext.SaveChanges();
                    sp = psp;
                }
            }
        }

        public ErrorMessage addProductToCart(int spdetailID, int? soluongMinus)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (checkQuantity(spdetailID, soluongMinus) == ErrorMessage.KhongDuSanPham || checkQuantity(spdetailID, soluongMinus) == ErrorMessage.HetHang)
                        return ErrorMessage.ThatBai;
                    if (GetProductDetails(spdetailID) == null)
                        return ErrorMessage.KhongTonTai;
                    UpdateQuantity(spdetailID, soluongMinus, null);
                    trans.Commit();
                    return ErrorMessage.ThanhCong;
                    // Commit transaction
                }
                catch (Exception)
                {
                    // Nếu có lỗi xảy ra, rollback transaction và ném ra ngoại lệ
                    trans.Rollback();
                    throw;
                }
            }
        }

        public ErrorMessage updateProductQuantity(int spdetailID, int soluong)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var sp = GetProductDetails(spdetailID);
                    if (sp == null)
                        return ErrorMessage.KhongTonTai;

                    sp.Quantity = soluong;
                    var updatedProducts = new List<ProductDetails>() { sp }; // Tạo danh sách các sản phẩm cần cập nhật


                    do
                    {
                        var psp = dbContext.ProductDetails.FirstOrDefault(x => x.ProductDetailId == sp.ParentId);
                        if (psp == null)
                        {
                            break;
                        }
                        psp.Quantity = dbContext.ProductDetails.Where(x => x.ParentId == psp.ProductDetailId).Sum(x => x.Quantity);
                        updatedProducts.Add(psp); // Thêm sản phẩm cha vào danh sách cần cập nhật
                        sp = psp;
                    }
                    while (sp.ParentId != null);
                    dbContext.UpdateRange(updatedProducts); // Cập nhật tất cả các sản phẩm trong danh sách
                    dbContext.SaveChanges();
                    trans.Commit();
                    return ErrorMessage.ThanhCong;
                    // Commit transaction
                }
                catch (Exception)
                {
                    // Nếu có lỗi xảy ra, rollback transaction và ném ra ngoại lệ
                    trans.Rollback();
                    throw;
                }
            }
        }

        public PageInfo<ProductDetails> getAllProductDetail(Pagination page)
        {
            var query = dbContext.ProductDetails
            .Where(x => !dbContext.ProductDetails.Any(y => y.ParentId == x.ProductDetailId)).AsQueryable();
            var data = PageInfo<ProductDetails>.ToPageInfo(page, query);
            page.TotalItem = query.Count();
            return new PageInfo<ProductDetails>(page, data);
        }
    }
}
