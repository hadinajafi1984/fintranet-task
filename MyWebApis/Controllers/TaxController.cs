
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MyWebApis.Model;

namespace MyWebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private CongestionTaxCalculator _taxCalculator;

        public TaxController(CongestionTaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        
        [HttpPost]
        public IActionResult GetTax(Parameter parameter)
        {
            return Ok(_taxCalculator.GetTax(parameter.Vehicle, parameter.dates));
        }
    }
}
