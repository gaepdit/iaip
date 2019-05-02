Imports System.ComponentModel
Imports Iaip.Apb
Imports Iaip.DAL

Public Class AirNumberEntryForm

    ' Custom Properties 

    <Category("Appearance"), Description("Message displayed when AIRS # entered does not exist in database.")>
    <DefaultValue("Facility does not exist.")>
    Public Property FacilityDoesNotExistMessage As String = "Facility does not exist."

    <Category("Appearance"), Description("Message displayed when AIRS # has an invalid format.")>
    <DefaultValue("Invalid AIRS number.")>
    Public Property InvalidFormatMessage As String = "Invalid AIRS number."

    <Category("Behavior"), Description("Label to display AIRS # error message.")>
    Public Property ErrorMessageLabel As Label
        Get
            Return _errorMessageLabel
        End Get
        Set(value As Label)
            _errorMessageLabel = value

            If value Is Nothing Then
                airsEntryErrorProvider = New IaipErrorProvider(AirsEntryTextBox)
            Else
                airsEntryErrorProvider = New IaipErrorProvider(AirsEntryTextBox, value)
            End If
        End Set
    End Property
    Private _errorMessageLabel As Label

    Public ReadOnly Property HasError As Boolean
        Get
            Return airsEntryErrorProvider.HasError
        End Get
    End Property

    ' Local fields

    Private airsEntryErrorProvider As IaipErrorProvider

    ' AirsEntryTextBox pass-thru properties 

    <Browsable(False)>
    Public Property AirsNumber As ApbFacilityId
        Get
            Return AirsEntryTextBox.AirsNumber
        End Get
        Set(value As ApbFacilityId)
            AirsEntryTextBox.AirsNumber = value
        End Set
    End Property

    <Category("Behavior"), Description("Specifies whether the AIRS number must exist in the database to be valid.")>
    Public Property FacilityMustExist As Boolean
        Get
            Return AirsEntryTextBox.FacilityMustExist
        End Get
        Set(value As Boolean)
            AirsEntryTextBox.FacilityMustExist = value
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property ValidationStatus As AirsNumberValidationResult
        Get
            Return AirsEntryTextBox.ValidationStatus
        End Get
    End Property

    ' CueTextBox pass-thru properties 

    <Category("Appearance"), Description("Specifies the placeholder text to display in the TextBox.")>
    <DefaultValue("000-00000")>
    Public Property Cue As String
        Get
            Return AirsEntryTextBox.Cue
        End Get
        Set(value As String)
            If AirsEntryTextBox.Cue <> value Then
                AirsEntryTextBox.Cue = value
            End If
        End Set
    End Property

    ' TextBox pass-thru properties 

    <Category("Data"), Description("User-defined data associated with the object.")>
    Public Overloads Property Tag As Object
        Get
            Return AirsEntryTextBox.Tag
        End Get
        Set(value As Object)
            AirsEntryTextBox.Tag = value
        End Set
    End Property

    <Category("Appearance"), Description("The text associated with the control.")>
    Public Overrides Property Text As String
        Get
            Return AirsEntryTextBox.Text
        End Get
        Set(value As String)
            AirsEntryTextBox.Text = value
        End Set
    End Property

    <Category("Behavior"), Description("Controls whether the text can be changed or not.")>
    Public Property [ReadOnly] As Boolean
        Get
            Return AirsEntryTextBox.ReadOnly
        End Get
        Set(value As Boolean)
            AirsEntryTextBox.ReadOnly = value
        End Set
    End Property

    <Category("Appearance"), Description("The background color of the text field.")>
    Public Property TextBoxBackColor As Color
        Get
            Return AirsEntryTextBox.BackColor
        End Get
        Set(value As Color)
            AirsEntryTextBox.BackColor = value
        End Set
    End Property

    ' Constructor

    Public Sub New()
        InitializeComponent()
        airsEntryErrorProvider = New IaipErrorProvider(AirsEntryTextBox)
    End Sub

    ' Methods

    Public Sub Clear()
        AirsEntryTextBox.Clear()
        airsEntryErrorProvider.ClearError()
    End Sub

    ' Events

    Private Sub AirsEntryTextBox_ValidationStatusChanged(sender As Object, e As EventArgs) Handles AirsEntryTextBox.ValidationStatusChanged
        Select Case AirsEntryTextBox.ValidationStatus

            Case AirsNumberValidationResult.NonExistent
                airsEntryErrorProvider.SetError(FacilityDoesNotExistMessage)

            Case AirsNumberValidationResult.InvalidFormat
                airsEntryErrorProvider.SetError(InvalidFormatMessage)

            Case Else
                airsEntryErrorProvider.ClearError()

        End Select
    End Sub

    ' Pass-thru events

    Public Event AirsNumberChanged As EventHandler

    Private Sub AirsEntryTextBox_AirsNumberChanged(sender As Object, e As EventArgs) Handles AirsEntryTextBox.AirsNumberChanged
        RaiseEvent AirsNumberChanged(Me, e)
    End Sub

    Public Event AirsTextChanged As EventHandler

    Private Sub AirsEntryTextBox_TextChanged(sender As Object, e As EventArgs) Handles AirsEntryTextBox.TextChanged
        RaiseEvent AirsTextChanged(sender, e)
    End Sub

    Public Event AirsTextEnter As EventHandler

    Private Sub AirsEntryTextBox_Enter(sender As Object, e As EventArgs) Handles AirsEntryTextBox.Enter
        RaiseEvent AirsTextEnter(sender, e)
    End Sub

    Public Event AirsTextLeave As EventHandler

    Private Sub AirsEntryTextBox_Leave(sender As Object, e As EventArgs) Handles AirsEntryTextBox.Leave
        RaiseEvent AirsTextLeave(sender, e)
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If airsEntryErrorProvider IsNot Nothing Then airsEntryErrorProvider.Dispose()
                If components IsNot Nothing Then components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class
