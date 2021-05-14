# Air Program Codes

Facility air programs are stored in the database in `APBHEADERDATA.STRAIRPROGRAMCODES` as a 15-character coded string. In the IAIP, these are transformed into the `FacilityEnums.AirPrograms` Flag Enum

| Bit | Legacy | Desc                 | New      | In use |
|-----|--------|----------------------|----------|--------|
| 1   | 0      | SIP                  | CAASIP   | Y      |
| 2   | 1      | Fed. SIP             | CAAFIP   | Y      |
| 3   | 3      | Non Fed SIP          | CAANFRP  |        |
| 4   | 4      | CFC Tracking         | CAACFC   |        |
| 5   | 6      | PSD                  | CAAPSD   | Y      |
| 6   | 7      | NSR                  | CAANSR   | Y      |
| 7   | 8      | NESHAP               | CAANESH  | Y      |
| 8   | 9      | NSPS                 | CAANSPS  | Y      |
| 9   | F      | FESOP                | CAAFESOP |        |
| 10  | A      | Acid Precipitation   | CAAAR    | Y      |
| 11  | I      | Native American      | CAANAM   |        |
| 12  | M      | MACT                 | CAAMACT  | Y      |
| 13  | V      | Title V              | CAATVP   | Y      |
| 14  |        | Risk Management Plan | CAARMP   |        |
| 15  |        | Unused               |          |        |
