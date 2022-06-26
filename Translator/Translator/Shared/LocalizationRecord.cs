namespace Translator.Shared
{
    public class LocalizationRecord
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ���ػ�����Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// ��ԴKey
        /// </summary>
        public string ResourceKey { get; set; }

        /// <summary>
        /// ���ػ�����
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// ���ػ��������ͣ���zh-CN
        /// </summary>
        public string LocalizationCulture { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string UpdatedTimestamp { get; set; }
    }
}