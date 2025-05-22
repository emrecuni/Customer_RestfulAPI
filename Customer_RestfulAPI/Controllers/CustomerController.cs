using Customer_RestfulAPI.Models;
using Customer_RestfulAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customer_RestfulAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customer;

        public CustomerController(CustomerService customer)
        {
            _customer = customer;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAll() => _customer.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Customer?> Get(int id ) => _customer.Get(id);

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {

        }
    }
}
