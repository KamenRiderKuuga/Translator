using Microsoft.AspNetCore.Mvc;
using Translator.Shared;

namespace Translator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslateController : ControllerBase
    {
        private readonly ILogger<TranslateController> _logger;

        public TranslateController(ILogger<TranslateController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> TranslateAsync([FromForm] IFormFile file)
        {
            var dbName = Guid.NewGuid().ToString("N") + ".db";
            try
            {
                using Stream fileStream = new FileStream(dbName, FileMode.Create);
                await file.CopyToAsync(fileStream);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            finally
            {
                if (System.IO.File.Exists(dbName))
                {
                    System.IO.File.Delete(dbName);
                }
            }
        }
    }
}