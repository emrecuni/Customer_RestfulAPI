using Customer_RestfulAPI.Models;
using Customer_RestfulAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customer_RestfulAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly PolicyService _policy;

        public PolicyController(PolicyService policyService)
        {
            _policy = policyService;
        }

        [HttpGet]
        public ActionResult<List<Policy>> GetAll() => _policy.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Policy> Get(int id)
        {
            var policy = _policy.Get(id);

            return policy != null ? Ok(policy) : NotFound();
        }

        [HttpPost]
        public ActionResult<Policy> Create(Policy policy)
        {
            var addedPolicy = _policy.Add(policy);
            return CreatedAtAction(nameof(Get), new { id = addedPolicy.Id }, addedPolicy);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Policy policy)
        {
            return _policy.Update(id, policy) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _policy.Delete(id) ? NoContent() : NotFound();
        }
    }
}
