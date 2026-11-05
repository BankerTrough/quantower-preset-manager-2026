using System;
using System.IO;
using System.Text.Json;

namespace QTPresetManager
{
    /// <summary>
    /// Application settings for Quantower Preset Manager.
    /// </summary>
    public class ConfigManager
    {
        public string QuantowerConfigPath { get; set; } = string.Empty;
        public string PresetFolderPath { get; set; } = string.Empty;
        public bool EnableBackup { get; set; } = true;

        private static readonly string StoragePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "config.json");

        /// <summary>
        /// Loads settings from local JSON storage.
        /// </summary>
        public static ConfigManager Load()
        {
            var config = new ConfigManager();
            try
            {
                if (!File.Exists(StoragePath))
                    return config;

                var json = File.ReadAllText(StoragePath);
                var loaded = JsonSerializer.Deserialize<ConfigManager>(json);
                if (loaded != null)
                {
                    config.QuantowerConfigPath = loaded.QuantowerConfigPath;
                    config.PresetFolderPath = loaded.PresetFolderPath;
                    config.EnableBackup = loaded.EnableBackup;
                }
            }
            catch
            {
                // Return default settings on failure.
            }

            return config;
        }

        /// <summary>
        /// Saves settings to local JSON storage.
        /// </summary>
        public void Save()
        {
            try
            {
                var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                var dir = Path.GetDirectoryName(StoragePath);
                if (!string.IsNullOrEmpty(dir))
                    Directory.CreateDirectory(dir);

                File.WriteAllText(StoragePath, json);
            }
            catch
            {
                // Ignore save failures.
            }
        }
    }
}
