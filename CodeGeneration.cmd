dotnet new tool-manifest --force
dotnet tool install pauldeen79.TextTemplateTransformationFramework.T4.Plus.Cmd --version 0.1.19
dotnet t4plus run -f src/CodeGeneration/CodeGeneration.template BasePath:src/ GenerateMultipleFiles:True DryRun:False
dotnet tool uninstall pauldeen79.TextTemplateTransformationFramework.T4.Plus.Cmd

rem dotnet tool install --global pauldeen79.TextTemplateTransformationFramework.T4.Plus.Cmd --version 0.1.19
rem t4plus run -f CodeGeneration.template BasePath:../ GenerateMultipleFiles:True DryRun:False
rem dotnet tool uninstall --global pauldeen79.TextTemplateTransformationFramework.T4.Plus.Cmd