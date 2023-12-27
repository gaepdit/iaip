cls
for /r "docs" %%A in (*.md) do (
	pandoc.exe -f markdown+startnum -t html5 --email-obfuscation=references --template="templates/default.html5" -H "includes/head.inc" -o "%%~dpnA.html" "%%A"
)
