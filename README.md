# CartAutoCollect

Automatically vacuums up dropped items near your cart as you pull it — inspired by the broken AutoStore mod, rebuilt for the current Valheim API.

## Features

- Scans a configurable radius around the cart every second (adjustable)
- Only collects item types **already present in the cart** by default, so your cart acts as a mobile storage filter
- Optionally collect **all** nearby items regardless of type
- Optionally only collects while you are **attached and pulling** the cart
- Fully configurable via **F1 ConfigManager** in-game
- **Client-side only** — safe to use on vanilla servers

## Configuration (F1 in-game)

| Setting | Default | Description |
|---|---|---|
| ModEnabled | true | Toggle the mod on/off |
| CollectionRadius | 8.0 | Radius in metres around the cart to check for items |
| CollectionInterval | 1.0 | Seconds between each collection check |
| OnlyMatchingItems | true | Only collect items already in the cart |
| CollectWhenPulling | true | Only collect while attached and pulling the cart |

## Installation

### Via r2modman / Thunderstore Mod Manager (recommended)
Search for **CartAutoCollect** and click Install.

### Manual
1. Install [BepInExPack Valheim](https://thunderstore.io/c/valheim/p/denikson/BepInExPack_Valheim/)
2. Copy `CartAutoCollect.dll` into `Valheim/BepInEx/plugins/`
3. Launch the game — config file is auto-generated on first run

## Usage Tips

- Put a few stone (or whatever you want to farm) into your cart before heading out — with **OnlyMatchingItems** on, it will only vacuum up stone
- Increase **CollectionRadius** to 12–15 if you want wider coverage while mining
- Turn off **CollectWhenPulling** if you want the cart to passively collect even when parked nearby

## Compatibility

- Works alongside **AzuAutoStore**, **SkilledCarryWeight**, **AdvancedTerrainModifiers**, and **ImpactfulSkills**
- No known conflicts with other mods in the QOL Valheim pack
- Does **not** require server installation

## Changelog

**1.0.0**
- Initial release
