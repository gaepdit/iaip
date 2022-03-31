# Permit Application Types

| Code | Type       | Active |
|------|------------|--------|
| 0    | N/A        | True   |
| 2    | Acid Rain  | True   |
| 3    | Closed     | True   |
| 4    | ERC        | True   |
| 8    | OFF PERMIT | True   |
| 9    | PBR        | True   |
| 11   | SIP        | True   |
| 12   | SM         | True   |
| 14   | TV-Initial | True   |
| 15   | 502(b)10   | True   |
| 16   | TV-Renewal | True   |
| 19   | MAWO       | True   |
| 20   | MAW        | True   |
| 21   | SAWO       | True   |
| 22   | SAW        | True   |
| 25   | NC         | True   |
| 26   | AA         | True   |
| 1    | 112(g)     | False  |
| 5    | FESOP      | False  |
| 6    | LTR        | False  |
| 7    | NPR        | False  |
| 10   | PSD        | False  |
| 13   | SM(TV)     | False  |
| 17   | TV-Amend   | False  |
| 18   | SLSM       | False  |
| 23   | SM80       | False  |
| 24   | PCP        | False  |
| 27   | Title V    | False  |

```sql
select STRAPPLICATIONTYPECODE as Code,
       STRAPPLICATIONTYPEDESC as Type,
       IIF(STRAPPLICATIONTYPEUSED is null, 'True', STRAPPLICATIONTYPEUSED)
                              as Active
from LOOKUPAPPLICATIONTYPES t
order by t.STRAPPLICATIONTYPEUSED, convert(int, t.STRAPPLICATIONTYPECODE);
```
