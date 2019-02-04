Imports System.ComponentModel

Friend Class CurrencyTextBox
    Inherits CueTextBox

    ' Properties

    <Category("Data")>
    Public Property Amount As Decimal
        Get
            Return _amount
        End Get
        Set
            If _amount <> Value Then
                _amount = Value
                Text = String.Format(Globalization.CultureInfo.CurrentCulture, "{0:C0}", Value)

                If Not isValidating Then
                    ValidationStatus = ValidateText()
                End If

                OnAmountChanged(EventArgs.Empty)
            End If
        End Set
    End Property
    Private _amount As Decimal

    <Browsable(False)>
    Public Property ValidationStatus As CurrencyValidationStatus
        Get
            Return _validationStatus
        End Get
        Private Set(value As CurrencyValidationStatus)
            If (value <> _validationStatus) Then
                _validationStatus = value

                OnValidationStatusChanged(EventArgs.Empty)
            End If
        End Set
    End Property
    Private _validationStatus As CurrencyValidationStatus

    <Category("Behavior")>
    Public Property MaxValue As Decimal = Decimal.MaxValue

    <Category("Behavior")>
    Public Property MinValue As Decimal = Decimal.MinValue

    <Browsable(False)>
    Public ReadOnly Property IsValid As Boolean
        Get
            Return ValidationStatus = CurrencyValidationStatus.Valid
        End Get
    End Property

    ' Cuebox pass-thru properties

    <Category("Appearance"), Description("Specifies the placeholder text to display in the TextBox.")>
    Public Overrides Property Cue As String = "$0"

    Public Enum CurrencyValidationStatus
        Valid
        InvalidFormat
        AboveMaximum
        BelowMinimum
    End Enum

    ' Local fields

    Private isValidating As Boolean = False

    ' Constructor

    Public Sub New()
        MyBase.Cue = "$0"

        InitializeComponent()
    End Sub

    ' Custom Events

    Public Event AmountChanged As EventHandler

    Protected Overridable Sub OnAmountChanged(e As EventArgs)
        RaiseEvent AmountChanged(Me, e)
    End Sub

    Public Event ValidationStatusChanged As EventHandler

    Protected Overridable Sub OnValidationStatusChanged(e As EventArgs)
        RaiseEvent ValidationStatusChanged(Me, e)
    End Sub

    ' Validation 

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Enter Then
            ValidateTextBox()
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub TextBox_Validating(sender As Object, e As CancelEventArgs) Handles MyBase.Validating
        ValidateTextBox()
    End Sub

    Private Sub ValidateTextBox()
        ValidationStatus = ValidateText()

        isValidating = True

        If ValidationStatus = CurrencyValidationStatus.InvalidFormat Then
            Amount = 0
        Else
            Amount = CDec(Text)
        End If

        isValidating = False
    End Sub

    Private Function ValidateText() As CurrencyValidationStatus
        Dim amt As Decimal

        If Not Decimal.TryParse(Text.Replace("$", "").Replace(" ", ""), amt) Then
            Return CurrencyValidationStatus.InvalidFormat
        End If

        If amt > MaxValue Then
            Return CurrencyValidationStatus.AboveMaximum
        End If

        If amt < MinValue Then
            Return CurrencyValidationStatus.BelowMinimum
        End If

        Return CurrencyValidationStatus.Valid
    End Function

    Private Sub InitializeComponent()
        SuspendLayout()
        '
        'CurrencyTextBox
        '
        TextAlign = HorizontalAlignment.Right
        ResumeLayout(False)
    End Sub

End Class