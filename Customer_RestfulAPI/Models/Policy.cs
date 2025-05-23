namespace Customer_RestfulAPI.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public string? PolicyNo { get; set; }
        public int ProductNo { get; set; }
        public string? Product { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Customer? Insurer { get; set; }
        public List<Customer>? InsuredList { get; set; }
    }
}
