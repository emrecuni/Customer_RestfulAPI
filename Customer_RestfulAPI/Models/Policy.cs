using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer_RestfulAPI.Models
{
    public class Policy
    {
        [Key]
        public int Id { get; set; }
        public string? PolicyNo { get; set; }
        public int ProductNo { get; set; }
        public string? Product { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? InsurerId { get; set; }

        [ForeignKey("InsurerId")]
        public Customer? Insurer { get; set; }
        public List<Customer>? InsuredList { get; set; }
    }
}
