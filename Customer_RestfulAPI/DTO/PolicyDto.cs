namespace Customer_RestfulAPI.DTO
{
    public class PolicyDto
    {
        public int Id { get; set; }
        public string? PolicyNo { get; set; }
        public int ProductNo { get; set; }
        public string? Product { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public CustomerDto? Insurer { get; set; }
        public List<CustomerDto>? InsuredList { get; set; }
    }

}
