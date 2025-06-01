namespace Customer_RestfulAPI.DTO
{
    public class PolicyCreateDto
    {
        public string? PolicyNo { get; set; }
        public int ProductNo { get; set; }
        public string? Product { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? InsurerId { get; set; }
        public List<int>? InsuredCustomerIds { get; set; }
    }
}
