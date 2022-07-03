namespace Translator.Server.Translator
{
    /// <summary>
    /// 翻译接口类
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="content">翻译内容</param>
        /// <returns></returns>
        public Task<(bool, string)> TranslateAsync(string from, string to, string content);
    }
}
