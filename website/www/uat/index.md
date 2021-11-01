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
<script src="https://cdnjs.cloudflare.com/ajax/libs/mermaid/8.11.5/mermaid.min.js" integrity="sha512-LEGEAp7eSh0xL8TV4ARXWfBz3TpnIDrGT61hbqAN/xjn+CnaoNfsJzdyMSO0IYhaAom+bCs9ELiGzljsi11qjw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
<script>mermaid.initialize({startOnLoad:true});</script>
