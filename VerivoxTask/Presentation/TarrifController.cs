using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VerivoxTask.Application.DTO;
using VerivoxTask.Domain.Interfaces;

namespace VerivoxTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarrifController : ControllerBase
    {
        
        private readonly ILogger<TarrifController> _logger;
        private readonly ITarrifCalculationService _tarrifCalculationService;
        public TarrifController(ILogger<TarrifController> logger, ITarrifCalculationService tarrifCalculationService)
        {
            _logger = logger;
            _tarrifCalculationService = tarrifCalculationService;
        }

        /// <summary>
        /// Supply the consumption to calculate the annual cost. This returns the product list based on the calculation and also persists the result in
        /// a the product table.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("compare")]
        public async Task<IActionResult> CompareTarrif([FromBody] ComparisonRequestDTO request)
        {
            var response = await _tarrifCalculationService.CompareTarrif(request.Consumption);
            return new OkObjectResult(response);

        }


        /// <summary>
        /// Retrieve all the products ordered by the consumption, then by the annual cost
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _tarrifCalculationService.GetProducts();
            return new OkObjectResult(response);
        }
    }

   
}
