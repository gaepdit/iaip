pandoc.exe -f markdown+abbreviations+startnum -t html5 --standalone --toc --smart -o "index.html" -c "http://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" -c "assets/style.css" -c "assets/style-footer.css" -B "assets/body-header.html" -A "assets/body-footer.html" "_index.md"