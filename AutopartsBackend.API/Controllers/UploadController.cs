using AutopartsBackend.API.Models;
using AutopartsBackend.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutopartsBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IRecognitionService _recognitionService;

        public UploadController(IRecognitionService recognitionService)
        {
            _recognitionService = recognitionService;
        }

        [HttpPost]
        public async Task<ActionResult<RecognitionResultDto>> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var result = await _recognitionService.RecognizePart(file);
            return Ok(result);
        }
    }
}