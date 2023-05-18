# dotnet run --project .\src\QueryFramework.CodeGeneration\QueryFramework.CodeGeneration.csproj

dotnet build -c Debug src/ExpressionFramework.CodeGeneration/ExpressionFramework.CodeGeneration.csproj
t4plus assembly -a C:\Git\ExpressionFramework\src\ExpressionFramework.CodeGeneration\bin\Debug\net7.0\ExpressionFramework.CodeGeneration.dll -p C:\Git\ExpressionFramework\src\ExpressionFramework.CodeGeneration\bin\Debug\net7.0\../../../../ -u C:\Git\ExpressionFramework\src\ExpressionFramework.CodeGeneration\bin\Debug\net7.0
