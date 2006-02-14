@ECHO OFF
SETLOCAL

SET VSDir=%ProgramFiles%\Microsoft Visual Studio 8
SET NORMADir=%ProgramFiles%\Neumont\ORM Architect for Visual Studio

DEL /F /Q "%VSDir%\Common7\IDE\PrivateAssemblies\Neumont.Tools.ORM.ORMCustomTool.*" 1>NUL 2>&1
XCOPY /Y /D /V /Q "%~dp0\bin\Neumont.Tools.ORM.ORMCustomTool.dll" "%NORMADir%\bin\"
XCOPY /Y /D /V /Q "%~dp0\bin\Neumont.Tools.ORM.ORMCustomTool.pdb" "%NORMADir%\bin\"

REG QUERY HKCR\CLSID\{977BD01E-F2B4-4341-9C47-459420624A20}\InprocServer32 /v "CodeBase" 1>NUL 2>&1
IF ERRORLEVEL 1 (GOTO:_InstallReg) ELSE (GOTO:EOF)

:_InstallReg
CALL:_AddRegCR
CALL:_AddRegGenerator "8.0" "{164b10b9-b200-11d0-8c61-00a0c91e29d5}"
CALL:_AddRegGenerator "8.0" "{fae04ec1-301f-11d3-bf4b-00c04f79efbc}"
CALL:_AddRegGenerator "8.0" "{e6fdf8b0-f3d1-11d4-8576-0002a516ece8}"
CALL:_AddRegGenerator "8.0Exp" "{164b10b9-b200-11d0-8c61-00a0c91e29d5}"
CALL:_AddRegGenerator "8.0Exp" "{fae04ec1-301f-11d3-bf4b-00c04f79efbc}"
CALL:_AddRegGenerator "8.0Exp" "{e6fdf8b0-f3d1-11d4-8576-0002a516ece8}"
GOTO:EOF

:_AddRegCR
REG ADD HKLM\SOFTWARE\Classes\CLSID\{977BD01E-F2B4-4341-9C47-459420624A20} /f /ve /d "Neumont.Tools.ORM.ORMCustomTool.ORMCustomTool, Neumont.Tools.ORM.ORMCustomTool, Version=1.0, Culture=neutral, PublicKeyToken=957d5b7d5e79e25f"
REG ADD HKLM\SOFTWARE\Classes\CLSID\{977BD01E-F2B4-4341-9C47-459420624A20}\InprocServer32 /f /ve /d "%SystemRoot%\System32\mscoree.dll"
REG ADD HKLM\SOFTWARE\Classes\CLSID\{977BD01E-F2B4-4341-9C47-459420624A20}\InprocServer32 /f /v "ThreadingModel" /d "Both"
REG ADD HKLM\SOFTWARE\Classes\CLSID\{977BD01E-F2B4-4341-9C47-459420624A20}\InprocServer32 /f /v "Class" /d "Neumont.Tools.ORM.ORMCustomTool.ORMCustomTool"
REG ADD HKLM\SOFTWARE\Classes\CLSID\{977BD01E-F2B4-4341-9C47-459420624A20}\InprocServer32 /f /v "CodeBase" /d "%NORMADir%\bin\Neumont.Tools.ORM.ORMCustomTool.dll"
REG ADD HKLM\SOFTWARE\Classes\CLSID\{977BD01E-F2B4-4341-9C47-459420624A20}\InprocServer32 /f /v "Assembly" /d "Neumont.Tools.ORM.ORMCustomTool, Version=1.0, Culture=neutral, PublicKeyToken=957d5b7d5e79e25f"
GOTO:EOF

:_AddRegGenerator
REG ADD HKLM\SOFTWARE\Microsoft\VisualStudio\%~1\Generators\%~2\ORMCustomTool /f /ve /d "ORM Custom Tool"
REG ADD HKLM\SOFTWARE\Microsoft\VisualStudio\%~1\Generators\%~2\ORMCustomTool /f /v "CLSID" /d "{977BD01E-F2B4-4341-9C47-459420624A20}"
GOTO:EOF