#!/usr/bin/env bash
dotnet restore
dotnet test "samples/MusicStore/MusicStore.Test/project.json" -c Release -f netcoreapp1.1
dotnet test "samples/ApplicationParts/ApplicationParts.Test/project.json" -c Release -f netcoreapp1.1
dotnet test "samples/NoStartup/NoStartup.Test/project.json" -c Release -f netcoreapp1.1
dotnet test "samples/Lite/Lite.Test/project.json" -c Release -f netcoreapp1.1