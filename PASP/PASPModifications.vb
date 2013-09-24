Imports Oracle.DataAccess.Client


Public Class PASPModifications
    Inherits DefaultForm
    Dim da As OracleDataAdapter
    'Dim feeyear As String
    Dim ds As DataSet

    Friend WithEvents txtYear3 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Image_List_All As System.Windows.Forms.ImageList
    Friend WithEvents TBFacilitySummary As System.Windows.Forms.ToolBar
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents PanelFacility As System.Windows.Forms.Panel
    Friend WithEvents cboFacilityName3 As System.Windows.Forms.ComboBox
    Friend WithEvents cboAirsNo3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents llbViewAll3 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkBankrupt As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkFinal As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave3 As System.Windows.Forms.Button
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PASPModifications))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.TBFacilitySummary = New System.Windows.Forms.ToolBar
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.PanelFacility = New System.Windows.Forms.Panel
        Me.txtYear3 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboFacilityName3 = New System.Windows.Forms.ComboBox
        Me.cboAirsNo3 = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.llbViewAll3 = New System.Windows.Forms.LinkLabel
        Me.Label = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnSave3 = New System.Windows.Forms.Button
        Me.chkFinal = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkBankrupt = New System.Windows.Forms.CheckBox
        Me.PanelFacility.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Image_List_All
        '
        Me.Image_List_All.ImageStream = CType(resources.GetObject("Image_List_All.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Image_List_All.TransparentColor = System.Drawing.Color.Transparent
        Me.Image_List_All.Images.SetKeyName(0, "")
        Me.Image_List_All.Images.SetKeyName(1, "")
        Me.Image_List_All.Images.SetKeyName(2, "")
        Me.Image_List_All.Images.SetKeyName(3, "")
        Me.Image_List_All.Images.SetKeyName(4, "")
        Me.Image_List_All.Images.SetKeyName(5, "")
        Me.Image_List_All.Images.SetKeyName(6, "")
        Me.Image_List_All.Images.SetKeyName(7, "")
        Me.Image_List_All.Images.SetKeyName(8, "")
        Me.Image_List_All.Images.SetKeyName(9, "")
        Me.Image_List_All.Images.SetKeyName(10, "")
        Me.Image_List_All.Images.SetKeyName(11, "")
        Me.Image_List_All.Images.SetKeyName(12, "")
        Me.Image_List_All.Images.SetKeyName(13, "")
        Me.Image_List_All.Images.SetKeyName(14, "")
        Me.Image_List_All.Images.SetKeyName(15, "")
        Me.Image_List_All.Images.SetKeyName(16, "")
        Me.Image_List_All.Images.SetKeyName(17, "")
        Me.Image_List_All.Images.SetKeyName(18, "")
        Me.Image_List_All.Images.SetKeyName(19, "")
        Me.Image_List_All.Images.SetKeyName(20, "")
        Me.Image_List_All.Images.SetKeyName(21, "")
        Me.Image_List_All.Images.SetKeyName(22, "")
        Me.Image_List_All.Images.SetKeyName(23, "")
        Me.Image_List_All.Images.SetKeyName(24, "")
        Me.Image_List_All.Images.SetKeyName(25, "")
        Me.Image_List_All.Images.SetKeyName(26, "")
        Me.Image_List_All.Images.SetKeyName(27, "")
        Me.Image_List_All.Images.SetKeyName(28, "")
        Me.Image_List_All.Images.SetKeyName(29, "")
        Me.Image_List_All.Images.SetKeyName(30, "")
        Me.Image_List_All.Images.SetKeyName(31, "")
        Me.Image_List_All.Images.SetKeyName(32, "")
        Me.Image_List_All.Images.SetKeyName(33, "")
        Me.Image_List_All.Images.SetKeyName(34, "")
        Me.Image_List_All.Images.SetKeyName(35, "")
        Me.Image_List_All.Images.SetKeyName(36, "")
        Me.Image_List_All.Images.SetKeyName(37, "")
        Me.Image_List_All.Images.SetKeyName(38, "")
        Me.Image_List_All.Images.SetKeyName(39, "")
        Me.Image_List_All.Images.SetKeyName(40, "")
        Me.Image_List_All.Images.SetKeyName(41, "")
        Me.Image_List_All.Images.SetKeyName(42, "")
        Me.Image_List_All.Images.SetKeyName(43, "")
        Me.Image_List_All.Images.SetKeyName(44, "")
        Me.Image_List_All.Images.SetKeyName(45, "")
        Me.Image_List_All.Images.SetKeyName(46, "")
        Me.Image_List_All.Images.SetKeyName(47, "")
        Me.Image_List_All.Images.SetKeyName(48, "")
        Me.Image_List_All.Images.SetKeyName(49, "")
        Me.Image_List_All.Images.SetKeyName(50, "")
        Me.Image_List_All.Images.SetKeyName(51, "")
        Me.Image_List_All.Images.SetKeyName(52, "")
        Me.Image_List_All.Images.SetKeyName(53, "")
        Me.Image_List_All.Images.SetKeyName(54, "")
        Me.Image_List_All.Images.SetKeyName(55, "")
        Me.Image_List_All.Images.SetKeyName(56, "")
        Me.Image_List_All.Images.SetKeyName(57, "")
        Me.Image_List_All.Images.SetKeyName(58, "")
        Me.Image_List_All.Images.SetKeyName(59, "")
        Me.Image_List_All.Images.SetKeyName(60, "")
        Me.Image_List_All.Images.SetKeyName(61, "")
        Me.Image_List_All.Images.SetKeyName(62, "")
        Me.Image_List_All.Images.SetKeyName(63, "")
        Me.Image_List_All.Images.SetKeyName(64, "")
        Me.Image_List_All.Images.SetKeyName(65, "")
        Me.Image_List_All.Images.SetKeyName(66, "")
        Me.Image_List_All.Images.SetKeyName(67, "")
        Me.Image_List_All.Images.SetKeyName(68, "")
        Me.Image_List_All.Images.SetKeyName(69, "")
        Me.Image_List_All.Images.SetKeyName(70, "")
        Me.Image_List_All.Images.SetKeyName(71, "")
        Me.Image_List_All.Images.SetKeyName(72, "")
        Me.Image_List_All.Images.SetKeyName(73, "")
        Me.Image_List_All.Images.SetKeyName(74, "")
        Me.Image_List_All.Images.SetKeyName(75, "")
        Me.Image_List_All.Images.SetKeyName(76, "")
        Me.Image_List_All.Images.SetKeyName(77, "")
        Me.Image_List_All.Images.SetKeyName(78, "")
        Me.Image_List_All.Images.SetKeyName(79, "")
        Me.Image_List_All.Images.SetKeyName(80, "")
        Me.Image_List_All.Images.SetKeyName(81, "")
        Me.Image_List_All.Images.SetKeyName(82, "")
        Me.Image_List_All.Images.SetKeyName(83, "")
        Me.Image_List_All.Images.SetKeyName(84, "")
        '
        'TBFacilitySummary
        '
        Me.TBFacilitySummary.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbClear, Me.tbbBack})
        Me.TBFacilitySummary.DropDownArrows = True
        Me.TBFacilitySummary.ImageList = Me.Image_List_All
        Me.TBFacilitySummary.Location = New System.Drawing.Point(0, 0)
        Me.TBFacilitySummary.Name = "TBFacilitySummary"
        Me.TBFacilitySummary.ShowToolTips = True
        Me.TBFacilitySummary.Size = New System.Drawing.Size(792, 28)
        Me.TBFacilitySummary.TabIndex = 140
        '
        'tbbClear
        '
        Me.tbbClear.ImageIndex = 84
        Me.tbbClear.Name = "tbbClear"
        Me.tbbClear.ToolTipText = "Clear"
        '
        'tbbBack
        '
        Me.tbbBack.ImageIndex = 2
        Me.tbbBack.Name = "tbbBack"
        Me.tbbBack.ToolTipText = "Back"
        '
        'PanelFacility
        '
        Me.PanelFacility.Controls.Add(Me.txtYear3)
        Me.PanelFacility.Controls.Add(Me.Label2)
        Me.PanelFacility.Controls.Add(Me.cboFacilityName3)
        Me.PanelFacility.Controls.Add(Me.cboAirsNo3)
        Me.PanelFacility.Controls.Add(Me.Label10)
        Me.PanelFacility.Controls.Add(Me.llbViewAll3)
        Me.PanelFacility.Controls.Add(Me.Label)
        Me.PanelFacility.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelFacility.Location = New System.Drawing.Point(0, 28)
        Me.PanelFacility.Name = "PanelFacility"
        Me.PanelFacility.Size = New System.Drawing.Size(792, 31)
        Me.PanelFacility.TabIndex = 143
        '
        'txtYear3
        '
        Me.txtYear3.Location = New System.Drawing.Point(580, 4)
        Me.txtYear3.Name = "txtYear3"
        Me.txtYear3.Size = New System.Drawing.Size(40, 20)
        Me.txtYear3.TabIndex = 5
        Me.txtYear3.Text = "2006"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(516, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 145
        Me.Label2.Text = "AND Year:"
        '
        'cboFacilityName3
        '
        Me.cboFacilityName3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFacilityName3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFacilityName3.Location = New System.Drawing.Point(92, 4)
        Me.cboFacilityName3.Name = "cboFacilityName3"
        Me.cboFacilityName3.Size = New System.Drawing.Size(215, 21)
        Me.cboFacilityName3.TabIndex = 1
        '
        'cboAirsNo3
        '
        Me.cboAirsNo3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboAirsNo3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboAirsNo3.Location = New System.Drawing.Point(410, 4)
        Me.cboAirsNo3.Name = "cboAirsNo3"
        Me.cboAirsNo3.Size = New System.Drawing.Size(90, 21)
        Me.cboAirsNo3.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(307, 7)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(94, 13)
        Me.Label10.TabIndex = 107
        Me.Label10.Text = "OR AIRS Number:"
        '
        'llbViewAll3
        '
        Me.llbViewAll3.AutoSize = True
        Me.llbViewAll3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llbViewAll3.Location = New System.Drawing.Point(626, 7)
        Me.llbViewAll3.Name = "llbViewAll3"
        Me.llbViewAll3.Size = New System.Drawing.Size(56, 13)
        Me.llbViewAll3.TabIndex = 143
        Me.llbViewAll3.TabStop = True
        Me.llbViewAll3.Text = "View Data"
        '
        'Label
        '
        Me.Label.AutoSize = True
        Me.Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label.Location = New System.Drawing.Point(7, 7)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(73, 13)
        Me.Label.TabIndex = 106
        Me.Label.Text = "Facility Name:"
        Me.Label.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSave3)
        Me.GroupBox1.Controls.Add(Me.chkFinal)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkBankrupt)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 59)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(792, 160)
        Me.GroupBox1.TabIndex = 144
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bankrptcy and Final Submission for Fee Form"
        '
        'btnSave3
        '
        Me.btnSave3.Location = New System.Drawing.Point(7, 132)
        Me.btnSave3.Name = "btnSave3"
        Me.btnSave3.Size = New System.Drawing.Size(62, 20)
        Me.btnSave3.TabIndex = 4
        Me.btnSave3.Text = "Save"
        '
        'chkFinal
        '
        Me.chkFinal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkFinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFinal.Location = New System.Drawing.Point(7, 76)
        Me.chkFinal.Name = "chkFinal"
        Me.chkFinal.Size = New System.Drawing.Size(386, 50)
        Me.chkFinal.TabIndex = 3
        Me.chkFinal.Text = "2. Check or uncheck this box to change the Final Submission status of the Fee For" & _
            "m. If checked, final submission is made. If un-checked, final submission not don" & _
            "e."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(640, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Select a facility from above. Then check or uncheck the appropriate boxes. If no " & _
            "changes are to be made, leave the check-box alone."
        '
        'chkBankrupt
        '
        Me.chkBankrupt.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkBankrupt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBankrupt.Location = New System.Drawing.Point(7, 49)
        Me.chkBankrupt.Name = "chkBankrupt"
        Me.chkBankrupt.Size = New System.Drawing.Size(386, 17)
        Me.chkBankrupt.TabIndex = 0
        Me.chkBankrupt.Text = "1. Check this box if the facility is bankrupt. If checked, facility is bankrupt."
        '
        'PASPModifications
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 448)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PanelFacility)
        Me.Controls.Add(Me.TBFacilitySummary)
        Me.Name = "PASPModifications"
        Me.Text = "PASP Modifications"
        Me.PanelFacility.ResumeLayout(False)
        Me.PanelFacility.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub PASPModifications_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try


            LoadComboBoxes()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub TBFacilitySummary_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFacilitySummary.ButtonClick
        Try

            Select Case TBFacilitySummary.Buttons.IndexOf(e.Button)
                Case 0
                    ClearPage()
                Case 1
                    Me.Close()
                Case Else
            End Select
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub ClearPage()
        Try

            cboFacilityName3.Text = ""
            cboAirsNo3.Text = ""
            chkBankrupt.Checked = False
            chkFinal.Checked = False
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Sub LoadComboBoxes()

        Dim SQL As String
        Dim dtAIRS As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            SQL = "Select DISTINCT substr(" & DBNameSpace & ".masteruser.strairsnumber, 5) as strairsnumber, " _
            + "" & DBNameSpace & ".APBFacilityInformation.strfacilityname " _
            + "from " & DBNameSpace & ".masteruser, " & DBNameSpace & ".APBFacilityInformation " _
            + "where " & DBNameSpace & ".masteruser.strairsnumber = " & DBNameSpace & ".APBFacilityInformation.strairsnumber " _
            + "order by " & DBNameSpace & ".APBFacilityInformation.strFacilityName "

            ds = New DataSet
            da = New OracleDataAdapter(SQL, Conn)

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            da.Fill(ds, "facilityInfo")

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

            dtAIRS.Columns.Add("strairsnumber", GetType(System.String))
            dtAIRS.Columns.Add("strfacilityname", GetType(System.String))

            drNewRow = dtAIRS.NewRow()
            drNewRow("strfacilityname") = " "
            drNewRow("strairsnumber") = " "
            dtAIRS.Rows.Add(drNewRow)

            For Each drDSRow In ds.Tables("facilityInfo").Rows()
                drNewRow = dtAIRS.NewRow()
                drNewRow("strairsnumber") = drDSRow("strairsnumber")
                drNewRow("strfacilityname") = drDSRow("strfacilityname")
                dtAIRS.Rows.Add(drNewRow)
            Next
            Dim temp As String

            temp = dtAIRS.Rows.Count

            With cboAirsNo3
                .DataSource = dtAIRS
                .DisplayMember = "strairsnumber"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

            With cboFacilityName3
                .DataSource = dtAIRS
                .DisplayMember = "strfacilityname"
                .ValueMember = "strairsnumber"
                .SelectedIndex = 0
            End With

            'dtAIRS = ds.Tables("facilityInfo")

            'Dim drAirs As DataRow()
            'Dim row As DataRow

            'cboAirsNo.Items.Clear()
            'cboFacilityName.Items.Clear()

            'cboAirsNo.Items.Add("")
            'cboFacilityName.Items.Add("")

            'drAirs = dtAIRS.Select()
            'For Each row In drAirs
            '    cboAirsNo.Items.Add(row("strairsnumber"))
            '    cboFacilityName.Items.Add(row("strfacilityname"))
            'Next

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub llbViewAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbViewAll3.LinkClicked

        Dim SQL, SQL1 As String

        Try


            SQL = "Select isbankrupt from " & DBNameSpace & ".APBSupplamentalData " _
                + "where strairsnumber = '0413" & cboAirsNo3.Text & "'"

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            Dim dr As OracleDataReader = cmd.ExecuteReader()
            Dim recExist As Boolean = dr.Read

            If recExist = True Then
                If dr.IsDBNull(0) Then
                    chkBankrupt.Checked = False
                Else
                    If dr.Item("isbankrupt") = "NO" Then
                        chkBankrupt.Checked = False
                    Else
                        chkBankrupt.Checked = True
                    End If
                End If
            End If

            SQL1 = "Select intsubmittal " & _
            "from " & DBNameSpace & ".FSPayAndSubmit " & _
            "where strairsnumber = '0413" & cboAirsNo3.Text & "' " & _
            "and intyear = '" & CInt(txtYear3.Text) & "'"

            Dim cmd1 As New OracleCommand(SQL1, Conn)
            cmd1.CommandType = CommandType.Text

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            Dim dr1 As OracleDataReader = cmd1.ExecuteReader()
            Dim recExist1 As Boolean = dr1.Read

            If recExist1 = True Then
                If dr1.Item("intsubmittal") = 0 Then
                    chkFinal.Checked = False
                Else
                    chkFinal.Checked = True
                End If
            End If

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave3.Click

        Dim SQL, bankrupt As String

        Try


            If chkBankrupt.Checked = True Then
                bankrupt = "YES"
            Else
                bankrupt = "NO"
            End If

            SQL = "Update " & DBNameSpace & ".APBSupplamentalData set " _
                + "isbankrupt = '" & bankrupt & "' " _
                + "where strairsnumber = '0413" & cboAirsNo3.Text & "'"

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            cmd.ExecuteNonQuery()

            If Conn.State = ConnectionState.Open Then
            Else
                Conn.Open()
            End If

            If chkFinal.Checked = True Then
                FinalSubmitAdd()
            Else
                FinalSubmitRemove()
            End If

            MsgBox("The facility information has been updated.", MsgBoxStyle.Information, "Update Success")
            ClearPage()

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub FinalSubmitAdd()
        Try

            Dim confirmation As String
            confirmation = cboAirsNo3.Text & "-" & Now
            Dim SQL As String = "Update " & DBNameSpace & ".FSPayAndSubmit set " & _
            "intsubmittal = '1' " & _
            "where strairsnumber = '0413" & cboAirsNo3.Text & "' " & _
            "and intyear = '" & CInt(txtYear3.Text) & "'"

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()

            SQL = "Insert into " & DBNameSpace & ".FSConfirmation (" & _
            "strairsnumber, intyear, strconfirmation, numuserid, datconfirmation) values(" & _
            "'0413" & cboAirsNo3.Text & "', '" & CInt(txtYear3.Text) & "', " & _
            "'" & confirmation & "', '" & UserGCode & "', " & _
            "to_date('" & Format$(Now, "dd-MMM-yyyy hh:mm:ss") & "', 'dd-mon-yyyy hh:mi:ss'))"

            cmd = New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub FinalSubmitRemove()
        Try


            Dim SQL As String = "Update " & DBNameSpace & ".FSPayAndSubmit set " & _
             "intsubmittal = '0' " & _
             "where strairsnumber = '0413" & cboAirsNo3.Text & "' " & _
             "and intyear = '" & CInt(txtYear3.Text) & "'"

            Dim cmd As New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()

            SQL = "Delete from " & DBNameSpace & ".FSConfirmation " & _
            "where strairsnumber = '0413" & cboAirsNo3.Text & "' " & _
             "and intyear = '" & CInt(txtYear3.Text) & "'"

            cmd = New OracleCommand(SQL, Conn)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()

            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If

        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub PASPModifications_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            If NavigationScreen Is Nothing Then
                NavigationScreen = New IAIPNavigation
            End If
            NavigationScreen.Show()
            Modifications = Nothing
            Me.Dispose()
        Catch ex As Exception
            ErrorReport(ex.ToString(), Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If Conn.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
End Class
