cls
for /r "www" %%A in (*.md) do (
	pandoc.exe -f markdown+startnum -t html5 --email-obfuscation=references --template="includes/template.html5" -H "includes/head.inc" -o "%%~dpnA.html" "%%A"
)
