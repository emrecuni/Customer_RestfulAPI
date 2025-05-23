using Customer_RestfulAPI.Models;
using System.Reflection;

namespace Customer_RestfulAPI.Services
{
    public class CustomerService
    {
        private readonly List<Customer> _customer = new();
        private int _id = 1;

        public List<Customer> GetAll() => _customer;

        public Customer? Get(int id) => _customer.FirstOrDefault(c => c.Id == id);

        public Customer Add(Customer customer)
        {
            customer.Id = _id++;
            _customer.Add(customer);
            return customer;
        }

        public bool Delete(int id) 
        {
            if(!_customer.Any(c => c.Id == id))
                return false;
            _customer.Remove(Get(id)!);
            return true;
        }

        public bool Update(int id, Customer customer) 
        {
            var willBeUpdatedCustomer = _customer.FirstOrDefault(c => c.Id == id);
            if (willBeUpdatedCustomer == null)
                return false;

            var props = typeof(Customer).GetProperties();

            foreach (var prop in props)
            {
                var newValue = prop.GetValue(customer);
                if (newValue != null)
                    prop.SetValue(willBeUpdatedCustomer, newValue);
            }

            willBeUpdatedCustomer.Id = id;
            return true;
        }
    }
}
