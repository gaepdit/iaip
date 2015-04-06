Public Class DmuEdtErrorDetail

#Region " Properties and variables "

    Private edtErrorDetails As DMU.EdtError

    Private _errorID As Integer
    Public Property ErrorID() As Integer
        Get
            Return _errorID
        End Get
        Set(ByVal value As Integer)
            _errorID = value
        End Set
    End Property

#End Region

#Region " Data "

    Public Sub GetData()
        ErrorIDDisplay.Text = ErrorID.ToString
    End Sub

#End Region


End Class