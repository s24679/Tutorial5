using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;
using Tutorial5.Services;

namespace Tutorial5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IDbService _dbService;
        public PrescriptionController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionDto request)
        {
            try
            {
                var prescriptionId = await _dbService.AddPrescription(request);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


    }
}
