using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Translator.Shared
{
    /// <summary>
    /// 多语言翻译请求实体
    /// </summary>
    public class TranslateInput
    {
        /// <summary>
        /// 文件内容
        /// </summary>
        [Required]
        public IFormFile File { get; set; }

        /// <summary>
        /// 语言类型
        /// </summary>
        [Required]
        public string Language { get; set; }
    }
}
