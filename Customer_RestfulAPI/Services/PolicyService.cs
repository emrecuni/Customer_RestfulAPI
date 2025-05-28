using Customer_RestfulAPI.Models;
using System.Threading.Tasks;

namespace Customer_RestfulAPI.Services
{
    public class PolicyService
    {
        private readonly DataContext _context;

        public PolicyService(DataContext context)
        {
            _context = context;
        }

        public List<Policy> GetAll() => _context.Policies.ToList();

        public Policy? Get(int id) => _context.Policies.FirstOrDefault(p => p.Id == id);

        public async Task<Policy> Add(Policy policy)
        {
            //Policy tempPolicy = new();
            //tempPolicy.PolicyNo = policy.PolicyNo;
            //tempPolicy.ProductNo = policy.ProductNo;
            //tempPolicy.Product = policy.Product;
            //tempPolicy.TransactionDate = policy.TransactionDate;
            //tempPolicy.StartDate = policy.StartDate;
            //tempPolicy.EndDate = policy.EndDate;
            //tempPolicy.Insurer = policy.Insurer;

            _context.Policies.Add(policy);

            await _context.SaveChangesAsync();
            return policy;
        }

        public async Task<bool> Delete(int id)
        {
            var policy = Get(id);
            if (policy == null)
                return false;
            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, Policy policy)
        {
            var existPolicy = Get(id);
            if (existPolicy == null)
                return false;

            var props = typeof(Policy).GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(policy);
                if (value != null)
                    prop.SetValue(existPolicy, value);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
