using Translator.Server.HttpApi;
using Translator.Shared;

namespace Translator.Server.Translator
{
    public class BaiduTranslator : ITranslator
    {
        private readonly IBaiduApi _api;

        public BaiduTranslator(IBaiduApi api)
        {
            _api = api;
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="conent">翻译内容</param>
        /// <returns></returns>
        public async Task<(bool, string)> TranslateAsync(string from, string to, string conent)
        {
            try
            {
                var content = await _api.TranslateAsync(new BaiduTranslateInput
                {
                    Appid = String.Empty,
                    From = from,
                    To = to,
                    Q = conent,
                    Salt = "",
                    Sign = CreateMD5("")
                });

                return (true, content.TransResult[0].Dst);
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
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }
    }
}
