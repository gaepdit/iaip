# Facility status codes

```sql
select distinct STRUPDATESTATUS as Status,
                count(*)        as Count
from AFSFACILITYDATA
group by STRUPDATESTATUS;
```

| Status code | Meaning              | When set          | Where set                  |
|:-----------:|----------------------|-------------------|----------------------------|
|      H      | New                  | AIRS # created    | Trigger on AIRS # table    |
|      A      | Approved             | Facility approved | IAIP facility creator tool |
|      I      | Inactive             | Facility unused   | IAIP facility creator tool |
|      C      | Changed (deprecated) | N/A               |                            |
