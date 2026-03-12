# Build Instructions for CartAutoCollect

## Prerequisites
- Visual Studio 2022 or JetBrains Rider (or dotnet CLI)
- .NET Framework 4.6.2 SDK
- Valheim installed via Steam

## Step 1 — Copy required DLLs

Create a `libs/` folder inside `CartAutoCollect/CartAutoCollect/` and copy these files into it:

**From your Valheim install** (`Steam/steamapps/common/Valheim/valheim_Data/Managed/`):
- `assembly_valheim.dll`
- `UnityEngine.dll`
- `UnityEngine.CoreModule.dll`
- `UnityEngine.PhysicsModule.dll`

**From your BepInEx install** (`Steam/steamapps/common/Valheim/BepInEx/core/`):
- `BepInEx.dll`
- `0Harmony.dll`

## Step 2 — Build

### Using dotnet CLI:
```
cd CartAutoCollect/CartAutoCollect
dotnet build --configuration Release
```

### Using Visual Studio:
Open `CartAutoCollect.csproj`, set configuration to Release, and Build.

## Step 3 — Install the DLL

Copy the compiled `CartAutoCollect.dll` from `bin/Release/net462/` into:
```
Steam/steamapps/common/Valheim/BepInEx/plugins/
```

## Step 4 — Package for Thunderstore (optional)

To share on Thunderstore, create a zip with this structure:
```
CartAutoCollect.zip
├── manifest.json
├── README.md
├── icon.png         (256x256 PNG icon — create your own)
└── plugins/
    └── CartAutoCollect.dll
```

Upload the zip at https://thunderstore.io/c/valheim/create/
