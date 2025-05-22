using Customer_RestfulAPI.Models;

namespace Customer_RestfulAPI.Services
{
    public class CustomerService
    {
        private readonly List<Customer> _customer = new();
        private int _id = 1;

        public List<Customer> GetAll() => _customer;

        public Customer? Get(int id) => _customer.FirstOrDefault(c => c.Id == id);

        public bool Add(Customer customer)
        {
            customer.Id = _id++;
            _customer.Add(customer);
            return true;
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
            if(!_customer.Any(c => c.Id == id))
                return false;
            var willBeUpdatedCustomer = _customer.FirstOrDefault(c => c.Id == id);
            willBeUpdatedCustomer = customer;
            willBeUpdatedCustomer.Id = id;
            return true;
        }
    }
}
