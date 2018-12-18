cls
for /r "www" %%A in (*.md) do (
	pandoc.exe -f markdown+startnum -t html5 --smart --email-obfuscation=references --template="templates/default.html5" -B "includes/body-header.inc" -A "includes/body-footer.inc" -H "includes/head.inc" -o "%%~dpnA.html" "%%A"
)
