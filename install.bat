@echo off
set rootPath=%1
if '%2'=='' (
set envPath="C:\Program Files\Microsoft Visual Studio 8\"
) else (
set envPath=%2
)
xcopy /Y /D /Q %rootPath%"ORMModel\bin\Debug\Northface.Tools.ORM.dll" %envPath%"Common7\IDE\PrivateAssemblies\"
xcopy /Y /D /Q %rootPath%"ORMModel\bin\Debug\Northface.Tools.ORM.pdb" %envPath%"Common7\IDE\PrivateAssemblies\"
xcopy /Y /D /Q %rootPath%"ORMModelSatDll\Debug\Northface.Tools.ORMUI.dll" %envPath%"Common7\IDE\PrivateAssemblies\1033"
xcopy /Y /D /Q %rootPath%"ORMModelSatDll\Debug\Northface.Tools.ORMUI.pdb" %envPath%"Common7\IDE\PrivateAssemblies\1033"
xcopy /Y /D /Q %rootPath%"ORMModel\Shell\ProjectItems\ORMProjectItems.vsdir" %envPath%"VC#\CSharpProjectItems"
xcopy /Y /D /Q %rootPath%"ORMModel\Shell\ProjectItems\ORMModel.orm" %envPath%"VC#\CSharpProjectItems"
xcopy /Y /D /Q %rootPath%"ORMModel\Shell\ProjectItems\FactEditor.fct" %envPath%"VC#\CSharpProjectItems"
xcopy /Y /D /Q %rootPath%"ORMModel\Shell\ProjectItems\ORMProjectItems.vsdir" %envPath%"Common7\IDE\NewFileItems"
xcopy /Y /D /Q %rootPath%"ORMModel\Shell\ProjectItems\ORMModel.orm" %envPath%"Common7\IDE\NewFileItems"
xcopy /Y /D /Q %rootPath%"ORMModel\Shell\ProjectItems\FactEditor.fct" %envPath%"Common7\IDE\NewFileItems"
xcopy /Y /D /Q %rootPath%"ORMModel\ObjectModel\ORM2Core.xsd" %envPath%"Xml\Schemas\"
xcopy /Y /D /Q %rootPath%"ORMModel\ObjectModel\ORM2Diagram.xsd" %envPath%"Xml\Schemas\"
xcopy /Y /D /Q %rootPath%"ORMModel\ObjectModel\ORM2Root.xsd" %envPath%"Xml\Schemas\"
regedit /s %rootPath%ORMDesigner.vrg
regedit /s %rootPath%FactEditor.vrg
