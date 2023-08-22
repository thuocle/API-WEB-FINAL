using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_WEB_FINAL.Entities
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [JsonIgnore]
        public IEnumerable<Properties>? Properties { get; set; }
        [JsonIgnore]
        public IEnumerable<ProductDetailPropertyDetails>? ProductDetailPropertyDetails { get; set; }
    }
}
