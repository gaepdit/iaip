Imports Oracle.DataAccess.Client


Public Class SSCPFacAssignment
    Inherits BaseForm

    Dim SQL As String
    Dim statusBar1 As New StatusBar
    Dim panel1 As New StatusBarPanel
    Dim panel2 As New StatusBarPanel
    Dim panel3 As New StatusBarPanel
    Dim Paneltemp1 As String
    Dim dsEngineerList As DataSet
    Dim daEngineerList As OracleDataAdapter
    Dim dsUnits As DataSet
    Dim daUnits As OracleDataAdapter


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
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents tbbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbClear As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbBack As System.Windows.Forms.ToolBarButton
    Friend WithEvents GBFacilityData As System.Windows.Forms.GroupBox
    Friend WithEvents txtAIRSNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFacilityName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboSSCPEngineer As System.Windows.Forms.ComboBox
    Friend WithEvents TBFacilityAssignment As System.Windows.Forms.ToolBar
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GBRequiresInspection As System.Windows.Forms.GroupBox
    Friend WithEvents rdbInspectionYes As System.Windows.Forms.RadioButton
    Friend WithEvents rdbInspectionNo As System.Windows.Forms.RadioButton
    Friend WithEvents PanelFaciltiyAssignmentLower As System.Windows.Forms.Panel
    Friend WithEvents cboSSCPUnits As System.Windows.Forms.ComboBox
    Friend WithEvents mmiClear As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSCPFacAssignment))
        Me.Image_List_All = New System.Windows.Forms.ImageList(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MmiSave = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MmiBack = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mmiClear = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.TBFacilityAssignment = New System.Windows.Forms.ToolBar
        Me.tbbSave = New System.Windows.Forms.ToolBarButton
        Me.tbbClear = New System.Windows.Forms.ToolBarButton
        Me.tbbBack = New System.Windows.Forms.ToolBarButton
        Me.GBFacilityData = New System.Windows.Forms.GroupBox
        Me.txtAIRSNumber = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFacilityName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboSSCPEngineer = New System.Windows.Forms.ComboBox
        Me.PanelFaciltiyAssignmentLower = New System.Windows.Forms.Panel
        Me.GBRequiresInspection = New System.Windows.Forms.GroupBox
        Me.rdbInspectionNo = New System.Windows.Forms.RadioButton
        Me.rdbInspectionYes = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboSSCPUnits = New System.Windows.Forms.ComboBox
        Me.GBFacilityData.SuspendLayout()
        Me.PanelFaciltiyAssignmentLower.SuspendLayout()
        Me.GBRequiresInspection.SuspendLayout()
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
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem5})
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
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mmiClear})
        Me.MenuItem2.Text = "Edit"
        '
        'mmiClear
        '
        Me.mmiClear.Index = 0
        Me.mmiClear.Text = "Clear"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 2
        Me.MenuItem5.Text = "Help"
        '
        'TBFacilityAssignment
        '
        Me.TBFacilityAssignment.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbSave, Me.tbbClear, Me.tbbBack})
        Me.TBFacilityAssignment.DropDownArrows = True
        Me.TBFacilityAssignment.ImageList = Me.Image_List_All
        Me.TBFacilityAssignment.Location = New System.Drawing.Point(0, 0)
        Me.TBFacilityAssignment.Name = "TBFacilityAssignment"
        Me.TBFacilityAssignment.ShowToolTips = True
        Me.TBFacilityAssignment.Size = New System.Drawing.Size(792, 28)
        Me.TBFacilityAssignment.TabIndex = 141
        '
        'tbbSave
        '
        Me.tbbSave.ImageIndex = 65
        Me.tbbSave.Name = "tbbSave"
        Me.tbbSave.ToolTipText = "Save"
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
        'GBFacilityData
        '
        Me.GBFacilityData.Controls.Add(Me.txtAIRSNumber)
        Me.GBFacilityData.Controls.Add(Me.Label1)
        Me.GBFacilityData.Controls.Add(Me.txtFacilityName)
        Me.GBFacilityData.Controls.Add(Me.Label2)
        Me.GBFacilityData.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBFacilityData.Location = New System.Drawing.Point(0, 28)
        Me.GBFacilityData.Name = "GBFacilityData"
        Me.GBFacilityData.Size = New System.Drawing.Size(792, 52)
        Me.GBFacilityData.TabIndex = 148
        Me.GBFacilityData.TabStop = False
        '
        'txtAIRSNumber
        '
        Me.txtAIRSNumber.Location = New System.Drawing.Point(472, 16)
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(384, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 142
        Me.Label2.Text = "AIRS Number:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 13)
        Me.Label3.TabIndex = 149
        Me.Label3.Text = "Compliance Engineer:"
        '
        'cboSSCPEngineer
        '
        Me.cboSSCPEngineer.Location = New System.Drawing.Point(128, 14)
        Me.cboSSCPEngineer.Name = "cboSSCPEngineer"
        Me.cboSSCPEngineer.Size = New System.Drawing.Size(184, 21)
        Me.cboSSCPEngineer.TabIndex = 150
        '
        'PanelFaciltiyAssignmentLower
        '
        Me.PanelFaciltiyAssignmentLower.Controls.Add(Me.GBRequiresInspection)
        Me.PanelFaciltiyAssignmentLower.Controls.Add(Me.Label4)
        Me.PanelFaciltiyAssignmentLower.Controls.Add(Me.cboSSCPUnits)
        Me.PanelFaciltiyAssignmentLower.Controls.Add(Me.Label3)
        Me.PanelFaciltiyAssignmentLower.Controls.Add(Me.cboSSCPEngineer)
        Me.PanelFaciltiyAssignmentLower.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelFaciltiyAssignmentLower.Location = New System.Drawing.Point(0, 80)
        Me.PanelFaciltiyAssignmentLower.Name = "PanelFaciltiyAssignmentLower"
        Me.PanelFaciltiyAssignmentLower.Size = New System.Drawing.Size(792, 113)
        Me.PanelFaciltiyAssignmentLower.TabIndex = 151
        '
        'GBRequiresInspection
        '
        Me.GBRequiresInspection.Controls.Add(Me.rdbInspectionNo)
        Me.GBRequiresInspection.Controls.Add(Me.rdbInspectionYes)
        Me.GBRequiresInspection.Location = New System.Drawing.Point(8, 48)
        Me.GBRequiresInspection.Name = "GBRequiresInspection"
        Me.GBRequiresInspection.Size = New System.Drawing.Size(120, 40)
        Me.GBRequiresInspection.TabIndex = 153
        Me.GBRequiresInspection.TabStop = False
        Me.GBRequiresInspection.Text = "Requires Inspection"
        '
        'rdbInspectionNo
        '
        Me.rdbInspectionNo.Location = New System.Drawing.Point(56, 16)
        Me.rdbInspectionNo.Name = "rdbInspectionNo"
        Me.rdbInspectionNo.Size = New System.Drawing.Size(40, 16)
        Me.rdbInspectionNo.TabIndex = 1
        Me.rdbInspectionNo.Text = "No"
        '
        'rdbInspectionYes
        '
        Me.rdbInspectionYes.Location = New System.Drawing.Point(8, 16)
        Me.rdbInspectionYes.Name = "rdbInspectionYes"
        Me.rdbInspectionYes.Size = New System.Drawing.Size(48, 16)
        Me.rdbInspectionYes.TabIndex = 0
        Me.rdbInspectionYes.Text = "Yes"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(371, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 13)
        Me.Label4.TabIndex = 151
        Me.Label4.Text = "Compliance Unit:"
        '
        'cboSSCPUnits
        '
        Me.cboSSCPUnits.Location = New System.Drawing.Point(472, 14)
        Me.cboSSCPUnits.Name = "cboSSCPUnits"
        Me.cboSSCPUnits.Size = New System.Drawing.Size(184, 21)
        Me.cboSSCPUnits.TabIndex = 152
        '
        'SSCPFacAssignment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 193)
        Me.Controls.Add(Me.PanelFaciltiyAssignmentLower)
        Me.Controls.Add(Me.GBFacilityData)
        Me.Controls.Add(Me.TBFacilityAssignment)
        Me.Menu = Me.MainMenu1
        Me.Name = "SSCPFacAssignment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Compliance Facility Assignment"
        Me.GBFacilityData.ResumeLayout(False)
        Me.GBFacilityData.PerformLayout()
        Me.PanelFaciltiyAssignmentLower.ResumeLayout(False)
        Me.PanelFaciltiyAssignmentLower.PerformLayout()
        Me.GBRequiresInspection.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub SSCPFacAssignment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        monitor.TrackFeature("Forms." & Me.Name)
        Try

            CreateStatusBar()

            LoadCombos()
            LoadCurrentData()

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        Finally

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
        End Try

    End Sub
    Sub LoadCombos()
        Try
            dsUnits = New DataSet
            dsEngineerList = New DataSet

            SQL = "Select " & _
            "strUnitDesc, numUnitCode  " & _
            "from AIRBranch.LookUpEPDUnits " & _
            "where numProgramCode = '4' "

            daUnits = New OracleDataAdapter(SQL, CurrentConnection)

            SQL = "Select " & _
            "numUserID, (strLastName||', ' ||strFirstName) as UserName,  " & _
            "strUnitDesc, strProgramDesc  " & _
            "from " & DBNameSpace & ".EPDUserProfiles, " & DBNameSpace & ".LookupEPDPrograms,  " & _
            "" & DBNameSpace & ".LookUpEPDUnits  " & _
            "where " & DBNameSpace & ".EPDUserProfiles.numProgram = " & DBNameSpace & ".LookUpEPDPrograms.numProgramCode  " & _
            "and " & DBNameSpace & ".EPDUserProfiles.numUnit = " & DBNameSpace & ".LookUpEPDUnits.numUnitCode  " & _
            "and numProgram = '4'  " & _
            "Order by strUnitDesc, strLastName "

            daEngineerList = New OracleDataAdapter(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            daUnits.Fill(dsUnits, "Units")
            daEngineerList.Fill(dsEngineerList, "EngineerList")

            Dim dtUnits As New DataTable
            Dim dtEngineer As New DataTable
            Dim drDSRow As DataRow
            Dim drDSRow2 As DataRow
            Dim drNewRow As DataRow

            dtUnits.Columns.Add("strUnitDesc", GetType(System.String))
            dtUnits.Columns.Add("numUnitCode", GetType(System.String))

            drNewRow = dtUnits.NewRow()
            drNewRow("strUnitDesc") = " "
            drNewRow("numUnitCode") = " "
            dtUnits.Rows.Add(drNewRow)

            For Each drDSRow In dsUnits.Tables("Units").Rows()
                drNewRow = dtUnits.NewRow()
                drNewRow("strUnitDesc") = drDSRow("strUnitDesc")
                drNewRow("numUnitCode") = drDSRow("numUnitCode")
                dtUnits.Rows.Add(drNewRow)
            Next

            With cboSSCPUnits
                .DataSource = dtUnits
                .DisplayMember = "strUnitDesc"
                .ValueMember = "numUnitCode"
                .SelectedIndex = 0
            End With

            dtEngineer.Columns.Add("UserName", GetType(System.String))
            dtEngineer.Columns.Add("numUserID", GetType(System.String))

            drNewRow = dtEngineer.NewRow()
            drNewRow("UserName") = " "
            drNewRow("numUserID") = " "
            dtEngineer.Rows.Add(drNewRow)

            For Each drDSRow2 In dsEngineerList.Tables("EngineerList").Rows()
                drNewRow = dtEngineer.NewRow()
                drNewRow("UserName") = drDSRow2("UserName")
                drNewRow("numUserID") = drDSRow2("numUserID")
                dtEngineer.Rows.Add(drNewRow)
            Next

            With cboSSCPEngineer
                .DataSource = dtEngineer
                .DisplayMember = "UserName"
                .ValueMember = "numUserID"
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Sub LoadCurrentData()
        Dim temp As String = ""

        Try

            SQL = "select " & _
            "ENGINEER as SSCPEngineer,  " & _
            "case " & _
            "when STRUNITDESC is null then 'N/A' " & _
            "else STRUNITDESC " & _
            "end STRUNITDESC,  " & _
            "NUMUNIT " & _
            "from " & DBNameSpace & ".VW_SSCPINSPECTION_LIST, " & _
            "" & DBNameSpace & ".EPDUSERPROFILES, " & DBNameSpace & ".LOOKUPEPDUNITS  " & _
            "where " & DBNameSpace & ".VW_SSCPINSPECTION_LIST.NUMSSCPENGINEER = " & DBNameSpace & ".EPDUSERPROFILES.NUMUSERID " & _
            "and " & DBNameSpace & ".EPDUSERPROFILES.NUMUNIT = " & DBNameSpace & ".LOOKUpEPDUnits.numUnitCode  (+) " & _
            "and AIRSNumber = '" & txtAIRSNumber.Text & "' "

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr.Item("SSCPEngineer")) Then
                    cboSSCPEngineer.Text = UserName
                Else
                    cboSSCPEngineer.Text = dr.Item("SSCPEngineer")
                End If
                If IsDBNull(dr.Item("numUnit")) Then
                    cboSSCPUnits.SelectedIndex = 0
                Else
                    cboSSCPUnits.SelectedValue = dr.Item("numUnit")
                End If
            End While

            SQL = "Select strInspectionRequired " & _
            "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "

            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                temp = dr.Item("strInspectionRequired")
                Select Case temp
                    Case "True"
                        rdbInspectionYes.Checked = True
                        rdbInspectionNo.Checked = False
                    Case "False"
                        rdbInspectionYes.Checked = False
                        rdbInspectionNo.Checked = True
                    Case Else
                        rdbInspectionYes.Checked = False
                        rdbInspectionNo.Checked = True
                End Select
            Else
                rdbInspectionYes.Checked = False
                rdbInspectionNo.Checked = True
            End If

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try


    End Sub
#End Region
#Region "Subs and Functions"
    Sub Save()
        Dim EngineerGCode As String = "0"
        Dim Inspection As String = "False"

        Try

            Paneltemp1 = panel1.Text
            panel1.Text = "Saving Facility Data."

            Dim dtEngineers As New DataTable
            dtEngineers = dsEngineerList.Tables("" & DBNameSpace & ".EPDUserProfiles")
            Dim drEngineers As DataRow()
            Dim row As DataRow

            drEngineers = dtEngineers.Select("UserName = '" & cboSSCPEngineer.Text & "'")
            For Each row In drEngineers
                EngineerGCode = row("numUserID")
            Next

            panel1.Text = "Saving Facility Data.."

            If rdbInspectionYes.Checked = True Then
                Inspection = "True"
            Else
                Inspection = "False"
            End If

            'SQL = "Update " & DBNameSpace & ".SSCPFacilityAssignment set " & _
            '"strSSCPUnit = '" & UserUnit & "', " & _
            '"strSSCPEngineer = '" & EngineerGCode & "', " & _
            '"strSSCPAssigningManager = '" & UserGCode & "', " & _
            '"datAssignmentdate = '" & OracleDate & "' " & _
            '"where strAIRSNumber = '0413" & txtAIRSNumber.Text & "'"
            MsgBox("ERROR PLEASE USE THE MANAGERS TOOLS UNTIL THIS CAN BE RESOLVED", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub

            'Dim cmd As New OracleCommand(SQL, conn)
            cmd = New OracleCommand(SQL, CurrentConnection)

            If CurrentConnection.State = ConnectionState.Closed Then
                CurrentConnection.Open()
            End If
            dr = cmd.ExecuteReader

            '            Dim dr As OracleDataReader = cmd.ExecuteReader

            panel1.Text = "Saving Facility Data..."

            SQL = "Select strInspectionRequired " & _
            "from " & DBNameSpace & ".SSCPInspectionsRequired " & _
            "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader
            recExist = dr.Read
            If recExist = True Then
                SQL = "Update " & DBNameSpace & ".SSCPInspectionsRequired set " & _
                "strInspectionRequired= '" & Inspection & "', " & _
                "strAssigningManager = '" & UserGCode & "', " & _
                "datAssigningDate = '" & OracleDate & "' " & _
                "where strAIRSNumber = '0413" & txtAIRSNumber.Text & "' "
            Else
                SQL = "Insert into " & DBNameSpace & ".SSCPInspectionsRequired " & _
                "(strAIRSNumber, strInspectionRequired, " & _
                "strAssigningManager, datAssigningDate) " & _
                "values " & _
                "('0413" & txtAIRSNumber.Text & "', " & _
                "'" & Inspection & "', '" & UserGCode & "', " & _
                "'" & OracleDate & "') "
            End If

            panel1.Text = "Saving Facility Data...."
            cmd = New OracleCommand(SQL, CurrentConnection)
            dr = cmd.ExecuteReader

            panel1.Text = "Refreshing Facility Summary Screen..."
            'xIAIPFacilitySummary.LoadStateContactInformation()
            panel1.Text = Paneltemp1
            MsgBox("Done")

        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub Clear()
        Try

            cboSSCPUnits.Text = " "
            cboSSCPEngineer.Text = " "
            rdbInspectionYes.Checked = False
            rdbInspectionNo.Checked = True
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Sub Back()
        Try

            SSCPFacAssign = Nothing
            Me.Hide()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
#End Region


    Private Sub SSCPFacAssignment_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try

            SSCPFacAssign = Nothing
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub TBFacilityAssignment_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles TBFacilityAssignment.ButtonClick
        Try

            Select Case TBFacilityAssignment.Buttons.IndexOf(e.Button)
                Case 0
                    Save()
                Case 1
                    Clear()
                Case 2
                    Back()
            End Select
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiBack.Click
        Try

            Back()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub MmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MmiSave.Click
        Try

            Save()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
    Private Sub mmiClear_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mmiClear.Click
        Try

            Clear()
        Catch ex As Exception
            ErrorReport(ex, Me.Name & "." & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub



    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        OpenDocumentationUrl(Me)
    End Sub
End Class
