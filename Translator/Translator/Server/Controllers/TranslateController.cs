using Microsoft.AspNetCore.Mvc;
using Translator.Server.FileConverters;
using Translator.Server.Translator;
using Translator.Shared;

namespace Translator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslateController : ControllerBase
    {
        private readonly ILogger<TranslateController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public TranslateController(ILogger<TranslateController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }


        [HttpPost]
        public async Task<IActionResult> TranslateAsync([FromForm] TranslateInput input)
        {
            var dbName = Guid.NewGuid().ToString("N") + ".db";
            try
            {
                using var fileStream = new FileStream(dbName, FileMode.Create);
                await input.File.CopyToAsync(fileStream);
                fileStream.Close();

                IFileConverter fileConverter = new SQLiteConverter(dbName);
                var content = await fileConverter.GetLocalizationRecordsAsync();
                ITranslator translator = GetAvailableTranslator();

                foreach (var item in content)
                {
                    (var success, var translatedContent) = await translator.TranslateAsync("zh", input.Language, item.Text);
                    if (success)
                    {
                        await fileConverter.AddLocalizationRecordAsync(input.Language, translatedContent, item);
                    }
                    else
                    {
                        return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
                    }
                }

                MemoryStream ms = new MemoryStream();
                using var file = new FileStream(dbName, FileMode.Open, FileAccess.Read);
                await file.CopyToAsync(ms);
                ms.Position = 0;

                return File(ms, "application/octet-stream");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[翻译过程中出现异常]\r\nError: {ex.Message}\r\nStackTrace: {ex.StackTrace}");
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

        /// <summary>
        /// 获取可用的翻译实现类
        /// </summary>
        /// <returns></returns>
        private ITranslator GetAvailableTranslator()
        {
            // 目前只实现了百度翻译类
            return new BaiduTranslator(_serviceProvider);
        }
    }
}