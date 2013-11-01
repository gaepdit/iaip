// DOM functions
function addElement(el, content, target) {
    var newEl = document.createElement(el);
    var newContent = document.createTextNode(content);

    newEl.appendChild(newContent);

    insertAfter(document.getElementById(target), newEl);
}
function insertAfter(referenceNode, newNode) {
    referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
}
// Add version info
if (version.number) {
    var versionString = "Version " + version.number;
    if (version.releaseDate) {
        versionString += " — " + version.releaseDate;
    }
    addElement("h2", versionString, "integrated-air-information-platform-release-notes");
}
