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

## Usage Tips

- Put a few stone (or whatever you want to farm) into your cart before heading out — with **OnlyMatchingItems** on, it will only vacuum up stone
- Increase **CollectionRadius** to 12–15 if you want wider coverage while mining
- Turn off **CollectWhenPulling** if you want the cart to passively collect even when parked nearby

## Compatibility

- Works alongside **AzuAutoStore**, **SkilledCarryWeight**, **AdvancedTerrainModifiers**, and **ImpactfulSkills**
- Does **not** require server installation

## Troubleshooting

### Auto-storing mods (e.g. AzuAutoStore) stopped picking up items from the ground after installing CartAutoCollect

This was caused by a bug in version 1.0.0 where the mod would corrupt the ZDO ownership state of nearby containers. This has been fixed in 1.0.1.

If you were running 1.0.0, any chests that were in range of a cart while the old version was active may still be affected. To fix them, simply **empty and break each affected chest, then replace it**. This resets the chest's ZDO state and auto-storing will work normally again. You only need to do this once — chests placed after updating to 1.0.1 will not be affected.

## Changelog

**1.0.1**
- Fixed a bug where the mod would corrupt the ZDO ownership state of nearby containers, causing other auto-storing mods such as AzuAutoStore to stop picking up items from the ground
- The mod now correctly claims ZDO ownership of both the cart and ground items before interacting with them, preventing conflicts with other mods on vanilla and modded servers

**1.0.0**
- Initial release
