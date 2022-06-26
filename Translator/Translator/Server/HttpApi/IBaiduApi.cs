using Translator.Shared;
using WebApiClientCore.Attributes;

namespace Translator.Server.HttpApi
{
    [HttpHost("https://fanyi-api.baidu.com/")]
    public interface IBaiduApi
    {
        [HttpPost("/api/trans/vip/translate")]
        Task<BaiduTranslateOutput> TranslateAsync([FormContent] BaiduTranslateInput input);
    }
}
