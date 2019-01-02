Namespace Apb.Sspp

    Public Class ApplicationFeeInfo

        ' Permit application
        Public Property ApplicationID As Integer
        Public Property FacilityID As ApbFacilityId
        Public Property ApplicationWithdrawn As Boolean

        ' Permit application fees
        Public Property ApplicationFeeApplies As Boolean
        Public Property ApplicationFeeType As Integer?
        Public Property ApplicationFeeAmount As Decimal
        Public Property ApplicationFeeOverride As Boolean
        Public Property ApplicationFeeOverrideReason As String

        ' Expedited review fees
        Public Property ExpeditedFeeApplies As Boolean
        Public Property ExpeditedFeeType As Integer?
        Public Property ExpeditedFeeAmount As Decimal
        Public Property ExpeditedFeeOverride As Boolean
        Public Property ExpeditedFeeOverrideReason As String

        ' Invoicing
        Public Property FeeDataFinalized As Boolean
        Public Property DateFeeDataFinalized As Date?
        Public Property DateFacilityNotifiedOfFees As Date?

    End Class

End Namespace
