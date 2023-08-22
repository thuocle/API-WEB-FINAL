using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_WEB_FINAL.Entities
{
    public class PropertyDetails
    {
        [Key]
        public int PropertyDetailId { get; set; }
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        [JsonIgnore]
        public Properties? Properties { get; set; }
        public string PropertyDetailCode { get; set; }
        public string PropertyDetailDetail { get; set; }
        [JsonIgnore]
        public IEnumerable<ProductDetailPropertyDetails>? ProductDetailPropertyDetails { get; set; }
    }
}
