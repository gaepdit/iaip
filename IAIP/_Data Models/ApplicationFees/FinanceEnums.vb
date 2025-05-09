Imports System.ComponentModel

Namespace Apb.ApplicationFees

    Public Enum InvoiceCategory
        EmissionsFees
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
