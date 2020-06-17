using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grundfos.GiC.Shared.Azure.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TableStorage.Commands;
using TableStorage.Models;

namespace TableStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly ITableStorageRepository<PolicyServerMap> _policyServerModelRepository;

        public PolicyController(ITableStorageRepository<PolicyServerMap> policyServerModelRepository)
        {
            _policyServerModelRepository = policyServerModelRepository;
        }

        [HttpGet("{policyId}")]
        public async Task<IActionResult> GetPolicy(string policyId)
        {

            return Ok("hello");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePolicy policy)
        {
            var id = Guid.NewGuid();
            var appId = "9fa3d2ec-8495-4067-919d-a111d301df2b";

            var policyId = 2; //PS REST, create policy and return policyId.

            var dto = new PolicyServerMap(id, policyId, appId);

            var response = await _policyServerModelRepository.InsertAsync(dto);

            if (response.IsFailure)
            {
                return BadRequest();
            }

            return Ok();
        }  
    }
}
