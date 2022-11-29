# Facility status codes

```sql
select distinct STRUPDATESTATUS as Status,
                count(*)        as Count
from AFSFACILITYDATA
group by STRUPDATESTATUS;
```

| Status | Count | Inferred meaning | When set             | Where set                      |
|:------:|------:|------------------|----------------------|--------------------------------|
|    H   |    41 | New              | New AIRS #           | Trigger on APBMASTERAIRS table |
|    A   |   574 | Approved         | On Facility approval | IAIP facility approval         |
|    C   |  6123 | Changed *        | On Facility update   | Various DB triggers            |

* Deprecated. Status is only changed to 'C' if it is currently 'N', which it never is.
