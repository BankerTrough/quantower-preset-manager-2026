using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;

namespace QTPresetManager
{
    /// <summary>
    /// Represents a version history entry.
    /// </summary>
    public class VersionEntry
    {
        public string PresetName { get; set; } = string.Empty;
        public int Version { get; set; }
        public DateTime TimestampUtc { get; set; }
    }

    /// <summary>
    /// Manages preset backups and restore operations.
    /// </summary>
    public class BackupManager
    {
        private readonly string _historyPath;

        public BackupManager(string? historyPath = null)
        {
            _historyPath = historyPath ?? Path.Combine(Directory.GetCurrentDirectory(), "data", "backup_history.json");
        }

        /// <summary>
        /// Exports all presets into a ZIP archive.
        /// </summary>
        public void ExportAllPresets(string targetPath)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "data", "presets");
            if (!Directory.Exists(folder))
                return;

            if (File.Exists(targetPath))
                File.Delete(targetPath);

            using var archive = new ZipArchive(targetPath, ZipArchiveMode.Create);
            foreach (var file in Directory.GetFiles(folder, "*.json"))
            {
                var entry = archive.CreateEntry(Path.GetFileName(file));
                using var entryStream = entry.Open();
                using var fileStream = File.OpenRead(file);
                entryStream.Write(fileStream.ToArray());
            }
        }

        /// <summary>
        /// Imports presets from a ZIP archive into the preset folder.
        /// </summary>
        public void ImportPresets(string archivePath)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "data", "presets");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            using var archive = ZipArchive.Open(archivePath);
            foreach (var entry in archive.Entries)
            {
                if (!entry.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    continue;

                var destination = Path.Combine(folder, Path.GetFileName(entry.FullName));
                entry.ExtractToFile(destination, true);
            }
        }

        /// <summary>
        /// Returns the latest versions of a preset, newest first, limited to five.
        /// </summary>
        public List<VersionEntry> GetVersionHistory(string presetName)
        {
            var versions = new List<VersionEntry>();
            if (!File.Exists(_historyPath))
                return versions;

            var json = File.ReadAllText(_historyPath);
            var entries = JsonSerializer.Deserialize<List<VersionEntry>>(json) ?? new List<VersionEntry>();

            return entries
                .Where(entry => entry.PresetName == presetName)
                .OrderByDescending(entry => entry.TimestampUtc)
                .Take(5)
                .ToList();
        }

        /// <summary>
        /// Records a preset version entry.
        /// </summary>
        public void RecordVersion(string presetName)
        {
            var history = new List<VersionEntry>();
            if (File.Exists(_historyPath))
            {
                var json = File.ReadAllText(_historyPath);
                history = JsonSerializer.Deserialize<List<VersionEntry>>(json) ?? new List<VersionEntry>();
            }

            var version = history.Count == 0 ? 1 : history.Max(entry => entry.Version) + 1;
            history.Add(new VersionEntry { PresetName = presetName, Version = version, TimestampUtc = DateTime.UtcNow });

            var json = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });
            var dir = Path.GetDirectoryName(_historyPath);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(_historyPath, json);
        }
    }
}
