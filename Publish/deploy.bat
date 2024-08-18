@echo off

@if "%1" == "" goto Usage

for %%X IN (Asset,Banking,Cache,Cipher,Common,Communication,Export,Licensing,Task) DO (
  @echo Processing Codev.Core.%%X
  @tools\nuget.exe push nupkg/Codev.Core.%%X.%1.nupkg perkwil42 -Timeout 600 -Source http://package.codev.net/nuget
)

goto Done

:Usage
@echo Usage: deploy.bat [version]

:Done