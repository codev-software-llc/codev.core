@rem --------------------------------------------------------------------------
@rem This will build all the target configurations.
@rem --------------------------------------------------------------------------
@echo off

pushd %~dp0%


@rem ----------------------------------------------------------------
@rem Remove old enums.
@rem ----------------------------------------------------------------
@erase /q ..\Common\Model\Source\Enums\Generated


@rem ----------------------------------------------------------------
@rem Generate the enums.
@rem ----------------------------------------------------------------
Bin\EnumGen.exe "application|Core" "company|Codev Software, LLC" "targetdirectory|..\Common\Model\Source\Enums\Generated" "namespace|Codev.Core.Model" "schema|core" "source|%CORE_DATABASE_SOURCE%" "catalog|Codev.Core" "username|%CORE_DATABASE_USERNAME%" "password|%CORE_DATABASE_PASSWORD%"

Bin\EnumGen.exe "application|Test" "company|Codev Software, LLC" "targetdirectory|..\Common\Model\Source\Enums\Generated" "namespace|Codev.Core.Model" "schema|core" "source|%CORE_DATABASE_SOURCE%" "catalog|Codev.Core" "username|%CORE_DATABASE_USERNAME%" "password|%CORE_DATABASE_PASSWORD%"


@rem ----------------------------------------------------------------
@rem Restore the directory.
@rem ----------------------------------------------------------------
popd