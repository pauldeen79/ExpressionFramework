# dotnet run --project src/ExpressionFramework.CodeGeneration/ExpressionFramework.CodeGeneration.csproj

dotnet build -c Debug src/ExpressionFramework.CodeGeneration/ExpressionFramework.CodeGeneration.csproj
t4plus assembly -a src/ExpressionFramework.CodeGeneration/bin/Debug/net7.0/ExpressionFramework.CodeGeneration.dll -p src/
