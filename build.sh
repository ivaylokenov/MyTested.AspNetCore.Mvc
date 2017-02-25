#!/usr/bin/env bash
dotnet restore
dotnet build --configuration Release
dotnet test "samples/MusicStore/MusicStore.Test/project.json" --configuration Release -f netcoreapp1.1
dotnet test "samples/ApplicationParts/ApplicationParts.Test/project.json" --configuration Release -f netcoreapp1.1
dotnet test "samples/NoStartup/NoStartup.Test/project.json" --configuration Release -f netcoreapp1.1
dotnet test "samples/Lite/Lite.Test/project.json" --configuration Release -f netcoreapp1.1

mono \  
./samples/MusicStore/MusicStore.Test/bin/Release/net451/*/dotnet-test-xunit.exe \
./samples/MusicStore/MusicStore.Test/bin/Release/net451/*/MusicStore.Test.dll

mono \  
./samples/ApplicationParts/ApplicationParts.Test/bin/Release/net451/*/dotnet-test-xunit.exe \
./samples/ApplicationParts/ApplicationParts.Test/bin/Release/net451/*/ApplicationParts.Test.dll

mono \  
./samples/NoStartup/NoStartup.Test/bin/Release/net451/*/dotnet-test-xunit.exe \
./samples/NoStartup/NoStartup.Test/bin/Release/net451/*/NoStartup.Test.dll

mono \  
./samples/Lite/Lite.Test/bin/Release/net451/*/dotnet-test-xunit.exe \
./samples/Lite/Lite.Test/bin/Release/net451/*/Lite.Test.dll