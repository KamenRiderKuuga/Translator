namespace Translator.Shared
{
    public class BaiduTranslateInput
    {
        /// <summary>
        /// 请求翻译query
        /// </summary>
        public string Q { get; set; }

        /// <summary>
        /// 翻译源语言	
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 翻译目标语言
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// APPID
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 随机数	
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
    }
}
