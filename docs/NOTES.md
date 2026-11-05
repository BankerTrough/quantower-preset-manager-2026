# Developer Notes

## Overview

Quantower Preset Manager is a local preset manager for the Quantower trading terminal. It copies workspace configuration files and does not modify the running application. It is strictly read-only with respect to trading execution.

## Backup and Restore

The backup manager creates a ZIP archive of all saved presets. The restore operation extracts a ZIP archive into the preset folder. The config diff feature compares saved preset files to the current Quantower configuration.

## Updating the Data

If the Quantower configuration format changes, update the preset file naming and parsing logic in the PresetManager.

## Known Limitations

- The manager does not launch or stop Quantower.
- If the Quantower configuration format changes, preset handling must be updated.
- Backup and restore operations require file system access.
