@echo off
chcp 65001 >nul
rem ---------------------------------------------------------------
rem  Собирает один SchoolSchedule.exe, которому не нужен .NET на
rem  компьютере — просто скопировать папку publish на телевизор.
rem  Нужен установленный .NET SDK 8: https://dotnet.microsoft.com/download
rem ---------------------------------------------------------------
cd /d "%~dp0"

dotnet publish SchoolSchedule.csproj -c Release -r win-x64 --self-contained true ^
  -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -o "%~dp0publish"

if errorlevel 1 (
  echo.
  echo Сборка не прошла. Скорее всего не установлен .NET SDK 8.
  pause
  exit /b 1
)

if not exist "%~dp0publish\sql" mkdir "%~dp0publish\sql"
copy /y "%~dp0sql\*.*" "%~dp0publish\sql\" >nul

echo.
echo Готово: %~dp0publish\SchoolSchedule.exe
echo Рядом лежит ip.txt — впишите в него адрес базы.
pause
