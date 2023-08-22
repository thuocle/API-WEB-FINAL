using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_WEB_FINAL.Entities
{
    public class Properties
    {
        [Key]
        public int PropertyId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Products? Products { get; set; }
        public string PropertyName { get; set; }
        public int PropertySort { get; set; }
        [JsonIgnore]
        public IEnumerable<PropertyDetails>? PropertyDetails { get; set; }
    }
}
