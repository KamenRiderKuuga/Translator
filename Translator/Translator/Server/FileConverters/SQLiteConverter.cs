using Dapper;
using System.Data.SQLite;
using Translator.Shared;

namespace Translator.Server.FileConverters
{
    public class SQLiteConverter : IFileConverter
    {
        private readonly string _fileName;

        public SQLiteConverter(string fileName)
        {
            _fileName = fileName;
        }

        public async Task<IEnumerable<LocalizationRecord>> GetLocalizationRecordsAsync(string culture = "zh-CN")
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            return await cnn.QueryAsync<LocalizationRecord>($"select * from LocalizationRecords where LocalizationCulture = @culture", new
            {
                culture
            });
        }

        public async Task<bool> AddLocalizationRecordAsync(string destCulture, string translatedContent, LocalizationRecord sourceRecord)
        {
            using var cnn = SimpleDbConnection();
            cnn.Open();
            return await cnn.ExecuteAsync("INSERT OR IGNORE INTO LocalizationRecords(Key, ResourceKey, Text, LocalizationCulture, UpdatedTimestamp) VALUES (@Key, @ResourceKey, @Text, @LocalizationCulture, @UpdatedTimestamp)", new
            {
                sourceRecord.Key,
                sourceRecord.ResourceKey,
                Text = translatedContent,
                sourceRecord.LocalizationCulture,
                UpdatedTimestamp = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(8)).ToString("yyyy-MM-dd HH:mm:ss.fffffff")
            }) > 0;
        }

        private SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection($"Data Source={_fileName}");
        }
    }
}
