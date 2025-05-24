using Customer_RestfulAPI.Models;
using Customer_RestfulAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            var addedCustomer = await _customer.Add(customer);
            return CreatedAtAction(nameof(Get), new { id = addedCustomer.Id }, addedCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,Customer customer)
        {
            return await _customer.Update(id, customer) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            return await _customer.Delete(id) ? NoContent() : NotFound();
        }
    }
}
