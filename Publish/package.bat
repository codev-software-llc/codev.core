@echo off

@if /I "%1" == "" goto Usage

pushd .\specs

@echo Processing Codev.Core
@..\tools\nuget.exe pack Codev.Core.nuspec -version %1 -OutputDirectory ..\nupkg

for %%X IN (Asset,Banking,Cache,Cipher,Common,Communication,Export,Licensing,Task) DO (

  @echo Processing Codev.Core.%%X
  @..\tools\nuget.exe pack Codev.Core.%%X.nuspec -version %1 -OutputDirectory ..\nupkg
)

popd

goto Done

:Usage
@echo Usage: package.exe [version]

:Done