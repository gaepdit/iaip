pandoc.exe -f markdown+startnum -t html5 --toc --toc-depth=2 --smart --email-obfuscation=references -c "http://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" -c "assets/style.css" -B "assets/body-header.html" -A "assets/body-footer.html" -o "index.html" "index.md"
pandoc.exe -f markdown+startnum -t html5 --smart --email-obfuscation=references -c "http://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" -c "../assets/style.css" -c "../assets/installer-style.css" -B "assets/body-header.html" -A "assets/body-footer.html" -o "install/index.html" "install/index.md"
pandoc.exe -f markdown+startnum -t html5 --smart --email-obfuscation=references -c "http://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" -c "assets/style.css" -c "assets/changelog-style.css" -B "assets/body-header.html" -A "assets/body-footer.html" -o "changelog.html" "changelog.md"