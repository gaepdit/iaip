﻿Imports System.ComponentModel

Namespace Apb.Finance

    Public Enum InvoiceCategory
        <Description("Emissions Fees")>
        EmissionsFees
        <Description("Permit Application Fees")>
        PermitApplicationFees
    End Enum

    Public Enum InvoiceItemStatus
        Pending = 0
        Canceled = 1
        Invoiced = 2
    End Enum

    Public Enum FeeRateCategory
        <Description("Permit Application Fee")>
        PermitApplication = 0
        <Description("Expedited Review Fee")>
        ExpeditedReview = 1
    End Enum

End Namespace
