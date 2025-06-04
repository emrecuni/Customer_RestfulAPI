using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Customer_RestfulAPI.Models
{
    public class Customer
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? IdentityNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        [JsonIgnore]
        public List<Policy>? PoliciesAsInsured { get; set; } // Many-to-many için
        [JsonIgnore]
        public List<Policy>? PoliciesAsInsurer { get; set; } // One-to-many
    }
}
