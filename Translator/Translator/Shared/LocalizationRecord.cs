namespace Translator.Shared
{
    public class LocalizationRecord
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 本地化内容Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 资源Key
        /// </summary>
        public string ResourceKey { get; set; }

        /// <summary>
        /// 本地化内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 本地化语言类型，如zh-CN
        /// </summary>
        public string LocalizationCulture { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdatedTimestamp { get; set; }
    }
}