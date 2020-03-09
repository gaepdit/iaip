% IAIP Development Version Installation Page

Click the button to download and run the setup file.

[Download the
IAIP Dev Edition](IaipDev.application)


## Important Notes -- Please Read

The IAIP Development Edition is for EPD IT testing use only. If you don't work for EPD IT, you're in the wrong place!

The Dev version of the IAIP can be installed side-by-side with the production and/or UAT versions of the IAIP. They can be used simultaneously and will not interfere with each other. The Dev version will *only* access the Dev database, so no changes to production data can be made.

<pre class="mermaid">
graph TD;
    A[fa:fa-desktop IAIP] --> B(fa:fa-database production database);
    G[fa:fa-laptop GECO] --> B;
    C[fa:fa-desktop IAIP UAT] --> D(fa:fa-database test database);
    H[fa:fa-laptop GECO UAT] --> D;
    E[fa:fa-desktop IAIP Dev] --> F(fa:fa-database dev database);
    I[fa:fa-laptop GECO Dev] --> F;
</pre>

<script src="https://use.fontawesome.com/73014ea0c4.js"></script>
<script src="https://unpkg.com/mermaid@8.0.0-rc.8/dist/mermaid.min.js"></script>
<script>
    var config = {
        startOnLoad:true,
        flowchart:{htmlLabels:true}
    };
    mermaid.initialize(config);
</script>
