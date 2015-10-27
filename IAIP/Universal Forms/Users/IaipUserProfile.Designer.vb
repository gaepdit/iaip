<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipUserProfile
    Inherits BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Save = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.MessageDisplay = New System.Windows.Forms.Label()
        Me.OfficeNumber = New System.Windows.Forms.TextBox()
        Me.EmailAddress = New System.Windows.Forms.TextBox()
        Me.LastName = New System.Windows.Forms.TextBox()
        Me.FirstName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LNameLabel = New System.Windows.Forms.Label()
        Me.FNameLabel = New System.Windows.Forms.Label()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.RequiredLabel = New System.Windows.Forms.Label()
        Me.PhoneNumber = New System.Windows.Forms.TextBox()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Save
        '
        Me.Save.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Save.Location = New System.Drawing.Point(111, 166)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(90, 23)
        Me.Save.TabIndex = 5
        Me.Save.Text = "OK"
        Me.Save.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(207, 166)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'MessageDisplay
        '
        Me.MessageDisplay.AutoSize = True
        Me.MessageDisplay.Location = New System.Drawing.Point(25, 202)
        Me.MessageDisplay.MaximumSize = New System.Drawing.Size(270, 0)
        Me.MessageDisplay.MinimumSize = New System.Drawing.Size(270, 0)
        Me.MessageDisplay.Name = "MessageDisplay"
        Me.MessageDisplay.Size = New System.Drawing.Size(270, 65)
        Me.MessageDisplay.TabIndex = 26
        Me.MessageDisplay.Text = "Message Label" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "5" & Global.Microsoft.VisualBasic.ChrW(13)
        Me.MessageDisplay.Visible = False
        '
        'OfficeNumber
        '
        Me.OfficeNumber.Location = New System.Drawing.Point(111, 129)
        Me.OfficeNumber.MaxLength = 10
        Me.OfficeNumber.Name = "OfficeNumber"
        Me.OfficeNumber.Size = New System.Drawing.Size(103, 20)
        Me.OfficeNumber.TabIndex = 4
        '
        'EmailAddress
        '
        Me.EmailAddress.Location = New System.Drawing.Point(111, 77)
        Me.EmailAddress.MaxLength = 100
        Me.EmailAddress.Name = "EmailAddress"
        Me.EmailAddress.Size = New System.Drawing.Size(184, 20)
        Me.EmailAddress.TabIndex = 2
        '
        'LastName
        '
        Me.LastName.Location = New System.Drawing.Point(111, 51)
        Me.LastName.MaxLength = 100
        Me.LastName.Name = "LastName"
        Me.LastName.Size = New System.Drawing.Size(183, 20)
        Me.LastName.TabIndex = 1
        '
        'FirstName
        '
        Me.FirstName.Location = New System.Drawing.Point(111, 25)
        Me.FirstName.MaxLength = 100
        Me.FirstName.Name = "FirstName"
        Me.FirstName.Size = New System.Drawing.Size(183, 20)
        Me.FirstName.TabIndex = 0
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(25, 132)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 13)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "Office Number"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(25, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 13)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Email Address *"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(25, 106)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Phone"
        '
        'LNameLabel
        '
        Me.LNameLabel.AutoSize = True
        Me.LNameLabel.Location = New System.Drawing.Point(25, 54)
        Me.LNameLabel.Name = "LNameLabel"
        Me.LNameLabel.Size = New System.Drawing.Size(65, 13)
        Me.LNameLabel.TabIndex = 22
        Me.LNameLabel.Text = "Last Name *"
        '
        'FNameLabel
        '
        Me.FNameLabel.AutoSize = True
        Me.FNameLabel.Location = New System.Drawing.Point(25, 28)
        Me.FNameLabel.Name = "FNameLabel"
        Me.FNameLabel.Size = New System.Drawing.Size(64, 13)
        Me.FNameLabel.TabIndex = 20
        Me.FNameLabel.Text = "First Name *"
        '
        'EP
        '
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'RequiredLabel
        '
        Me.RequiredLabel.AutoSize = True
        Me.RequiredLabel.Location = New System.Drawing.Point(25, 171)
        Me.RequiredLabel.Name = "RequiredLabel"
        Me.RequiredLabel.Size = New System.Drawing.Size(58, 13)
        Me.RequiredLabel.TabIndex = 23
        Me.RequiredLabel.Text = "(* required)"
        '
        'PhoneNumber
        '
        Me.PhoneNumber.Location = New System.Drawing.Point(111, 103)
        Me.PhoneNumber.MaxLength = 15
        Me.PhoneNumber.Name = "PhoneNumber"
        Me.PhoneNumber.Size = New System.Drawing.Size(103, 20)
        Me.PhoneNumber.TabIndex = 4
        '
        'IaipUserProfile
        '
        Me.AcceptButton = Me.Save
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(329, 282)
        Me.Controls.Add(Me.FirstName)
        Me.Controls.Add(Me.LastName)
        Me.Controls.Add(Me.EmailAddress)
        Me.Controls.Add(Me.PhoneNumber)
        Me.Controls.Add(Me.OfficeNumber)
        Me.Controls.Add(Me.MessageDisplay)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.RequiredLabel)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.LNameLabel)
        Me.Controls.Add(Me.FNameLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IaipUserProfile"
        Me.Text = "User Profile"
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OfficeNumber As System.Windows.Forms.TextBox
    Friend WithEvents EmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents LastName As System.Windows.Forms.TextBox
    Friend WithEvents FirstName As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents LNameLabel As System.Windows.Forms.Label
    Friend WithEvents FNameLabel As System.Windows.Forms.Label
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents EP As System.Windows.Forms.ErrorProvider
    Friend WithEvents MessageDisplay As System.Windows.Forms.Label
    Friend WithEvents RequiredLabel As System.Windows.Forms.Label
    Friend WithEvents PhoneNumber As System.Windows.Forms.TextBox
End Class
