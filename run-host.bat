SET ASPNETCORE_ENVIRONMENT=Development
SET LAUNCHER_PATH=\BlizzardAPIExternalMetaDataRetriever\bin\Debug\netcoreapp3.1\BlizzardAPIExternalMetaDataRetriever.exe
cd /d "C:\Program Files\IIS Express\"
iisexpress.exe /config:"D:\Development\repos\blizzard-api-external-services\.vs\blizzard-api-external-services\config\applicationhost.config" /site:"BlizzardAPIExternalMetaDataRetriever" /apppool:"BlizzardAPIExternalMetaDataRetriever AppPool"
