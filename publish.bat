@echo off

dotnet publish -c Release -r win-x64

copy HardHead\bin\Release\netcoreapp2.1\win-x64\publish\* ..\liars-dice\dist\win-x64\csharp
