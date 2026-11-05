using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QTPresetManager
{
    /// <summary>
    /// Main window for Quantower Preset Manager.
    /// Shows saved presets with apply and delete actions plus settings.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PresetManager _presetManager = new PresetManager();
        private readonly BackupManager _backupManager = new BackupManager();
        private readonly ConfigManager _configManager = ConfigManager.Load();

        public MainWindow()
        {
            InitializeComponent();
            RefreshPresets();
            UpdateStatus("Ready");
        }

        private void BtnSavePreset_Click(object sender, RoutedEventArgs e)
        {
            var name = txtPresetName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name)) { UpdateStatus("Preset name required"); return; }

            _presetManager.SavePreset(name, _configManager.QuantowerConfigPath);
            RefreshPresets();
            UpdateStatus("Preset saved");
        }

        private void BtnApplyPreset_Click(object sender, RoutedEventArgs e)
        {
            if (lstPresets.SelectedItem is not Preset selected) { UpdateStatus("Select a preset"); return; }

            _presetManager.ApplyPreset(selected.Name, _configManager.QuantowerConfigPath);
            UpdateStatus("Preset applied");
        }

        private void BtnDeletePreset_Click(object sender, RoutedEventArgs e)
        {
            if (lstPresets.SelectedItem is not Preset selected) { UpdateStatus("Select a preset"); return; }

            _presetManager.DeletePreset(selected.Name);
            RefreshPresets();
            UpdateStatus("Preset deleted");
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            _configManager.QuantowerConfigPath = txtConfigPath.Text.Trim();
            _configManager.PresetFolderPath = txtPresetFolder.Text.Trim();
            _configManager.EnableBackup = chkBackupEnabled.IsChecked ?? false;
            _configManager.Save();
            UpdateStatus("Settings saved");
        }

        private void RefreshPresets()
        {
            var presets = _presetManager.GetPresets(_configManager.QuantowerConfigPath);
            lstPresets.ItemsSource = presets;
        }

        private void UpdateStatus(string message)
        {
            txtStatus.Text = message;
        }
    }
}
