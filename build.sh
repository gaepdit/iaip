cd www
find ./ -iname "*.md" -type f -exec sh -c 'pandoc -f markdown+startnum -t html5 --email-obfuscation=references --template="../templates/default.html5" -B "../includes/body-header.inc" -A "../includes/body-footer.inc" -H "../includes/head.inc" -o "${0%.md}.html" "${0}" ' {} \;
 