using Customer_RestfulAPI.Models;

namespace Customer_RestfulAPI.DTO
{
    public static class DtoMapper
    {
        public static PolicyDto ToDto(this Policy policy)
        {
            return new PolicyDto
            {
                Id = policy.Id,
                PolicyNo = policy.PolicyNo,
                ProductNo = policy.ProductNo,
                Product = policy.Product,
                TransactionDate = policy.TransactionDate,
                StartDate = policy.StartDate,
                EndDate = policy.EndDate,
                Insurer = policy.Insurer != null ? new CustomerDto
                {
                    Id = policy.Insurer.Id,
                    Name = policy.Insurer.Name,
                    Surname = policy.Insurer.Surname,
                    IdentityNo = policy.Insurer.IdentityNo,
                    Email = policy.Insurer.Email,
                    Address = policy.Insurer.Address
                } : null,
                InsuredList = policy.InsuredList?.Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Surname = c.Surname,
                    IdentityNo = c.IdentityNo,
                    Email = c.Email,
                    Address = c.Address
                }).ToList()
            };
        }
    }

}
