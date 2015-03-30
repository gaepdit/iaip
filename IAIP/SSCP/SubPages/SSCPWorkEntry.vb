Imports Oracle.ManagedDataAccess.Client


Public Class SSCPWorkEnTry
    Inherits BaseForm
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Paneltemp1 As String
    Dim dsWorkEnTry As DataSet
    Dim daWorkEnTry As OracleDataAdapter
    Dim SQL, SQL2 As String
    Dim cmd, cmd2 As OracleCommand
    Dim dr, dr2 As OracleDataReader
    Dim recExist As Boolean
    Dim dsCompliance As DataSet
    Dim daCompliance As OracleDataAdapter

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
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MmiBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents tbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents GBFacilityData As System.Windows.Forms.GroupBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents txtTrackingNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DTPDateReceived As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboEvent As System.Windows.Forms.ComboBox
    Friend WithEvents LabEventDescription As System.Windows.Forms.Label
    Friend WithEvents PanelSSCPWorkEnTry As System.Windows.Forms.Panel
    Friend WithEvents TBComplianceEvents As System.Windows.Forms.ToolBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSCPWorkEnTry))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.TBComplianceEvents = New System.Windows.Forms.ToolBar
        Me.tbbSave = New System.Windows.Forms.ToolBarButton
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.GBFacilityData = New System.Windows.Forms.GroupBox
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.txtTrackingNumber = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.DTPDateReceived = New System.Windows.Forms.DateTimePicker
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboEvent = New System.Windows.Forms.ComboBox
        Me.LabEventDescription = New System.Windows.Forms.Label
        Me.PanelSSCPWorkEnTry = New System.Windows.Forms.Panel
        Me.GBFacilityData.SuspendLayout()
        Me.PanelSSCPWorkEnTry.SuspendLayout()
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
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem5})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MmiSave, Me.MenuItem10, Me.MmiBack})
        Me.MenuItem1.Text = "File"
        '
        'MmiSave
        '
        Me.MmiSave.Index = 0
        Me.MmiSave.Text = "Save"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 1
        Me.MenuItem10.Text = "-"
        '
        'MmiBack
        '
        Me.MmiBack.Index = 2
        Me.MmiBack.Text = "Back"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 1
        Me.MenuItem5.Text = "Help"
        '
        'TBComplianceEvents
        '
        Me.TBComplianceEvents.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSave, Me.tbbClear, Me.tbbBack})
        Me.TBComplianceEvents.DropDownArrows = True
        Me.TBComplianceEvents.ImageList = Me.Image_List_All
        Me.TBComplianceEvents.Location = New System.Drawing.Point(0, 0)
        Me.TBComplianceEvents.Name = "TBComplianceEvents"
        Me.TBComplianceEvents.ShowToolTips = True
        Me.TBComplianceEvents.Size = New System.Drawing.Size(792, 28)
        Me.TBComplianceEvents.TabIndex = 141
        '
        'tbbSave
        '
        Me.tbbSave.ImageIndex = 65
        Me.tbbSave.Name = "tbbSave"
        '
        'tbbClear
        '
        Me.tbbClear.ImageIndex = 84
        Me.tbbClear.Name = "tbbClear"
        '
        'tbbBack
        '
        Me.tbbBack.ImageIndex = 2
        Me.tbbBack.Name = "tbbBack"
        '
        'GBFacilityData
        '
        Me.GBFacilityData.Controls.Add(Me.txtAIRSNumber)
        Me.GBFacilityData.Controls.Add(Me.Label1)
        Me.GBFacilityData.Controls.Add(Me.txtFacilityName)
        Me.GBFacilityData.Controls.Add(Me.txtTrackingNumber)
        Me.GBFacilityData.Controls.Add(Me.Label3)
        Me.GBFacilityData.Controls.Add(Me.Label2)
        Me.GBFacilityData.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBFacilityData.Location = New System.Drawing.Point(0, 28)
        Me.GBFacilityData.Name = "GBFacilityData"
        Me.GBFacilityData.Size = New System.Drawing.Size(792, 68)
        Me.GBFacilityData.TabIndex = 148
        Me.GBFacilityData.TabStop = False
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(104, 40)
        Me.txtAIRSNumber.Name = "txtAIRSNumber"
        Me.txtAIRSNumber.ReadOnly = True
        Me.txtAIRSNumber.Size = New System.Drawing.Size(72, 20)
        Me.txtAIRSNumber.TabIndex = 144
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 141
        Me.Label1.Text = "Facility Name: "
        '
        'txtFacilityName
        '
        Me.txtFacilityName.Location = New System.Drawing.Point(104, 16)
        Me.txtFacilityName.Name = "txtFacilityName"
        Me.txtFacilityName.ReadOnly = True
        Me.txtFacilityName.Size = New System.Drawing.Size(184, 20)
        Me.txtFacilityName.TabIndex = 143
        '
        'txtTrackingNumber
        '
        Me.txtTrackingNumber.Location = New System.Drawing.Point(504, 16)
        Me.txtTrackingNumber.Name = "txtTrackingNumber"
        Me.txtTrackingNumber.ReadOnly = True
        Me.txtTrackingNumber.Size = New System.Drawing.Size(80, 20)
        Me.txtTrackingNumber.TabIndex = 146
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(400, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 145
        Me.Label3.Text = "Tracking Number:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 142
        Me.Label2.Text = "AIRS Number:"
        '
        'DTPDateReceived
        '
        Me.DTPDateReceived.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDateReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDateReceived.Location = New System.Drawing.Point(125, 6)
        Me.DTPDateReceived.Name = "DTPDateReceived"
        Me.DTPDateReceived.Size = New System.Drawing.Size(100, 20)
        Me.DTPDateReceived.TabIndex = 264
        Me.DTPDateReceived.Value = New Date(2005, 3, 24, 0, 0, 0, 0)
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(16, 10)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(103, 13)
        Me.Label21.TabIndex = 263
        Me.Label21.Text = "Received by GEPD:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(400, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 246
        Me.Label4.Text = "Compliance Event"
        '
        'cboEvent
        '
        Me.cboEvent.Location = New System.Drawing.Point(504, 8)
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.Size = New System.Drawing.Size(224, 21)
        Me.cboEvent.TabIndex = 243
        '
        'LabEventDescription
        '
        Me.LabEventDescription.Location = New System.Drawing.Point(504, 32)
        Me.LabEventDescription.Name = "LabEventDescription"
        Me.LabEventDescription.Size = New System.Drawing.Size(224, 40)
        Me.LabEventDescription.TabIndex = 248
        Me.LabEventDescription.Visible = False
        '
        'PanelSSCPWorkEnTry
        '
        Me.PanelSSCPWorkEnTry.Controls.Add(Me.Label21)
        Me.PanelSSCPWorkEnTry.Controls.Add(Me.Label4)
        Me.PanelSSCPWorkEnTry.Controls.Add(Me.cboEvent)
        Me.PanelSSCPWorkEnTry.Controls.Add(Me.LabEventDescription)
        Me.PanelSSCPWorkEnTry.Controls.Add(Me.DTPDateReceived)
        Me.PanelSSCPWorkEnTry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSSCPWorkEnTry.Location = New System.Drawing.Point(0, 96)
        Me.PanelSSCPWorkEnTry.Name = "PanelSSCPWorkEnTry"
        Me.PanelSSCPWorkEnTry.Size = New System.Drawing.Size(792, 81)
        Me.PanelSSCPWorkEnTry.TabIndex = 294
        '
        'SSCPWorkEnTry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 177)
        Me.Controls.Add(Me.PanelSSCPWorkEnTry)
        Me.Controls.Add(Me.GBFacilityData)
        Me.Controls.Add(Me.TBComplianceEvents)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPWorkEnTry"
        Me.Text = "Compliance Work Entry"
        Me.GBFacilityData.ResumeLayout(False)
        Me.GBFacilityData.PerformLayout()
        Me.PanelSSCPWorkEnTry.ResumeLayout(False)
        Me.PanelSSCPWorkEnTry.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub SSCPWorkEnTry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()
            DTPDateReceived.Value = Date.Today
            LoadDataSets()
            LoadComboBox()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

#Region "Page Load"
    Sub CreateStatusBar()
        Try

            panel1.Text = "Select a Function..."
            panel2.Text = UserName
            panel3.Text = OracleDate

            panel1.AutoSize = StatusBarPanelAutoSize.Spring
            panel2.AutoSize = StatusBarPanelAutoSize.Contents
            panel3.AutoSize = StatusBarPanelAutoSize.Contents

            panel1.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel2.BorderStyle = StatusBarPanelBorderStyle.Sunken
            panel3.BorderStyle = StatusBarPanelBorderStyle.Sunken

            panel1.Alignment = HorizontalAlignment.Left
            panel2.Alignment = HorizontalAlignment.Left
            panel3.Alignment = HorizontalAlignment.Right

            ' Display panels in the StatusBar control.
            statusBar1.ShowPanels = True

            ' Add both panels to the StatusBarPanelCollection of the StatusBar.            
            statusBar1.Panels.Add(panel1)
            statusBar1.Panels.Add(panel2)
            statusBar1.Panels.Add(panel3)

            ' Add the StatusBar to the form.
            Me.Controls.Add(statusBar1)
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Private Sub LoadDataSets()
        Dim SQL As String
        Dim cmd As OracleCommand

        Try

            dsCompliance = New DataSet

            SQL = "select strActivityType, strActivityName, strActivityDescription " & _
            "from AIRBRANCH.LookUPComplianceActivities " & _
            "order by strActivityName"

            daCompliance = New OracleDataAdapter
            cmd = New OracleCommand(SQL, CurrentConnection)

            daCompliance = New OracleDataAdapter(cmd)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daCompliance.Fill(dsCompliance, "ComplianceActivity")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub LoadComboBox()
        Dim dtActivity As New DataTable
        Dim drDSRow As DataRow
        Dim drNewRow As DataRow

        Try

            dtActivity.Columns.Add("strActivityName", GetType(System.String))
            dtActivity.Columns.Add("strActivityType", GetType(System.String))

            drNewRow = dtActivity.NewRow()
            drNewRow("strActivityName") = " "
            drNewRow("strActivityType") = " "
            dtActivity.Rows.Add(drNewRow)

            For Each drDSRow In dsCompliance.Tables("ComplianceActivity").Rows()
                drNewRow = dtActivity.NewRow()
                drNewRow("strActivityName") = drDSRow("strActivityName")
                drNewRow("strActivityType") = drDSRow("strActivityType")
                dtActivity.Rows.Add(drNewRow)
            Next

            With cboEvent
                .DataSource = dtActivity
                .DisplayMember = "strActivityName"
                .ValueMember = "strActivityType"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
#End Region
#Region "Subs and Functions"
    Sub Save()
        Dim DateReceived As String = DTPDateReceived.Text

        Try

            Paneltemp1 = panel1.Text

            If cboEvent.Text <> " " Then

                panel1.Text = "Getting New Tracking Number.."

                panel1.Text = "Saving New Compliance Event."

                SQL = "Insert into AIRBRANCH.SSCPItemMaster " & _
                "(strTrackingNumber, strAIRSnumber, DatReceivedDate, strEventType, " & _
                "strModifingPerson, datModifingDate) values " & _
                "(AIRBRANCH.SSCPTrackingNumber.nextval, '0413" & txtAIRSNumber.Text & "', '" & DateReceived & "', " & _
                "(Select strActivityType from AIRBRANCH.LookUPComplianceActivities where strActivityName = '" & cboEvent.Text & "'), " & _
                "'" & UserGCode & "', '" & OracleDate & "')"

                SQL2 = "Select AIRBRANCH.SSCPTrackingNumber.Currval from Dual"

                cmd = New OracleCommand(SQL, CurrentConnection)
                cmd2 = New OracleCommand(SQL2, CurrentConnection)

                If CurrentConnection.State = ConnectionState.Closed Then
                    CurrentConnection.Open()
                End If

                panel1.Text = "Saving New Compliance Event.."
                dr = cmd.ExecuteReader

                dr2 = cmd2.ExecuteReader
                While dr2.Read
                    txtTrackingNumber.Text = dr2.Item(0)
                End While

                If CurrentConnection.State = ConnectionState.Open Then
                    'conn.close()
                End If

                panel1.Text = "Saving New Compliance Event..."
                panel1.Text = "Refreshing Facility Summary Screen..."
                panel1.Text = Paneltemp1
                MsgBox("Done")

            Else
                panel1.Text = Paneltemp1
                MsgBox("Please Select an Event type.")
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try


    End Sub
    Sub Clear()
        Try

            DTPDateReceived.Value = Date.Today
            cboEvent.Text = " "
            txtTrackingNumber.Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Sub Back()
        Try

            SSCPEngWork = Nothing
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
#End Region
    Private Sub SSCPWorkEnTry_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            SSCPEngWork = Nothing
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub TBComplianceEvents_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBComplianceEvents.ButtonClick
        Try

            Select Case TBComplianceEvents.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Clear()
                Case 2
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub cboEvent_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEvent.SelectedValueChanged
        Dim dtActivity As New DataTable

        Try

            If cboEvent.SelectedIndex = 0 Then
                LabEventDescription.Text = ""
            Else
                dtActivity = dsCompliance.Tables("ComplianceActivity")

                Dim drActivity As DataRow()
                Dim row As DataRow

                drActivity = dtActivity.Select("strActivityName = '" & cboEvent.Text & "'")

                For Each row In drActivity
                    LabEventDescription.Text = row("strActivityDescription").ToString
                Next
            End If

            If LabEventDescription.Text <> "" Then
                LabEventDescription.Visible = True
            Else
                LabEventDescription.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub
    Private Sub cboEvent_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEvent.Leave
        Dim dtActivity As New DataTable

        Try

            If cboEvent.SelectedIndex = 0 Then
                LabEventDescription.Text = ""
            Else
                dtActivity = dsCompliance.Tables("ComplianceActivity")

                Dim drActivity As DataRow()
                Dim row As DataRow

                drActivity = dtActivity.Select("strActivityName = '" & cboEvent.Text & "'")

                For Each row In drActivity
                    LabEventDescription.Text = row("strActivityDescription").ToString
                Next
            End If

            If LabEventDescription.Text <> "" Then
                LabEventDescription.Visible = True
            Else
                LabEventDescription.Visible = False
            End If
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally
            If CurrentConnection.State = ConnectionState.Open Then
                'conn.close()
            End If
        End Try

    End Sub

    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class
