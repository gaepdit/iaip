% IAIP Development Version Installation Page

Click the button to download and run the setup file.

[Download the IAIP<br />Dev Edition](IaipDev.application)

## Important Notes -- Please Read

The IAIP Development Edition is for EPD IT testing use only.

The Dev version of the IAIP can be installed side-by-side with the production and/or UAT versions of the IAIP. They can be used simultaneously and will not interfere with each other. The Dev version will *only* access the Dev database, so no changes to production data can be made.

<pre class="mermaid">
flowchart LR
    subgraph PU[" "]
        subgraph Production
            direction TB
            A[fa:fa-tablet IAIP] --> B(fa:fa-database production database);
            C[fa:fa-tablet GECO] --> B;
        end
        subgraph UAT
            direction TB
            D[fa:fa-tablet IAIP UAT] --> E(fa:fa-database test database);
            F[fa:fa-tablet GECO UAT] --> E;
        end
    end
    subgraph Dev["Dev (You are here)"]
        direction TB
        G[fa:fa-tablet IAIP Dev] --> H(fa:fa-database dev database);
        I[fa:fa-tablet GECO Dev] --> H;
    end
    style Production stroke:#6e6e6e,fill:#f0f0f0
    style UAT stroke:#6e6e6e,fill:#f0f0f0
    style Dev stroke:#cb3838,fill:#ffe1e1
    style PU stroke:none,fill:none
</pre>

<script src="https://use.fontawesome.com/73014ea0c4.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/mermaid/8.11.5/mermaid.min.js" integrity="sha512-LEGEAp7eSh0xL8TV4ARXWfBz3TpnIDrGT61hbqAN/xjn+CnaoNfsJzdyMSO0IYhaAom+bCs9ELiGzljsi11qjw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
<script>mermaid.initialize({startOnLoad:true,theme:'neutral'});</script>
