# Build Instructions for CartAutoCollect

## Prerequisites
- Visual Studio 2022 or JetBrains Rider (or dotnet CLI)
- .NET Framework 4.6.2 SDK

## Step 1 — Build

### Using dotnet CLI:
```
dotnet restore CartAutoCollect.csproj
dotnet build CartAutoCollect.csproj --configuration Release -o build/
```

### Using Visual Studio:
Open `CartAutoCollect.csproj`, set configuration to Release, and Build.

## Step 2 — Install the DLL

Copy the compiled `CartAutoCollect.dll` from `build/` into:
```
Steam/steamapps/common/Valheim/BepInEx/plugins/
```

## Step 3 — Package for Thunderstore (optional)

To share on Thunderstore, create a zip with this structure:
```
CartAutoCollect.zip
├── manifest.json
├── README.md
├── icon.png         (256x256 PNG icon)
└── plugins/
    └── CartAutoCollect.dll
```

Upload the zip at https://thunderstore.io/c/valheim/create/
