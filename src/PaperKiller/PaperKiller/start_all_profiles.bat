@echo off
cd "C:\Users\gimna\Desktop\BMSTU\WEB\bmstu-web\src\PaperKiller\PaperKiller"
start /b cmd /c "dotnet run --project PaperKiller.csproj --configuration Release --launch-profile PaperKiller"
start /b cmd /c "dotnet run --project PaperKiller.csproj --configuration Release --launch-profile PaperKillerReadOnly1"
start /b cmd /c "dotnet run --project PaperKiller.csproj --configuration Release --launch-profile PaperKillerReadOnly2"
start /b cmd /c "dotnet run --project PaperKiller.csproj --configuration Release --launch-profile PaperKillerMirror"
