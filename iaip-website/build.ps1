Get-ChildItem -Path "www" -Recurse -Include "*.md" | ForEach-Object {
    $file = $_.FullName
    $output = "$($_.DirectoryName)\$($_.BaseName).html"
    pandoc -f markdown+startnum -t html5 --email-obfuscation=references --template="includes/template.html5" -H "includes/head.inc" -o $output $file
}
