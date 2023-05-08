$list = New-Object Collections.Generic.List[LineCountInfo]
foreach ($directory in Get-ChildItem -Path ./src -Directory)
{
    [long]$totalCount = 0;
    [long]$generatedCount = 0;
    [long]$notGeneratedCount = 0;
    foreach ($file in Get-ChildItem -Path $directory -File -Filter *.cs -Recurse)
    {
        $length = (Get-Content $file).Length
        $totalCount += $length
        if ($file.FullName.EndsWith(".template.generated.cs"))
        {
            $generatedCount += $length
        }
        else
        {
            $notGeneratedCount += $length
        }
    }
    $item = [LineCountInfo]::new()
    $item.Directory = $directory.Name
    $item.GeneratedCount = $generatedCount
    $item.NotGeneratedCount = $notGeneratedCount
    $item.TotalCount = $totalCount
    $list.Add($item)
}

Write-Host "Statistics:"
$list | Format-Table -Property Directory, GeneratedCount, NotGeneratedCount, TotalCount

class LineCountInfo {
    [string]$Directory;
    [long]$GeneratedCount;
    [long]$NotGeneratedCount;
    [long]$TotalCount;
}