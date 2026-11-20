@rem ----------------------------------------------------------------
@rem Setup the script.  This will save the current directory.
@rem ----------------------------------------------------------------
@echo off
@echo Clean - Codev.Core

pushd %~dp0%

@rem ----------------------------------------------------------------
@rem Remove VS generated directories.
@rem ----------------------------------------------------------------
@if /I "%~1" == "all" (

  pushd ..

  FOR %%X IN (.vs, TestResults) DO (

    @if EXIST "%%X" rd /S /Q "%%X"
  )

  popd

)


@rem ----------------------------------------------------------------
@rem Run the clean script.
@rem ----------------------------------------------------------------
pushd ..\Publish

@if EXIST bin (
  rd /s /q bin
)
popd

for %%X IN (Core,Test) DO (

  @if EXIST "..\Data\Ado\%%X" (
    pushd ..\Data\Ado\%%X

    FOR %%Y IN (obj,bin) DO (
      @if EXIST %%Y (
        rd /s /q %%Y
      )
    )
    popd
  )
)

for %%X IN (Base,Interface,Model) DO (

  @if EXIST "..\Common\%%X" (
    pushd ..\Common\%%X

    FOR %%Y IN (obj,bin) DO (
      @if EXIST %%Y (
        rd /s /q %%Y
      )
    )
    popd
  )
)

for %%X IN (Service) DO (

  @if EXIST "..\%%X" (
    pushd ..\%%X

    FOR %%Y IN (obj,bin) DO (
      @if EXIST %%Y (
        rd /s /q %%Y
      )
    )
    popd
  )
)

for %%X IN (Cache,Cipher,Common,Communication,Export,Secret) DO (

  @if EXIST "..\Provider\%%X" (
    pushd ..\Provider\%%X

    FOR %%Y IN (obj,bin) DO (
      @if EXIST %%Y (
      rd /s /q %%Y
      )
    )

    popd
  )
)

for %%X IN (Fake,Memory,PayPal,Stripe,Stub) DO (

  @if EXIST "..\Provider\Banking\%%X" (
    pushd ..\Provider\Banking\%%X

    FOR %%Y IN (obj,bin) DO (
      @if EXIST %%Y (
        rd /s /q %%Y
      )
    )

    popd
  )
)

for %%X IN (Ado,Sqlite) DO (

  @if EXIST "..\Repository\%%X" (
    pushd ..\Repository\%%X

    FOR %%Y IN (obj,bin) DO (
        @if EXIST %%Y (
        rd /s /q %%Y
      )
    )

    popd
  )
)


for %%X IN (Infrastructure) DO (

  @if EXIST "..\%%X" (
    pushd ..\%%X

    FOR %%Y IN (obj,bin) DO (
      @if EXIST %%Y (
        rd /s /q %%Y
      )
    )

    popd
  )
)

pushd ..\Test

for %%X IN (Test.Common.Base,Test.Provider.Cipher,Test.Provider.Communication,Test.Provider.Export,Test.Service.Clock) DO (

  @if EXIST "%%X" (
    pushd %%X

    FOR %%Y IN (obj,bin) DO (
      @if EXIST %%Y (
        rd /s /q %%Y
      )
    )

    popd
  )
)
popd


@rem ----------------------------------------------------------------
@rem Restore the directory.
@rem ----------------------------------------------------------------
popd