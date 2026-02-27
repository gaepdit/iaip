<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IaipCreateUser
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
        Me.Unit = New System.Windows.Forms.ComboBox()
        Me.Program = New System.Windows.Forms.ComboBox()
        Me.Branch = New System.Windows.Forms.ComboBox()
        Me.BranchLabel = New System.Windows.Forms.Label()
        Me.ProgramLabel = New System.Windows.Forms.Label()
        Me.UnitLabel = New System.Windows.Forms.Label()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.Username = New System.Windows.Forms.TextBox()
        Me.PhoneNumber = New System.Windows.Forms.TextBox()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Save
        '
        Me.Save.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Save.Location = New System.Drawing.Point(113, 269)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(86, 23)
        Me.Save.TabIndex = 9
        Me.Save.Text = "Save"
        Me.Save.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.CausesValidation = False
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(205, 269)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(86, 23)
        Me.Cancel.TabIndex = 10
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'MessageDisplay
        '
        Me.MessageDisplay.AutoSize = True
        Me.MessageDisplay.Location = New System.Drawing.Point(27, 305)
        Me.MessageDisplay.MaximumSize = New System.Drawing.Size(264, 0)
        Me.MessageDisplay.MinimumSize = New System.Drawing.Size(264, 0)
        Me.MessageDisplay.Name = "MessageDisplay"
        Me.MessageDisplay.Size = New System.Drawing.Size(264, 65)
        Me.MessageDisplay.TabIndex = 26
        Me.MessageDisplay.Text = "Message Label" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "5" & Global.Microsoft.VisualBasic.ChrW(13)
        Me.MessageDisplay.Visible = False
        '
        'OfficeNumber
        '
        Me.OfficeNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.OfficeNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.OfficeNumber.Location = New System.Drawing.Point(113, 153)
        Me.OfficeNumber.MaxLength = 10
        Me.OfficeNumber.Name = "OfficeNumber"
        Me.OfficeNumber.Size = New System.Drawing.Size(181, 20)
        Me.OfficeNumber.TabIndex = 5
        '
        'EmailAddress
        '
        Me.EmailAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.EmailAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.EmailAddress.Location = New System.Drawing.Point(113, 101)
        Me.EmailAddress.MaxLength = 100
        Me.EmailAddress.Name = "EmailAddress"
        Me.EmailAddress.Size = New System.Drawing.Size(181, 20)
        Me.EmailAddress.TabIndex = 3
        '
        'LastName
        '
        Me.LastName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.LastName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.LastName.Location = New System.Drawing.Point(113, 75)
        Me.LastName.MaxLength = 100
        Me.LastName.Name = "LastName"
        Me.LastName.Size = New System.Drawing.Size(180, 20)
        Me.LastName.TabIndex = 2
        '
        'FirstName
        '
        Me.FirstName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.FirstName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.FirstName.Location = New System.Drawing.Point(113, 49)
        Me.FirstName.MaxLength = 100
        Me.FirstName.Name = "FirstName"
        Me.FirstName.Size = New System.Drawing.Size(180, 20)
        Me.FirstName.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(27, 156)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 13)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "Office Number"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(27, 104)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 13)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Email Address *"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(27, 130)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Phone"
        '
        'LNameLabel
        '
        Me.LNameLabel.AutoSize = True
        Me.LNameLabel.Location = New System.Drawing.Point(27, 78)
        Me.LNameLabel.Name = "LNameLabel"
        Me.LNameLabel.Size = New System.Drawing.Size(65, 13)
        Me.LNameLabel.TabIndex = 22
        Me.LNameLabel.Text = "Last Name *"
        '
        'FNameLabel
        '
        Me.FNameLabel.AutoSize = True
        Me.FNameLabel.Location = New System.Drawing.Point(27, 52)
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
        Me.RequiredLabel.Location = New System.Drawing.Point(27, 274)
        Me.RequiredLabel.Name = "RequiredLabel"
        Me.RequiredLabel.Size = New System.Drawing.Size(58, 13)
        Me.RequiredLabel.TabIndex = 23
        Me.RequiredLabel.Text = "(* required)"
        '
        'Unit
        '
        Me.Unit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Unit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Unit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Unit.FormattingEnabled = True
        Me.Unit.Location = New System.Drawing.Point(113, 230)
        Me.Unit.Name = "Unit"
        Me.Unit.Size = New System.Drawing.Size(180, 21)
        Me.Unit.TabIndex = 8
        '
        'Program
        '
        Me.Program.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Program.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Program.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Program.FormattingEnabled = True
        Me.Program.Location = New System.Drawing.Point(113, 205)
        Me.Program.Name = "Program"
        Me.Program.Size = New System.Drawing.Size(180, 21)
        Me.Program.TabIndex = 7
        '
        'Branch
        '
        Me.Branch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Branch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Branch.FormattingEnabled = True
        Me.Branch.Location = New System.Drawing.Point(113, 179)
        Me.Branch.Name = "Branch"
        Me.Branch.Size = New System.Drawing.Size(180, 21)
        Me.Branch.TabIndex = 6
        '
        'BranchLabel
        '
        Me.BranchLabel.AutoSize = True
        Me.BranchLabel.Location = New System.Drawing.Point(29, 182)
        Me.BranchLabel.Name = "BranchLabel"
        Me.BranchLabel.Size = New System.Drawing.Size(41, 13)
        Me.BranchLabel.TabIndex = 32
        Me.BranchLabel.Text = "Branch"
        '
        'ProgramLabel
        '
        Me.ProgramLabel.AutoSize = True
        Me.ProgramLabel.Location = New System.Drawing.Point(29, 208)
        Me.ProgramLabel.Name = "ProgramLabel"
        Me.ProgramLabel.Size = New System.Drawing.Size(46, 13)
        Me.ProgramLabel.TabIndex = 31
        Me.ProgramLabel.Text = "Program"
        '
        'UnitLabel
        '
        Me.UnitLabel.AutoSize = True
        Me.UnitLabel.Location = New System.Drawing.Point(29, 233)
        Me.UnitLabel.Name = "UnitLabel"
        Me.UnitLabel.Size = New System.Drawing.Size(26, 13)
        Me.UnitLabel.TabIndex = 30
        Me.UnitLabel.Text = "Unit"
        '
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.Location = New System.Drawing.Point(27, 26)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(62, 13)
        Me.UsernameLabel.TabIndex = 20
        Me.UsernameLabel.Text = "Username *"
        '
        'Username
        '
        Me.Username.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Username.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.Username.Location = New System.Drawing.Point(111, 23)
        Me.Username.MaxLength = 50
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(183, 20)
        Me.Username.TabIndex = 0
        '
        'PhoneNumber
        '
        Me.PhoneNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.PhoneNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.PhoneNumber.Location = New System.Drawing.Point(113, 127)
        Me.PhoneNumber.MaxLength = 15
        Me.PhoneNumber.Name = "PhoneNumber"
        Me.PhoneNumber.Size = New System.Drawing.Size(181, 20)
        Me.PhoneNumber.TabIndex = 4
        '
        'IaipCreateUser
        '
        Me.AcceptButton = Me.Save
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(339, 386)
        Me.Controls.Add(Me.Unit)
        Me.Controls.Add(Me.Program)
        Me.Controls.Add(Me.Branch)
        Me.Controls.Add(Me.BranchLabel)
        Me.Controls.Add(Me.ProgramLabel)
        Me.Controls.Add(Me.UnitLabel)
        Me.Controls.Add(Me.Username)
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
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.LNameLabel)
        Me.Controls.Add(Me.FNameLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IaipCreateUser"
        Me.Text = "Create New IAIP User"
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
    Friend WithEvents Unit As System.Windows.Forms.ComboBox
    Friend WithEvents Program As System.Windows.Forms.ComboBox
    Friend WithEvents Branch As System.Windows.Forms.ComboBox
    Friend WithEvents BranchLabel As System.Windows.Forms.Label
    Friend WithEvents ProgramLabel As System.Windows.Forms.Label
    Friend WithEvents UnitLabel As System.Windows.Forms.Label
    Friend WithEvents Username As System.Windows.Forms.TextBox
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents PhoneNumber As System.Windows.Forms.TextBox
End Class
