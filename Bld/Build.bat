@rem --------------------------------------------------------------------------
@rem This will build all the target configurations.
@rem --------------------------------------------------------------------------
@echo off

pushd %~dp0%


@rem ----------------------------------------------------------------
@rem Clean all the targets.
@rem ----------------------------------------------------------------
call Clean.bat


@rem ----------------------------------------------------------------
@rem Build the generate enumerations.
@rem ----------------------------------------------------------------
call BuildEnums.bat > nul


@rem ----------------------------------------------------------------
@rem Run the build script.
@rem ----------------------------------------------------------------
pushd ..\
FOR %%X IN (Dev,Live) DO (
  @echo Build - Codev.Core [%%X]
  dotnet build --configuration %%X /p:WarningLevel=0 /restore:true /verbosity:quiet
)
popd


@rem ----------------------------------------------------------------
@rem Restore the directory.
@rem ----------------------------------------------------------------
popd
