pandoc.exe -f markdown -t html5 -s -o "index.html" -c "http://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" -c "assets/style.css" -A "assets/body-footer.html" "_index.md"
