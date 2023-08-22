using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace API_WEB_FINAL.Entities
{
    public class ProductDetails
    {
        [Key]
        public int ProductDetailId { get; set; }
        public string ProductDetailName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double ShellPrice { get; set; }
        public int? ParentId { get; set; }
        public ProductDetails ParentProductDetail { get; set; }
        [JsonIgnore]
        public IEnumerable<ProductDetails> ChildProductDetails { get; set; }
        [JsonIgnore]
        public IEnumerable<ProductDetailPropertyDetails> ProductDetailPropertyDetails { get; set; } 

    }
}
