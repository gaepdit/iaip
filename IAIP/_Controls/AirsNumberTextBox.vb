Imports System.ComponentModel
Imports Iaip.Apb
Imports Iaip.Apb.ApbFacilityId
Imports Iaip.DAL

Public Class AirsNumberTextBox
    Inherits CueTextBox

    ' Properties

    <Browsable(False)>
    Public Property AirsNumber As ApbFacilityId
        Get
            Return _airsNumber
        End Get
        Set(value As ApbFacilityId)
            If _airsNumber <> value Then
                _airsNumber = value

                If value IsNot Nothing Then
                    Text = _airsNumber.FormattedString
                End If

                If Not isValidating Then
                    ValidationStatus = ValidateText()
                End If

                OnAirsNumberChanged(EventArgs.Empty)
            End If
        End Set
    End Property
    Private _airsNumber As ApbFacilityId

    <Category("Behavior"), Description("Specifies whether the AIRS number must exist in the database to be valid.")>
    <DefaultValue(False)>
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
    Public ReadOnly Property IsValid As Boolean
        Get
            Return _validationStatus = AirsNumberValidationResult.Valid
        End Get
    End Property

    <Category("Appearance"), Description("Specifies the placeholder text to display in the TextBox.")>
    <DefaultValue("000-00000")>
    <CodeAnalysis.SuppressMessage("Critical Bug", "S4275:Getters and setters should access the expected fields", Justification:="<Pending>")>
    Public Overrides Property Cue As String
        Get
            Return MyBase.Cue
        End Get
        Set(value As String)
            If MyBase.Cue <> value Then
                MyBase.Cue = value
            End If
        End Set
    End Property

    <Browsable(False)>
    Public Overrides Property MaxLength As Integer = 9

    ' Local fields

    Private isValidating As Boolean

    ' Constructor

    Public Sub New()
        MyBase.MaxLength = 9
        MyBase.Cue = "000-00000"
    End Sub

    ' Events

    Public Event AirsNumberChanged As EventHandler

    Protected Overridable Sub OnAirsNumberChanged(e As EventArgs)
        RaiseEvent AirsNumberChanged(Me, e)
    End Sub

    Public Event ValidationStatusChanged As EventHandler

    Protected Overridable Sub OnValidationStatusChanged(e As EventArgs)
        RaiseEvent ValidationStatusChanged(Me, e)
    End Sub

    ' Methods

    Public Overloads Sub Clear()
        MyBase.Clear()
        AirsNumber = Nothing
    End Sub

    ' Validation

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

        isValidating = True

        If ValidationStatus = AirsNumberValidationResult.Valid OrElse ValidationStatus = AirsNumberValidationResult.NonExistent Then
            AirsNumber = New ApbFacilityId(Text)
        Else
            AirsNumber = Nothing
        End If

        isValidating = False
    End Sub

    Private Function ValidateText() As AirsNumberValidationResult
        If String.IsNullOrEmpty(Text) Then
            Return AirsNumberValidationResult.Empty
        End If

        If Not IsValidAirsNumberFormat(Text) Then
            Return AirsNumberValidationResult.InvalidFormat
        End If

        If Not FacilityMustExist OrElse AirsNumberExists(Text) Then
            Return AirsNumberValidationResult.Valid
        End If

        Return AirsNumberValidationResult.NonExistent
    End Function

End Class

