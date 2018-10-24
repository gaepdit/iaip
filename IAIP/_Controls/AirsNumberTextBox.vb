Imports System.ComponentModel
Imports Iaip.Apb
Imports Iaip.Apb.ApbFacilityId
Imports Iaip.DAL

Friend Class AirsNumberTextBox
    Inherits CueTextBox

    ' Properties

    <Browsable(False)>
    Public Property AirsNumber As ApbFacilityId
        Get
            Return _airsNumber
        End Get
        Set(value As ApbFacilityId)
            _airsNumber = value

            If value IsNot Nothing Then
                Text = _airsNumber.FormattedString
            End If
        End Set
    End Property
    Private _airsNumber As ApbFacilityId

    <Category("Behavior"), Description("Specifies whether the AIRS number must exist in the database to be valid.")>
    Public Property FacilityMustExist As Boolean
        Get
            Return _facilityMustExist
        End Get
        Set(value As Boolean)
            _facilityMustExist = value
            ValidationStatus = ValidateText()
        End Set
    End Property
    Private _facilityMustExist As Boolean

    <Browsable(False)>
    Public Property ValidationStatus As AirsNumberValidationResult
        Get
            Return _validationStatus
        End Get
        Private Set(value As AirsNumberValidationResult)
            If (value <> _validationStatus) Then
                _validationStatus = value
                OnValidationStatusChanged(EventArgs.Empty)
            End If
        End Set
    End Property
    Private _validationStatus As AirsNumberValidationResult

    <Browsable(False)>
    Public Overrides Property MaxLength As Integer = 9

    <Category("Appearance"), Description("Specifies the placeholder text to display in the TextBox.")>
    Public Overrides Property Cue As String = "000-00000"

    ' Constructor

    Public Sub New()
        MyBase.MaxLength = 9
        MyBase.Cue = "000-00000"
    End Sub

    ' Events

    Public Event ValidationStatusChanged As EventHandler

    Protected Overridable Sub OnValidationStatusChanged(e As EventArgs)
        RaiseEvent ValidationStatusChanged(Me, e)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Enter Then
            ValidateTextBox()
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub AirsNumberTextBox_Validating(sender As Object, e As CancelEventArgs) Handles MyBase.Validating
        ValidateTextBox()
    End Sub

    Private Sub ValidateTextBox()
        ValidationStatus = ValidateText()

        If ValidationStatus = AirsNumberValidationResult.Valid Or ValidationStatus = AirsNumberValidationResult.NonExistent Then
            AirsNumber = New ApbFacilityId(Text)
        Else
            AirsNumber = Nothing
        End If
    End Sub

    Private Function ValidateText() As AirsNumberValidationResult
        If String.IsNullOrEmpty(Me.Text) Then
            Return AirsNumberValidationResult.Empty
        End If

        If Not IsValidAirsNumberFormat(Me.Text) Then
            Return AirsNumberValidationResult.InvalidFormat
        End If

        If Not FacilityMustExist OrElse AirsNumberExists(Me.Text) Then
            Return AirsNumberValidationResult.Valid
        End If

        Return AirsNumberValidationResult.NonExistent
    End Function

End Class

