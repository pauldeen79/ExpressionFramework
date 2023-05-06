git pull origin
$original_file = '.github/workflows/build.yml'
$destination_file =  '.github/workflows/build.yml'
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'uses: actions/checkout@v2', 'uses: actions/checkout@v3' `
       -replace 'uses: actions/setup-dotnet@v1', 'uses: actions/setup-dotnet@v3'
    } | Set-Content $destination_file

    $original_file = '.github/workflows/sonarcloud.yml'
    $destination_file =  '.github/workflows/sonarcloud.yml'
    (Get-Content $original_file) | Foreach-Object {
        $_ -replace 'uses: actions/checkout@v2', 'uses: actions/checkout@v3' `
           -replace 'uses: actions/setup-dotnet@v1', 'uses: actions/setup-dotnet@v3'
        } | Set-Content $destination_file

git commit -a -m "Upgraded build pipelines"
git push origin
