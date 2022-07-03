using System.Collections.ObjectModel;

namespace Translator.Server
{
    /// <summary>
    /// 因不同Translator需要传入的目标语言代码不完全相同，通过这个类获取到与标准语言代码对应的特定Translator的语言代码
    /// </summary>
    public static class LanguageMapper
    {
        public readonly static ReadOnlyDictionary<string, string> BaiduTranslatorLanguages = new(new Dictionary<string, string>
        {
            { "zh-CN", "zh" },
            { "en-US", "en" },
        });
    }
}
