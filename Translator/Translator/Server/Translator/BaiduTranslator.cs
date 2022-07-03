using Microsoft.Extensions.Options;
using Translator.Server.HttpApi;
using Translator.Shared;

namespace Translator.Server.Translator
{
    /// <summary>
    /// 百度翻译类
    /// </summary>
    /// <remarks>
    /// 使用百度通用翻译接口，相关接口文档：https://fanyi-api.baidu.com/doc/21
    /// </remarks>
    public class BaiduTranslator : ITranslator
    {
        private readonly IBaiduApi _api;
        private readonly BaiduTranslatorOptions _options;

        public BaiduTranslator(IServiceProvider serviceProvider)
        {
            _api = serviceProvider.GetRequiredService<IBaiduApi>();
            _options = serviceProvider.GetRequiredService<IOptions<BaiduTranslatorOptions>>().Value;
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="content">翻译内容</param>
        /// <returns></returns>
        public async Task<(bool, string)> TranslateAsync(string from, string to, string content)
        {
            try
            {
                var salt = new Random().Next().ToString();
                var response = await _api.TranslateAsync(new BaiduTranslateInput
                {
                    Appid = _options.AppId,
                    From = from,
                    To = LanguageMapper.BaiduTranslatorLanguages[to],
                    Q = content,
                    Salt = salt,
                    Sign = CreateMD5($"{_options.AppId}{content}{salt}{_options.SecretKey}").ToLower()
                });

                // 访问频率受限，QPS限制为每秒10次
                if (response.ErrorCode == "54003")
                {
                    await Task.Delay(1000);
                    return await TranslateAsync(from, to, content, 1);
                }

                return (true, response.TransResult[0].Dst);
            }
            catch (Exception)
            {
                return (false, default);
            }
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="content">翻译内容</param>
        /// <param name="retryTimes">当前重试次数，用于访问频率受限时重新请求计数</param>
        /// <returns></returns>
        public async Task<(bool, string)> TranslateAsync(string from, string to, string content, int retryTimes = 0)
        {
            try
            {
                var salt = new Random().Next().ToString();
                var response = await _api.TranslateAsync(new BaiduTranslateInput
                {
                    Appid = _options.AppId,
                    From = from,
                    To = LanguageMapper.BaiduTranslatorLanguages[to],
                    Q = content,
                    Salt = salt,
                    Sign = CreateMD5($"{_options.AppId}{content}{salt}{_options.SecretKey}").ToLower()
                });

                // 访问频率受限，QPS限制为每秒10次
                if (response.ErrorCode == "54003" && retryTimes <= 5)
                {
                    await Task.Delay(1000);
                    retryTimes++;
                    return await TranslateAsync(from, to, content, retryTimes);
                }

                return (true, response.TransResult[0].Dst);
            }
            catch (Exception)
            {
                return (false, default);
            }
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }
    }
}
