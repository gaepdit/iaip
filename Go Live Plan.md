# IAIP SQL Server Edition Go Live Plan

Steps to take when readying IAIP SQL Server Edition for production...

## Branch management

✓ Merge ss-develop branch into develop.

## Compilation constants

✓ Unset the SqlServer compilation constant.

## Application settings

* ✓ Change icon to correct icon file (from GA-outline.ico)
* ✓ Change assembly name to IAIP (from IAIP_SQL_Server)
* ✓ Change publish and installation locations to `.../iaip/install/` (from `.../iaip/test/`)
* ✗ Change update timing to "after the application starts" (instead of before)
* ✓ Change product name to "Integrated Air Information Platform" (from "IAIP SQL Server Edition")
* ✓ Change publish version and minimum required version
* ✓ Unset automatically increment revision with each publish