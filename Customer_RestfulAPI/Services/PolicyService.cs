using Customer_RestfulAPI.Models;

namespace Customer_RestfulAPI.Services
{
    public class PolicyService
    {
        private readonly List<Policy> _policies = new();
        private int _id = 1;

        public List<Policy> GetAll() => _policies;

        public Policy? Get(int id) => _policies.FirstOrDefault(p => p.Id == id);

        public Policy Add(Policy policy)
        {
            policy.Id = _id++;
            _policies.Add(policy);
            return policy;
        }

        public bool Delete(int id) 
        {
            var policy = Get(id);
            if (policy == null)
                return false;
            _policies.Remove(policy);
            return true;
        }

        public bool Update(int id, Policy policy)
        {
            var existPolicy = Get(id);
            if(existPolicy == null)
                return false;

            var props = typeof(Policy).GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(policy);
                if( value != null) 
                    prop.SetValue(existPolicy, value);
            }
            existPolicy.Id = id;
            return true;
        }
    }
}
