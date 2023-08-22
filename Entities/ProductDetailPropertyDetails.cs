using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_WEB_FINAL.Entities
{
    public class ProductDetailPropertyDetails
    {
        [Key]
        public int ProductDetailPropertyDetailId { get; set; }
        public int? ProductDetailId { get; set; }
        [ForeignKey("ProductDetailId")]
        [JsonIgnore]
        public ProductDetails? ProductDetails { get; set; }
        public int? PropertyDetailId { get; set; }
        [ForeignKey("PropertyDetailId")]
        [JsonIgnore]
        public PropertyDetails? PropertyDetails { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Products? Products { get; set; }
    }
}
