using Microsoft.AspNetCore.Mvc;
using RoutingApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingApi.Controllers
{
    [ApiController]
    [Route(template:"api/companies")]
    public class CompaniesController:ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            //404 NotFound();
            return Ok(companies);
            
        }
        [HttpGet(template:"{companyId}")] //  api/Companies/123
        public async Task<IActionResult> GetCompany(Guid companyId )
        {
          
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);

        }
    }
}
