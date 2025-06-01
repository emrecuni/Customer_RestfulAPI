using Customer_RestfulAPI.DTO;
using Customer_RestfulAPI.Models;
using Customer_RestfulAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Customer_RestfulAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly PolicyService _policy;
        private readonly CustomerService _customer;

        public PolicyController(PolicyService policyService, CustomerService customerService)
        {
            _policy = policyService;
            _customer = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolicyDto>>> GetAll()
        {
            var temp = await _policy.GetAllAsync();
            return Ok(temp.Select(p => p.ToDto()));
        }

        [HttpGet("{id}")]
        public ActionResult<PolicyDto> Get(int id)
        {
            var policy = _policy.Get(id);

            return policy != null ? Ok(policy) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Policy>> Create([FromBody] PolicyCreateDto dto)
        {

            if (dto == null)
                return BadRequest("Veri boş olamaz.");

            var insurer = _customer.Get((int)dto.InsurerId!);
            if (insurer == null)
                return NotFound("Sigorta ettiren (Insurer) bulunamadı.");

            var insuredList = _customer.GetAll()
                .Where(c => dto.InsuredCustomerIds != null && dto.InsuredCustomerIds.Contains(c.Id))
                .ToList();

            var policy = new Policy
            {
                PolicyNo = dto.PolicyNo,
                ProductNo = dto.ProductNo,
                Product = dto.Product,
                TransactionDate = dto.TransactionDate,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Insurer = insurer,
                InsuredList = insuredList
            };
            var addedPolicy = await _policy.Add(policy);
            var policyDto = addedPolicy.ToDto();

            return CreatedAtAction(nameof(Get), new { id = addedPolicy.Id }, policyDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PolicyCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Veri boş olamaz.");

            var insurer = _customer.Get((int)dto.InsurerId);
            if (insurer == null)
                return NotFound("Sigorta ettiren (Insurer) bulunamadı.");

            var insuredList = _customer.GetAll()
                .Where(c => dto.InsuredCustomerIds != null && dto.InsuredCustomerIds.Contains(c.Id))
                .ToList();


            Policy policy = new Policy
            {
                PolicyNo = dto.PolicyNo,
                ProductNo = dto.ProductNo,
                Product = dto.Product,
                TransactionDate = dto.TransactionDate,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Insurer = insurer,
                InsuredList = insuredList
            };

            return await _policy.Update(id, policy) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _policy.Delete(id) ? NoContent() : NotFound();
        }

        /*jwt ekle*/
    }
}
