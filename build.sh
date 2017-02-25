#!/usr/bin/env bash
dotnet restore
dotnet test "samples/MusicStore/MusicStore.Test/project.json" -c Release -f netcoreapp1.1
dotnet test "samples/ApplicationParts/ApplicationParts.Test/project.json" -c Release -f netcoreapp1.1
dotnet test "samples/NoStartup/NoStartup.Test/project.json" -c Release -f netcoreapp1.1
dotnet test "samples/Lite/Lite.Test/project.json" -c Release -f netcoreapp1.1

dotnet build ./samples/MusicStore/MusicStore.Test -c Release -f net451
dotnet build ./samples/ApplicationParts/ApplicationParts.Test -c Release -f net451
dotnet build ./samples/NoStartup/NoStartup.Test -c Release -f net451
dotnet build ./samples/Lite/Lite.Test -c Release -f net451

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