# dotnet run --project src/QueryFramework.CodeGeneration\QueryFramework.CodeGeneration.csproj

dotnet build -c Debug src/ExpressionFramework.CodeGeneration/ExpressionFramework.CodeGeneration.csproj
Invoke-Expression "t4plus assembly -a $(Resolve-Path "src/ExpressionFramework.CodeGeneration/bin/Debug/net7.0/ExpressionFramework.CodeGeneration.dll") -p $(Resolve-Path "src") -u $(Resolve-Path "src/ExpressionFramework.CodeGeneration/bin/Debug/net7.0")"
