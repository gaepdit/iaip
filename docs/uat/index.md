% IAIP UAT Installation Page

Click the button to download and run the setup file.

[Download the IAIP<br />UAT Edition](IaipHorizon.application)

## Important Notes -- Please Read

The IAIP UAT (User Acceptance Testing) Edition is made available when users are requested to test new features or bugfixes before they are released to production. Unless someone in EPD IT has asked you to install this, you probably don't need it.

The UAT version of the IAIP can be installed side-by-side with the production version of the IAIP. Both can be used simultaneously and will not interfere with each other. The UAT version will *only* access the testing (UAT) database, so no changes to production data can be made.

<pre class="mermaid">
flowchart TD;
    subgraph UAT["UAT (You are here)"]
        D[fa:fa-tablet IAIP UAT] --> E(fa:fa-database test database);
        F[fa:fa-tablet GECO UAT] --> E;
    end
    subgraph Production
        A[fa:fa-tablet IAIP] --> B(fa:fa-database production database);
        C[fa:fa-tablet GECO] --> B;
    end
    style Production stroke:#6e6e6e,fill:#f0f0f0
    style UAT stroke:#8fc982,fill:#dff0db
</pre>

<script src="https://use.fontawesome.com/73014ea0c4.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/mermaid/9.1.5/mermaid.min.js" integrity="sha512-ETySttxD+gVb33UXscT3G5gtPxWepHF6fs69M13VEzJfvA6wcDwMjmkCHwrH4NPzPzMarhk/aZoVsY7BTGfnQw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
<script>mermaid.initialize({startOnLoad:true,theme:'neutral'});</script>
