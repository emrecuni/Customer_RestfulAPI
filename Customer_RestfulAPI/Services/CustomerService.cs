using Customer_RestfulAPI.Models;
using System.Reflection;
using System.Threading.Tasks;

namespace Customer_RestfulAPI.Services
{
    public class CustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public List<Customer> GetAll() => _context.Customers.ToList();

        public Customer? Get(int id) => _context.Customers.FirstOrDefault(c => c.Id == id);

        public async Task<Customer> Add(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> Delete(int id) 
        {
            if(!_context.Customers.Any(c => c.Id == id))
                return false;
            _context.Customers.Remove(Get(id)!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, Customer customer) 
        {
            var willBeUpdatedCustomer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (willBeUpdatedCustomer == null)
                return false;

            var props = typeof(Customer).GetProperties();

            foreach (var prop in props)
            {
                var newValue = prop.GetValue(customer);
                if (newValue != null)
                    prop.SetValue(willBeUpdatedCustomer, newValue);
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
