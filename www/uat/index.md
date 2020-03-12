% IAIP UAT Installation Page

Click the button to download and run the setup file.

[Download the
IAIP UAT Edition](IaipHorizon.application)


## Important Notes -- Please Read

The IAIP UAT (User Acceptance Testing) Edition is made available when users are requested to test new features or bugfixes before they are released to production. Unless someone in EPD IT has asked you to install this, you probably don't need it.

The UAT version of the IAIP can be installed side-by-side with the production version of the IAIP. Both can be used simultaneously and will not interfere with each other. The UAT version will *only* access the testing (UAT) database, so no changes to production data can be made.

<pre class="mermaid">
graph TD;
    A[fa:fa-desktop IAIP] --> B(fa:fa-database production database);
    G[fa:fa-laptop GECO] --> B;
    C[fa:fa-desktop IAIP UAT] --> D(fa:fa-database test database);
    H[fa:fa-laptop GECO UAT] --> D;
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
