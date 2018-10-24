Imports System.ComponentModel
Imports Iaip.Apb
Imports Iaip.DAL

Public Class AirNumberEntryForm

    ' Properties

    <Category("Appearance"), Description("Message displayed when AIRS # entered does not exist in database.")>
    Public Property FacilityDoesNotExistMessage As String = "Facility does not exist."

    <Category("Appearance"), Description("Message displayed when AIRS # has an invalid format.")>
    Public Property InvalidFormatMessage As String = "Invalid AIRS #."

    <Category("Appearance"), Description("Whether to display the error message label.")>
    Public Property DisplayErrorMessage As Boolean = True

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

    <Category("Appearance"), Description("Specifies the placeholder text to display in the TextBox.")>
    Public Property Cue As String
        Get
            Return AirsEntryTextBox.Cue
        End Get
        Set(value As String)
            AirsEntryTextBox.Cue = value
        End Set
    End Property

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

    ' Constructor

    Public Sub New()
        InitializeComponent()
        AirsEntryErrorLabel.ForeColor = IaipColors.ErrorForeColor
    End Sub

    ' Methods

    Public Sub Clear()
        AirsEntryTextBox.Clear()
    End Sub

    ' Events

    Private Sub AirsEntryTextBox_ValidationStatusChanged(sender As Object, e As EventArgs) Handles AirsEntryTextBox.ValidationStatusChanged
        SetAirsEntryErrorProvider()
    End Sub

    Private Sub SetAirsEntryErrorProvider()
        Select Case AirsEntryTextBox.ValidationStatus
            Case AirsNumberValidationResult.NonExistent
                AirsEntryErrorProvider.SetError(AirsEntryTextBox, FacilityDoesNotExistMessage)
            Case AirsNumberValidationResult.InvalidFormat
                AirsEntryErrorProvider.SetError(AirsEntryTextBox, InvalidFormatMessage)
            Case Else
                AirsEntryErrorProvider.SetError(AirsEntryTextBox, String.Empty)
        End Select

        If DisplayErrorMessage Then
            AirsEntryErrorLabel.Text = AirsEntryErrorProvider.GetError(AirsEntryTextBox)
        End If
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

End Class
