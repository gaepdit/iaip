# iaip.gaepd.org

This is the website for hosting the IAIP installation files plus some documentation.

## Editing Instructions

Edit or add new Markdown (*.md) files.

## Testing Instructions

To generate and view the HTML files locally, run the `build.ps1` script. This requires that [Pandoc](https://pandoc.org/) be installed on your computer. (Generated HTML files are ignored by Git.)

## Deployment Instructions

The website is hosted on GitHub Pages and is automatically deployed by the `deploy-application.yml` GitHub workflow when a new production version of the IAIP is deployed.

The website can also be manually deployed by running the `update-website.yml` GitHub workflow.
