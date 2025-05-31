using Customer_RestfulAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Policy> Add(Policy? policy)
        {
            // 1. Insurer işleme alınıyor
            if (policy?.Insurer != null)
            {
                if (policy.Insurer.Id == 0)
                {
                    _context.Customers.Add(policy.Insurer);
                    await _context.SaveChangesAsync();
                }
                policy.InsurerId = policy.Insurer.Id;
                policy.Insurer = null; // circular dependency'yi önlemek için
            }

            // 2. InsuredList işleme alınıyor
            var insuredList = policy!.InsuredList;
            policy!.InsuredList = null; // EF çakışmaması için geçici olarak null'la

            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();

            // 3. Policy ile insuredList ilişkilendiriliyor
            if (insuredList != null && insuredList.Any())
            {
                policy = await _context.Policies
                    .Include(p => p.InsuredList)
                    .FirstOrDefaultAsync(p => p.Id == policy.Id);

                policy!.InsuredList = new List<Customer>();

                foreach (var insured in insuredList)
                {
                    Customer? customer;
                    if (insured.Id == 0)
                    {
                        customer = insured;
                        _context.Customers.Add(customer);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        customer = await _context.Customers.FindAsync(insured.Id);
                    }

                    if (customer != null)
                        policy.InsuredList.Add(customer);
                }

                await _context.SaveChangesAsync();
            }

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
