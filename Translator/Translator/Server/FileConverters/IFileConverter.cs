using Translator.Shared;

namespace Translator.Server.FileConverters
{
    /// <summary>
    /// 文件读取接口类
    /// </summary>
    public interface IFileConverter
    {
        /// <summary>
        /// 从文件读取本地化记录内容
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LocalizationRecord>> GetLocalizationRecordsAsync(string culture = "zh-CN");

        /// <summary>
        /// 向文件新增本地化记录
        /// </summary>
        /// <param name="destCulture">目标语言</param>
        /// <param name="translatedContent">翻译后的Text内容</param>
        /// <param name="sourceRecord">原记录</param>
        /// <returns></returns>
        Task<bool> AddLocalizationRecordAsync(string destCulture, string translatedContent, LocalizationRecord sourceRecord);
    }
}
