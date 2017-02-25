#!/usr/bin/env bash
dotnet restore
dotnet build --configuration Release
dotnet test "samples/MusicStore/MusicStore.Test/project.json" --configuration Release
dotnet test "samples/ApplicationParts/ApplicationParts.Test/project.json" --configuration Release
dotnet test "samples/NoStartup/NoStartup.Test/project.json" --configuration Release
dotnet test "samples/Lite/Lite.Test/project.json" --configuration Release