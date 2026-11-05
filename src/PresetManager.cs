using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace QTPresetManager
{
    /// <summary>
    /// Represents a saved workspace preset.
    /// </summary>
    public class Preset
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public DateTime DateModified { get; set; }
        public int MonitorCount { get; set; }
    }

    /// <summary>
    /// Manages workspace presets by copying config files.
    /// </summary>
    public class PresetManager
    {
        private readonly string _presetsFolder;
        private readonly BackupManager _backupManager = new BackupManager();

        public PresetManager(string? presetsFolder = null)
        {
            _presetsFolder = presetsFolder ?? Path.Combine(Directory.GetCurrentDirectory(), "data", "presets");
        }

        /// <summary>
        /// Returns the saved presets from the preset folder.
        /// </summary>
        public List<Preset> GetPresets(string configPath)
        {
            var presets = new List<Preset>();
            var folder = GetPresetFolder(configPath);
            if (!Directory.Exists(folder))
                return presets;

            foreach (var file in Directory.GetFiles(folder, "*.json"))
            {
                presets.Add(new Preset
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    Path = file,
                    DateModified = File.GetLastWriteTimeUtc(file),
                    MonitorCount = CountMonitorEntries(file)
                });
            }

            presets.Sort((a, b) => b.DateModified.CompareTo(a.DateModified));
            return presets;
        }

        /// <summary>
        /// Saves the current Quantower config as a named preset.
        /// </summary>
        public void SavePreset(string name, string configPath)
        {
            var folder = GetPresetFolder(configPath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var destination = Path.Combine(folder, name + ".json");
            File.Copy(configPath, destination, true);
            _backupManager.RecordVersion(name);
        }

        /// <summary>
        /// Applies a saved preset to the Quantower config path.
        /// </summary>
        public void ApplyPreset(string presetName, string configPath)
        {
            var folder = GetPresetFolder(configPath);
            var source = Path.Combine(folder, presetName + ".json");
            if (!File.Exists(source))
                throw new FileNotFoundException("Preset not found: " + presetName);

            File.Copy(source, configPath, true);
        }

        /// <summary>
        /// Deletes a saved preset.
        /// </summary>
        public void DeletePreset(string presetName)
        {
            var target = Path.Combine(_presetsFolder, presetName + ".json");
            if (File.Exists(target))
                File.Delete(target);
        }

        private string GetPresetFolder(string configPath)
        {
            if (string.IsNullOrWhiteSpace(configPath))
                return _presetsFolder;

            return Path.Combine(Path.GetDirectoryName(configPath) ?? ".", "presets");
        }

        private static int CountMonitorEntries(string jsonPath)
        {
            try
            {
                using var document = JsonDocument.Parse(File.ReadAllText(jsonPath));
                return CountMonitorEntriesCore(document.RootElement);
            }
            catch
            {
                return 0;
            }
        }

        private static int CountMonitorEntriesCore(JsonElement element)
        {
            var count = 0;
            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in element.EnumerateObject())
                {
                    if (property.Name.IndexOf("monitor", StringComparison.OrdinalIgnoreCase) >= 0)
                        count++;
                    count += CountMonitorEntriesCore(property.Value);
                }
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    count += CountMonitorEntriesCore(item);
                }
            }

            return count;
        }
    }
}
